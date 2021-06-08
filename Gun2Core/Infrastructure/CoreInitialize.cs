using Gun2Core.Models;
using Gun2Core.Views.Windows;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

            XDocument xml = XDocument.Load(settings.SettingsFileName);
            string manualLayerFile = GetCadLayerSettingsFile(xml, "ManualLayers");
            string manualLayerFilterFile = GetCadLayerSettingsFile(xml, "ManualLayerFilters");
            string programLayerFile = GetCadLayerSettingsFile(xml, "ProgramLayers");
            // ToDo Проверить корректность чтения из settings.xml
            // ToDo Создать чтение настроек слоев из "ручных" файлов
            // ToDo Создать чтение настроек фильтров из "ручных" файлов
        }

        private static string GetCadLayerSettingsFile(XDocument Doc, string NodeName)
        {
            var foundNode = Doc.Root.Descendants()
                .Where(node => node.Name == NodeName)
                .FirstOrDefault();

            var files = foundNode.Descendants()
                .Select(node => new NodeItem(node));

#if DEBUG
            files = files.OrderBy(o => o.Priority);
#else
            files = files.OrderByDescending(o => o.Priority);
#endif
            NodeItem foundFile = files.Where(o => File.Exists(o.Path)).FirstOrDefault();
            if (foundFile == null)
            {
                return null;
            }
            return foundFile.Path;
        }

        private class NodeItem
        {
            public NodeItem(XElement node)
            {
                Path = node.Attribute("path").Value.ToString();
                if (int.TryParse(node.Attribute("priority").Value, out int pr))
                {
                    Priority = pr;
                }
                else
                {
                    Priority = int.MinValue;
                }
            }
            public string Path { get; private set; }
            public int Priority { get; private set; }
        }
    }
}
