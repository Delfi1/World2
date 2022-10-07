using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DelfiApp
{
    public partial class DeLog : KryptonForm
    {
        WebClient client = new WebClient();
        public DeLog()
        {
            InitializeComponent();
            kryptonRichTextBox1.Text = client.DownloadString("https://raw.githubusercontent.com/Delfi1/DeWorld/master/Text1.txt"); 
            
        }
    }
}
