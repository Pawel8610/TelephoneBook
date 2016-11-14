using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneBook
{
    public class CreateVcard
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public byte[] Image { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine("BEGIN:VCARD");
            builder.AppendLine("VERSION:2.1");
           // Name
            builder.AppendLine("N:" + LastName + ";" + FirstName);
            // Full name
            builder.AppendLine("FN:" + FirstName + " " + LastName);
            // Address
            //builder.Append("ADR;HOME;PREF:;;");
            //builder.Append(StreetAddress + ";");
            //builder.Append(City + ";;");
            //builder.Append(Zip + ";");
            //builder.AppendLine(CountryName);
            // Other data
            //builder.AppendLine("ORG:" + Organization);
            //builder.AppendLine("TITLE:" + JobTitle);
            //builder.AppendLine("TEL;HOME;VOICE:" + Phone);
            builder.AppendLine("TEL;CELL;VOICE:" + Mobile);
            //builder.AppendLine("URL;" + HomePage);
            builder.AppendLine("EMAIL;PREF;INTERNET:" + Email);
            // Add image
            if (Image != null)//aby w przypadku kiedy nie ma zdjęcia nie wysypało się-nie próbowało konwertować pustej tablicy byte
            { builder.AppendLine("PHOTO;ENCODING=BASE64;TYPE=JPEG:" + Convert.ToBase64String(Image)); }
        //    builder.AppendLine(string.Empty);
            builder.AppendLine("END:VCARD");
            return builder.ToString();
        }

        public string Display //metoda określająca sposób wyświetlenia danych
        {//metoda zwraca Title oraz Price w odpowiednim formacie "Title - $Price"
            get
            { return string.Format("{0}, {1}, {2}, {3}", FirstName, LastName, Mobile, Email); }

        }







    }
}
