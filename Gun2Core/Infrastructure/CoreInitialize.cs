using Gun2Core.Models;
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
            if (string.IsNullOrEmpty( settings.DbConnectionString)
                || string.IsNullOrEmpty(settings.SettingsFileName)
                || !File.Exists(settings.SettingsFileName)
                )
            {
                // ToDo Принудительную установку снять
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                
                builder.DataSource = Environment.GetEnvironmentVariable("computername").ToString();
                builder.InitialCatalog = "Gun2CAD";
                                builder.ConnectTimeout = 180;
                builder.IntegratedSecurity = true;
                builder.PersistSecurityInfo = false;

                settings.DbConnectionString = builder.ConnectionString;

                settings.Save();

                if (string.IsNullOrEmpty(settings.SettingsFileName)
                || !File.Exists(settings.SettingsFileName))
                {

                }

            }
        }
    }
}
