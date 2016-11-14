using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneBook
{
    interface ISettings
    {
        void SetSettings();
        void InsertNewPersonToDB(string First, string Last, string Mobile, string Emmail, string Category, bool CzyWalidacjaEmaila, bool WithImage, params byte[] img);//params byte[] img - opcjonalny parametr
    }
}
