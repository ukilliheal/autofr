using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoFR
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new loadingbarForm()); 
            Application.Run(new Form1());
            }
            catch (Exception crash)
            {
                MessageBox.Show(crash.Message.ToString());
            }
        }
    }
}
