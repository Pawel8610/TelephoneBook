using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace TelephoneBook
{
    public partial class ServerSettings : Form
    {
        
        public ServerSettings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveXmlServerSettings.SaveServerName(textBox1.Text);
            textBox1.Text = SaveXmlServerSettings.ReadServerName();         
        }
    
        private void ServerSettings_Load(object sender, EventArgs e)
        {
             textBox1.Text = SaveXmlServerSettings.ReadServerName();
         }

     }
}
