using recreation_centre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace User
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
            /*Application.SetCompatibleTextRenderingDefault(false);*/
            Application.Run(new Admin(new Backend.VisitorProcess(), new Backend.TicketProcess()));
            Application.Run(new CheckinStaff(new Backend.VisitorProcess(), "fuck you nigga"));
            Application.Run(new CheckoutStaff(new Backend.VisitorProcess(), new Backend.TicketProcess(), "Fuck your ass"));

        }
    }
}
