using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using MessagingToolkit.QRCode.Codec;

namespace TelephoneBook
{
    public partial class Email : Form
    {
        public Email()
        {
            InitializeComponent();
        }

        private void button1Send_Click(object sender, EventArgs e)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Port = 587;//Convert.ToInt32(comboBox2Port.Text);
                client.Host = comboBox1SmtpServer.Text;
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(textBox3UserName.Text, textBox6Password.Text);

                MailMessage mm = new MailMessage(textBox1From.Text, textBox4To.Text, textBox2Subject.Text, richTextBox1.Text);
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                client.Send(mm);
                MessageBox.Show("Mail Sent!", "Success", MessageBoxButtons.OK);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Sending problem"+ex);
            }
        }
        public void SetSettings4()
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
        private void Email_Load(object sender, EventArgs e)
        {
            SetSettings4();
            textBox1From.Text = LogIn.Email;
            textBox3UserName.Text = LogIn.Email;
            textBox4To.Text=Form1.pobierzEmail;
            comboBox2Port.SelectedIndex=0;//defaultowy port

            //creating QRCode image:
            if (Form1.pobierzEmail != "")
            {
            QRCodeEncoder encoder = new QRCodeEncoder();
            Bitmap qrcode = encoder.Encode(Form1.pobierzEmail);
            pictureBox1.Image = qrcode as Image; 
            }
        }
    }
}
