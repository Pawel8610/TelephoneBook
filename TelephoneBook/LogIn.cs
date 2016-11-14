using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TelephoneBook
{
    public partial class LogIn : Form
    {   public static string UserID;//potrzebne do ustawień użytkownika
        public static string Namee;
        public static string LastName;
        public static string Mobile;
        public static string Email;
        public static string Permissions;
        public static string ServerName = SaveXmlServerSettings.ReadServerName();

     //  SqlConnection con = new SqlConnection("Data Source=PAWEL-PC\\KURS; Initial Catalog=Phone; Integrated Security=true");
        SqlConnection con = new SqlConnection("Data Source=" + ServerName + "; Initial Catalog=Phone; Integrated Security=true");
        string imgLocation = "";

        public LogIn()
        {
            InitializeComponent();
            ServerName = SaveXmlServerSettings.ReadServerName();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.AcceptButton = button1LogIn;//aby można było enterem kliknąć ten button
        }
       
        private void button1LogIn_Click(object sender, EventArgs e)
        {
             try
            {
                pictureBox1.Visible = true;
                //System.Threading.Thread.Sleep(1000);//usypianie bieżącego wątku na 1s
                string sql = "SELECT UserID, FirstName, LastName, Permissions, Mobile, Email FROM [dbo].[User] WHERE (LoginName='" + textBox1User.Text + "') AND (PasswordHash=HASHBYTES('SHA1','" + textBox2Password.Text + "'+CAST(Salt AS NVARCHAR(36))))";
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlCommand comm = new SqlCommand(sql, con);
                SqlDataReader reader = comm.ExecuteReader();
                reader.Read();
                if (reader.HasRows)//jeśli wynik zapytania nie zwróci żadnego wiersza, nie wykona się kod w tym ifie
                {
                     if (reader[0].ToString() != null)
                     {
                        UserID = reader[0].ToString();
                        Namee = reader[1].ToString();
                        LastName = reader[2].ToString();
                        Permissions = reader[3].ToString();
                        Mobile = reader[4].ToString();
                        Email = reader[5].ToString();
                        //UserName(Name);
                        //UserLastName(LastName);
                        //UserPermissions(Permissions);
                        
                        this.Hide();
                        Form1 form2 = new Form1();
                        form2.ShowDialog();
                        form2.Show();
                        this.Close();
                       }
                }
                else
                {
                    MessageBox.Show("Invalid login and/or password.");
                    con.Close();
                }
                con.Close();
                pictureBox1.Visible = false;
               }

            catch (Exception ex)
            {
               MessageBox.Show("Problem with connection to database.");
               con.Close();
             }
            //if (textBox1User.Text == "Pawel")
            //{
            //    if (textBox2Password.Text == "123")
            //    {
            //        Form1 settingsForm = new Form1();
            //        // Show the settings form
            //        settingsForm.Show();
            //        //this.Close();//closing this form
            //    }
            //    else { MessageBox.Show("Invalid User or/and Password"); }
            //}
            //else { MessageBox.Show("Invalid User or/and Password"); }
            //textBox1User.Text = "";
            //textBox2Password.Text = "";
        }
        private void serverSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServerSettings formm = new ServerSettings();
            //formm.Show();
            formm.ShowDialog();//aby nie było możliwości otwarciu wielu okienek
        }

      
    }
}
