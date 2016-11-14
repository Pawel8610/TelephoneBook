
namespace TelephoneBook
{
    public class settings
    {
        private string background;
        private string textColor;
        private string font;
        private string servername;

        public string Background//metada zapisująca/odczytująca pole background
        {
            get { return background; }
            set { background = value; }
        }
        public string TextColor
        {
            get { return textColor; }
            set { textColor = value; }
        }
        public string Font
        {
            get { return font; }
            set { font = value; }
        }
        public string Servername
        {
            get { return servername; }
            set { servername = value; }
        }
    }
}
