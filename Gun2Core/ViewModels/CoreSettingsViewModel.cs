using Gun2Core.Models;
using Gun2Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gun2Core.ViewModels
{
    internal class CoreSettingsViewModel : ViewModel
    {
        public CoreSettingsViewModel()
        {
            Title = "Настройки приложения";
            _CoreSettings = new CoreSettings();
            DbConnectionString = _CoreSettings.DbConnectionString;
            SettingsFileName = _CoreSettings.SettingsFileName;
        }

        #region Публичные свойства
        public string DbConnectionString
        {
            get { return _DbConnectionString; }
            set { Set(ref _DbConnectionString, value); }
        }

        public string SettingsFileName
        {
            get { return _SettingsFileName; }
            set { Set(ref _SettingsFileName, value); }
        } 
        #endregion

        private CoreSettings _CoreSettings;
        private string _DbConnectionString;
        private string _SettingsFileName;
    }
}
