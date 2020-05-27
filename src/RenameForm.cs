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
    public delegate void renameEventHandler(string NewFileName);
    public partial class RenameForm : Form
    {
        
        public event renameEventHandler renameEvent;
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
        private void ok()
        {
            if (NewFileName.Equals(string.Empty))
            {
                MessageBox.Show("名称为空");
            }
            else
            {
                renameEvent?.Invoke(NewFileName);
                Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ok();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue == 13)
            {
                ok();
            }
        }
    }
}
