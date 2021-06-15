using Gun2Core.Models;
using Gun2Core.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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

            ObservableCollection<CadLayer> manualLayers = GetLayersFromManualFile(manualLayerFile);

            // ToDo Создать чтение настроек фильтров из "ручных" файлов
            // ToDo Читать настройки слоев и фильтров из "программного" файла.
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

        private static ObservableCollection<CadLayer> GetLayersFromManualFile(string XmlFileName)
        {
            ObservableCollection<CadLayer> res = new ObservableCollection<CadLayer>();

            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFileName);
            XmlNodeList xmlLayersNode = doc.GetElementsByTagName("Layers");
            foreach (XmlElement node in xmlLayersNode[0].ChildNodes)
            {
                if (node.HasAttribute("name"))
                {
                    string name = node.GetAttribute("name");
                    CadLayer item = res.Where(o => o.Name == name).FirstOrDefault();

                    if (item == null)
                    {
                        item = new CadLayer { Name = node.GetAttribute("name") };
                        item.CadLayerIdNames = new List<string>();
                        
                        if (node.HasAttribute("linetype"))
                        {
                            item.Linetype = node.GetAttribute("linetype");
                            if (node.HasAttribute("ltfile"))
                            {
                                item.LinetypeFileName = node.GetAttribute("ltfile");
                            }
                        }
                        if (node.HasAttribute("lineweight"))
                        {
                            item.Lineweight = int.Parse(node.GetAttribute("lineweight"));
                        }
                        if (node.HasAttribute("color"))
                        {
                            item.IndexColor = byte.Parse(node.GetAttribute("color"));
                        }
                        else
                        {
                            item.IndexColor = 7;
                        }
                        if (node.HasAttribute("Description"))
                        {
                            item.Description = node.GetAttribute("Description");
                        }
                        if (node.HasAttribute("description") && string.IsNullOrEmpty(item.Description))
                        {
                            item.Description = node.GetAttribute("description");
                        }
                        res.Add(item);
                    }
                    item.CadLayerIdNames.Add(node.HasAttribute("id") ? node.GetAttribute("id") : item.Name);
                }
            }

            return res;
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
