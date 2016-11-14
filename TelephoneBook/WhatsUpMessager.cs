using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WhatsAppApi;
using WhatsAppApi.Account;

namespace TelephoneBook
{
    public partial class WhatsUpMessager : Form
    {
        WhatsApp wa;
        WhatsUser user;

        public WhatsUpMessager()
        {
            InitializeComponent();
        }
        private void WhatsUpMessager_Load(object sender, EventArgs e)
        {
            SetSettings3();

            if ((Left(Form1.pobierzMobile, 2) != "48" && Left(Form1.pobierzMobile, 3) != "+48"))
            { textBox1TO.Text = "48" + Form1.pobierzMobile; }
            else {textBox1TO.Text = Form1.pobierzMobile;}
            
            textBox4Name.Text = LogIn.Namee;
            textBox5MyMobile.Text = LogIn.Mobile;
        }
        public string Left(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            maxLength = Math.Abs(maxLength);

            return (value.Length <= maxLength
                   ? value
                   : value.Substring(0, maxLength)
                   );
        }
        public void SetSettings3()
        {
            //Background color settings
            if (SaveXmlSettingsFile.readingSettingsBackgroundColor() == "Yellow")
            { this.BackColor = System.Drawing.Color.Yellow; }
            if (SaveXmlSettingsFile.readingSettingsBackgroundColor() == "Green")
            { this.BackColor = System.Drawing.Color.Green; }
            if (SaveXmlSettingsFile.readingSettingsBackgroundColor() == "Red")
            { this.BackColor = System.Drawing.Color.Red; }
            //TextColor settings
            if (SaveXmlSettingsFile.readingSettingsTextColor() == "Black")
            { this.ForeColor = System.Drawing.Color.Black; }
            if (SaveXmlSettingsFile.readingSettingsTextColor() == "DarkRed")
            { this.ForeColor = System.Drawing.Color.DarkRed; }
            if (SaveXmlSettingsFile.readingSettingsTextColor() == "Green")
            { this.ForeColor = System.Drawing.Color.Green; }

            if (SaveXmlSettingsFile.readingFont() == "10")
            { this.Font = SaveXmlSettingsFile.ChangeFontSize(this.Font, 10.0F, GraphicsUnit.Pixel); }
            if (SaveXmlSettingsFile.readingFont() == "12")
            { this.Font = SaveXmlSettingsFile.ChangeFontSize(this.Font, 12.0F, GraphicsUnit.Pixel); }

            this.Refresh();
        }

        private delegate void UpdateTextBox(TextBox textbox, string value);//delegate to update main thread, czyli ponowne wykonanie metody UpdateTextBox
        public void UpdateDataTextBox(TextBox textbox, string value)
        {
            textbox.Text += value;
        }

        private void send()
        {
            try
            {
                WhatsUserManager manager = new WhatsUserManager();
                user = manager.CreateUser(textBox5MyMobile.Text, textBox4Name.Text);
                var thread = new Thread(t =>
                    {
                        UpdateTextBox textbox = UpdateDataTextBox;
                        wa = new WhatsApp(textBox5MyMobile.Text, textBox6Password.Text, textBox4Name.Text, true);
                        wa.OnConnectSuccess += () =>
                            {
                                if (textBox3Status.InvokeRequired)
                                    Invoke(textbox, textBox3Status, "Connected...");
                                wa.OnLoginSuccess += (phone, data) =>
                                    {
                                        if (textBox3Status.InvokeRequired)
                                            Invoke(textbox, textBox3Status, "\r\nLogin success!");
                                        while (wa != null)
                                        {
                                            wa.PollMessages();
                                            Thread.Sleep(200);
                                            continue;
                                        }
                                    };
                                wa.OnGetMessage += (node, from, id, name, message, receipt_sent) =>
                                    {
                                        if (textBox3Status.InvokeRequired)
                                            Invoke(textbox, textBox3Status, string.Format("\r\n{0}:{1}", name, message));
                                    };
                                wa.OnLoginFailed += (data) =>
                                    {
                                        if (textBox3Status.InvokeRequired)
                                            Invoke(textbox, textBox3Status, string.Format("\r\nLogin failed:{0}", data));
                                    };
                                wa.Login();
                            };
                        wa.OnConnectFailed += (ex) =>
                            {
                                if (textBox3Status.InvokeRequired)
                                    Invoke(textbox, textBox3Status, string.Format("\r\nConnect failed: {0}", ex.StackTrace));
                            };
                        wa.Connect();
                    }) { IsBackground = true };
                thread.Start();
            }
            catch (Exception ex) { MessageBox.Show("Connection failed. Check input data, remove polish letters, to telephone number add prefix 48" + ex); }
        }

        private void button2Login_Click(object sender, EventArgs e)
        {
            send();
        }

        private void button1Send_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2Message.Text))
                return;
            if (wa != null)
            {
                if (wa.ConnectionStatus == ApiBase.CONNECTION_STATUS.LOGGEDIN)
                {
                    wa.SendMessage(textBox1TO.Text, textBox2Message.Text);
                    textBox3Status.Text += string.Format("\r\n{0}:{1}", user.Nickname, textBox2Message.Text);
                    textBox2Message.Clear();
                    textBox2Message.Focus();
               
                }
             
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process gettingpassword = new Process();//uruchomienie pliku exe który jest w głównym katalogu
            gettingpassword.StartInfo.FileName = "WART.exe";
            gettingpassword.Start();
        }

        
    }
}
