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
        private ftpservice ftpsvc;

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
        #endregion
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
        /// 返回工作目录
        /// </summary>
        /// <returns></returns>
        private string getDir()
        {
            string response = ftpsvc.Pwd();
            string workingdir = "";
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
            if (isPassive)
            {
                ftpsvc.connectDataSocketForPassive();
                string response1 = ftpsvc.List();
                int code1 = ftpsvc.getResponseCode(response1);
                if(code1 != 150 && code1 != 125)
                {
                    return false;
                }
            }
            else
            {
                ftpsvc.Port();
                string response2 = ftpsvc.List();
                int code2 = ftpsvc.getResponseCode(response2);
                if (code2 != 150 && code2 != 125)
                {
                    return false;
                }
                ftpsvc.connectDataSocketForActive();
            }
            rawstring = ftpsvc.getListData();
            ftpsvc.closeDataSocket();
            string response3 = ftpsvc.getResponseString();
            int code3 = ftpsvc.getResponseCode(response3);
            if (code3 != 226)
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
        public void NewFolder(ref string[] filelist, ref string workingdir, string foldername)
        {
            ftpsvc.Mkd(foldername);
            Refresh(ref filelist, ref workingdir);
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
        /// <param name="filelist">文件列表</param>
        /// <param name="workingdir">工作目录</param>
        /// <param name="filename">文件名称</param>
        /// <returns>是否上传成功</returns>
        public bool Upload(ref string[] filelist, ref string workingdir, string filename)
        {
            if (ftpsvc.Upload(filename))
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
        public void Abort()
        {
            ftpsvc.abortDataSocket();
        }
    }
}