using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace MiniPomodoro
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region Check Windows 7

            if (!Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager.IsPlatformSupported)
            {
                MessageBox.Show("This program required Window's 7 to run.", "Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            #endregion

            // 对 ApplicationId 赋值后，创建程序的多个实例会在任务栏里显示为一组
            TaskbarManager.Instance.ApplicationId = "84ECEBF4-90C6-414D-8A5A-93C0834110EB";

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
