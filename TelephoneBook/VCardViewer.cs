using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Thought.vCards;

namespace TelephoneBook
{
    public partial class VCardViewer : Form
    {
        //SqlConnection con = new SqlConnection("Data Source=PAWEL-PC\\KURS; Initial Catalog=Phone; Integrated Security=true");
        SqlConnection con = new SqlConnection("Data Source=" + LogIn.ServerName + "; Initial Catalog=Phone; Integrated Security=true");
        public byte[] pic;
        public List<CreateVcard> vCardss = new List<CreateVcard>();
        BindingSource itemsBinding = new BindingSource();//do wiązania danych na liście
        delegate string ShowProgressDelegate(ProgressBar progressbar, int totalDigits, int digitsSoFar);

        public VCardViewer()
        {
            InitializeComponent();
            itemsBinding.DataSource = vCardss;
            listBox1.DataSource = itemsBinding;
            listBox1.DisplayMember = "Display";
            listBox1.ValueMember = "Display";
        }
        private void VCardViewer_Load(object sender, EventArgs e)
        {
            SetSettings();
        }
      public string ShowProgress(ProgressBar progressbar, int totalDigits, int digitsSoFar)
        {
                progressbar.Maximum = totalDigits;
                progressbar.Step = 1;
                progressbar.Value = digitsSoFar;
               // Show progress asynchronously
                if (InvokeRequired)
                {
                    ShowProgressDelegate showProgresss = new ShowProgressDelegate(ShowProgress);
                    BeginInvoke(showProgresss, new object[] { totalDigits, digitsSoFar });
                }
              return ((digitsSoFar * 100) / totalDigits).ToString() + " %";
           }
  private void button2_Click(object sender, EventArgs e)//opening vcf file
        {
            try
            {
                label3Percentage.Text = "0%";
                progressBar1.Value = 0;
                string F = "";
                string L = "";
                string M = "";
                string E = "";
                OpenFileDialog op2 = new OpenFileDialog();
                op2.Filter = "VCF Files|*.vcf";
                if (op2.ShowDialog() == DialogResult.Cancel)
                    return;

//wrzucam zawartość pliku vcf do stringa:
                StringBuilder sb = new StringBuilder();
     using (StreamReader sr = new StreamReader(op2.FileName)) 
    {
       String line;
        while ((line = sr.ReadLine()) != null) 
       {
            sb.AppendLine(line);
       }
    }
    string allines = sb.ToString();

    //tne na kawałki wg stringa "BEGIN:VCARD" i kazwałki te od razu wrzucam do tablicy splitText
    String[] splitText = allines.Split(new string[] { "BEGIN:VCARD" }, StringSplitOptions.None);

     ShowProgress(progressBar1, splitText.Length, 0);

                for(int x=1;x<splitText.Length;x++) //tu nie od x=0 ponieważ pierwszy element to "Begin:VCARD" - chcę go pominąć
                {
                    F = "";
                    L = "";
                    M = "";
                    E = "";
                    pic = null;

                    TextReader input = new StringReader("BEGIN:VCARD" + splitText[x]); //do każdego otrzymanego stringa muszę dokleić "BEGIN:VCARD", bo powyżej split usunął

      //z każdym elementem listy tworzę obiekt klasy vCard, która wydobędzie z niego dane
                vCard card = new vCard(input); //niestety biblioteka ta nie zwraca osobno firstname i lastname tylko wszzystko razem - FormattedName
                if (card.FormattedName != null)
                {
                      String[] splitText2 = card.FormattedName.Split(new char[] { ' ' }); //pocięcie imie i nazwsko spacjami i wrzucenie do tablicy

                  F = splitText2[0];//zakładam, że pierwszy element to imię
                  if (splitText2.Length > 1) //jeśli jest więcej elementów niż 1 , to wrzucam je wszystkie do textBox2LastName oddzielając spacją
                  {
                      for (int i = 1; i < splitText2.Length; i++)
                      { L += splitText2[i]+" "; }
                      L=L.Trim();//ucinam ostatnią spację
                      L=L.TrimEnd();
                  }
                 }
                else { F = ""; L = ""; }
                if (card.Phones.GetFirstChoice(vCardPhoneTypes.Cellular) != null)
                { M = card.Phones.GetFirstChoice(vCardPhoneTypes.Cellular).FullNumber; }
                else { M = ""; }
                if (card.EmailAddresses.GetFirstChoice(vCardEmailAddressType.Internet) != null)
                { E = card.EmailAddresses.GetFirstChoice(vCardEmailAddressType.Internet).Address; }
                else { E = ""; }
                if (card.Photos.Count > 0)
                {
                      pic = card.Photos[0].GetBytes();//do zmiennej typu byte[] wczytuje zdjęcie
                }
            //każdy obiekt wrzucam na listę (łącznie ze ewentualnymi zdjęciami, aby potem pokazać w pictureboxie oraz/lub móc dodać do bazy)
                //byte[] First = Encoding.Default.GetBytes(F);
                //byte[] Last = Encoding.Default.GetBytes(L);
                //F = Encoding.UTF8.GetString(First);
                //F = Encoding.UTF8.GetString(Last);
                vCardss.Add(new CreateVcard { FirstName = F, LastName = L, Mobile = M, Email = E, Image=pic });
                itemsBinding.ResetBindings(false);//odświeżenie wiązań listy
                ShowProgress(progressBar1, splitText.Length, x + 1);//update progressbara
                label3Percentage.Text = ShowProgress(progressBar1, splitText.Length, x + 1);
              }
         }
            catch { MessageBox.Show("Invalid file"); }
            label2.Text = vCardss.Count.ToString();
           
        }
        //public void SavePictureToDB(byte[] img)
        //{
        //    try
        //    {

