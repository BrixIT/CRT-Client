using CRT.Helper;
using CRT.OutputFormatter;
using System;
using System.Collections.Generic;

namespace CRT.Module
{
    /// <summary>
    /// Module that lists all installed antivirus tools, antimalware tools and firewalls
    /// 
    /// Supported: 
    /// - Microsoft Security Essentials
    /// - AVG Antivirus
    /// - Avast
    /// - Malwarebytes anti-malware
    /// </summary>
    class VirusScannerReport : ReportingModuleInterface
    {
        public string ModuleName
        {
            get
            {
                return "Installed virus scanners";
            }
        }

        public List<OutputInterface> getResult()
        {
            var result = new List<OutputInterface>();

            var installedSoftware = InstalledSoftwareHelper.GetInstalledSoftware();

            var table = new Table();
            table.SetHeaders(new string[] { "Name", "Version", "Install date" });

            var antivirusKeys = new string[] {
                "Microsoft Security Client",
                "AVG",
                "Malwarebytes Anti-Malware_is1",
                "Avast"
            };

            foreach(string antivirusKey in antivirusKeys)
            {
                if (installedSoftware.ContainsKey(antivirusKey))
                {
                    var key = installedSoftware[antivirusKey];
                    table.AddRow(new object[] { key.Name, key.Version, key.InstallDate });
                }
            }
            if(table.RowCount > 0)
            {
                result.Add(table);
            }
            else
            {
                result.Add(new Text("No installed virusscanners or malware protection found"));
            }
            return result;
        }

        public bool isSupportedPlatform()
        {
            return true;
        }
    }
}
