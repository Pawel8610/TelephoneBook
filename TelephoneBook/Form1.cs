using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.Drawing;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using Thought.vCards;
using System.Threading;

namespace TelephoneBook
{
    public partial class Form1 : Form, ISettings
    {
        SqlConnection con = new SqlConnection("Data Source=" + LogIn.ServerName + "; Initial Catalog=Phone; Integrated Security=true");
        //SqlConnection con = new SqlConnection("Data Source=PAWEL-PC\\KURS; Initial Catalog=Phone; Integrated Security=true");
        string imgLocation = "";
        public static string pobierzEmail;//abym mógł wartość przekazać do innej klasy(np. okienka)
        public static string pobierzMobile;
        public static string pobierzID;//pole static aby nie tworzyć nowego obiektu (z innej klasy Form1.PobierzID aby uzyskać wartość tego pola)
        public static string pobierzBackground;//potrzebne też do zapisu ustawień do xmla
        public static string pobierzTextColor;
        public static string pobierzFontsize;
        public static bool CzyDefaultoweZdjecieWpikczerboksie = true;
        public byte[] pic;
        public static bool CzyDodano;

        public Form1()
        {
            InitializeComponent();
        }
  delegate void ShowPicturee(PictureBox pictureboxxxxx);
  delegate void Displayyy();
        private void Form1_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(nameee);
         
            if (LogIn.Permissions != "A")
            {
                button1New.Visible = false;
                button2Insert.Visible = false;
                button3Delete.Visible = false;
                button4Update.Visible = false;
                button2PictureOpen.Visible = false;
                button2RemovePict.Visible = false;
                button2AddNewUser.Visible = false;
                button2Save.Visible = false;
                dataGridView1.ReadOnly=true;
            }
            label7.Text = "Witaj " + LogIn.Namee + " " + LogIn.LastName;
            Display();
            SetSettings();
       
            this.ActiveControl = textBox1FirstName;//aby po odpaleniu kursor był w tym textboxie
       }
      
