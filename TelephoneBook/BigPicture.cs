using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TelephoneBook
{
    public partial class BigPicture : Form
    {
        //SqlConnection con = new SqlConnection("Data Source=PAWEL-PC\\KURS; Initial Catalog=Phone; Integrated Security=true");
        SqlConnection con = new SqlConnection("Data Source=" + LogIn.ServerName + "; Initial Catalog=Phone; Integrated Security=true");
        Point PanelMouseDownLocation;
        
        public BigPicture()
        {
            InitializeComponent();
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
        private void BigPicture_Load(object sender, EventArgs e)
        {
            SetSettings3();
            //SqlConnection con = new SqlConnection("Data Source=PAWEL-PC\\KURS; Initial Catalog=Phone; Integrated Security=true");
            SqlConnection con = new SqlConnection("Data Source=" + LogIn.ServerName + "; Initial Catalog=Phone; Integrated Security=true");
            showPicture(pictureBox2);
            
        }

        public void showPicture(PictureBox pikczerboks)
        {
            try
            {
                string sql = "Select Picture From Mobiles Where ID = '" + Form1.pobierzID + "'";
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlCommand comm = new SqlCommand(sql, con);
                SqlDataReader reader = comm.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                   
                    byte[] img = (byte[])(reader[0]);//pobieram zdjęcie z bazy do tablicy img
                    if (img == null)
                        pikczerboks.Image = null;
                    else
                    {
                        MemoryStream ms = new MemoryStream(img);
                        pikczerboks.Image = System.Drawing.Image.FromStream(ms);//zmiana z Image na System.Drawing.Image bo też framework pdf itectsharp.pdfa odnosił się do Image
                       
                    }
                }
                else { }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                pikczerboks.Image = null;

            }
        }

           private void button1ZoomIn_Click(object sender, EventArgs e)
        {
         //   pictureBox2.Top = (int)(pictureBox2.Top - (pictureBox2.Height * 0.25));
         //   pictureBox2.Left = (int)(pictureBox2.Left - (pictureBox2.Width * 0.25));
            pictureBox2.Height = (int)(pictureBox2.Height + (pictureBox2.Height * 0.25));
           pictureBox2.Width = (int)(pictureBox2.Width + (pictureBox2.Width * 0.25));
        }

        private void button2ZoomOut_Click(object sender, EventArgs e)
        {
       //     pictureBox2.Top = (int)(pictureBox2.Top + (pictureBox2.Height * 0.25));
       //     pictureBox2.Left = (int)(pictureBox2.Left + (pictureBox2.Width * 0.25));
            pictureBox2.Height = (int)(pictureBox2.Height - (pictureBox2.Height * 0.25));
            pictureBox2.Width = (int)(pictureBox2.Width - (pictureBox2.Width * 0.25));
        }
        //moving picture
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)

            PanelMouseDownLocation.X = e.X;//albo  PanelMouseDownLocation = e.Location;
            PanelMouseDownLocation.Y = e.Y;
         }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                pictureBox2.Left += e.X - PanelMouseDownLocation.X;
                pictureBox2.Top += e.Y - PanelMouseDownLocation.Y;
                
            }
        }

        private void button1CenterImage_Click(object sender, EventArgs e)
        {
             pictureBox2.Location=panel1.Location;
        }

        private void button1oryginalSize_Click(object sender, EventArgs e)
        {
            pictureBox2.Height=pictureBox2.Image.Height;
            pictureBox2.Width = pictureBox2.Image.Width;
            pictureBox2.Location = panel1.Location;
        }

       
    }


    
}
