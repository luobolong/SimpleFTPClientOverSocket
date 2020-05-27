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
    public delegate void newFolderEventHandler(string FolderName);
    public partial class NewFolderForm : Form
    {
        
        public event newFolderEventHandler newFolderEvent;
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
        private void ok()
        {
            if (FolderName.Equals(string.Empty))
            {
                MessageBox.Show("文件夹名为空");
            }
            else
            {
                newFolderEvent?.Invoke(FolderName);
                Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ok();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                ok();
            }
        }
    }
}
