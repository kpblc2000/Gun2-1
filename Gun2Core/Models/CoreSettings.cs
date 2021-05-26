using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gun2Core.Models
{
    internal class CoreSettings
    {
        public CoreSettings()
        {
            RegistryKey curUser = Registry.CurrentUser;
            RegistryKey _BaseKey = curUser.CreateSubKey(_ParentRegistryHive);
            DbConnectionString = _BaseKey.GetValue(_DbConnectionStringKey) as string;
            SettingsFileName = _BaseKey.GetValue(_SettingsFileName) as string;
            _BaseKey.Close();
        }

        public void Save()
        {
            _BaseKey.SetValue(_DbConnectionStringKey, DbConnectionString);
            _BaseKey.SetValue(_SettingsFileName, SettingsFileName);
            _BaseKey.Close();
        }

        public string DbConnectionString { get; set; }
        public string SettingsFileName { get; set; }

        private readonly string _ParentRegistryHive = @"SOFTWARE\kpblc2000";
        private readonly string _DbConnectionStringKey = "DbConnectionString";
        private readonly string _SettingsFileName = "SettingsFileName";
        private RegistryKey _BaseKey;
        
    }
}
