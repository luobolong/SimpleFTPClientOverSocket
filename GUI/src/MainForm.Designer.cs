namespace ftpclient
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.上传文件夹UToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resumeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.parentDirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeDirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.connectButton = new System.Windows.Forms.ToolStripButton();
            this.disconnectButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.uploadFilesButton = new System.Windows.Forms.ToolStripButton();
            this.uploadFoldersButton = new System.Windows.Forms.ToolStripButton();
            this.downloadButton = new System.Windows.Forms.ToolStripButton();
            this.pauseButton = new System.Windows.Forms.ToolStripButton();
            this.resumeButton = new System.Windows.Forms.ToolStripButton();
            this.abortButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.parentDirButton = new System.Windows.Forms.ToolStripButton();
            this.refreshButton = new System.Windows.Forms.ToolStripButton();
            this.changeDirButton = new System.Windows.Forms.ToolStripButton();
            this.newFolderButton = new System.Windows.Forms.ToolStripButton();
            this.deleteButton = new System.Windows.Forms.ToolStripButton();
            this.renameButton = new System.Windows.Forms.ToolStripButton();
            this.logRichTextBox = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.pathLabel = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.serverStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.clearLogButton = new System.Windows.Forms.Button();
            this.exportLogButton = new System.Windows.Forms.Button();
            this.completedRateProgressBar = new System.Windows.Forms.ProgressBar();
            this.completedRateLabel = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.opToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(634, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.disconnectToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.fileToolStripMenuItem.Text = "文件(F)";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.connectToolStripMenuItem.Text = "快速连接(U)...";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.disconnectToolStripMenuItem.Text = "断开连接(D)";
            this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(148, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.exitToolStripMenuItem.Text = "退出(X)";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.Exit_Click);
            // 
            // opToolStripMenuItem
            // 
            this.opToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uploadFilesToolStripMenuItem,
            this.上传文件夹UToolStripMenuItem,
            this.downloadToolStripMenuItem,
            this.pauseToolStripMenuItem,
            this.resumeToolStripMenuItem,
            this.abortToolStripMenuItem,
            this.toolStripSeparator3,
            this.parentDirToolStripMenuItem,
            this.refreshToolStripMenuItem,
            this.changeDirToolStripMenuItem,
            this.newFolderToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.renameToolStripMenuItem});
            this.opToolStripMenuItem.Name = "opToolStripMenuItem";
            this.opToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.opToolStripMenuItem.Text = "操作(O)";
            // 
            // uploadFilesToolStripMenuItem
            // 
            this.uploadFilesToolStripMenuItem.Name = "uploadFilesToolStripMenuItem";
            this.uploadFilesToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.uploadFilesToolStripMenuItem.Text = "上传文件(U)...";
            this.uploadFilesToolStripMenuItem.Click += new System.EventHandler(this.uploadButton_Click);
            // 
            // 上传文件夹UToolStripMenuItem
            // 
            this.上传文件夹UToolStripMenuItem.Name = "上传文件夹UToolStripMenuItem";
            this.上传文件夹UToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.上传文件夹UToolStripMenuItem.Text = "上传文件夹(U)...";
            this.上传文件夹UToolStripMenuItem.Click += new System.EventHandler(this.uploadFoldersButton_Click);
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.downloadToolStripMenuItem.Text = "下载文件或文件夹(D)...";
            this.downloadToolStripMenuItem.Click += new System.EventHandler(this.downloadButton_Click);
            // 
            // pauseToolStripMenuItem
            // 
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.pauseToolStripMenuItem.Text = "暂停传输(P)";
            this.pauseToolStripMenuItem.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // resumeToolStripMenuItem
            // 
            this.resumeToolStripMenuItem.Name = "resumeToolStripMenuItem";
            this.resumeToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.resumeToolStripMenuItem.Text = "恢复传输(R)";
            this.resumeToolStripMenuItem.Click += new System.EventHandler(this.resumeButton_Click);
            // 
            // abortToolStripMenuItem
            // 
            this.abortToolStripMenuItem.Name = "abortToolStripMenuItem";
            this.abortToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.abortToolStripMenuItem.Text = "中止传输(S)";
            this.abortToolStripMenuItem.Click += new System.EventHandler(this.abortButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(200, 6);
            // 
            // parentDirToolStripMenuItem
            // 
            this.parentDirToolStripMenuItem.Name = "parentDirToolStripMenuItem";
            this.parentDirToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.parentDirToolStripMenuItem.Text = "返回上一层目录(P)";
            this.parentDirToolStripMenuItem.Click += new System.EventHandler(this.parentDirButton_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.refreshToolStripMenuItem.Text = "刷新(F)";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // changeDirToolStripMenuItem
            // 
            this.changeDirToolStripMenuItem.Name = "changeDirToolStripMenuItem";
            this.changeDirToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.changeDirToolStripMenuItem.Text = "打开路径(O)";
            this.changeDirToolStripMenuItem.Click += new System.EventHandler(this.changeDirButton_Click);
            // 
            // newFolderToolStripMenuItem
            // 
            this.newFolderToolStripMenuItem.Name = "newFolderToolStripMenuItem";
            this.newFolderToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.newFolderToolStripMenuItem.Text = "新建文件夹(C)...";
            this.newFolderToolStripMenuItem.Click += new System.EventHandler(this.newFolderButton_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.deleteToolStripMenuItem.Text = "删除(D)";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.renameToolStripMenuItem.Text = "重命名(R)...";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameButton_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.helpToolStripMenuItem.Text = "帮助(H)";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.aboutToolStripMenuItem.Text = "关于(A)...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.showAboutForm);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectButton,
            this.disconnectButton,
            this.toolStripSeparator5,
            this.uploadFilesButton,
            this.uploadFoldersButton,
            this.downloadButton,
            this.pauseButton,
            this.resumeButton,
            this.abortButton,
            this.toolStripSeparator2,
            this.parentDirButton,
            this.refreshButton,
            this.changeDirButton,
            this.newFolderButton,
            this.deleteButton,
            this.renameButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(634, 43);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // connectButton
            // 
            this.connectButton.AutoSize = false;
            this.connectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.connectButton.Image = global::ftpclient.Properties.Resources.digital_learning;
            this.connectButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.connectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(40, 40);
            this.connectButton.Text = "快速连接";
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // disconnectButton
            // 
            this.disconnectButton.AutoSize = false;
            this.disconnectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.disconnectButton.Image = global::ftpclient.Properties.Resources.disconnect32px;
            this.disconnectButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.disconnectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(40, 40);
            this.disconnectButton.Text = "断开连接";
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 43);
            // 
            // uploadFilesButton
            // 
            this.uploadFilesButton.AutoSize = false;
            this.uploadFilesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.uploadFilesButton.Image = global::ftpclient.Properties.Resources.up32px;
            this.uploadFilesButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.uploadFilesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.uploadFilesButton.Name = "uploadFilesButton";
            this.uploadFilesButton.Size = new System.Drawing.Size(40, 40);
            this.uploadFilesButton.Text = "上传文件";
            this.uploadFilesButton.Click += new System.EventHandler(this.uploadButton_Click);
            // 
            // uploadFoldersButton
            // 
            this.uploadFoldersButton.AutoSize = false;
            this.uploadFoldersButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.uploadFoldersButton.Image = global::ftpclient.Properties.Resources.folder_upload32px;
            this.uploadFoldersButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.uploadFoldersButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.uploadFoldersButton.Name = "uploadFoldersButton";
            this.uploadFoldersButton.Size = new System.Drawing.Size(40, 40);
            this.uploadFoldersButton.Text = "上传文件夹";
            this.uploadFoldersButton.Click += new System.EventHandler(this.uploadFoldersButton_Click);
            // 
            // downloadButton
            // 
            this.downloadButton.AutoSize = false;
            this.downloadButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.downloadButton.Image = global::ftpclient.Properties.Resources.download32px;
            this.downloadButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.downloadButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(40, 40);
            this.downloadButton.Text = "下载文件或文件夹";
            this.downloadButton.Click += new System.EventHandler(this.downloadButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.AutoSize = false;
            this.pauseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pauseButton.Image = ((System.Drawing.Image)(resources.GetObject("pauseButton.Image")));
            this.pauseButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.pauseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(40, 40);
            this.pauseButton.Text = "暂停传输";
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // resumeButton
            // 
            this.resumeButton.AutoSize = false;
            this.resumeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.resumeButton.Image = global::ftpclient.Properties.Resources.play32px;
            this.resumeButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resumeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.resumeButton.Name = "resumeButton";
            this.resumeButton.Size = new System.Drawing.Size(40, 40);
            this.resumeButton.Text = "恢复传输";
            this.resumeButton.Click += new System.EventHandler(this.resumeButton_Click);
            // 
            // abortButton
            // 
            this.abortButton.AutoSize = false;
            this.abortButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.abortButton.Image = global::ftpclient.Properties.Resources.close32px;
            this.abortButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.abortButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.abortButton.Name = "abortButton";
            this.abortButton.Size = new System.Drawing.Size(40, 40);
            this.abortButton.Text = "中止传输";
            this.abortButton.Click += new System.EventHandler(this.abortButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 43);
            // 
            // parentDirButton
            // 
            this.parentDirButton.AutoSize = false;
            this.parentDirButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.parentDirButton.Image = global::ftpclient.Properties.Resources.back32px;
            this.parentDirButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.parentDirButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.parentDirButton.Name = "parentDirButton";
            this.parentDirButton.Size = new System.Drawing.Size(40, 40);
            this.parentDirButton.Text = "返回上一层目录";
            this.parentDirButton.Click += new System.EventHandler(this.parentDirButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.AutoSize = false;
            this.refreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshButton.Image")));
            this.refreshButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(40, 40);
            this.refreshButton.Text = "刷新";
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // changeDirButton
            // 
            this.changeDirButton.AutoSize = false;
            this.changeDirButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.changeDirButton.Image = global::ftpclient.Properties.Resources.redo32px;
            this.changeDirButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.changeDirButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.changeDirButton.Name = "changeDirButton";
            this.changeDirButton.Size = new System.Drawing.Size(40, 40);
            this.changeDirButton.Text = "打开路径";
            this.changeDirButton.Click += new System.EventHandler(this.changeDirButton_Click);
            // 
            // newFolderButton
            // 
            this.newFolderButton.AutoSize = false;
            this.newFolderButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newFolderButton.Image = global::ftpclient.Properties.Resources.folder32px;
            this.newFolderButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.newFolderButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newFolderButton.Name = "newFolderButton";
            this.newFolderButton.Size = new System.Drawing.Size(40, 40);
            this.newFolderButton.Text = "新建文件夹";
            this.newFolderButton.Click += new System.EventHandler(this.newFolderButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.AutoSize = false;
            this.deleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteButton.Image = global::ftpclient.Properties.Resources.trash32px;
            this.deleteButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.deleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(40, 40);
            this.deleteButton.Text = "删除";
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // renameButton
            // 
            this.renameButton.AutoSize = false;
            this.renameButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.renameButton.Image = global::ftpclient.Properties.Resources.name32px;
            this.renameButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.renameButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.renameButton.Name = "renameButton";
            this.renameButton.Size = new System.Drawing.Size(40, 40);
            this.renameButton.Text = "重命名";
            this.renameButton.Click += new System.EventHandler(this.renameButton_Click);
            // 
            // logRichTextBox
            // 
            this.logRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logRichTextBox.Location = new System.Drawing.Point(6, 22);
            this.logRichTextBox.Name = "logRichTextBox";
            this.logRichTextBox.ReadOnly = true;
            this.logRichTextBox.Size = new System.Drawing.Size(598, 192);
            this.logRichTextBox.TabIndex = 3;
            this.logRichTextBox.Text = "";
            this.logRichTextBox.TextChanged += new System.EventHandler(this.logRichTextBox_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.logRichTextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(610, 221);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "消息日志";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.listView1);
            this.groupBox2.Controls.Add(this.pathTextBox);
            this.groupBox2.Controls.Add(this.pathLabel);
            this.groupBox2.Location = new System.Drawing.Point(12, 313);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(610, 593);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "远程站点";
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView1.HideSelection = false;
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.Location = new System.Drawing.Point(6, 52);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(598, 534);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 220;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Size";
            this.columnHeader2.Width = 135;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Type";
            this.columnHeader3.Width = 75;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Last modified";
            this.columnHeader4.Width = 150;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "file.png");
            this.imageList1.Images.SetKeyName(1, "folder.png");
            // 
            // pathTextBox
            // 
            this.pathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pathTextBox.Location = new System.Drawing.Point(66, 23);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(538, 20);
            this.pathTextBox.TabIndex = 1;
            // 
            // pathLabel
            // 
            this.pathLabel.AutoSize = true;
            this.pathLabel.Location = new System.Drawing.Point(7, 26);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(47, 13);
            this.pathLabel.TabIndex = 0;
            this.pathLabel.Text = "路径(P):";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 911);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(634, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // serverStatusLabel
            // 
            this.serverStatusLabel.Name = "serverStatusLabel";
            this.serverStatusLabel.Size = new System.Drawing.Size(46, 17);
            this.serverStatusLabel.Text = "未连接";
            // 
            // clearLogButton
            // 
            this.clearLogButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearLogButton.Font = new System.Drawing.Font("SimSun", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.clearLogButton.Location = new System.Drawing.Point(564, 294);
            this.clearLogButton.Margin = new System.Windows.Forms.Padding(2);
            this.clearLogButton.Name = "clearLogButton";
            this.clearLogButton.Size = new System.Drawing.Size(51, 22);
            this.clearLogButton.TabIndex = 4;
            this.clearLogButton.Text = "清空";
            this.clearLogButton.UseVisualStyleBackColor = true;
            this.clearLogButton.Click += new System.EventHandler(this.clearLogButton_Click);
            // 
            // exportLogButton
            // 
            this.exportLogButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exportLogButton.Font = new System.Drawing.Font("SimSun", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exportLogButton.Location = new System.Drawing.Point(509, 294);
            this.exportLogButton.Margin = new System.Windows.Forms.Padding(2);
            this.exportLogButton.Name = "exportLogButton";
            this.exportLogButton.Size = new System.Drawing.Size(51, 22);
            this.exportLogButton.TabIndex = 8;
            this.exportLogButton.Text = "导出";
            this.exportLogButton.UseVisualStyleBackColor = true;
            this.exportLogButton.Click += new System.EventHandler(this.exportLogButton_Click);
            // 
            // completedRateProgressBar
            // 
            this.completedRateProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.completedRateProgressBar.Location = new System.Drawing.Point(363, 912);
            this.completedRateProgressBar.Name = "completedRateProgressBar";
            this.completedRateProgressBar.Size = new System.Drawing.Size(209, 17);
            this.completedRateProgressBar.TabIndex = 9;
            // 
            // completedRateLabel
            // 
            this.completedRateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.completedRateLabel.BackColor = System.Drawing.Color.Transparent;
            this.completedRateLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.completedRateLabel.ForeColor = System.Drawing.Color.Black;
            this.completedRateLabel.Location = new System.Drawing.Point(572, 912);
            this.completedRateLabel.Name = "completedRateLabel";
            this.completedRateLabel.Size = new System.Drawing.Size(44, 21);
            this.completedRateLabel.TabIndex = 10;
            this.completedRateLabel.Text = "0%";
            this.completedRateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(634, 933);
            this.Controls.Add(this.completedRateLabel);
            this.Controls.Add(this.completedRateProgressBar);
            this.Controls.Add(this.exportLogButton);
            this.Controls.Add(this.clearLogButton);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(650, 972);
            this.Name = "MainForm";
            this.Text = "FTP客户端 V1.2";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton refreshButton;
        private System.Windows.Forms.ToolStripButton uploadFilesButton;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton deleteButton;
        private System.Windows.Forms.ToolStripButton renameButton;
        private System.Windows.Forms.ToolStripMenuItem uploadFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.RichTextBox logRichTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.ToolStripButton connectButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton disconnectButton;
        private System.Windows.Forms.ToolStripMenuItem changeDirToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton changeDirButton;
        private System.Windows.Forms.ToolStripMenuItem newFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton newFolderButton;
        private System.Windows.Forms.ToolStripButton downloadButton;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ToolStripButton parentDirButton;
        private System.Windows.Forms.ToolStripMenuItem parentDirToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton abortButton;
        private System.Windows.Forms.ToolStripMenuItem abortToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel serverStatusLabel;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Button clearLogButton;
        private System.Windows.Forms.Button exportLogButton;
        private System.Windows.Forms.ProgressBar completedRateProgressBar;
        private System.Windows.Forms.Label completedRateLabel;
        private System.Windows.Forms.ToolStripButton pauseButton;
        private System.Windows.Forms.ToolStripButton resumeButton;
        private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resumeToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton uploadFoldersButton;
        private System.Windows.Forms.ToolStripMenuItem 上传文件夹UToolStripMenuItem;
    }
}

