using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utility
{
    public static class FileHelper
    {
        public static void CopyFileWithFileStream(string sourceFile, string destinationFile)
        {
            if (!Directory.Exists(Path.GetDirectoryName(destinationFile)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(destinationFile));
            }


            // 获取源文件的大小
            long fileLength = new FileInfo(sourceFile).Length;
            long totalBytesCopied = 0;

            // 使用 FileStream 进行复制
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                using (FileStream destinationStream = new FileStream(destinationFile, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    byte[] buffer = new byte[1024 * 1024]; // 1MB 缓冲区
                    int bytesRead;

                    // 逐块复制文件
                    while ((bytesRead = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        destinationStream.Write(buffer, 0, bytesRead);
                        totalBytesCopied += bytesRead;
                    }
                }
            }
        }

        /// <summary>
        /// 将 sourceDir 下的所有文件复制到 targetDir。
        /// </summary>
        /// <param name="sourceDir">源文件夹路径</param>
        /// <param name="targetDir">目标文件夹路径</param>
        /// <param name="overwrite">如果目标文件已存在，是否覆盖（默认 false）</param>
        public static void CopyFiles(string sourceDir, string targetDir, bool overwrite = false)
        {
            if (string.IsNullOrWhiteSpace(sourceDir))
                throw new ArgumentException(nameof(sourceDir));
            if (string.IsNullOrWhiteSpace(targetDir))
                throw new ArgumentException(nameof(targetDir));

            // 确保源文件夹存在
            if (!Directory.Exists(sourceDir))
                throw new DirectoryNotFoundException($"源文件夹不存在：{sourceDir}");

            // 如果目标文件夹不存在，则创建
            if (!Directory.Exists(targetDir))
                Directory.CreateDirectory(targetDir);

            // 遍历并复制所有文件（不包含子目录）
            foreach (var filePath in Directory.GetFiles(sourceDir))
            {
                var fileName = Path.GetFileName(filePath);
                var destPath = Path.Combine(targetDir, fileName);
                File.Copy(filePath, destPath, overwrite);
            }
        }
    }
}
