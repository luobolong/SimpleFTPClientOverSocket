using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ftpclient
{
    class ftpservice
    {
        #region 成员变量
        private string hostaddress; // 服务器IP地址
        private string username; // 用户名
        private string password; // 密码
        private int port; // 端口号
        private int timeout; // 响应时间
        private string logmessage; // 日志信息
        private bool connect_mode; // 连接方式 被动为true 主动为false
        private const int BUFFER_SIZE = 4096;
        private int Port_times = 1; // Port模式的连接次数
        private Socket mainsocket; // 主套接字
        private Socket datasocket; // 数据套接字
        private Socket listeningsocket; // 监听套接字(用于主动模式)
        private FileStream uploadFileStream; // 上传文件
        private FileStream downloadFileStream; // 下载文件
        private long dataTransfered = 0; //已传输的数据字节数

        private static ftpservice service;
        #endregion
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public ftpservice()
        {

        }
        public ftpservice(string hostaddr, string usr, string psw, int port, bool connect_mode)
        {
            this.hostaddress = hostaddr;
            this.username = usr;
            this.password = psw;
            this.port = port;
            this.connect_mode = connect_mode;
        }
        #endregion
        #region 单例模式
        /// <summary>
        /// 单例模式
        /// </summary>
        public static ftpservice getInstance()
        {
            if (service == null)
            {
                service = new ftpservice();
            }
            return service;
        }
        public static ftpservice getInstance(string hostaddr, string usr, string psw, int port, bool connect_mode)
        {
            if (service == null)
            {
                service = new ftpservice(hostaddr, usr, psw, port, connect_mode);
            }
            return service;
        }
        #endregion
        #region 导入配置
        /// <summary>
        /// 导入配置
        /// </summary>
        public void loadPreferences(string hostaddr, string usr, string psw, int port, bool connect_mode)
        {
            this.hostaddress = hostaddr;
            this.username = usr;
            this.password = psw;
            this.port = port;
            this.connect_mode = connect_mode;
        }
        #endregion
        #region 主套接字
        /// <summary>
        /// 主套接字连接到服务器
        /// </summary>
        /// <returns></returns>
        public bool connectMainsocket()
        {
            mainsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                mainsocket.Connect(IPAddress.Parse(hostaddress), port);
            }
            catch (Exception)
            {
                return false;
            }
            string response = getResponseString();
            int code = getResponseCode(response);
            if (code == 220)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 主套接字发送命令
        /// </summary>
        /// <param name="cmd"></param>
        public void sendCommand(string cmd)
        {
            try
            {
                cmd += "\r\n";
                logmessage += "ftp> " + cmd;
                mainsocket.Send(Encoding.UTF8.GetBytes(cmd));
            }
            catch (Exception)
            {
                return;
            }

        }

        /// <summary>
        /// 主套接字接收服务器的所有响应消息
        /// </summary>
        /// <returns>返回响应消息</returns>
        public string getResponseString()
        {
            string response = "";
            for (; ; )
            {
                Byte[] bytes = new byte[BUFFER_SIZE];
                int rlen = 0;
                rlen = mainsocket.Receive(bytes, bytes.Length, 0);
                string temp = Encoding.UTF8.GetString(bytes).Replace("\0", string.Empty);
                logmessage += temp;
                response = temp;
                if (temp.Substring(3, 1) != " ") //如果响应消息的第四个位置不是空格，则说明这是一条问候消息
                {
                    string[] lines = temp.Split('\n');
                    if(lines.Length > 2)
                    {
                        if(lines[lines.Length - 2].Substring(3, 1).Equals(" ")) //判断倒数第二个字符串的响应代码后是否包含空格
                        {
                            response = lines[lines.Length - 2] + "\n";
                            break;
                        }
                    }
                    continue; //继续循环接收响应消息
                }
                if (rlen < bytes.Length)
                {
                    break;
                }
            }
            return response;
        }
        /// <summary>
        /// 提取回应消息中的数字代码
        /// </summary>
        /// <returns></returns>
        public int getResponseCode(string response)
        {
            if (response == "")
            {
                return 0;
            }
            else
            {
                string substr = response.Substring(0, 3);
                int res;
                try
                {
                    res = int.Parse(substr);
                }
                catch (Exception)
                {
                    return 0;
                }
                return res;
            }
        }
        /// <summary>
        /// 关闭主套接字，断开连接
        /// </summary>
        public void Disconnect()
        {
            if (mainsocket != null)
            {
                if (mainsocket.Connected)
                {
                    Quit();
                    mainsocket.Close();
                }
                mainsocket = null;
            }
        }
        #endregion
        #region 数据套接字
        /// <summary>
        /// 建立数据套接字
        /// </summary>
        /// <returns>是否成功</returns>
        public bool connectDataSocket()
        {
            if (isPassive) // 被动模式
            {
                string[] pasv_info;
                string dataip;
                int dataport;

                string response = Pasv();
                int code = getResponseCode(response);
                if (code != 227)
                {
                    return false;
                }

                // 提取PASV返回的字符串中的信息
                int startindex = response.IndexOf('(') + 1;
                int length = response.IndexOf(')') - startindex;
                pasv_info = response.Substring(startindex, length).Split(',');

                dataip = String.Format("{0}.{1}.{2}.{3}", pasv_info[0], pasv_info[1], pasv_info[2], pasv_info[3]);
                dataport = (int.Parse(pasv_info[4]) << 8) + int.Parse(pasv_info[5]);
                try
                {
                    datasocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    datasocket.Connect(new IPEndPoint(IPAddress.Parse(dataip), dataport));
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else // 主动模式
            {
                string response = Port();
                int code = getResponseCode(response);
                if (code != 200)
                {
                    return false;
                }
                datasocket = listeningsocket.Accept();
                closeListeningSocket();
            }
            return true;
        }
        /// <summary>
        /// 数据套接字接收列表数据
        /// </summary>
        /// <returns>返回接收到的字符串数据</returns>
        public string getListData()
        {
            string data = "";
            int times = 1;
            for (; ; )
            {
                Byte[] bytes = new Byte[times * BUFFER_SIZE];
                int rlen = datasocket.Receive(bytes, bytes.Length, 0);
                data += Encoding.UTF8.GetString(bytes);
                times *= 2;
                if (rlen < bytes.Length)
                {
                    break;
                }
                Thread.Sleep(1000); //进程等待1秒钟，尽量保证数据传输完成
            }
            return data.Replace("\0", string.Empty);
        }

        /// <summary>
        /// 关闭数据套接字
        /// </summary>
        public void closeDataSocket()
        {
            if (datasocket != null)
            {
                datasocket.Dispose();
            }
        }
        /// <summary>
        /// 中断数据连接
        /// </summary>
        public void abortDataSocket()
        {
            if (uploadFileStream != null)
            {
                uploadFileStream.Flush();
                uploadFileStream.Close();
            }
            if (downloadFileStream != null)
            {
                downloadFileStream.Flush();
                downloadFileStream.Close();
            }
            closeDataSocket();
            getResponseString();
        }
        #endregion
        #region 监听套接字
        /// <summary>
        /// 打开监听套接字
        /// </summary>
        /// <param name="LocalIP"></param>
        /// <param name="port"></param>
        private void connectListeningSocket(IPAddress IP, int port)
        {
            listeningsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listeningsocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
            IPEndPoint listen = new IPEndPoint(IP, port);
            try
            {
                listeningsocket.Bind(listen);
                listeningsocket.Listen(1);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 关闭监听套接字
        /// </summary>
        private void closeListeningSocket()
        {
            if (listeningsocket != null)
            {
                listeningsocket.Dispose();
                listeningsocket = null;
            }
        }
        #endregion
        #region 发送命令
        /// <summary>
        /// 设置编码为UTF8模式
        /// </summary>
        /// <returns></returns>
        public string setUTF()
        {
            sendCommand("OPTS UTF8 ON");
            return getResponseString();
        }
        /// <summary>
        /// 设置二进制或ASCII码类型
        /// </summary>
        /// <param name="mode"></param>
        public string setType(bool mode)
        {
            if (mode)
            {
                sendCommand("TYPE I");
            }
            else
            {
                sendCommand("TYPE A");
            }
            return getResponseString();
        }
        /// <summary>
        /// 发送SYST命令
        /// </summary>
        /// <returns></returns>
        public string Syst()
        {
            sendCommand("SYST");
            return getResponseString();
        }
        /// <summary>
        /// 发送USER命令
        /// </summary>
        /// <returns></returns>
        public string User()
        {
            sendCommand("USER " + username);
            return getResponseString();
        }
        /// <summary>
        /// 发送PASS命令
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string Pass()
        {
            sendCommand("PASS " + password);
            return getResponseString();
        }
        /// <summary>
        /// 发送PORT命令，并监听套接字监听端口
        /// </summary>
        public string Port()
        {
            IPAddress LocalIP = ((IPEndPoint)mainsocket.LocalEndPoint).Address; //本机IP
            int local_port = ((IPEndPoint)mainsocket.LocalEndPoint).Port; //本机地址的端口号
            int p;
            do
            {
                p = local_port + Port_times++;
                if (p > 65535)
                {
                    Port_times = 1;
                    p = local_port + Port_times++;
                }
            } while (isPortOccuped(p)); //检查端口是否被占用
            int p1 = p / 256;
            int p2 = p % 256;
            string sLoclaIP = LocalIP.ToString().Replace('.', ',');
            string cmd = "PORT " + sLoclaIP + "," + p1.ToString() + "," + p2.ToString();
            connectListeningSocket(LocalIP, p); // 监听套接字监听端口
            sendCommand(cmd);
            return getResponseString();
        }
        /// <summary>
        /// 发送PASV命令
        /// </summary>
        public string Pasv()
        {
            sendCommand("PASV");
            return getResponseString();
        }
        /// <summary>
        /// 发送QUIT命令
        /// </summary>
        /// <returns></returns>
        public string Quit()
        {
            sendCommand("QUIT");
            return getResponseString();
        }
        /// <summary>
        /// 发送PWD命令
        /// </summary>
        /// <returns></returns>
        public string Pwd()
        {
            sendCommand("PWD");
            return getResponseString();
        }
        /// <summary>
        /// 发送LIST命令
        /// </summary>
        /// <param name="response"></param>
        public string List()
        {
            sendCommand("LIST");
            return getResponseString();
        }
        /// <summary>
        /// 发送CDUP命令
        /// </summary>
        /// <returns></returns>
        public string Cdup()
        {
            sendCommand("CDUP");
            return getResponseString();
        }
        /// <summary>
        /// 发送CWD命令
        /// </summary>
        /// <param name="workingdirectory"></param>
        /// <returns></returns>
        public string Cwd(string workingdirectory)
        {
            sendCommand("CWD " + workingdirectory);
            return getResponseString();
        }
        /// <summary>
        /// 发送MKD命令
        /// </summary>
        /// <returns></returns>
        public string Mkd(string foldername)
        {
            sendCommand("MKD " + foldername);
            return getResponseString();
        }
        /// <summary>
        /// 发送DELE命令
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public string Dele(string filename)
        {
            sendCommand("DELE " + filename);
            return getResponseString();
        }
        /// <summary>
        /// 发送RMD命令
        /// </summary>
        /// <param name="foldername"></param>
        /// <returns></returns>
        public string Rmd(string foldername)
        {
            sendCommand("RMD " + foldername);
            return getResponseString();
        }
        /// <summary>
        /// 发送RNFR命令
        /// </summary>
        /// <param name="oldname"></param>
        /// <returns></returns>
        public string Rnfr(string filename)
        {
            sendCommand("RNFR " + filename);
            return getResponseString();
        }
        /// <summary>
        /// 发送RNTO命令
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public string Rnto(string filename)
        {
            sendCommand("RNTO " + filename);
            return getResponseString();
        }
        #endregion
        #region 上传和下载文件
        /// <summary>
        /// 上传单个文件
        /// </summary>
        /// <param name="filepath">完整的路径和文件名</param>
        public bool Upload(string filepath)
        {
            #region 设置传输方式
            string getName = Path.GetFileName(filepath); //提取路径中的文件名
            string extensionName = "";
            if (getName.Contains("."))
            {
                int x = filepath.LastIndexOf(".") + 1;
                extensionName = getName.Substring(getName.LastIndexOf(".") + 1); //提取文件名中的扩展名
                if (extensionName.Equals("txt") ||
                    extensionName.Equals("TXT") ||
                    extensionName.Equals("htm") ||
                    extensionName.Equals("HTM") ||
                    extensionName.Equals("html") ||
                    extensionName.Equals("HTML"))
                {
                    setType(false);
                }
                else
                {
                    setType(true);
                }
            }
            else
            {
                setType(true);
            }
            #endregion
            dataTransfered = 0;
            try
            {
                if (!connectDataSocket())
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            sendCommand("STOR " + getName);
            string response2 = getResponseString();
            int code2 = getResponseCode(response2);
            if (code2 != 150 && code2 != 125)
            {
                return false;
            }
            uploadFileStream = new FileStream(filepath, FileMode.Open);
            int slen = 0;
            Byte[] bytes = new byte[BUFFER_SIZE];
            while ((slen = uploadFileStream.Read(bytes, 0, bytes.Length)) > 0)
            {
                datasocket.Send(bytes, slen, 0);
                dataTransfered += slen;
            }
            uploadFileStream.Close();
            uploadFileStream = null;
            closeDataSocket();
            string response3 = getResponseString();
            int code3 = getResponseCode(response3);
            if (code3 != 226)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 下载单个文件
        /// </summary>
        /// <param name="remote_filename">远程文件名</param>
        /// <param name="local_filepath">本地路径</param>
        public bool Download(string remote_filename, string local_filepath)
        {
            #region 设置传输方式
            string extensionName = "";
            if (remote_filename.Contains("."))
            {
                extensionName = remote_filename.Substring(remote_filename.LastIndexOf(".") + 1); //提取文件名中的扩展名
                if (extensionName.Equals("txt") ||
                    extensionName.Equals("TXT") ||
                    extensionName.Equals("htm") ||
                    extensionName.Equals("HTM") ||
                    extensionName.Equals("html") ||
                    extensionName.Equals("HTML"))
                {
                    setType(false);
                }
                else
                {
                    setType(true);
                }
            }
            else
            {
                setType(true);
            }
            #endregion
            dataTransfered = 0;
            try
            {
                if (!connectDataSocket())
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            sendCommand("RETR " + remote_filename);
            string response1 = getResponseString();
            int code2 = getResponseCode(response1);
            if (code2 != 150 && code2 != 125)
            {
                return false;
            }
            if (!Directory.Exists(local_filepath))
            {
                Directory.CreateDirectory(local_filepath);
            }
            downloadFileStream = new FileStream(local_filepath + "\\" + remote_filename, FileMode.OpenOrCreate);
            for (; ; )
            {
                Byte[] bytes = new byte[BUFFER_SIZE];
                int rlen = datasocket.Receive(bytes, bytes.Length, 0);
                dataTransfered += rlen;
                downloadFileStream.Write(bytes, 0, rlen);
                if (rlen <= 0)
                {
                    break;
                }
            }
            downloadFileStream.Close();
            downloadFileStream = null;
            closeDataSocket();
            string response3 = getResponseString();
            int code3 = getResponseCode(response3);
            if (code3 != 226)
            {
                return false;
            }
            return true;
        }
        #endregion
        #region 其他功能
        /// <summary>
        /// 判断指定端口号是否被占用
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        internal static Boolean isPortOccuped(Int32 port)
        {
            Boolean result = false;
            try
            {
                System.Net.NetworkInformation.IPGlobalProperties iproperties = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties();
                System.Net.IPEndPoint[] ipEndPoints = iproperties.GetActiveTcpListeners();
                foreach (var item in ipEndPoints)
                {
                    if (item.Port == port)
                    {
                        result = true;
                        break;
                    }
                }
            }
            catch (Exception)
            {
            }
            return result;
        }
        #endregion
        #region 属性
        public string getLogMessage
        {
            get
            {
                return logmessage;
            }
            set
            {
                logmessage = value;
            }
        }

        public bool isPassive
        {
            get
            {
                return connect_mode;
            }
        }

        public bool isConnected
        {
            get
            {
                if (mainsocket != null)
                {
                    if (mainsocket.Connected)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public long getTransferedDataSize
        {
            get
            {
                return dataTransfered;
            }
        }
        #endregion
    }
}