        private void button1New_Click(object sender, EventArgs e)
        {
            //textBox1ID.Text = "";
            textBox1FirstName.Text = "";
            textBox2LastName.Clear();
            textBox3Mobile.Text = "";
            textBox4Email.Clear();
            comboBox1Category.SelectedIndex = -1;   // też zmieniłem properties dropdownstyle na dropdownlist aby combobox nie był edytowalny
            textBox1FirstName.Focus();
        }
        public void InsertNewPersonToDB(string First, string Last, string Mobile, string Emmail, string Category, bool CzyWalidacjaEmaila, bool WithImage , params byte[] img) //img jest opcjonalnym parametrem
        {
            try
            {
                Regex emailRegExp = new Regex("^[a-zA-Z]{1,1}[a-zA-Z0-9_.]{2,20}[a-zA-Z0-9]{1,1}@[a-zA-Z0-9-]{1,20}[.][a-zA-Z]{2,3}$");
                Regex mobileRegExp = new Regex("^[0-9+ -]{0,20}$");
            
            Liczba licz = new Liczba();
            string jjjj = licz.liczba(First, Last, Mobile, Emmail);//metoda zwraca numer ID w bazie dla wiersza o podanych parametrach
            CzyDodano = false;
                 
           
               // emailRegExp.IsMatch(Emmail) = NOVaidation;
                  //jeśli nie istnieje w bazie człowiek o identycznych danych, to go dodaj
                    if (jjjj == "") 
                    {
                        if (mobileRegExp.IsMatch(Mobile))
                        {
                            if (emailRegExp.IsMatch(Emmail) || CzyWalidacjaEmaila==false)
                            {
                            con.Open();

                            if ((img != null)&&(WithImage==true))
                            {
                                SqlCommand cmd = new SqlCommand(@"INSERT INTO Mobiles (First, Last, Mobile, Email, Category, Picture) 
            VALUES ('" + First + "','" + Last + "','" + Mobile + "','" + Emmail + "','" + Category + "',@img)", con);      //@ daje możliwość rozbicia stringy na wiele linijek 
                                cmd.Parameters.Add(new SqlParameter("@img", img));
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                SqlCommand cmd = new SqlCommand(@"INSERT INTO Mobiles (First, Last, Mobile, Email, Category) 
            VALUES ('" + First + "','" + Last + "','" + Mobile + "','" + Emmail + "','" + Category + "')", con);      //@ daje możliwość rozbicia stringy na wiele linijek 
                                cmd.ExecuteNonQuery();
                            }
                            
                            con.Close();
                            Display();
                            int x = dataGridView1.Rows.Count - 1;
                            dataGridView1.Focus();
                            dataGridView1.CurrentCell = dataGridView1.Rows[x].Cells[0];
                            CzyDodano = true;
                           }
                            else {
                               MessageBox.Show("Check your email format: " + Emmail + " Email musi zaczynać się od przynajmniej jednej małej lub dużej litery, potem może być być . i/lub _ i ciąg znaków min.2 max 20. Przed małpą musi być przynajmniej jeden znak alfanumeryczny (max 20), potem małpa, potem od 1 do 20 alfanum. znaków (lub -), następnie kropka, na końcu 2 lub 3 litery (małe lub duże).");
                               CzyDodano = false;
                                con.Close();
                                }  
                        }
                        else {
                            MessageBox.Show("Incorrect value: "+Mobile+" Mobile can contain only numerical or characters: +,- or be null. Max. length of one piece of data is 20 char.");
                            CzyDodano = false;
                            con.Close();
                             }                
                        
                    }
                    else
                    {
                        CzyDodano = false;
                       // MessageBox.Show("Input valid and uniqe data.");
                        con.Close();
                    }
           
            }
            catch
            {
                con.Close();
                MessageBox.Show("Input valid data.");
            }
        }
        private void button2Insert_Click(object sender, EventArgs e)
        {

            Liczba licz = new Liczba();
            string jjjj = licz.liczba(textBox1FirstName.Text, textBox2LastName.Text, textBox3Mobile.Text, textBox4Email.Text);//metoda zwraca numer ID w bazie dla wiersza o podanych parametrach

            if (jjjj == "")
            {

                if (checkBox1SkipEmail.Checked)
                {
                    InsertNewPersonToDB(textBox1FirstName.Text, textBox2LastName.Text, textBox3Mobile.Text, textBox4Email.Text, comboBox1Category.Text, false, false);
                }
                else { InsertNewPersonToDB(textBox1FirstName.Text, textBox2LastName.Text, textBox3Mobile.Text, textBox4Email.Text, comboBox1Category.Text, true, false); }
                if (CzyDodano)
                { MessageBox.Show("Sucessfully saved"); }
            }
            else { MessageBox.Show("Input uniqe value. This peron exist in database."); }
        }
        void Display()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * from Mobiles", con);
            System.Data.DataTable dt = new System.Data.DataTable();//było: DataTable dt = new DataTable();// na potrzeby excela zmieniłem
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                textBox1ID.Text = item["ID"].ToString();
                dataGridView1.Rows[n].Cells[0].Value = item["ID"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["First"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item[2].ToString(); //Cells[0] to pierwsza kolumna Cells[1] - druga
                dataGridView1.Rows[n].Cells[3].Value = item[3].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item[4].ToString();
                dataGridView1.Rows[n].Cells[5].Value = item[5].ToString();
           }
         }
        void Sortuj()
        { dataGridView1.Sort(dataGridView1.Columns[2], ListSortDirection.Ascending);//sortowanie według nazwiska
        }
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)//po kliknięciu na wiersz w datagridzie jego dane przepiszą się do kontrolek
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            progressBar1.Value = 0;
            label12Progress.Text = "0%";
            textBox1ID.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox1FirstName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox2LastName.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox3Mobile.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox4Email.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            if (dataGridView1.SelectedRows[0].Cells[5].Value.ToString() == "")  //jeśli dany człowiek nie ma kategorii, to ustaw comboboxa pustego
            { comboBox1Category.SelectedIndex = -1; }
            else { comboBox1Category.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString(); }

            if (dataGridView1.Rows[0].Selected)
            { pictureBox2.Image = System.Drawing.Image.FromFile("1.jpg"); }
            else{pictureBox2.Image =null;}

            showPicture(pictureBox1);
            stopwatch.Stop();
            textBox1.Text = stopwatch.ElapsedMilliseconds.ToString();
            imgLocation = "";//czyszczę bo jeśli np. wczytam zdjęcie, ale nie zapiszę go do danej osoby, to nadal zmienna będzie przechowywana i jesli kliknę na inną osobę a następnie save, to zdjęcie przypisze się
         }
        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)//to co powyżej ale po kliknięciu strzałkami
        {    //z wykorzystaniem nowego wątku:
            Stopwatch stopwatch = Stopwatch.StartNew();
            progressBar1.Value = 0;
            label12Progress.Text = "0%";
            textBox1ID.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox1FirstName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox2LastName.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox3Mobile.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox4Email.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            if (dataGridView1.SelectedRows[0].Cells[5].Value.ToString() == "")  //jeśli dany człowiek nie ma kategorii, to ustaw comboboxa pustego
            { comboBox1Category.SelectedIndex = -1; }
            else { comboBox1Category.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString(); }


            if (dataGridView1.Rows[0].Selected)
            { pictureBox2.Image = System.Drawing.Image.FromFile("1.jpg"); }
            else { pictureBox2.Image = null; }
           // showPicture(pictureBox1);
      //      if (InvokeRequired)
         //   {
                ShowPicturee sh = new ShowPicturee(showPicture);   
                sh.BeginInvoke((PictureBox)pictureBox1, null, null);
      //      }
                stopwatch.Stop();
                textBox1.Text = stopwatch.ElapsedMilliseconds.ToString();
            imgLocation = "";
        }
        private void button3Delete_Click(object sender, EventArgs e)
        {
            if (textBox1ID.Text == "")
            { MessageBox.Show("Click on proper row in Datagridview to delete."); }
            else
            {
                if (MessageBox.Show("Do you really want to delete?", "Deleting", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int x = 0;
                    x = dataGridView1.CurrentCell.RowIndex;//pobieram aktualny index
                    try
                    {
                        con.Open();
                        //SqlCommand służy do: insert, update, delete, a SqlDataReader do :Select
                        SqlCommand cmd = new SqlCommand(@"DELETE FROM Mobiles WHERE (ID ='" + textBox1ID.Text + "')", con);//pole Mobile jest kluczem w bazie          
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Deleted Successfully.");

                        Display();
                        dataGridView1.Focus();
                        dataGridView1.CurrentCell = dataGridView1.Rows[x].Cells[0];//po usunięciu wiersza focus ustawi się na kolejny wiersz w kolejności
                    }
                    catch { con.Close(); }
                }
            }
        }
        private void button4Update_Click(object sender, EventArgs e)
        {
            Regex emailRegExp = new Regex("^[a-zA-Z]{1,1}[a-zA-Z0-9_.]{2,20}[a-zA-Z0-9]{1,1}@[a-zA-Z0-9-]{1,20}[.][a-zA-Z]{2,3}$");
            Regex mobileRegExp = new Regex("^[0-9+ -]{0,20}$");
            try
            {
                Liczba licz = new Liczba();
                string jjjj = licz.liczba(textBox1FirstName.Text, textBox2LastName.Text, textBox3Mobile.Text, textBox4Email.Text);//metoda zwraca numer ID w bazie dla wiersza o podanych parametrach
                CzyDodano = false;

                if (jjjj == "")
                {
                    if ((textBox3Mobile.Text != "") || (textBox1FirstName.Text != ""))
                    {
                        if ((mobileRegExp.IsMatch(textBox3Mobile.Text)) && ((emailRegExp.IsMatch(textBox4Email.Text) || checkBox1SkipEmail.Checked)))
                        {
                            int x = 0;
                            con.Open();
                            //SqlCommand służy do: insert, update, delete, a SqlDataReader do :Select
                            SqlCommand cmd = new SqlCommand(@"UPDATE Mobiles SET First='" + textBox1FirstName.Text + "', Last='" + textBox2LastName.Text + "', Mobile='" + textBox3Mobile.Text + "', Email='" + textBox4Email.Text + "', Category='" + comboBox1Category.Text + "' WHERE (ID='" + textBox1ID.Text + "')", con);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Updated Successfully.");
                            x = dataGridView1.CurrentCell.RowIndex;//pobieram aktualny index

                            con.Close();
                            Display();
                            dataGridView1.Focus();
                            dataGridView1.CurrentCell = dataGridView1.Rows[x].Cells[0];//ustawiam na niego focus
                        }
                        else { MessageBox.Show("Mobile can contain only numerical values or - +. Check your email format."); }
                    }
                    else { MessageBox.Show("Input at least Mobile or First Name"); }
                }
                else { MessageBox.Show("To update please change at least one piece of data."); }
            }
            catch
            {
                MessageBox.Show("Input valid data.");
            }
        }
        //wyszukiwarka
          private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * from Mobiles Where (((Mobile LIKE '%" + textBox1Wyszukiwarka.Text + "%')or(First LIKE '%" + textBox1Wyszukiwarka.Text + "%')or(Last LIKE '%" + textBox1Wyszukiwarka.Text + "%'))and(Category LIKE '%" + comboBox2Category.Text + "%'))", con);
            System.Data.DataTable dt = new System.Data.DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["ID"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["First"].ToString(); //Cells[0] to pierwsza kolumna Cells[1] - druga
                dataGridView1.Rows[n].Cells[2].Value = item[2].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item[3].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item[4].ToString();
                dataGridView1.Rows[n].Cells[5].Value = item[5].ToString();
            }
        }

          private void button1_Click(object sender, EventArgs e)
          {
              Display();
              Sortuj();
          }
       
          private void button2Picture_Click(object sender, EventArgs e)
          {
              try{
                OpenFileDialog opend = new OpenFileDialog();
                opend.Filter = "Images only. |*.jpg; *.jpeg; *.png; *.bmp; *.gif;";
                   if (opend.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (opend.CheckFileExists)
                    {
                        imgLocation = opend.FileName.ToString();//wczytanie ścieżki zdjęcia wraz z jego nazwą do zmiennej imgLocation
                        pictureBox1.ImageLocation = imgLocation;//wczytanie zdjęcia do pikczerboxa
                     }
                    }}
                       catch (Exception ex)
              {
                  con.Close();
                           MessageBox.Show(ex.Message); }
              CzyDefaultoweZdjecieWpikczerboksie = true; //zaznaczam tu flagę, aby po samym załadowaniu zdjęcia nie dało się go powiększyć - otworzyć w BigPicture
                     }
          private void button2Save_Click(object sender, EventArgs e)
          {
              SavePictureToDB(pic);//metoda zapisuje zdjęcie z vcard a jeśli nie zapodam go, to z imgLocation
              pic = null;//czyszcze, aby nie można było jednego zdjęcia wielokrotnie przypisać do wielu osób
              imgLocation = "";//aby nie pamiętało ścieżki ostatio ładowanego pliku
              CzyDefaultoweZdjecieWpikczerboksie = true;//aby po kliknięciu "na pusto" nie dało się powiększyć np. pustego zdjęcia
          }
         
          public void SavePictureToDB(byte[] img)
          {
              try
              { 
                  if(img==null) //jeśli nie zapodam zdjęcia z vcarda, to zdjęcie zostanie zapisane z lokalizacji imgLocation (po otworzeniu zdjęcia przyciskiem load)
                  //this code saving to DB:
              { 
                  FileStream fs = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
                  BinaryReader br = new BinaryReader(fs);
                  img = br.ReadBytes((int)fs.Length);//konwersja zdjęcia do formatu binarnego, abym mógł go przekazać do bazy
              }
                  if ((img != null) || (imgLocation != null))//aby jeśli nie ma zdjęcia vCard, ani pliku załadowanego niepotrzebnie nie wykonywało się zapytanie (sprawa wydajnościowa)
                  {
                      string sql = "UPDATE Mobiles SET Picture=@img WHERE (ID='" + textBox1ID.Text + "')";
                      con.Open();
                      SqlCommand cmd = new SqlCommand(sql, con);
                      cmd.Parameters.Add(new SqlParameter("@img", img));
                      cmd.ExecuteNonQuery();
                      con.Close();
                  }
              }
              catch { con.Close(); }
          }
        public void showPicture(PictureBox pikczerboks)
          {
              try
              {
                  //double sum = 0;
                  //for (int i = 0; i <= 80000000; i++) //dodałem celowe opóźnienie, aby przetestować wątki
                  //{
                  //    if (i % 2 == 0)
                  //    {
                  //        sum = sum + i;
                  //    }
                  //}
                  pobierzID = textBox1ID.Text;//w tym miejscu pobieram wartość ID, aby potem wartość tego pola wykorzystać w klasie BigPictute do otworzenia zdjęcia w większym rozmiarze
                  string sql = "Select Picture From Mobiles Where ID = '" + textBox1ID.Text + "'";
                  if (con.State != ConnectionState.Open)
                      con.Open();
                  SqlCommand comm = new SqlCommand(sql, con);
                  SqlDataReader reader = comm.ExecuteReader();
                  reader.Read();
                  if (reader.HasRows)
                  {
                      //textBox1ID.Text = reader[0].ToString();to pobrałoby wartość ID z bazy do textboxa
                      byte[] img = (byte[])(reader[0]);//pobieram zdjęcie z bazy do tablicy img
                      if (img == null)
                      {
                          CzyDefaultoweZdjecieWpikczerboksie = true;
                           pikczerboks.Image = System.Drawing.Image.FromFile("Untitled.jpg");//defaultowe zdjęcie jeśli ktoś nie ma zdjęcia
                         // pikczerboks.Image = null;
                       }
                      else
                      {
                          MemoryStream ms = new MemoryStream(img);
                          pikczerboks.Image = System.Drawing.Image.FromStream(ms);//zmiana z Image na System.Drawing.Image bo też framework pdf itectsharp.pdfa odnosił się do Image
                          CzyDefaultoweZdjecieWpikczerboksie = false;//zmieniam flagę na false, która później jest sprawdzana przy kliknięciu na zdjęcie pikczerboxa, jeśli będzie true nie odpali się bigpicture
                      }
                  }
                  else { CzyDefaultoweZdjecieWpikczerboksie = true; }
                    con.Close();
                   }
              catch(Exception ex)
              {
                  con.Close();
                  pikczerboks.Image = System.Drawing.Image.FromFile("Untitled.jpg");//defaultowe zdjęcie jeśli ktoś nie ma zdjęcia
                 // pikczerboks.Image = null;
                  CzyDefaultoweZdjecieWpikczerboksie = true;
              }
        }  
         
          private void button2RemovePict_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want delete this picture?", "Deleting", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {  
              try
              {
                  con.Open();
                  SqlCommand cmd = new SqlCommand(@"UPDATE Mobiles SET Picture=null Where ID = '" + textBox1ID.Text + "'", con);
                  cmd.ExecuteNonQuery();
                  OpenFileDialog opend = new OpenFileDialog();
                  con.Close();
                  pictureBox1.Image = System.Drawing.Image.FromFile("Untitled.jpg");
              }
              catch
              { MessageBox.Show("Something goes wrong. Try again."); }
          }
            CzyDefaultoweZdjecieWpikczerboksie = true;
        }
          private void button2AddNewUser_Click(object sender, EventArgs e)
          {
              Form3AddNewUser form3 = new Form3AddNewUser();
              //form3.Show();
              form3.ShowDialog();//aby nie było możliwości otwarciu wielu okienek
          }

          private void allToolStripMenuItem_Click(object sender, EventArgs e)
          {
              int x;
              x = dataGridView1.CurrentCell.RowIndex;//pobieram aktualny index
              string alll = dataGridView1.Rows[x].Cells[1].Value.ToString() + ", " + dataGridView1.Rows[x].Cells[2].Value.ToString() + ", Tel: " + dataGridView1.Rows[x].Cells[3].Value.ToString() + ", E-mail: " + dataGridView1.Rows[x].Cells[4].Value.ToString() + ", Category: " + dataGridView1.Rows[x].Cells[5].Value.ToString();
              Clipboard.SetText(alll);
          }

          private void moblieNumberToolStripMenuItem_Click(object sender, EventArgs e)
          {
                  int x;
                  x = dataGridView1.CurrentCell.RowIndex;//pobieram aktualny index
                  Clipboard.SetText(dataGridView1.Rows[x].Cells[3].Value.ToString());
          }

          private void emailToolStripMenuItem_Click(object sender, EventArgs e)
          {
              int x;
              x = dataGridView1.CurrentCell.RowIndex;//pobieram aktualny index
              Clipboard.SetText(dataGridView1.Rows[x].Cells[4].Value.ToString());
          }

          private void aboutProgramToolStripMenuItem_Click(object sender, EventArgs e)
          {
              MessageBox.Show("Version 1.0 \n Author: Paweł Andrzejczyk");
          }

          private void exitToolStripMenuItem_Click(object sender, EventArgs e)
          {
              if (MessageBox.Show("Do you really want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
              { this.Close(); }
          }
          private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
          {
              int x;
              x = dataGridView1.CurrentCell.RowIndex;//pobieram aktualny index
              pobierzEmail=dataGridView1.Rows[x].Cells[4].Value.ToString();//pobieram zaznaczony email do zmiennej aby przekazać do formy Email
              Email form3 = new Email();
              //form3.Show();
              form3.ShowDialog();
          }
          private void pictureBox1_Click(object sender, EventArgs e)
          {
              BigPicture bigpic = new BigPicture();
              
              if ((pictureBox1.Image!=null)&&(CzyDefaultoweZdjecieWpikczerboksie==false))
              bigpic.Show();
          }
          private void excelToolStripMenuItem_Click(object sender, EventArgs e)
          {
              try
              {
                  Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();
                  Workbook wb = Excel.Workbooks.Add(XlSheetType.xlWorksheet);
                  Worksheet ws = (Worksheet)Excel.ActiveSheet;
                  Excel.Visible = true;
                  ws.Cells[1, 1] = "ID"; //dowolnie nazywam koolumny jakie będą w pliku excela (usuwam spację bo potem będę pisał zapytania do tego pliku przy imporcie)
                  ws.Cells[1, 2] = "FirstName";
                  ws.Cells[1, 3] = "LastName";
                  ws.Cells[1, 4] = "Mobile";
                  ws.Cells[1, 5] = "Email";
                  ws.Cells[1, 6] = "Category";

                  progressBar1.Value = 0;
                  VCardViewer vc = new VCardViewer();
                  vc.ShowProgress(progressBar1, dataGridView1.Rows.Count, 0);
                  //jeśli zamiast Rows dam SelectedRows, to exportuję tylko zaznaczone wiersze
                  for (int i = 2; i <= dataGridView1.Rows.Count + 1; i++)//zaczynam od i=2 aby nie nadpisało nagłówków powyżej stworzonych  //lub SelectedRows
                  {  
                      for (int j = 1; j <= dataGridView1.Columns.Count; j++)
                      {
                          ws.Cells[i, j] = dataGridView1.Rows[i - 2].Cells[j - 1].Value;//j-1 ponieważ w datagridzie Cells zaczynają się od 0  //lub SelectedRows
                      }
                      vc.ShowProgress(progressBar1, dataGridView1.Rows.Count, i -1);
                      label12Progress.Text = vc.ShowProgress(progressBar1, dataGridView1.Rows.Count, i-1);
                  }
                }
              catch(Exception ex)
              {
                  MessageBox.Show("Saving error. Retry."+ex);
              }
          }
          private void pdfToolStripMenuItem_Click(object sender, EventArgs e)
          {
              try
              {
                  progressBar1.Value = 0;
                  VCardViewer vc = new VCardViewer();
                  vc.ShowProgress(progressBar1, dataGridView1.Rows.Count, 0);
                  string PdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss")+".pdf";//nazwa pliku to dokładana data co do sec (HH = 24 hour clock hh = 12 hour clock)
                  //Random randomname = new Random();
                  //string PdfFileName = "Test" + randomname.Next(0, 100000).ToString() + ".pdf";
                  Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
                  PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(PdfFileName, FileMode.Create));
                  doc.Open();

                  //free text:
                  Paragraph paragraf = new Paragraph("Hello! \n This is from my aplication.\n ");
                  doc.Add(paragraf);
                  
                  iTextSharp.text.Image PNG = iTextSharp.text.Image.GetInstance("picture.jpeg");
                  PNG.ScaleToFit(250f, 250f);
                  PNG.Border = iTextSharp.text.Rectangle.BOX;
                  PNG.BorderColor = iTextSharp.text.BaseColor.YELLOW;
                  PNG.BorderWidth = 5f;
                  doc.Add(PNG);

                  PdfPTable table = new PdfPTable(dataGridView1.Columns.Count);
                  //headers
                  for (int l = 0; l < dataGridView1.Columns.Count; l++)
                  {
                      table.AddCell(new Phrase(dataGridView1.Columns[l].HeaderText));
                  }
                  //flag first row as header
                  table.HeaderRows = 1;
                  //adding rows
                  for (int m = 0; m < dataGridView1.Rows.Count; m++)
                  {
                      for (int k = 0; k < dataGridView1.Columns.Count; k++)
                      {
                          if (dataGridView1[k, m].Value != null)
                          { table.AddCell(new Phrase(dataGridView1[k, m].Value.ToString())); }
                      }
                      vc.ShowProgress(progressBar1, dataGridView1.Rows.Count, m + 1);
                      label12Progress.Text = vc.ShowProgress(progressBar1, dataGridView1.Rows.Count, m + 1);
                  }
                  doc.Add(table);
                  doc.Close();
                  MessageBox.Show("File: " +PdfFileName+" Sucessfully saved.");
              }
              catch(Exception ex)
              { MessageBox.Show("Saving error. Retry."+ex); }
          }

          private void notepadToolStripMenuItem_Click(object sender, EventArgs e)
          {
              try
              {
                  Random randomname = new Random();
                  string TxtFileName = "Test" + randomname.Next(0, 100000).ToString() + ".txt";
                  TextWriter sw = new StreamWriter(TxtFileName);

                  progressBar1.Value = 0;
                  VCardViewer vc = new VCardViewer();
                  vc.ShowProgress(progressBar1, dataGridView1.Rows.Count, 0);

                  for (int i = 0; i < dataGridView1.Rows.Count; i++) //tu nie ma dataGridView1.Rows.Count-1 do jest "<", a nie "<="         
                  {
                      for (int j = 0; j < dataGridView1.Columns.Count; j++)
                      {
                          //sw.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString() + "\t" + "|");
                          sw.Write(dataGridView1.Rows[i].Cells[j].Value.ToString() + "|"); //wersja bez \t, czyli tabulatora pomiędzy danymi a |
                      }
                      sw.WriteLine("");
                      sw.WriteLine("____________________________________________________________________");

                      vc.ShowProgress(progressBar1, dataGridView1.Rows.Count, i + 1);
                      label12Progress.Text = vc.ShowProgress(progressBar1, dataGridView1.Rows.Count, i + 1);
                  }
                  sw.Close();
                  MessageBox.Show("Data was exported to file: " + TxtFileName);
              }
              catch { MessageBox.Show("Problem with export. Please try again."); }
          }
          private void cSVToolStripMenuItem_Click(object sender, EventArgs e)
          {
              try
              {
                  string FullTextForCSVfile = "";
                  int rowCount = dataGridView1.RowCount;
                  int cellCount = dataGridView1.Rows[0].Cells.Count;
                  progressBar1.Value = 0;
                  VCardViewer vc = new VCardViewer();
                  vc.ShowProgress(progressBar1, rowCount, 0);
                  for (int row_index = 0; row_index <= rowCount - 1; row_index++)//dla każdego wiersza
                  {
                      for (int cell_index = 0; cell_index <= cellCount - 1; cell_index++)//pętla po wszystkich kolumnach
                      {
                          FullTextForCSVfile += dataGridView1.Rows[row_index].Cells[cell_index].Value.ToString() + ";";
                      }
                     // FullTextForCSVfile += "\n\r";//wersja z dodatkowym odstępem pomiędzy wierszami
                      FullTextForCSVfile += "\n";
                      vc.ShowProgress(progressBar1, rowCount, row_index + 1);
                      label12Progress.Text = vc.ShowProgress(progressBar1, rowCount, row_index + 1);
                  }
                  System.IO.File.WriteAllText(@"export.csv", FullTextForCSVfile);
                  MessageBox.Show("Export sucessful, 'export.csv' was created in main folder.");
              }
              catch { MessageBox.Show("Export to csv failed. Try again."); }
          }
          private System.Data.DataTable GetDataTableFromDGV(DataGridView dgv)//metoda zwraca DataTable
          {
              var dt = new System.Data.DataTable();
              //foreach (DataGridViewColumn column in dgv.Columns)
              //{
              //    if (column.Visible)
              //    {
                      // You could potentially name the column based on the DGV column name (beware of dupes)
                      // or assign a type based on the data type of the data bound to this DGV column.
                      dt.Columns.Add("ID");
                      dt.Columns.Add("FirstName");
                      dt.Columns.Add("LastName");
                      dt.Columns.Add("Mobile");
                      dt.Columns.Add("Email");
                      dt.Columns.Add("Category");
                      //dt.Columns.Add();gdybym zostawił foreach i to, to automatycznie numerator stworzyłby column1, column2 itd.
              //    }
              //}
              object[] cellValues = new object[dgv.Columns.Count];
              foreach (DataGridViewRow row in dgv.Rows)
              {
                  for (int i = 0; i < row.Cells.Count; i++) //uzupełnienie kolumn wartościami
                  {
                      cellValues[i] = row.Cells[i].Value;
                  }
                  dt.Rows.Add(cellValues);//uzupełnienie wierszy
              }
              return dt;
          }
          private void xMLToolStripMenuItem_Click(object sender, EventArgs e)
          {
              SaveFileDialog sfd = new SaveFileDialog();
              sfd.Filter = "XML|*.xml";
              if (sfd.ShowDialog() == DialogResult.OK)
              {
                  try
                  {
                      System.Data.DataTable dT = GetDataTableFromDGV(dataGridView1);
                      DataSet dS = new DataSet();
                      dS.Tables.Add(dT);
                      dS.WriteXml(File.OpenWrite(sfd.FileName));
                      MessageBox.Show("Saving Succesfully");
                  }
                  catch (Exception ex)
                  {
                      Console.WriteLine(ex);
                  }
              }
         }
          private void authorToolStripMenuItem_Click(object sender, EventArgs e)
          {    //ReadMe file opening
              Process notePad = new Process();
              notePad.StartInfo.FileName = "notepad.exe";
              notePad.StartInfo.Arguments = "ReadMe.txt";
              notePad.Start();
          }

          public void SetSettings()
          {
           //można użyć switch case, a można na ifach
              switch (SaveXmlSettingsFile.readingSettingsBackgroundColor())
              {
                  case "Yellow":
                      this.BackColor = System.Drawing.Color.Yellow;
                      break;
                  case "Green":
                      this.BackColor = System.Drawing.Color.Green;
                      break;
                  case "Red":
                      this.BackColor = System.Drawing.Color.Red;
                      break;
                  //default:
                  //    this.BackColor = System.Drawing.Color.Red;
                  //    break;
              }
              switch (SaveXmlSettingsFile.readingSettingsTextColor())
              {
                  case "Black":
                      this.ForeColor = System.Drawing.Color.Black;
                      break;
                  case "DarkRed":
                      this.ForeColor = System.Drawing.Color.DarkRed;
                      break;
                  case "Green":
                      this.ForeColor = System.Drawing.Color.Green;
                      break;
              }
              switch (SaveXmlSettingsFile.readingFont())
              {
                  case "10":
                      this.Font = SaveXmlSettingsFile.ChangeFontSize(this.Font, 10.0F, GraphicsUnit.Pixel);
                      break;
                  case "12":
                      this.Font = SaveXmlSettingsFile.ChangeFontSize(this.Font, 12.0F, GraphicsUnit.Pixel);
                      break;
               }
               //Background color settings
              //if (SaveXmlSettingsFile.readingSettingsBackgroundColor() == "Yellow")
              //{ this.BackColor = System.Drawing.Color.Yellow; }
              //if (SaveXmlSettingsFile.readingSettingsBackgroundColor() == "Green")
              //{ this.BackColor = System.Drawing.Color.Green; }
              //if (SaveXmlSettingsFile.readingSettingsBackgroundColor() == "Red")
              //{ this.BackColor = System.Drawing.Color.Red; }
              //TextColor settings
              //if (SaveXmlSettingsFile.readingSettingsTextColor() == "Black")
              //{ this.ForeColor = System.Drawing.Color.Black; }
              //if (SaveXmlSettingsFile.readingSettingsTextColor() == "DarkRed")
              //{ this.ForeColor = System.Drawing.Color.DarkRed; }
              //if (SaveXmlSettingsFile.readingSettingsTextColor() == "Green")
              //{ this.ForeColor = System.Drawing.Color.Green; }
              //font settings
              //if (SaveXmlSettingsFile.readingFont() == "10")
              //{ this.Font = SaveXmlSettingsFile.ChangeFontSize(this.Font, 10.0F, GraphicsUnit.Pixel); }
              //if (SaveXmlSettingsFile.readingFont() == "12")
              //{ this.Font = SaveXmlSettingsFile.ChangeFontSize(this.Font, 12.0F, GraphicsUnit.Pixel); }

              pobierzBackground = this.BackColor.Name.ToString();//pobieram przy load formy aktualne ustawienia, bo gdymym w ustawieniach zmienił tylko jedną wartość, to do pliku xml zapisałoby się tylko to ustawienie, a to drugie by się skasowało (przy każdej zmianie plik w całości się przepisuje)
              pobierzTextColor = this.ForeColor.Name.ToString();
              pobierzFontsize = this.Font.Size.ToString();
              this.Refresh();
          }
         private void greenToolStripMenuItem_Click(object sender, EventArgs e)
          {
             SaveXmlSettingsFile.SaveSettings("Green", "","");
             SetSettings();
          }
          private void yellowToolStripMenuItem_Click(object sender, EventArgs e)
          {

              SaveXmlSettingsFile.SaveSettings("Yellow", "","");
              SetSettings();
          }
          private void redToolStripMenuItem_Click(object sender, EventArgs e)
          {

              SaveXmlSettingsFile.SaveSettings("Red", "","");
              SetSettings();
          }
          private void blackToolStripMenuItem_Click(object sender, EventArgs e)
          {
              SaveXmlSettingsFile.SaveSettings("", "Black","");
              SetSettings();
          }
          private void darkRedToolStripMenuItem_Click(object sender, EventArgs e)
          {

              SaveXmlSettingsFile.SaveSettings("", "DarkRed","");
              SetSettings();
          }
          private void greenToolStripMenuItem1_Click(object sender, EventArgs e)
          {
              SaveXmlSettingsFile.SaveSettings("", "Green","");
              SetSettings();
          }
          private void ToolStripMenuItem12pxfont_Click(object sender, EventArgs e)
          {
              SaveXmlSettingsFile.SaveSettings("", "", "12");
              SetSettings();
          }
          private void ToolStripMenuItem10pxfont_Click(object sender, EventArgs e)
          {
              SaveXmlSettingsFile.SaveSettings("", "", "10");
              SetSettings();
          }
          private void creteBusinessCardToolStripMenuItem_Click(object sender, EventArgs e)
          {
              try
              {
                  int x;
                  x = dataGridView1.CurrentCell.RowIndex;//pobieram aktualny index
                  string FirstName = dataGridView1.Rows[x].Cells[1].Value.ToString();
                  string LastName = dataGridView1.Rows[x].Cells[2].Value.ToString();
                  string Mobile = dataGridView1.Rows[x].Cells[3].Value.ToString();
                  string Email = dataGridView1.Rows[x].Cells[4].Value.ToString();
                  
                  Random randomname = new Random();
                  string PdfFileName = FirstName +" "+LastName+" BuisnessCard" + randomname.Next(0, 100000).ToString() + ".pdf";
                  // string PdfFileName = "";
                  Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
                  PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(PdfFileName, FileMode.Create));
                  doc.Open();

                //dodawanie zdjęcia:
                  try
                  {
                      pobierzID = textBox1ID.Text;//w tym miejscu pobieram wartość ID, aby potem wartość tego pola wykorzystać w klasie BigPictute do otworzenia zdjęcia w większym rozmiarze
                      string sql = "Select Picture From Mobiles Where ID = '" + textBox1ID.Text + "'";
                      if (con.State != ConnectionState.Open)
                          con.Open();
                      SqlCommand comm = new SqlCommand(sql, con);
                      SqlDataReader reader = comm.ExecuteReader();
                      reader.Read();
                      if (reader.HasRows)
                      {
                          byte[] img = (byte[])(reader[0]);//pobieram zdjęcie z bazy do tablicy img
                         
                              MemoryStream ms = new MemoryStream(img);
                              iTextSharp.text.Image PNG = iTextSharp.text.Image.GetInstance(ms);
                             
                              PNG.ScaleToFit(150f, 150f);
                              PNG.Border = iTextSharp.text.Rectangle.BOX;
                              PNG.BorderColor = iTextSharp.text.BaseColor.YELLOW;
                              PNG.BorderWidth = 5f;
                              PNG.SpacingBefore = 5f;
                              PNG.SpacingAfter = 5f;
                              PNG.Alignment = Element.ALIGN_LEFT;
                              doc.Add(PNG);
                        
                      }
                      else { iTextSharp.text.Image PNG = iTextSharp.text.Image.GetInstance("Untitled.jpg"); }
                      con.Close();
                  }
                  catch { iTextSharp.text.Image PNG = iTextSharp.text.Image.GetInstance("Untitled.jpg");//jeśli człowiek nie ma zdjęcia, to difoltowe się dorzuci
                  PNG.ScaleToFit(150f, 150f);
                  PNG.Border = iTextSharp.text.Rectangle.BOX;
                  PNG.BorderColor = iTextSharp.text.BaseColor.YELLOW;
                  PNG.BorderWidth = 5f;
                  PNG.SpacingBefore = 5f;
                  PNG.SpacingAfter = 5f;
                  PNG.Alignment = Element.ALIGN_LEFT;
                  doc.Add(PNG);
                  }
                  //free text:
                  Paragraph paragraf = new Paragraph(" First Name: " + FirstName + "\n Last Name: " + LastName + "\n Mobile: " + Mobile + "\n Email: " + Email);
                  doc.Add(paragraf);
                             
                  doc.Close();
                  MessageBox.Show(PdfFileName+" sucessfully saved.");
              }
              catch (Exception ex)
              { MessageBox.Show("Saving error. Retry." + ex); }
          }

          private void importDataFromExcelToolStripMenuItem_Click(object sender, EventArgs e)
          {
              try
              {
                  CzyDodano = false;
                  int ile = 0;
                  OpenFileDialog op = new OpenFileDialog();
                  op.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                  if (op.ShowDialog() == DialogResult.Cancel)
                      return;
                  //   FileStream stream = new FileStream(op.FileName, FileMode.Open);

                  OleDbConnection conn = new OleDbConnection();
                  conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + op.FileName + ";Extended Properties=Excel 12.0;";
                  OleDbCommand command = new OleDbCommand("SELECT FirstName, LastName, Mobile, Email, Category FROM [Arkusz1$]", conn);

                  OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                  System.Data.DataTable datatable = new System.Data.DataTable();
                  //       DataSet dataset = new DataSet();
                  //   adapter.Fill(dataset);
                  adapter.Fill(datatable);
                  //         dataGridView1.DataSource = dataset.Tables[0];
                  //   dataGridView1.DataSource = datatable;
                  
                  foreach (DataRow item in datatable.Rows)
                  {
                      //   int n;
                      Liczba licz = new Liczba();//przy każdym przejściu pętli foreach (dla każdego wiersza w datatable) tworzony jest obiekt klasy Liczba, którego poniższa metoda sprawdza, czy taki wiersz już jest w bazie danych
                      string jjjj = licz.liczba(item[0].ToString(), item[1].ToString(), item[2].ToString(), item[3].ToString());//metoda zwraca numer ID w bazie dla wiersza o podanych parametrach
                      //metoda InsertNewPersonToDB też sprawdza, ale tak wyświetla tez komunikat w razie powtzrzającej się pozycji przy każdym przejściu pętli
                      if (jjjj == "")//jeśli dana pozycja nie istnieje w bazie danych, to ją dodaj:
                      {
                          CzyDodano = true;
                          //bezpośrednio do datagridview:
                          //n = dataGridView1.Rows.Add();
                          //dataGridView1.Rows[n].Cells[1].Value = item[0].ToString();
                          //dataGridView1.Rows[n].Cells[2].Value = item[1].ToString();
                          //dataGridView1.Rows[n].Cells[3].Value = item[2].ToString();
                          //dataGridView1.Rows[n].Cells[4].Value = item[3].ToString();
                          //dataGridView1.Rows[n].Cells[5].Value = item[4].ToString();
                          if (checkBox1SkipEmail.Checked)
                          {
                              InsertNewPersonToDB(item[0].ToString(), item[1].ToString(), item[2].ToString(), item[3].ToString(), item[4].ToString(), false, false);
                          }
                          else { InsertNewPersonToDB(item[0].ToString(), item[1].ToString(), item[2].ToString(), item[3].ToString(), item[4].ToString(), true, false); }//dodaje do bazy danych nieduplikujące się pozycje
                          if (CzyDodano)//zwiększ licznik tylko jeśli po wyjściu z metody jest CzyDodano=true
                          { ile++; }
                          
                      }
                      else
                      {
                          CzyDodano = false;
                      }
                  }
                  Display();
                  if (CzyDodano)
                  {
                      MessageBox.Show("Sucessfully saved.");
                  }
                  //else { ile = 0; } //jeżeli nic nie dodano zeruj licznik - on będzie nabijać się w przypadku wadliwycha emaili
                  MessageBox.Show(ile + " new row(s) was added.");
              }
              catch { MessageBox.Show("Probably your Excel file is corrupted. Check for proper columns headers and if your sheet name is 'Arkusz1'"); }
          }

          private void button2Refresh_Click(object sender, EventArgs e)
          {
              SetSettings();
              Display();
              label7.Text = "Witaj " + LogIn.Namee + " " + LogIn.LastName;//aby odświeżyć napis powitalny w razie zmiany w klasie MyProfile
          }
          private void sendSMSToolStripMenuItem_Click(object sender, EventArgs e)
          {
              int x;
              x = dataGridView1.CurrentCell.RowIndex;//pobieram aktualny index
              pobierzMobile = dataGridView1.Rows[x].Cells[3].Value.ToString();//pobieram zaznaczony email do zmiennej aby przekazać do formy Email
              WhatsUpMessager form = new WhatsUpMessager();
              //form.Show();
              form.ShowDialog();
          }

          private void importContactFromVCFFileToolStripMenuItem_Click(object sender, EventArgs e)
          {
              try
              {
                  //   textBox1ID.Text = "";
                  textBox1FirstName.Text = "";
                  textBox2LastName.Clear();
                  textBox3Mobile.Text = "";
                  textBox4Email.Clear();
                  comboBox1Category.SelectedIndex = -1;
                  OpenFileDialog op2 = new OpenFileDialog();
                  op2.Filter = "VCF Files|*.vcf";
                  if (op2.ShowDialog() == DialogResult.Cancel)
                      return;

                  vCard card = new vCard(new StreamReader(op2.FileName));
                  String[] splitText = card.FormattedName.Split(new char[] { ' ' }); //pocięcie imie i nazwsko spacjami i wrzucenie do tablicy

                  textBox1FirstName.Text = splitText[0];//zakładam, że pierwszy element to imię
                  if (splitText.Length > 1) //jeśli jest więcej elementów niż 1 , to wrzucam je wszystkie do textBox2LastName oddzielając spacją
                  {
                      for (int i = 1; i < splitText.Length; i++)
                      { textBox2LastName.Text += splitText[i]+" "; }
                      textBox2LastName.Text=textBox2LastName.Text.Trim();//ucinam ostatnią spację
                      textBox2LastName.Text=textBox2LastName.Text.TrimEnd();
                  }
                  if (card.Phones.GetFirstChoice(vCardPhoneTypes.Cellular) != null)
                  { textBox3Mobile.Text = card.Phones.GetFirstChoice(vCardPhoneTypes.Cellular).FullNumber; }
                  if (card.EmailAddresses.GetFirstChoice(vCardEmailAddressType.Internet) != null)
                  { textBox4Email.Text = card.EmailAddresses.GetFirstChoice(vCardEmailAddressType.Internet).Address; }

                  if (card.Photos.Count > 0)
                  {
                      pictureBox1.Image = card.Photos[0].GetBitmap();
                      pic = card.Photos[0].GetBytes();//do zmiennej typu byte[] wczytuje zdjęcie
                  }
                }
              catch { MessageBox.Show("Invalid file"); }
          }
          
          private void button2_Click(object sender, EventArgs e)
          {
              MyProfile formm = new MyProfile();
              //formm.Show();
              formm.ShowDialog();//aby nie było możliwości otwarciu wielu okienek
          }
       
         private void createVCardToolStripMenuItem_Click(object sender, EventArgs e)
          {
              SaveFileDialog sfd = new SaveFileDialog();
              sfd.Filter = "vCard|*.vcf";
              if (sfd.ShowDialog() == DialogResult.OK)
              {
                  try
                  {
                      int x;
                      x = dataGridView1.CurrentCell.RowIndex;//pobieram aktualny index

                      var myCard = new CreateVcard
                      {
                          FirstName = dataGridView1.Rows[x].Cells[1].Value.ToString(),
                          LastName = dataGridView1.Rows[x].Cells[2].Value.ToString(),
                          Mobile = dataGridView1.Rows[x].Cells[3].Value.ToString(),
                          Email = dataGridView1.Rows[x].Cells[4].Value.ToString()
                      };
                      // dodawanie zdjęcia:
                      try
                      {
                          byte[] img = null;
                          pobierzID = textBox1ID.Text;//w tym miejscu pobieram wartość ID, aby potem wartość tego pola wykorzystać w klasie BigPicture do otworzenia zdjęcia w większym rozmiarze
                          string sql = "Select Picture From Mobiles Where ID = '" + textBox1ID.Text + "'";
                          if (con.State != ConnectionState.Open)
                              con.Open();
                          SqlCommand comm = new SqlCommand(sql, con);
                          SqlDataReader reader = comm.ExecuteReader();
                          reader.Read();
                          if (reader.HasRows)
                          {
                              img = (byte[])(reader[0]);//pobieram zdjęcie z bazy do tablicy img

                              MemoryStream ms = new MemoryStream(img);
                              myCard.Image = img;
                          }
                          else
                          {
                              img = null;
                              myCard.Image = img;
                          }
                          con.Close();
                      }
                      catch { }

                      using (var file = File.OpenWrite(sfd.FileName))
                      using (var writer = new StreamWriter(file))
                      {
                          writer.Write(myCard.ToString());//w metda ToString() jest overload
                      }

                      MessageBox.Show("Saving Succesfully");
                  }
                  catch (Exception ex)
                  {
                      Console.WriteLine(ex);
                  }
              }
          }

          private void allDataVcardToolStripMenuItem_Click(object sender, EventArgs e)
          {
              SaveAllVcard(true);//jeśli jest true to w pętli jest dodawane zdjęcie
          }
          private void vCardwithoutImagesToolStripMenuItem_Click(object sender, EventArgs e)
          {
              SaveAllVcard(false);
          }
          public void SaveAllVcard(bool withImagesOrNot)
          {
              SaveFileDialog sfd = new SaveFileDialog();
              sfd.Filter = "vCard|*.vcf";
              if (sfd.ShowDialog() == DialogResult.OK)
              {
                  try
                  {
                      progressBar1.Value = 0;
                      VCardViewer vc = new VCardViewer();
                      vc.ShowProgress(progressBar1, dataGridView1.Rows.Count, 0);
                      using (var file = File.OpenWrite(sfd.FileName))
                      using (var writer = new StreamWriter(file))

                          for (int i = 0; i < dataGridView1.Rows.Count; i++)
                          {
                              CreateVcard myCard = new CreateVcard
                              {
                                  FirstName = dataGridView1.Rows[i].Cells[1].Value.ToString(),
                                  LastName = dataGridView1.Rows[i].Cells[2].Value.ToString(),
                                  Mobile = dataGridView1.Rows[i].Cells[3].Value.ToString(),
                                  Email = dataGridView1.Rows[i].Cells[4].Value.ToString(),
                              };
                              // dodawanie zdjęcia: 
                              //            Liczba licz = new Liczba();//tu jest ta funkcja zbędna, bo zapytanie zwraca pusstą tablicę img jeśli nic nie znajdzie dla danego ID, a w klasie CreateVcard jest warunek sprawdzający nulle dla zmiennej Image. A funkca liczba też ma w sobie zapytanie, które zostałoby wykonane i razy
                              //            string jjjj = licz.liczba(dataGridView1.Rows[i].Cells[1].Value.ToString(), dataGridView1.Rows[i].Cells[2].Value.ToString(), dataGridView1.Rows[i].Cells[3].Value.ToString(), dataGridView1.Rows[i].Cells[4].Value.ToString(), dataGridView1.Rows[i].Cells[5].Value.ToString());//metoda zwraca numer ID w bazie dla wiersza o podanych parametrach

                              //     if (jjjj != "")//jeśli dana pozycja istnieje w bazie danych, to dodaj zdjęcie:
                              //     {
                              if (withImagesOrNot == true)//jeśli podam parametr true, to będzie ze zdjęciem
                              {
                                  try
                                  {
                                      byte[] img = null; //przy każdym biegu pętli img musi być zerowane (null) aby potem była odpowiednia wartość zmiennej Image
                                      string sql = "Select Picture From Mobiles Where ID = '" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "'";
                                      if (con.State != ConnectionState.Open)
                                          con.Open();
                                      SqlCommand comm = new SqlCommand(sql, con);
                                      SqlDataReader reader = comm.ExecuteReader();
                                      reader.Read();
                                      if (reader.HasRows)
                                      {
                                          img = (byte[])(reader[0]);//pobieram zdjęcie z bazy do tablicy img

                                          MemoryStream ms = new MemoryStream(img);
                                          myCard.Image = img;
                                      }
                                      else
                                      {
                                          img = null;
                                          myCard.Image = img;//jęśli zapytanie nic nie zwróci (nie ma zdjęcia) muszę tu do zmiennej Image przypisać nulla, aby potem w klasie CreteVcard podczas tworzenia pomijało wiersz Image, a nie wstawiało z poprzedniej osoby lub pusty wiersz
                                      }
                                      con.Close();
                                  }

                                  catch { con.Close(); }
                                      //     }
                              }
                              writer.Write(myCard.ToString());
                              vc.ShowProgress(progressBar1, dataGridView1.Rows.Count, i+1);
                              label12Progress.Text = vc.ShowProgress(progressBar1, dataGridView1.Rows.Count, i + 1);
                          }
                      MessageBox.Show("Saving Succesfully");
                  }
                  catch { MessageBox.Show("Retray operation."); }
              }
          }

          private void vCardViewerToolStripMenuItem_Click(object sender, EventArgs e)
          {
              VCardViewer formm = new VCardViewer();
              //formm.Show();
              formm.ShowDialog();//aby nie było możliwości otwarciu wielu okienek
          }
           
      



        
      
              
    }
}
