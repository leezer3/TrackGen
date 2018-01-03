using System;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;

namespace Weiche
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(CultureInfo.InvariantCulture.Name);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Weichengenerator());
        }
    }
}
