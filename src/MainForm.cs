using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace ftpclient
{
    public partial class MainForm : Form
    {
        private ftpeventlayer ftp;
        private string currentPath;
        private string syst;
        private Thread upload_thread;
        private Thread download_thread;
        public MainForm()
        {
            InitializeComponent();
            ftp = new ftpeventlayer();
            Control.CheckForIllegalCrossThreadCalls = false; //取消跨线程访问检查
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// 刷新列表
        /// </summary>
        /// <param name="filelist">文件列表</param>
        /// <param name="workingdir">工作目录</param>
        private void refreshListView(ref string[] filelist, ref string workingdir)
        {
            if(filelist == null || workingdir == "")
            {
                return;
            }
            listView1.Items.Clear();
            listView1.Items.Add("..", 1);
            ulong file_count = 0;
            ulong folder_count = 0;
            ulong size_sum = 0;
            foreach (string f in filelist)
            {
                ListViewItem item = readFileList(f, syst);
                try
                {
                    if (item.SubItems[2].Text == "File")
                    {
                        item.ImageIndex = 0;
                        file_count++;
                        size_sum += ulong.Parse(item.SubItems[1].Text);
                    }
                    else if (item.SubItems[2].Text == "Folder")
                    {
                        item.ImageIndex = 1;
                        folder_count++;
                    }
                }
                catch (Exception)
                {

                }
                listView1.Items.Add(item);
            }
            logRichTextBox.Text = ftp.LogMessage;
            pathTextBox.Text = workingdir;
            currentPath = workingdir;
            serverStatusLabel.Text = file_count.ToString() + " 个文件 和 " + folder_count.ToString() + " 个目录。大小总计：" + size_sum.ToString() + " 字节";
        }

        #region 读取原始数据的一行
        private ListViewItem readFileList(string line, string syst)
        {
            ListViewItem item = new ListViewItem();
            try
            {
                if (syst.Contains("UNIX"))
                {
                    string name = "";
                    string size = "";
                    string type = "";
                    string date = "";
                    string authority = "";
                    string owner = "";
                    int var_flag = 0;

                    for (int i = 0; i < line.Length;)
                    {
                        while (line[i] == ' ')
                        {
                            i++;
                        }
                        if (var_flag == 0)
                        {
                            for (; line[i] != ' '; i++)
                            {
                                authority += line[i];
                            }
                            var_flag = 1;
                            continue;
                        }
                        if (var_flag == 1)
                        {
                            string typecode = "";
                            for (; line[i] != ' '; i++)
                            {
                                typecode += line[i];
                            }
                            if (typecode.Equals("1"))
                            {
                                type = "File";
                            }
                            else
                            {
                                type = "Folder";
                            }
                            var_flag = 2;
                            continue;
                        }
                        if (var_flag == 2)
                        {
                            string o1 = "", o2 = "";
                            for (; line[i] != ' '; i++)
                            {
                                o1 += line[i];
                            }
                            while (line[i] == ' ')
                            {
                                i++;
                            }
                            for (; line[i] != ' '; i++)
                            {
                                o2 += line[i];
                            }
                            owner = o1 + " " + o2;
                            var_flag = 3;
                            continue;
                        }
                        if (var_flag == 3)
                        {
                            string fs = "";
                            for (; line[i] != ' '; i++)
                            {
                                fs += line[i];
                            }
                            if (type.Equals("File"))
                            {
                                size = fs;
                            }
                            else
                            {
                                size = "";
                            }
                            var_flag = 4;
                            continue;
                        }
                        if (var_flag == 4)
                        {
                            string d1 = "", d2 = "", d3 = "";
                            for (; line[i] != ' '; i++)
                            {
                                d1 += line[i];
                            }
                            while (line[i] == ' ')
                            {
                                i++;
                            }
                            for (; line[i] != ' '; i++)
                            {
                                d2 += line[i];
                            }
                            while (line[i] == ' ')
                            {
                                i++;
                            }
                            for (; line[i] != ' '; i++)
                            {
                                d3 += line[i];
                            }
                            date = d1 + "/" + d2 + "/" + d3;
                            var_flag = 5;
                            continue;
                        }
                        if (var_flag == 5)
                        {
                            for (; i < line.Length; i++)
                            {
                                name += line[i];
                            }
                            break;
                        }
                    }
                    item.Text = name;
                    item.SubItems.Add(size);
                    item.SubItems.Add(type);
                    item.SubItems.Add(date);
                    //item.SubItems.Add(authority);
                    //item.SubItems.Add(owner);
                }
                else if (syst.Contains("Windows_NT"))
                {
                    string name = "";
                    string size = "";
                    string type = "";
                    string date = "";
                    int var_flag = 0;
                    for (int i = 0; i < line.Length;)
                    {
                        while (line[i] == ' ')
                        {
                            i++;
                        }
                        if (var_flag == 0)
                        {
                            for (; line[i] != ' '; i++)
                            {
                                date += line[i];
                            }
                            while (line[i] == ' ')
                            {
                                i++;
                            }
                            date += ' ';
                            for (; line[i] != ' '; i++)
                            {
                                date += line[i];
                            }
                            var_flag = 1;
                            continue;
                        }
                        if (var_flag == 1)
                        {
                            string temp = "";
                            for (; line[i] != ' '; i++)
                            {
                                temp += line[i];
                            }
                            if (temp.Equals("<DIR>"))
                            {
                                type = "Folder";
                            }
                            else
                            {
                                type = "File";
                                size = string.Copy(temp);
                            }
                            var_flag = 2;
                            continue;
                        }
                        if (var_flag == 2)
                        {
                            for (; i < line.Length; i++)
                            {
                                name += line[i];
                            }
                            break;
                        }
                    }
                    item.Text = name;
                    item.SubItems.Add(size);
                    item.SubItems.Add(type);
                    item.SubItems.Add(date);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("读取列表错误");
            }
            return item;
        }
        #endregion
        private void login_transferEvent(string Hostaddress, int Port, string Protocol, bool isPasstive, bool isAnonymous, string Username, string Password)
        {
            string[] filelist = null;
            string workingdir = "";
            string system = "";
            if (isAnonymous)
            {
                Username = "anonymous";
                Password = "";
            }
            if (ftp.QuickConnect(ref filelist, ref workingdir, ref system, Hostaddress, Username, Password, Port, isPasstive))
            {
                syst = string.Copy(system);
                refreshListView(ref filelist, ref workingdir);
            }
            else
            {
                MessageBox.Show("登录失败");
            }
            logRichTextBox.Text = ftp.LogMessage;
        }
        private void connectButton_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.transferEvent += new transferInfo(login_transferEvent);
            login.ShowDialog();
        }
        
        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            if (ftp.isConnected) 
            {
                abortButton_Click(sender, e);
                ftp.Disconnect();
                logRichTextBox.Text = ftp.LogMessage;
                pathTextBox.Text = "";
                currentPath = "";
                syst = "";
                serverStatusLabel.Text = "断开连接";
                listView1.Items.Clear();
            }
            else
            {
                MessageBox.Show("请先连接到一个FTP服务器");
            }
        }

        private void logRichTextBox_TextChanged(object sender, EventArgs e)
        {
            logRichTextBox.SelectionStart = logRichTextBox.TextLength;
            logRichTextBox.ScrollToCaret();
        }
        private void refreshButton_Click(object sender, EventArgs e)
        {
            if (ftp.isConnected)
            {
                string[] filelist = null;
                string workingdir = "";
                if(!ftp.Refresh(ref filelist, ref workingdir))
                {
                    MessageBox.Show("读取目录列表失败");
                }
                refreshListView(ref filelist, ref workingdir);
            }
            else
            {
                MessageBox.Show("请先连接到一个FTP服务器");
            }
        }

        private void parentDirButton_Click(object sender, EventArgs e)
        {
            if (ftp.isConnected)
            {
                string[] filelist = null;
                string workingdir = "";
                if(ftp.ParentDir(ref filelist, ref workingdir))
                {
                    refreshListView(ref filelist, ref workingdir);
                }
                else
                {
                    MessageBox.Show("操作失败");
                }
            }
            else
            {
                MessageBox.Show("请先连接到一个FTP服务器");
            }
        }

        private void changeDirButton_Click(object sender, EventArgs e)
        {
            if (ftp.isConnected)
            {
                string[] filelist = null;
                string workingdir = pathTextBox.Text;
                if (ftp.ChangeDir(ref filelist, ref workingdir))
                {
                    refreshListView(ref filelist, ref workingdir);
                }
                else
                {
                    MessageBox.Show("改变路径失败，请检查路径是否正确");
                }
            }
            else
            {
                MessageBox.Show("请先连接到一个FTP服务器");
            }
        }
        private void newFolder_tranferEvent(string foldername)
        {
            string[] filelist = null;
            string workingdir = "";
            ftp.NewFolder(ref filelist, ref workingdir, foldername);
            refreshListView(ref filelist, ref workingdir);
        }
        private void newFolderButton_Click(object sender, EventArgs e)
        {
            if (ftp.isConnected)
            {
                NewFolderForm newFolder = new NewFolderForm();
                newFolder.transferFolderNameEvent += new transferFolderName(newFolder_tranferEvent);
                newFolder.ShowDialog();
            }
            else
            {
                MessageBox.Show("请先连接到一个FTP服务器");
            }
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (ftp.isConnected)
            {
                listView1.Items[0].Selected = false;
                if (listView1.SelectedItems.Count > 0)
                {
                    string[] filelist = null;
                    string workingdir = "";
                    foreach (ListViewItem item in listView1.SelectedItems)
                    {
                        string name = item.Text;
                        bool isFile = (listView1.SelectedItems[0].SubItems[2].Text.Equals("File")) ? true : false;
                        if(!ftp.Delete(ref filelist, ref workingdir, name, isFile))
                        {
                            MessageBox.Show(name + " 无法删除");
                        }
                    }
                    refreshListView(ref filelist, ref workingdir);
                }
                else
                {
                    MessageBox.Show("请选择要删除的文件或空文件夹");
                }
            }
            else
            {
                MessageBox.Show("请先连接到一个FTP服务器");
            }
        }
        private void rename_tranferEvent(string newname)
        {
            string[] filelist = null;
            string workingdir = "";
            string oldpath = "";
            string newpath = "";
            listView1.Items[0].Selected = false;
            if (listView1.SelectedItems.Count == 1)
            {
                string oldname = listView1.SelectedItems[0].Text;
                List<string> allnames = new List<string>();
                foreach (ListViewItem item in listView1.Items)
                {
                    allnames.Add(item.Text);
                }
                if (!allnames.Contains(newname))
                {
                    if (currentPath.Equals("/"))
                    {
                        oldpath = currentPath + oldname;
                        newpath = currentPath + newname;
                    }
                    else
                    {
                        oldpath = currentPath + "/" + oldname;
                        newpath = currentPath + "/" + newname;
                    }
                    if (ftp.Rename(ref filelist, ref workingdir, oldpath, newpath))
                    {
                        refreshListView(ref filelist, ref workingdir);
                    }
                    else
                    {
                        MessageBox.Show("重命名失败");
                    }
                }
                else
                {
                    MessageBox.Show("此位置存在同名文件或文件夹");
                }
            }
            else
            {
                MessageBox.Show("请选择一个要重命名的文件或文件夹");
            }
        }
        private void renameButton_Click(object sender, EventArgs e)
        {
            if (ftp.isConnected)
            {
                RenameForm renameForm = new RenameForm();
                renameForm.transferNewFileNameEvent += new transferNewFileName(rename_tranferEvent);
                renameForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("请先连接到一个FTP服务器");
            }
        }
        private void upload()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".*";
            ofd.Filter = "file|*.*";
            ofd.ShowDialog();
            string filename = ofd.FileName;
            if(filename != "")
            {
                try
                {
                    string[] filelist = null;
                    string workingdir = "";
                    if (ftp.Upload(ref filelist, ref workingdir, filename))
                    {
                        serverStatusLabel.Text = "上传成功";
                    }
                    else
                    {
                        MessageBox.Show("上传失败");
                    }
                    refreshListView(ref filelist, ref workingdir);
                    logRichTextBox.Text = ftp.LogMessage;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("上传失败");
                    //MessageBox.Show(ex.ToString());
                }
            }
            enableOPS();
        }
        private void download()
        {
            listView1.Items[0].Selected = false;
            if (listView1.SelectedItems.Count > 0)
            {
                bool checkAllItems = true;
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    if (!item.SubItems[2].Text.Equals("File"))
                    {
                        checkAllItems = false;
                    }
                }
                if (checkAllItems == true)
                {
                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    fbd.Description = "请选择下载目录";
                    fbd.ShowDialog();
                    try
                    {
                        foreach (ListViewItem item in listView1.SelectedItems)
                        {
                            string remote_filename = item.Text;
                            string local_filepath = fbd.SelectedPath;
                            if(local_filepath != "")
                            {
                                if (!ftp.Download(remote_filename, local_filepath))
                                {
                                    MessageBox.Show("文件 " + remote_filename + " 下载失败");
                                }
                            }
                            else
                            {
                                return;
                            }
                        }
                        serverStatusLabel.Text = "下载完成";
                        logRichTextBox.Text = ftp.LogMessage;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("下载失败");
                        //MessageBox.Show(ex.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("不能选择文件夹");
                }
            }
            else
            {
                MessageBox.Show("请先选择要下载的文件");
            }
            enableOPS();
        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            if (ftp.isConnected)
            {
                serverStatusLabel.Text = "正在上传";
                //upload();
                
                upload_thread = new Thread(upload);
                upload_thread.TrySetApartmentState(ApartmentState.STA);
                upload_thread.Start();
                disableOPS();
            }
            else
            {
                MessageBox.Show("请先连接到一个FTP服务器");
            }
        }

        private void downloadButton_Click(object sender, EventArgs e)
        {
            if (ftp.isConnected)
            {
                serverStatusLabel.Text = "正在下载";
                //download();
                
                download_thread = new Thread(download);
                download_thread.TrySetApartmentState(ApartmentState.STA);
                download_thread.Start();
                disableOPS();
            }
            else
            {
                MessageBox.Show("请先连接到一个FTP服务器");
            }
        }

        private void abortButton_Click(object sender, EventArgs e)
        {
            if (ftp.isConnected)
            {
                if (upload_thread != null && upload_thread.ThreadState == ThreadState.Running)
                {
                    ftp.Abort();
                    upload_thread.Abort();
                    serverStatusLabel.Text = "上传中止";
                    logRichTextBox.Text = ftp.LogMessage;
                }
                if (download_thread != null && download_thread.ThreadState == ThreadState.Running)
                {
                    ftp.Abort();
                    download_thread.Abort();
                    serverStatusLabel.Text = "下载中止";
                    logRichTextBox.Text = ftp.LogMessage;
                }
                enableOPS();
            }
            else
            {
                MessageBox.Show("请先连接到一个FTP服务器");
            }
        }

        private void showAboutForm(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count == 1)
            {
                if (listView1.Items[0].Selected)
                {
                    string[] filelist = null;
                    string workingdir = "";
                    if (ftp.ParentDir(ref filelist, ref workingdir))
                    {
                        refreshListView(ref filelist, ref workingdir);
                    }
                    else
                    {
                        MessageBox.Show("操作失败");
                    }
                }
                else if(listView1.SelectedItems[0].SubItems[2].Text.Equals("Folder"))
                {
                    if(pathTextBox.Text.Equals("/"))
                    {
                        pathTextBox.Text += listView1.SelectedItems[0].Text;
                    }
                    else
                    {
                        pathTextBox.Text += "/" + listView1.SelectedItems[0].Text;
                    }
                    string[] filelist = null;
                    string workingdir = pathTextBox.Text;
                    if (ftp.ChangeDir(ref filelist, ref workingdir))
                    {
                        refreshListView(ref filelist, ref workingdir);
                    }
                    else
                    {
                        MessageBox.Show("改变路径失败，请检查路径是否正确");
                    }
                }
            }
        }

        private void clearLogButton_Click(object sender, EventArgs e)
        {
            logRichTextBox.Text = "";
            if (ftp != null)
            {
                ftp.LogMessage = "";
            }
        }

        private void exportLogButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "请选择导出目录";
            sfd.FileName = "log";
            sfd.DefaultExt = ".txt";
            sfd.Filter = "文本文件(*.txt)|*.txt";
            sfd.ShowDialog();

            string localFilePath = sfd.FileName.ToString();
            FileStream logStream = new FileStream(localFilePath, FileMode.OpenOrCreate, FileAccess.Write);
            byte[] buffer = Encoding.UTF8.GetBytes(logRichTextBox.Text);
            logStream.Write(buffer, 0, buffer.Length);
            logStream.Close();
        }

        private void disableOPS()
        {
            connectButton.Enabled = false;
            disconnectButton.Enabled = false;
            downloadButton.Enabled = false;
            uploadButton.Enabled = false;
            parentDirButton.Enabled = false;
            refreshButton.Enabled = false;
            changeDirButton.Enabled = false;
            newFolderButton.Enabled = false;
            deleteButton.Enabled = false;
            renameButton.Enabled = false;
            connectToolStripMenuItem.Enabled = false;
            disconnectToolStripMenuItem.Enabled = false;
            exitToolStripMenuItem.Enabled = false;
            uploadToolStripMenuItem.Enabled = false;
            downloadToolStripMenuItem.Enabled = false;
            parentDirToolStripMenuItem.Enabled = false;
            refreshToolStripMenuItem.Enabled = false;
            changeDirToolStripMenuItem.Enabled = false;
            newFolderToolStripMenuItem.Enabled = false;
            deleteToolStripMenuItem.Enabled = false;
            renameToolStripMenuItem.Enabled = false;
        }
        private void enableOPS()
        {
            connectButton.Enabled = true;
            disconnectButton.Enabled = true;
            downloadButton.Enabled = true;
            uploadButton.Enabled = true;
            parentDirButton.Enabled = true;
            refreshButton.Enabled = true;
            changeDirButton.Enabled = true;
            newFolderButton.Enabled = true;
            deleteButton.Enabled = true;
            renameButton.Enabled = true;
            connectToolStripMenuItem.Enabled = true;
            disconnectToolStripMenuItem.Enabled = true;
            exitToolStripMenuItem.Enabled = true;
            uploadToolStripMenuItem.Enabled = true;
            downloadToolStripMenuItem.Enabled = true;
            parentDirToolStripMenuItem.Enabled = true;
            refreshToolStripMenuItem.Enabled = true;
            changeDirToolStripMenuItem.Enabled = true;
            newFolderToolStripMenuItem.Enabled = true;
            deleteToolStripMenuItem.Enabled = true;
            renameToolStripMenuItem.Enabled = true;
        }
    }
}
