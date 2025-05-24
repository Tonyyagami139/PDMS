using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static string UserDataPath = "C:\\ProgramData\\zTx\\PDMS\\user.dat";
    }
}