        //        string sql = "UPDATE Mobiles SET Picture=@img WHERE (ID='" + textBox1ID.Text + "')";
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand(sql, con);
        //        cmd.Parameters.Add(new SqlParameter("@img", img));
        //        cmd.ExecuteNonQuery();
        //        con.Close();

        //    }
        //    catch { con.Close(); }
        //}
        public void InsertNewPersonToDB(string First, string Last, string Mobile, string Emmail, string Category, byte[] img)
        {
          try
            {
                        con.Open();
                        if ((img != null)&&(checkBox1AddImage.Checked))
                        {
                            ISettings add = new Form1();
                            if (checkBox1SkipEmail.Checked)
                            {
                                add.InsertNewPersonToDB(First, Last, Mobile, Emmail, Category, false, true, img);
                            }
                            else { add.InsertNewPersonToDB(First, Last, Mobile, Emmail, Category, true, true, img);}
                           
//                            SqlCommand cmd = new SqlCommand(@"INSERT INTO Mobiles (First, Last, Mobile, Email, Category, Picture) 
//            VALUES ('" + First + "','" + Last + "','" + Mobile + "','" + Emmail + "','" + Category + "',@img)", con);      //@ daje możliwość rozbicia stringy na wiele linijek 
//                            cmd.Parameters.Add(new SqlParameter("@img", img));
//                            cmd.ExecuteNonQuery();
                        }
                        else {
                               ISettings add = new Form1();
                               if (checkBox1SkipEmail.Checked)
                               {
                                   add.InsertNewPersonToDB(First, Last, Mobile, Emmail, Category, false, false);
                               }
                               else { add.InsertNewPersonToDB(First, Last, Mobile, Emmail, Category, true, false); }
                              // SqlCommand cmd = new SqlCommand(@"INSERT INTO Mobiles (First, Last, Mobile, Email, Category) 
//            VALUES ('" + First + "','" + Last + "','" + Mobile + "','" + Emmail + "','" + Category + "')", con);      //@ daje możliwość rozbicia stringy na wiele linijek 
//                            cmd.ExecuteNonQuery();
                             }
                        con.Close();
          }
            catch
            {
                con.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e) //dodawanie zaznaczonych el. listy
        {
            try
            {
                int ile = 0;
                for (int x = 0; x < listBox1.Items.Count; x++)
                {
                    if (listBox1.GetSelected(x) == true) //jeśli zaznaczony
                    {
                        CreateVcard selected = (CreateVcard)listBox1.Items[x];
                        Liczba licz = new Liczba();
                        string jjjj = licz.liczba(selected.FirstName, selected.LastName, selected.Mobile, selected.Email);//metoda zwraca numer ID w bazie dla wiersza o podanych parametrach

                        if (jjjj == "")
                        {
                            InsertNewPersonToDB(selected.FirstName, selected.LastName, selected.Mobile, selected.Email, "", selected.Image);
                            if (Form1.CzyDodano)
                            {ile++;}
                        }
                        else { MessageBox.Show("Person: " + selected.FirstName + " " + selected.LastName + " " + selected.Mobile + " " + selected.Email + " exist in database."); }
                       // listBox1.SetSelected(x, false); //odznacz zaznaczone
                    }
                  }
                MessageBox.Show(ile + " new row(s) was added.");
            }
            catch { MessageBox.Show("Please select data."); }
        }

        private void button3_Click(object sender, EventArgs e) //dodawanie wszystkich pozycji z listy do bazy
        {
            try
            {
                int ile = 0;
                // CreateVcard selected = (CreateVcard)listBox1.SelectedItems[x];
                //for (int i = 0; i < vCardss.Count;i++ )
                //{
                //    CreateVcard selected = (CreateVcard)listBox1.SelectedItems[i];
                //    // var ms = new MemoryStream(selected.Image);
                //    InsertNewPersonToDB(selected.FirstName, selected.LastName, selected.Mobile, selected.Email, "", selected.Image);
                //}
                foreach (CreateVcard vkars in vCardss)
                {
                    InsertNewPersonToDB(vkars.FirstName, vkars.LastName, vkars.Mobile, vkars.Email, "", vkars.Image);
                    if (Form1.CzyDodano)
                    { ile++; }
                }
                MessageBox.Show(ile + " new row(s) was added.");
            }
            catch { MessageBox.Show("Please select data."); }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Display();
        }
        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {
            Display();
        }
        private void Display()
        {
            try
            {
                if (vCardss == null)
                { MessageBox.Show("Please add some vCard files to this list"); }
                else
                {
                    Bitmap bmp;
                    pictureBox1.Image = null;
                    CreateVcard selected = (CreateVcard)listBox1.SelectedItem;//np. selected.FirstName zwróci tylko FirstName zaznaczonej pozycji listy vCardss

                    //selected.FirstName=
                    //selected.LastName=
                    //selected.Mobile=
                    //selected.Email=

                    if (selected.Image != null)
                    {
                        using (var ms = new MemoryStream(selected.Image))
                        {
                            bmp = new Bitmap(ms);  //konwersja byte do bitmapy
                        }
                        pictureBox1.Image = bmp;
                    }
                    else { pictureBox1.Image = null; }
                }
            }
            catch { }
            //  MessageBox.Show(selected.FirstName + " " + selected.LastName + " " + selected.Mobile + " " + selected.Email);
            //  itemsBinding.ResetBindings(false);//odświeżenie wiązań listy
        }
        
        public void SaveAllVcard(bool withImagesOrNot)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "vCard|*.vcf";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var file = File.OpenWrite(sfd.FileName))
                    using (var writer = new StreamWriter(file))
                        for (int i = 0; i < listBox1.Items.Count; i++)
                        {
                            CreateVcard selected = (CreateVcard)listBox1.Items[i];
                            CreateVcard myCard = new CreateVcard
                            {
                                FirstName = selected.FirstName,
                                LastName = selected.LastName,
                                Mobile = selected.Mobile,
                                Email = selected.Email,
                            };
                             
                            if (withImagesOrNot == true)//jeśli podam parametr true, to będzie ze zdjęciem
                            {
                                try
                                {
                                    byte[] img = null; //przy każdym biegu pętli img musi być zerowane (null) aby potem była odpowiednia wartość zmiennej Image
                                   
                                    if (selected.Image!=null)
                                    {
                                        img = (byte[])(selected.Image);//pobieram zdjęcie z bazy do tablicy img

                                        MemoryStream ms = new MemoryStream(img);
                                        myCard.Image = img;
                                    }
                                    else
                                    {
                                        img = null;
                                        myCard.Image = img;//jęśli nie ma zdjęcia muszę tu do zmiennej Image przypisać nulla, aby potem w klasie CreteVcard podczas tworzenia pomijało wiersza Image, a nie wstawiało z poprzedniej osoby lub pusty wiersz
                                    }
                                    con.Close();
                                }

                                catch
                                {
                                    con.Close();
                                    MessageBox.Show("Retray operation.");
                                }
                             }
                            writer.Write(myCard.ToString());
                        }
                    MessageBox.Show("Saving Succesfully");
                }
                catch { MessageBox.Show("Retray operation."); }
            }
        }

        public void SetSettings()
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

        private void button4Remove_Click(object sender, EventArgs e) //removing selected positions
        {
                int ile = 0;
                for (int x = 0; x < listBox1.Items.Count; x++)
                {
                    if (listBox1.GetSelected(x) == true) //jeśli zaznaczony
                    {
                        CreateVcard selected = (CreateVcard)listBox1.Items[x];
                        vCardss.Remove(selected);
                           ile++;
                    }
                  }
                MessageBox.Show(ile + " new row(s) was removed.");
                itemsBinding.ResetBindings(false);//odświeżenie wiązań listy
                label2.Text = vCardss.Count.ToString();
        }
        private void button3SaveVcard_Click(object sender, EventArgs e)
        {
            if (checkBox1AddImage.Checked)
            {SaveAllVcard(true); }
            else
            { SaveAllVcard(false); }
        }

        private void button3ClearList_Click(object sender, EventArgs e)
        {
            vCardss.Clear();
            itemsBinding.ResetBindings(false);//odświeżenie wiązań listy
            label2.Text = "0";
        }

        


    }
}
