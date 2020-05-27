using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ftpclient
{
    public delegate void loginEventHandler(string Hostaddress, int Port, string Protocol, bool isPasstive, bool isAnonymous, string Username, string Password);
    public partial class LoginForm : Form
    {
        public event loginEventHandler loginEvent;
        #region 属性
        public string Hostaddress
        {
            get
            {
                return addressTextBox.Text;
            }
            set
            {
                addressTextBox.Text = value;
            }
        }
        public int Port 
        {
            get
            {
                int port;
                try 
                { 
                    port = int.Parse(portTextBox.Text);
                }
                catch
                {
                    return -1;
                }
                return port;
            }
        }
        public string Protocol 
        {
            get
            {
                return protocolComboBox.SelectedItem.ToString();
            }
        }
        public bool isPasstive
        {
            get
            {
                return radioButton1.Checked;
            }
        }
        public bool isAnonymous
        {
            get
            {
                if (anonymousCheckBox.Checked)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public string Username
        {
            get
            {
                return usernameTextBox.Text;
            }
        }
        public string Password 
        {
            get
            {
                return passwordTextBox.Text;
            }
        }
        #endregion
        public LoginForm()
        {
            InitializeComponent();
        }
        private void Login_Load(object sender, EventArgs e)
        {
            protocolComboBoxItems();
            radioButton1.Checked = true;
        }
        /// <summary>
        /// 默认为FTP协议
        /// </summary>
        private void protocolComboBoxItems()
        {
            protocolComboBox.Items.Add("FTP");
            protocolComboBox.SelectedItem = protocolComboBox.Items[0];
        }
        private void anonymousCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            usernameTextBox.Text = "";
            usernameTextBox.ReadOnly = !usernameTextBox.ReadOnly;
            passwordTextBox.Text = "";
            passwordTextBox.ReadOnly = !passwordTextBox.ReadOnly;
        }

        private bool checkInput()
        {
            if (Regex.IsMatch(Hostaddress, "^[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}$") &&
                (Port > -1 && Port < 65536))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void connect()
        {
            if (checkInput())
            {
                loginEvent?.Invoke(Hostaddress, Port, Protocol, isPasstive, isAnonymous, Username, Password); // 使用委托把参数传给主窗口
                Close();
            }
            else
            {
                MessageBox.Show("服务器地址或端口为无效值");
            }
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            connect();
        }

        private void passwordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue == 13)
            {
                connect();
            }
        }
    }
}
