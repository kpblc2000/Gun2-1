using Gun2Core.Models;
using Gun2Core.Views.Windows;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gun2Core.Infrastructure
{
    public class CoreInitialize
    {
        public static void Initialize()
        {
            CoreSettings settings = new CoreSettings();
            if (string.IsNullOrEmpty(settings.DbConnectionString)
                || string.IsNullOrEmpty(settings.SettingsFileName)
                || !File.Exists(settings.SettingsFileName)
                )
            {
                CoreSettingsView winCoreSettings = new CoreSettingsView();
                winCoreSettings.ShowDialog();
                if (winCoreSettings.DialogResult != true)
                {
                    throw new Exception("Настройки не сохранены");
                }
            }
        }
    }
}
