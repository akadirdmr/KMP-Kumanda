using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace kumanda
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frm_giris());
            Application.Run(new frmekle());
            Application.Run(new hakkinda());
           

        }
    }
}
