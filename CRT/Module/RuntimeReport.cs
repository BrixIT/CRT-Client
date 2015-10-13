using CRT.Helper;
using CRT.OutputFormatter;
using System;
using System.Collections.Generic;

namespace CRT.Module
{
    /// <summary>
    /// Module that lists all installed runtimes
    /// 
    /// Supported: 
    /// - Java 8
    /// - Adobe AIR
    /// - Adobe Shockwave
    /// </summary>
    class RuntimeReport : ReportingModuleInterface
    {
        public string ModuleName
        {
            get
            {
                return "Installed runtimes";
            }
        }

        public List<OutputInterface> getResult()
        {
            var result = new List<OutputInterface>();

            var installedSoftware = InstalledSoftwareHelper.GetInstalledSoftware();

            var table = new Table();
            table.SetHeaders(new string[] { "Runtime", "Installed version" });

            var runtimes = new Dictionary<string, string>();
            runtimes.Add("Adobe AIR", "Adobe AIR");
            runtimes.Add("Adobe Shockwave", "Adobe Shockwave Player");
            runtimes.Add("Java 8", "{26A24AE4-039D-4CA4-87B4-2F83218060F0}");

            foreach (var runtime in runtimes)
            {
                if (installedSoftware.ContainsKey(runtime.Value))
                {
                    var key = installedSoftware[runtime.Value];
                    table.AddRow(new object[] { runtime.Key, key.Version });
                }
                else
                {
                    table.AddRow(new object[] { runtime.Key, "None" });
                }
            }

            result.Add(table);
            return result;
        }

        public bool isSupportedPlatform()
        {
            return true;
        }
    }
}
