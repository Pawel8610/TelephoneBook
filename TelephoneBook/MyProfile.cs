using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelephoneBook
{
    public partial class MyProfile : Form
    {
        //SqlConnection con = new SqlConnection("Data Source=PAWEL-PC\\KURS; Initial Catalog=Phone; Integrated Security=true");
        SqlConnection con = new SqlConnection("Data Source=" + LogIn.ServerName + "; Initial Catalog=Phone; Integrated Security=true");

        public MyProfile()
        {
            InitializeComponent();
        }
        public void UpdateUserData()
        {
            try
            {
                con.Open();
                //SqlCommand służy do: insert, update, delete, a SqlDataReader do :Select
                SqlCommand cmd = new SqlCommand(@"UPDATE [dbo].[User] SET FirstName='" + textBox2.Text + "', LastName='" + textBox3.Text + "', Mobile='" + textBox4.Text + "', Email='" + textBox5.Text + "' WHERE UserID='" + textBox7UserID.Text + "'", con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Updated Successfully.");
                con.Close();
            }
            catch
            {
                MessageBox.Show("Input valid data.");
            }
        
        }
        private void MyProfile_Load(object sender, EventArgs e)
        {
            SetSettings6();
            textBox7UserID.Text = LogIn.UserID;
            Display();
  
        }
        public void SetSettings6()
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
        void Display()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * from [dbo].[User] where UserID='" + textBox7UserID.Text + "'", con);
            System.Data.DataTable dt = new System.Data.DataTable();//było: DataTable dt = new DataTable();// na potrzeby excela zmieniłem
            sda.Fill(dt);
            
            foreach (DataRow item in dt.Rows)
            {
                textBox1.Text = item["LoginName"].ToString();
                textBox2.Text = item["FirstName"].ToString();
                textBox3.Text = item["LastName"].ToString();
                textBox4.Text = item["Mobile"].ToString();
                textBox5.Text = item["Email"].ToString();
                if (item["Permissions"].ToString() == "A")
                {
                    textBox6.Text = "Administrator";
                }
                else { textBox6.Text = "User"; }
                LogIn.Namee = item["FirstName"].ToString();//update zmiennych, którę są potem użyte przy tworzeniu emaila, smsa itd.
                LogIn.LastName = item["LastName"].ToString();
                LogIn.Mobile = item["Mobile"].ToString();
                LogIn.Email = item["Email"].ToString();
            }
         }
        private void button1_Click(object sender, EventArgs e)
        {
            UpdateUserData();
            Display();
        }
    }
}
