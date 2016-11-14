using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TelephoneBook
{
    public class SaveXmlServerSettings
    {
        //private string servername;

        //public string Servername
        //{
        //    get { return servername; }
        //    set { servername = value; }
        //}

       
         public static void SaveServerName(string NewServerNameToSave)//metoda zapisuje nazwę serwera w pliku xml - blok servername
         {
             try
             {
                 settings save = new settings();
                 save.Servername = NewServerNameToSave;
                 XmlSerializer sr = new XmlSerializer(save.GetType());
                 TextWriter writer = new StreamWriter("serversettings.xml");
                 sr.Serialize(writer, save);
                 writer.Close();
               }
             catch { }
          }
         public static string ReadServerName()
         {
             try
             {
                 if (File.Exists("serversettings.xml")) //metoda czyta blok serwername z pliku xml
                 {
                     XmlSerializer xs = new XmlSerializer(typeof(settings));
                     FileStream read = new FileStream("serversettings.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                     settings info = (settings)xs.Deserialize(read);
                     read.Close();  //aby zwolnić użycie pliku po odczycie i możliwić np. zapis
                     return info.Servername;
                 }
                 return "";
             }
             catch { return ""; }
         }
       

    }
}
