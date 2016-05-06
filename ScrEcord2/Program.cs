using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScrEcord2
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //二重起動をチェックする
            if (System.Diagnostics.Process.GetProcessesByName(
                System.Diagnostics.Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                //すでに起動していると判断して終了
                MessageBox.Show("既に同じプロセス名のプロセスが稼働しています");
                return;
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            new Form1();
            Application.Run();
        }
    }
}
