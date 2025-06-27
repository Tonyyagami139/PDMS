using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDMS
{
    public static class Global
    {
        public static string DbSettingTym { get; set; } = "Tym";
        public static string DbSettingSqlserver { get; set; } = "Sqlserver";

        public static string DefaultPicturePath { get; set; } = "\\\\192.168.31.223\\TestingDataRoot\\PDMS\\FailureRecord\\default.png";

        public static string FailureModesFileName { get; set; } = "\\\\192.168.31.223\\TestingDataRoot\\PDMS\\FailureRecord\\FailureModes.txt";

        public static string ProcessesFileName { get; set; } = "\\\\192.168.31.223\\TestingDataRoot\\PDMS\\FailureRecord\\ProcessNodes.txt";
        public static string UserName { get; set; } = "NA";

        public static int UserLevel { get; set; } = -1;

        public static string FailurePictureRootPath { get; set; } = "\\\\192.168.31.223\\TestingDataRoot\\PDMS\\FailureRecord\\FailurePictures";

        public static string LogoPicture { get; set; } = "\\\\192.168.31.223\\TestingDataRoot\\PDMS\\FailureRecord\\logo.png";

        public static string LogInPicture { get; set; } = "\\\\192.168.31.223\\TestingDataRoot\\PDMS\\FailureRecord\\login.png";

        public static string UserDataPath = "C:\\ProgramData\\zTx\\PDMS\\user.dat";



        public static string GetAppVersion()
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                var version = ApplicationDeployment.CurrentDeployment.CurrentVersion;
                return version.ToString();
            }
            else
            {
                // 获取当前程序集的版本信息
                return "Local Build";
            }
        }

        public static void SleepMilliSeconds(int milliseconds)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            while (stopwatch.ElapsedTicks < milliseconds * (Stopwatch.Frequency / 1000)) { Application.DoEvents(); }
            stopwatch.Stop();
        }

        
    }
}
