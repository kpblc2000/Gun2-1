using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gun2Core.Models
{
    public class CoreSettings
    {
        public CoreSettings()
        {
            RegistryKey curUser = Registry.CurrentUser;
            RegistryKey _BaseKey = curUser.CreateSubKey(ParentRegistryHive);
#if DEBUG
            DbConnectionString = _BaseKey.GetValue(_DbConnectionStringKey) as string;
            SettingsFileName = _BaseKey.GetValue(_SettingsFileName) as string;
#else
            DbConnectionString = "Data Source=SqlServer;Initial Catalog=Gun2Db;Integrated Security=True";
            SettingsFileName = @"\\server\library\settings.xml";
            Save();
#endif
            _BaseKey.Close();
        }

        internal void Save()
        {
            RegistryKey curUser = Registry.CurrentUser;
            RegistryKey _BaseKey = curUser.CreateSubKey(ParentRegistryHive);
            _BaseKey.SetValue(_DbConnectionStringKey, DbConnectionString);
            _BaseKey.SetValue(_SettingsFileName, SettingsFileName);
            _BaseKey.Close();
        }

        public string DbConnectionString { get; internal set; }
        public string SettingsFileName { get; internal set; }

        public string ParentRegistryHive
        {
            get
            {
#if DEBUG
                return @"SOFTWARE\kpblc2000\debug";
#else
                return _ParentRegistryHive;
#endif
            }
        }

        private readonly string _ParentRegistryHive = @"SOFTWARE\kpblc2000";
        private readonly string _DbConnectionStringKey = "DbConnectionString";
        private readonly string _SettingsFileName = "SettingsFileName";

    }
}
