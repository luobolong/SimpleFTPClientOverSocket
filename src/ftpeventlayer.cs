using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace ftpclient
{
    public class ftpeventlayer //对ftpservice的封装
    {
        #region 成员变量
        private ftpservice ftpsvc;
        #endregion
        #region 构造函数
        public ftpeventlayer()
        {
            ftpsvc = ftpservice.getInstance();
        }
        public ftpeventlayer(string hostaddress, string username, string password, int port, bool connect_mode)
        {
            ftpsvc = ftpservice.getInstance(hostaddress, username, password, port, connect_mode);
        }
        #endregion
        #region 属性
        public string LogMessage
        {
            get
            {
                return ftpsvc.getLogMessage;
            }
            set
            {
                ftpsvc.getLogMessage = value;
            }
        }
        public bool isPassive => ftpsvc.isPassive;
        public bool isConnected => ftpsvc.isConnected;
        public long getTransferedDataSize => ftpsvc.getTransferedDataSize;
        private string syst_info = "";
        #endregion
        #region 事件
        /// <summary>
        /// 快速连接
        /// </summary>
        /// <param name="filelist">文件列表</param>
        /// <param name="workingdir">工作目录</param>
        /// <param name="syst">系统信息</param>
        /// <param name="hostaddress">远程主机地址</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="port">端口号</param>
        /// <param name="connect_mode">连接方式</param>
        /// <returns>是否连接成功</returns>
        public bool QuickConnect(ref string[] filelist, ref string workingdir, ref string syst, string hostaddress, string username, string password, int port, bool connect_mode)
        {
            ftpsvc.loadPreferences(hostaddress, username, password, port, connect_mode);
            if (ftpsvc.connectMainsocket())
            {
                ftpsvc.setUTF();

                string response1 = ftpsvc.User();
                int code1 = ftpsvc.getResponseCode(response1);
                if(code1 == 331)
                {
                    string reponse2 = ftpsvc.Pass();
                    int code2 = ftpsvc.getResponseCode(reponse2);
                    if (code2 == 230) //登录成功
                    {
                        syst = ftpsvc.Syst(); //获取系统信息
                        syst_info = syst; //记录系统信息
                        if (!Refresh(ref filelist, ref workingdir)) //刷新
                        {
                            return false;
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 断开连接
        /// </summary>
        public void Disconnect() => ftpsvc.Disconnect();
        /// <summary>
        /// 得到工作目录
        /// </summary>
        /// <returns></returns>
        private string getDir()
        {
            string response = ftpsvc.Pwd();
            string workingdir = "";
            if (response != "")
            {
                for (int i = 0; i < response.Length; i++)
                {
                    if (response[i].Equals('"'))
                    {
                        for (int j = i + 1; !response[j].Equals('"'); j++)
                        {
                            workingdir += response[j];
                        }
                        break;
                    }
                }
            }
            return workingdir;
        }
        /// <summary>
        /// 刷新，获取文件列表和工作目录
        /// </summary>
        /// <param name="filelist">文件列表</param>
        /// <param name="connect_mode">工作目录</param>
        /// <returns>是否刷新成功</returns>
        public bool Refresh(ref string[] filelist, ref string workingdir)
        {
            string rawstring = "";
            workingdir = getDir();
            try
            {
                if (!ftpsvc.connectDataSocket())
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            string response1 = ftpsvc.List();
            int code1 = ftpsvc.getResponseCode(response1);
            if (code1 != 150 && code1 != 125)
            {
                return false;
            }
            rawstring = ftpsvc.getListData();
            ftpsvc.closeDataSocket();
            string response2 = ftpsvc.getResponseString();
            int code2 = ftpsvc.getResponseCode(response2);
            if (code2 != 226)
            {
                return false;
            }
            filelist = rawstring.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            return true;
        }
        /// <summary>
        /// 返回上一级目录
        /// </summary>
        /// <param name="filelist">文件列表</param>
        /// <param name="workingdir">工作目录</param>
        public bool ParentDir(ref string[] filelist, ref string workingdir)
        {
            string response = ftpsvc.Cdup();
            int code = ftpsvc.getResponseCode(response);
            if(code == 250)
            {
                Refresh(ref filelist, ref workingdir);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 改变目录，返回是否改变成功
        /// </summary>
        /// <param name="filelist">文件列表</param>
        /// <param name="workingdir">工作目录</param>
        /// <returns></returns>
        public bool ChangeDir(ref string[] filelist, ref string workingdir)
        {
            string response = ftpsvc.Cwd(workingdir);
            int code = ftpsvc.getResponseCode(response);
            if (code == 250)
            {
                Refresh(ref filelist, ref workingdir);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 新建文件夹
        /// </summary>
        /// <param name="filelist">文件列表</param>
        /// <param name="workingdir">工作目录</param>
        /// <param name="foldername">文件夹名称</param>
        public bool NewFolder(ref string[] filelist, ref string workingdir, string foldername)
        {
            string response = ftpsvc.Mkd(foldername);
            int code = ftpsvc.getResponseCode(response);
            if (code != 257)
            {
                return false;
            }
            Refresh(ref filelist, ref workingdir);
            return true;
        }
        private bool deleteFolder(string folderPath, string folderName)
        {
            string[] filelist = null;
            string workingdir = folderPath;
            ChangeDir(ref filelist, ref workingdir);
            foreach (var line in filelist)
            {
                try
                {
                    string[] res = readLine(line, syst_info);
                    string name = res[0];
                    string type = res[2];
                    if (type != "")
                    {
                        if (type.Equals("File"))
                        {
                            string response1 = ftpsvc.Dele(name);
                            int code1 = ftpsvc.getResponseCode(response1);
                            if (code1 != 250)
                            {
                                return false;
                            }
                        }
                        else if (type.Equals("Folder"))
                        {
                            if (!deleteFolder(workingdir + "/" + name, name))
                            {
                                return false;
                            }
                            ftpsvc.Cdup();
                            string response2 = ftpsvc.Rmd(name);
                            int code2 = ftpsvc.getResponseCode(response2);
                            if (code2 != 250)
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 删除文件或文件夹
        /// </summary>
        /// <param name="filelist">文件列表</param>
        /// <param name="workingdir">工作目录</param>
        /// <param name="name">名称</param>
        /// <param name="isFile">判断是文件还是文件夹的标志</param>
        public bool Delete(ref string[] filelist, ref string workingdir, string name, bool isFile)
        {
            if (isFile)
            {
                string response1 = ftpsvc.Dele(name);
                int code1 = ftpsvc.getResponseCode(response1);
                if (code1 != 250)
                {
                    return false;
                }
            }
            else
            {
                if(!deleteFolder(workingdir + "/" + name, name))
                {
                    return false;
                }
                ftpsvc.Cdup();
                string response2 = ftpsvc.Rmd(name);
                int code2 = ftpsvc.getResponseCode(response2);
                if (code2 != 250)
                {
                    return false;
                }
            }
            Refresh(ref filelist, ref workingdir);
            return true;
        }
        /// <summary>
        /// 重命名
        /// </summary>
        /// <param name="filelist">文件列表</param>
        /// <param name="workingdir">工作目录</param>
        /// <param name="oldpath">原文件路径</param>
        /// <param name="newpath">新文件路径</param>
        /// <returns></returns>
        public bool Rename(ref string[] filelist, ref string workingdir, string oldpath, string newpath)
        {
            string response1 = ftpsvc.Rnfr(oldpath);
            int code1 = ftpsvc.getResponseCode(response1);
            if (code1 == 350)
            {
                string reponse2 = ftpsvc.Rnto(newpath);
                int code2 = ftpsvc.getResponseCode(reponse2);
                if (code2 == 250)
                {
                    Refresh(ref filelist, ref workingdir);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="filepath">完整的路径和文件名</param>
        /// <returns>是否上传成功</returns>
        public bool Upload(string filepath)
        {
            if (ftpsvc.Upload(filepath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 以递归方式上传文件夹
        /// </summary>
        /// <param name="localFolderPath">本地的目录路径</param>
        /// <param name="remoteFolderPath">远程当前的目录路径</param>
        /// <returns>是否上传成功</returns>
        private bool uploadFolder(string localFolderPath, string remoteFolderPath)
        {
            string localFolderName = Path.GetFileName(localFolderPath);
            string[] allLocalFilePaths = Directory.GetFiles(localFolderPath);
            string[] allLocalDirectoryPaths = Directory.GetDirectories(localFolderPath);

            ftpsvc.Mkd(localFolderName); // 新建文件夹
            string newRemoteFolderPath = remoteFolderPath + "/" + localFolderName;
            string response = ftpsvc.Cwd(newRemoteFolderPath); // 打开远程文件夹
            int code = ftpsvc.getResponseCode(response);
            if (code != 250)
            {
                return false; // 打不开文件夹则停止
            }
            if (allLocalFilePaths != null) // 上传所有文件
            {
                foreach (var filePath in allLocalFilePaths)
                {
                    if (!Upload(filePath))
                    {
                        return false;
                    }
                }
            }
            if (allLocalDirectoryPaths != null) // 递归
            {
                foreach (var directoryPath in allLocalDirectoryPaths)
                {
                    if (!uploadFolder(directoryPath, newRemoteFolderPath))
                    {
                        return false;
                    }
                    ftpsvc.Cdup();
                }
            }
            return true;
        }
        /// <summary>
        /// 上传文件夹
        /// </summary>
        /// <param name="localFolderPath">本地的目录路径</param>
        /// <param name="remoteFolderPath">远程当前的目录路径</param>
        /// <returns>是否上传成功</returns>
        public bool UploadFolder(string localFolderPath, string remoteFolderPath)
        {
            if (uploadFolder(localFolderPath, remoteFolderPath))
            {
                ftpsvc.Cdup();
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filelist">文件列表</param>
        /// <param name="workingdir">工作目录</param>
        /// <param name="remote_filename">远程文件名称</param>
        /// <param name="local_filepath">本地文件路径</param>
        public bool Download(string remote_filename, string local_filepath)
        {
            return ftpsvc.Download(remote_filename, local_filepath);
        }
        /// <summary>
        /// 以递归方式下载文件夹
        /// </summary>
        private bool downloadFolder(string folderPath, string local_filepath)
        {
            string[] filelist = null;
            string workingdir = folderPath;
            ChangeDir(ref filelist, ref workingdir);
            foreach (var line in filelist)
            {
                try
                {
                    string[] res = readLine(line, syst_info);
                    string name = res[0];
                    string type = res[2];
                    if (type != "")
                    {
                        if(type.Equals("File"))
                        {
                            if (!Download(name, local_filepath))
                            {
                                return false;
                            }
                        } else if (type.Equals("Folder"))
                        {
                            if(!downloadFolder(workingdir + "/" + name, local_filepath + "\\" + name))
                            {
                                return false;
                            }
                            ftpsvc.Cdup();
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 下载文件夹
        /// </summary>
        /// <param name="remote_folderpath">远程服务器的当前目录</param>
        /// <param name="local_filepath">本地的下载目录</param>
        /// <param name="remote_filename">远程文件夹名</param>
        public bool DownloadFolder(string remote_folderpath, string local_filepath, string remote_filename)
        {
            if(!downloadFolder(remote_folderpath + "/" + remote_filename, local_filepath + "\\" + remote_filename))
            {
                return false;
            }
            ftpsvc.Cdup();
            return true;
        }
        /// <summary>
        /// 中止传输
        /// </summary>
        public void Abort()
        {
            ftpsvc.abortDataSocket();
        }

        public string[] readLine(string line, string syst)
        {
            string[] res = new string[4];
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
                            for (; i<line.Length; i++)
                            {
                                name += line[i];
                            }
                            break;
                        }
                    }
                    res[0] = name;
                    res[1] = size;
                    res[2] = type;
                    res[3] = date;
                }
                else if (syst.Contains("Windows_NT"))
                {
                    string name = "";
                    string size = "";
                    string type = "";
                    string date = "";
                    int var_flag = 0;
                    for (int i = 0; i<line.Length;)
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
                            for (; i<line.Length; i++)
                            {
                                name += line[i];
                            }
                            break;
                        }
                    }
                    res[0] = name;
                    res[1] = size;
                    res[2] = type;
                    res[3] = date;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }
        #endregion
    }
}