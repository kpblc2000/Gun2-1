using Gun2Core.Infrastructure;
using Gun2Core.Infrastructure.Commands;
using Gun2Core.Models;
using Gun2Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

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

            GetSettingsFileNameCommand = new RelayCommand(OnGetSettingsFileNameCommandExecuted, CanGetSettingsFileNameCommandExecute);
        }

        internal void SaveSettigns()
        {
            _CoreSettings.Save();
        }

        #region Commands

        #region GetSettingsFileNameCommand

        public ICommand GetSettingsFileNameCommand { get; }

        private void OnGetSettingsFileNameCommandExecuted(object p)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = GeneralFunc.GetDialogTitle("Путь к settings.xml");
            dlg.Filter = "Файлы xml|*.xml";
            if (dlg.ShowDialog() == DialogResult.OK
                && Path.GetFileNameWithoutExtension(dlg.FileName).ToUpper() == "SETTINGS")
            {
                SettingsFileName = dlg.FileName;
            }
        }

        private bool CanGetSettingsFileNameCommandExecute(object p)
        {
            return true;
        }

        #endregion

        #endregion

        #region Публичные свойства
        public string DbConnectionString
        {
            get { return _DbConnectionString; }
            set
            {
                if (Set(ref _DbConnectionString, value))
                {
                    _CoreSettings.DbConnectionString = value;
                    OnPropertyChanged(nameof(CanSaveChanges));
                }
            }
        }

        public string SettingsFileName
        {
            get { return _SettingsFileName; }
            set
            {
                if (Set(ref _SettingsFileName, value))
                {
                    _CoreSettings.SettingsFileName = value;
                    OnPropertyChanged(nameof(CanSaveChanges));
                }
            }
        }

        public bool CanSaveChanges
        {
            get
            {
                return !string.IsNullOrEmpty(SettingsFileName)
                    && File.Exists(SettingsFileName)
                    && !string.IsNullOrEmpty(DbConnectionString);
            }
        }

        public bool CanChangeSettings
        { get
            {
#if DEBUG
                return true;
#else
return false;
#endif
            }
        }
#endregion

        private CoreSettings _CoreSettings;
        private string _DbConnectionString;
        private string _SettingsFileName;
    }
}
