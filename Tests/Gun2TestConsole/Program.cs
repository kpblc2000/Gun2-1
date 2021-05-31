using Gun2Core.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gun2TestConsole
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            //UserWindowView winUser = new UserWindowView();
            //winUser.ShowDialog();

            CoreSettingsView winSettings = new CoreSettingsView();
            winSettings.ShowDialog();

            Console.WriteLine("Press any key");
            Console.ReadKey();

        }
    }
}
