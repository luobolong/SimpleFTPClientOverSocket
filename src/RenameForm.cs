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
    public delegate void transferNewFileName(string NewFileName);
    public partial class RenameForm : Form
    {
        
        public event transferNewFileName transferNewFileNameEvent;
        public RenameForm()
        {
            InitializeComponent();
        }
        public string NewFileName
        {
            get
            {
                return textBox1.Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            transferNewFileNameEvent?.Invoke(NewFileName);
            Close();
        }
    }
}
