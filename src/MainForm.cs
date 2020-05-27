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
using Microsoft.WindowsAPICodePack.Dialogs;

namespace ftpclient
{
    public partial class MainForm : Form
    {
        private ftpeventlayer ftp;
        private string currentPath;
        private string syst;
        private Thread upload_thread;
        private Thread download_thread;
        private Thread updateProgressBar_thread;
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
                ListViewItem item = getItem(f, syst);
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

        private ListViewItem getItem(string line, string syst)
        {
            ListViewItem item = new ListViewItem();
            try
            {
                string[] res = ftp.readLine(line, syst);
                item.Text = res[0];
                item.SubItems.Add(res[1]);
                item.SubItems.Add(res[2]);
                item.SubItems.Add(res[3]);
            }
            catch
            {
                MessageBox.Show("读取列表错误");
            }
            return item;
        }
        private void login(string Hostaddress, int Port, string Protocol, bool isPasstive, bool isAnonymous, string Username, string Password)
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
            LoginForm loginForm = new LoginForm();
            loginForm.loginEvent += new loginEventHandler(login);
            loginForm.ShowDialog();
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
        private void refreshList()
        {
            string[] filelist = null;
            string workingdir = "";
            if (!ftp.Refresh(ref filelist, ref workingdir))
            {
                MessageBox.Show("读取目录列表失败");
            }
            refreshListView(ref filelist, ref workingdir);
        }
        private void refreshButton_Click(object sender, EventArgs e)
        {
            if (ftp.isConnected)
            {
                refreshList();
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
        private void newFolder(string foldername)
        {
            string[] filelist = null;
            string workingdir = "";
            if (!ftp.NewFolder(ref filelist, ref workingdir, foldername))
            {
                MessageBox.Show("新建文件夹失败");
            }
            refreshListView(ref filelist, ref workingdir);
        }
        private void newFolderButton_Click(object sender, EventArgs e)
        {
            if (ftp.isConnected)
            {
                NewFolderForm newFolderForm = new NewFolderForm();
                newFolderForm.newFolderEvent += new newFolderEventHandler(newFolder);
                newFolderForm.ShowDialog();
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
        private void rename(string newname)
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
                renameForm.renameEvent += new renameEventHandler(rename);
                renameForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("请先连接到一个FTP服务器");
            }
        }
        private void updateProgressBar()
        {
            int m = completedRateProgressBar.Maximum;
            int transferedDataSize = 0;
            while (transferedDataSize < m)
            {
                Thread.Sleep(100);
                try
                {
                    transferedDataSize = (int)(ftp.getTransferedDataSize / 1024);
                    completedRateProgressBar.Value = transferedDataSize;
                }
                catch
                {
                    completedRateProgressBar.Value = 0;
                    completedRateLabel.Text = "0%";
                    if (updateProgressBar_thread != null && updateProgressBar_thread.ThreadState == ThreadState.Running)
                    {
                        updateProgressBar_thread.Abort();
                    }
                    return;
                }
                completedRateLabel.Text = ((float)transferedDataSize / m * 100).ToString("F1") + "%";
            }
            Thread.Sleep(100);
            completedRateProgressBar.Value = 0;
            completedRateLabel.Text = "0%";
            if (updateProgressBar_thread != null && updateProgressBar_thread.ThreadState == ThreadState.Running)
            {
                updateProgressBar_thread.Abort();
            }
        }
        private void upload()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            
            ofd.DefaultExt = ".*";
            ofd.Filter = "file|*.*";
            ofd.Multiselect = true;
            ofd.ShowDialog();
            
            string[] fileNames = ofd.FileNames;
            if (fileNames.Length > 0)
            {
                try
                {
                    foreach (string fileName in fileNames)
                    {
                        FileInfo fileInfo = new FileInfo(fileName);
                        long fileSize = fileInfo.Length;
                        completedRateProgressBar.Minimum = 0;
                        completedRateProgressBar.Maximum = (int)(fileSize / 1024);
                        updateProgressBar_thread = new Thread(updateProgressBar);
                        updateProgressBar_thread.Start();
                        serverStatusLabel.Text += Path.GetFileName(fileName);
                        if (ftp.Upload(fileName))
                        {
                            serverStatusLabel.Text = "上传成功";
                        }
                        else
                        {
                            MessageBox.Show("上传失败");
                        }
                        serverStatusLabel.Text = serverStatusLabel.Text.Substring(0, 4);
                        logRichTextBox.Text = ftp.LogMessage;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("上传失败");
                    //MessageBox.Show(ex.ToString());
                }
            }
            refreshList();
            enableOPS();
        }
        private void download()
        {
            listView1.Items[0].Selected = false;
            if (listView1.SelectedItems.Count > 0)
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

                        if (local_filepath != "")
                        {
                            serverStatusLabel.Text += remote_filename;
                            if (item.SubItems[2].Text.Equals("File"))
                            {
                                long fileSize = long.Parse(item.SubItems[1].Text);
                                completedRateProgressBar.Minimum = 0;
                                completedRateProgressBar.Maximum = (int)(fileSize / 1024);
                                updateProgressBar_thread = new Thread(updateProgressBar);
                                updateProgressBar_thread.Start();
                                if (!ftp.Download(remote_filename, local_filepath))
                                {
                                    MessageBox.Show("文件 " + remote_filename + " 下载失败");
                                }
                                serverStatusLabel.Text = serverStatusLabel.Text.Substring(0, 4);
                            }
                            else if (item.SubItems[2].Text.Equals("Folder"))
                            {
                                if (!ftp.DownloadFolder(currentPath, local_filepath, remote_filename))
                                {
                                    MessageBox.Show("文件夹 " + remote_filename + " 下载失败");
                                }
                            }
                        }
                        else
                        {
                            enableOPS();
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
                MessageBox.Show("请先选择要下载的文件");
            }
            enableOPS();
        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            if (ftp.isConnected)
            {
                serverStatusLabel.Text = "正在上传";
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
                if (upload_thread != null)
                {
                    if(upload_thread.ThreadState == ThreadState.Suspended)
                    {
                        upload_thread.Resume();
                    }
                    if(upload_thread.ThreadState == ThreadState.Running)
                    {
                        ftp.Abort();
                        upload_thread.Abort();
                        serverStatusLabel.Text = "上传中止";
                        logRichTextBox.Text = ftp.LogMessage;
                        refreshList();  //自动执行一次刷新
                    }
                }
                if (download_thread != null)
                {
                    if(download_thread.ThreadState == ThreadState.Suspended)
                    {
                        download_thread.Resume();
                    }
                    if (download_thread.ThreadState == ThreadState.Running)
                    {
                        ftp.Abort();
                        download_thread.Abort();
                        serverStatusLabel.Text = "下载中止";
                        logRichTextBox.Text = ftp.LogMessage;
                    }
                }
                if (updateProgressBar_thread != null && updateProgressBar_thread.ThreadState == ThreadState.Running)
                {
                    updateProgressBar_thread.Abort();
                }
                completedRateProgressBar.Value = 0;
                completedRateLabel.Text = "0%";
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
            uploadFilesButton.Enabled = false;
            uploadFoldersButton.Enabled = false;
            parentDirButton.Enabled = false;
            refreshButton.Enabled = false;
            changeDirButton.Enabled = false;
            newFolderButton.Enabled = false;
            deleteButton.Enabled = false;
            renameButton.Enabled = false;
            connectToolStripMenuItem.Enabled = false;
            disconnectToolStripMenuItem.Enabled = false;
            exitToolStripMenuItem.Enabled = false;
            uploadFilesToolStripMenuItem.Enabled = false;
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
            uploadFilesButton.Enabled = true;
            uploadFoldersButton.Enabled = true;
            parentDirButton.Enabled = true;
            refreshButton.Enabled = true;
            changeDirButton.Enabled = true;
            newFolderButton.Enabled = true;
            deleteButton.Enabled = true;
            renameButton.Enabled = true;
            connectToolStripMenuItem.Enabled = true;
            disconnectToolStripMenuItem.Enabled = true;
            exitToolStripMenuItem.Enabled = true;
            uploadFilesToolStripMenuItem.Enabled = true;
            downloadToolStripMenuItem.Enabled = true;
            parentDirToolStripMenuItem.Enabled = true;
            refreshToolStripMenuItem.Enabled = true;
            changeDirToolStripMenuItem.Enabled = true;
            newFolderToolStripMenuItem.Enabled = true;
            deleteToolStripMenuItem.Enabled = true;
            renameToolStripMenuItem.Enabled = true;
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (ftp.isConnected)
            {
                if (upload_thread != null && upload_thread.ThreadState == ThreadState.Running)
                {
                    upload_thread.Suspend();
                    serverStatusLabel.Text = "上传暂停";
                }
                if (download_thread != null && download_thread.ThreadState == ThreadState.Running)
                {
                    download_thread.Suspend();
                    serverStatusLabel.Text = "下载暂停";
                }
            }
            else
            {
                MessageBox.Show("请先连接到一个FTP服务器");
            }
        }

        private void resumeButton_Click(object sender, EventArgs e)
        {
            if (ftp.isConnected)
            {
                if (upload_thread != null && upload_thread.ThreadState == ThreadState.Suspended)
                {
                    upload_thread.Resume();
                    serverStatusLabel.Text = "正在上传";
                }
                if (download_thread != null && download_thread.ThreadState == ThreadState.Suspended)
                {
                    download_thread.Resume();
                    serverStatusLabel.Text = "正在下载";
                }
            }
            else
            {
                MessageBox.Show("请先连接到一个FTP服务器");
            }
        }

        private void uploadFolders()
        {
            if (ftp.isConnected)
            {
                CommonOpenFileDialog cofd = new CommonOpenFileDialog();
                cofd.IsFolderPicker = true;
                cofd.Multiselect = true;
                cofd.ShowDialog();
                try
                {
                    foreach (var folderPath in cofd.FileNames)
                    {
                        if (ftp.UploadFolder(folderPath, currentPath))
                        {
                            serverStatusLabel.Text = "上传成功";
                        }
                        else
                        {
                            MessageBox.Show("上传失败");
                        }
                    }
                }
                catch (System.InvalidOperationException)
                {

                }
                catch
                {
                    MessageBox.Show("上传失败");
                }
                refreshList();
                enableOPS();
            }
            else
            {
                MessageBox.Show("请先连接到一个FTP服务器");
            }
        }

        private void uploadFoldersButton_Click(object sender, EventArgs e)
        {
            if (ftp.isConnected)
            {
                serverStatusLabel.Text = "正在上传";
                upload_thread = new Thread(uploadFolders);
                upload_thread.TrySetApartmentState(ApartmentState.STA);
                upload_thread.Start();
                disableOPS();
            }
            else
            {
                MessageBox.Show("请先连接到一个FTP服务器");
            }
        }
    }
}
