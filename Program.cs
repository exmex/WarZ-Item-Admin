using System;
using System.Windows.Forms;

namespace WarZLocal_Admin
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "An error occurred!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
