using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ftpclient
{
    public delegate void transferFolderName(string FolderName);
    public partial class NewFolderForm : Form
    {
        
        public event transferFolderName transferFolderNameEvent;
        public NewFolderForm()
        {
            InitializeComponent();
        }
        public string FolderName
        {
            get
            {
                return textBox1.Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            transferFolderNameEvent?.Invoke(FolderName);
            Close();
        }
    }
}
