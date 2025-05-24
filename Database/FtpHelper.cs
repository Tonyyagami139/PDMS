using FluentFTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class FtpHelper
    {
        private FtpClient _client;

        public FtpHelper(string host, int port, string username, string password)
        {
            _client = new FtpClient(host, port)
            {
                Credentials = new System.Net.NetworkCredential(username, password)
            };

        }

        public void Connect()
        {
            try
            {
                _client.AutoConnect(); // 自动连接并选择合适的加密模式
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void GetListing()
        {
            FtpListItem[] r = _client.GetListing();
            return;
        }
        public void Disconnect()
        {
            if (_client.IsConnected)
            {
                _client.Disconnect();
            }
        }

        public void CreateDir(string FolderPath)
        {
            _client.CreateDirectory(FolderPath);
        }
        public void UploadFileContent(string fileContent, string remoteFilePath)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(fileContent);
            _client.UploadBytes(bytes, remoteFilePath);
        }
        public void UploadFileContent(byte[] fileBytes, string remoteFilePath)
        {
            _client.UploadBytes(fileBytes, remoteFilePath);
        }

        // 上传文件
        public void UploadFile(string localFilePath, string remoteFilePath)
        {
            _client.UploadFile(localFilePath, remoteFilePath);
        }

        // 下载文件
        public void DownloadFile(string remoteFilePath, string localFilePath)
        {
            _client.DownloadFile(localFilePath, remoteFilePath);
        }
    }
}
