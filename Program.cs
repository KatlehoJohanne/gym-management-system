using System;
using System.Windows.Forms;
using GymManagement.Forms;

namespace GymManagement
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // start with login form
            Application.Run(new LoginForm());
        }
    }
}
