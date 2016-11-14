using System;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace TelephoneBook
{
    class SaveXmlSettingsFile
    {

       public static string pobierzIDUsera = LogIn.UserID+ ".xml";//generator nazwy pliku xml, w zależności od zalogowanej osoby, ustawienia będą ładowały się
         
        public static void SaveData(object obj, string filename)
       {
           try
           {
               XmlSerializer sr = new XmlSerializer(obj.GetType());
               TextWriter writer = new StreamWriter(filename);
               sr.Serialize(writer, obj);
               writer.Close();
           }
           catch { }
        }

        public static void SaveSettings(string ColorOfBackground, string ColorOfText, string FontSizee)
        {
                 try
            {
                settings info = new settings();
                if (ColorOfBackground != "")//jeśli kolor tła jest podany to 
                {
                    info.Background = ColorOfBackground;
                    info.TextColor = Form1.pobierzTextColor;
                    info.Font = Form1.pobierzFontsize;
                    Form1.pobierzBackground = ColorOfBackground;
                }
                if (ColorOfText != "")
                {
                    info.Background = Form1.pobierzBackground;//zostanie ono pobrane ze zmiennej, która ma tą właściwaść
                    info.TextColor = ColorOfText;//kolor napisów zostanie ustawiony na na podany jako 2 argument
                    info.Font = Form1.pobierzFontsize;//zostanie ono pobrane ze zmiennej, która ma tą właściwaść
                    Form1.pobierzTextColor = ColorOfText;//update zmiennej, aby nie zawierała już wartości początkowej (wartość początkowa do tej zmiennej jest zapisywana przy starcie programu)
                }
                if(FontSizee !="")
                {
                    info.Background = Form1.pobierzBackground;
                    info.TextColor = Form1.pobierzTextColor;
                    info.Font = FontSizee;
                    Form1.pobierzFontsize = FontSizee;
    
                }
                SaveData(info, pobierzIDUsera);//Form1.pobierzIDUsera to unikalna nazwa pliku xml o nazwie jego UserID z bazy
            }
            catch (Exception ex)
            {
             //   MessageBox.Show(ex.Message);
            }
        }

//metody do odczytu z pliku xml
        public static string readingSettingsBackgroundColor()
        {
            try
            {
                if (File.Exists(SaveXmlSettingsFile.pobierzIDUsera))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(settings));
                    FileStream read = new FileStream(SaveXmlSettingsFile.pobierzIDUsera, FileMode.Open, FileAccess.Read, FileShare.Read);
                    settings info = (settings)xs.Deserialize(read);
                    read.Close();
                    return info.Background;//metoda zwraca string koloru background
                }
                return "";
            }
            catch { return ""; }
        }
        public static string readingSettingsTextColor()
        {
            try
            {
                if (File.Exists(SaveXmlSettingsFile.pobierzIDUsera))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(settings));
                    FileStream read = new FileStream(SaveXmlSettingsFile.pobierzIDUsera, FileMode.Open, FileAccess.Read, FileShare.Read);
                    settings info = (settings)xs.Deserialize(read);
                    read.Close();
                    return info.TextColor;
                }
                return "";
            }
            catch { return ""; }
        }

        public static string readingFont()
        {
            try
            {
                if (File.Exists(SaveXmlSettingsFile.pobierzIDUsera))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(settings));
                    FileStream read = new FileStream(SaveXmlSettingsFile.pobierzIDUsera, FileMode.Open, FileAccess.Read, FileShare.Read);
                    settings info = (settings)xs.Deserialize(read);
                    read.Close();
                    return info.Font;
                }
                return "";
            }
            catch { return ""; }
        }

        public static System.Drawing.Font ChangeFontSize(System.Drawing.Font font, float fontSize, GraphicsUnit unit)
        {//dodatkowa metda zmieniająca rozmiar fontu
            if (font != null)
            {
                float currentSize = font.Size;
                if (currentSize != fontSize)
                {
                    font = new System.Drawing.Font(font.Name, fontSize, unit);
                }
            }
            return font;
        }


    }
}