using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace PDMS
{
    internal static class Program
    {
        // 这个名字要全局唯一，建议用你的应用程序名或 GUID
        private const string MutexName = "MyApp_OnlyOneInstance";
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzkxNTExMEAzMjM5MmUzMDJlMzAzYjMyMzkzYmw3NTF3U1U1ZC9ibHY3b3hqVVdmTjJwVVBUKzcvS1ZrOTZmOEozWC9sL3c9");

            bool createdNew;
            using (var mutex = new Mutex(true, MutexName, out createdNew))
            {
                if (!createdNew)
                {
                    // 已有实例在运行
                    MessageBox.Show("程序已经在运行中，不能重复启动。",
                                    "提示",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    return;
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormLogIn());
                //FormLogIn formLogIn = new FormLogIn();
                //if (formLogIn.ShowDialog() == DialogResult.OK)
                //{
                //    Application.Run(new Form1());
                //}
            }

        }
    }
}
