using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace TelephoneBook
{
    public partial class Form3AddNewUser : Form
    {
       //SqlConnection con = new SqlConnection("Data Source=PAWEL-PC\\KURS; Initial Catalog=Phone; Integrated Security=true");
       SqlConnection con = new SqlConnection("Data Source=" + LogIn.ServerName + "; Initial Catalog=Phone; Integrated Security=true");
        public static string usserID = "";
        private void Form3AddNewUser_Load(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
            comboBox1Permissions.SelectedIndex = -1;
           
          SetSettings2();
        }
        public Form3AddNewUser()
        {
            InitializeComponent();
        }
        public void SetSettings2()
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
        private void button1Addddd_Click(object sender, EventArgs e)
        {
            try
             {con.Open();
                string sql = "SELECT LoginName FROM [dbo].[User] WHERE LoginName='" + textBox1.Text + "'";
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlCommand comm3 = new SqlCommand(sql, con);
                SqlDataReader reader = comm3.ExecuteReader();
                reader.Read();
                if ((textBox1.Text == "") || (textBox2.Text == "") || (textBox3.Text == "") || (textBox4.Text == "") || (comboBox1Permissions.SelectedIndex == -1))
                { MessageBox.Show("Fill all textboxes."); }
                else
                {
                    if (reader.HasRows)
                    {
                        string login = reader[0].ToString();
                        MessageBox.Show("Login: " + login + " already exists in database. Put another one.");
                    }
                    else
                    {
                        AddUser();
                    }
                }
                con.Close();
            }

            catch (Exception ex)
            {
                con.Close();
            }
        }
      
        void AddUser()
        {
            //SqlConnection con = new SqlConnection("Data Source=PAWEL-PC\\KURS; Initial Catalog=Phone; Integrated Security=true");
            SqlConnection con = new SqlConnection("Data Source=" + LogIn.ServerName + "; Initial Catalog=Phone; Integrated Security=true");
            if (con.State != ConnectionState.Open)
                con.Open();
            SqlCommand cmd = new SqlCommand("AddUser", con);//AddUser to procedura składowana, która dodaj dane nowego user do bazy oraz szyfruje jego hasło i dodaje salt
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pLogin", textBox1.Text);
            cmd.Parameters.AddWithValue("@pPassword", textBox2.Text);
            cmd.Parameters.AddWithValue("@pFirstName", textBox3.Text);
            cmd.Parameters.AddWithValue("@pLastName", textBox4.Text);
            cmd.Parameters.AddWithValue("@pMobile", textBox5.Text);
            cmd.Parameters.AddWithValue("@pEmail", textBox6.Text);
            cmd.Parameters.AddWithValue("@pPermissions", comboBox1Permissions.Text);
            int rowAffected = cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("SuccessFully Saved.");
        }
     public string GetUserID()
        {
           
            try
            {
                //SqlConnection con = new SqlConnection("Data Source=PAWEL-PC\\KURS; Initial Catalog=Phone; Integrated Security=true");
                SqlConnection con = new SqlConnection("Data Source=" + LogIn.ServerName + "; Initial Catalog=Phone; Integrated Security=true");
                con.Open();
                string sql ="SELECT UserID FROM [dbo].[User] WHERE (LoginName='" + textBox1.Text + "') AND (PasswordHash=HASHBYTES('SHA1','" + textBox2.Text + "'+CAST(Salt AS NVARCHAR(36))))";
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlCommand comm3 = new SqlCommand(sql, con);
                SqlDataReader reader = comm3.ExecuteReader();
                reader.Read();
                if ((textBox1.Text == "") || (textBox2.Text == ""))
                { MessageBox.Show("Input valid login and password to remove."); }
                else
                {
                    if (reader.HasRows)
                    {
                        usserID = reader[0].ToString();
                        return usserID;
                    }
                    else
                    {
                        MessageBox.Show("This user not exist in database.");
                        return "";
                    }
                }
                return "";
               }
            catch (Exception ex)
            {
                MessageBox.Show("This user not exist in database.");
                con.Close();
                return "";
            }
         }
        
        private void button1Remove_Click(object sender, EventArgs e)
        {
               GetUserID();
            try
            {
                if ((textBox1.Text != "") && (textBox2.Text != "") && (usserID != ""))//jest to dodatkowe zabezpieczenie, aby w przypadku pustych pól lub z błędnymi danymi nie wyskoczył MessageBox.Show("Deleted Successfully.");
                {      if (MessageBox.Show("Do you really want delete this user?", "Deleting", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(@"DELETE FROM [dbo].[User] WHERE UserID ='" + usserID + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Deleted Successfully.");
                }
                }
                else { }
            }
            catch
            {
                MessageBox.Show("Invalid data to remove.");
                con.Close();
            }
        
        }
        
    }
}
