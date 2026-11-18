using System;
using System.Windows.Forms;

namespace EDATRACK
{
    static class Program
    {
        public static int IdSesi = 0;
        public static string RoleSesi = "";
        public static string NamaSesi = "";

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new View.FormLogin());
        }
    }
}