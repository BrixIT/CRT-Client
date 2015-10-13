using CRT.Helper;
using CRT.OutputFormatter;
using Microsoft.Win32;
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

            foreach(var dotnetVersion in RuntimeReport.GetOldDotNetVersions())
            {
                table.AddRow(new object[] { ".NET Framework", dotnetVersion });
            }

            try
            {
                var version = Get45or451FromRegistry();
                table.AddRow(new object[] { ".NET Framework", version });
            }
            catch(Exception ex) { }

            result.Add(table);
            return result;
        }

        public bool isSupportedPlatform()
        {
            return true;
        }

        private static string[] GetOldDotNetVersions()
        {
            var result = new List<string>();
            // Opens the registry key for the .NET Framework entry.
            using (RegistryKey ndpKey =
                RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, "").
                OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\"))
            {
                foreach (string versionKeyName in ndpKey.GetSubKeyNames())
                {
                    if (versionKeyName.StartsWith("v"))
                    {

                        RegistryKey versionKey = ndpKey.OpenSubKey(versionKeyName);
                        string name = (string)versionKey.GetValue("Version", "");
                        string sp = versionKey.GetValue("SP", "").ToString();
                        string install = versionKey.GetValue("Install", "").ToString();
                        if (install == "") //no install info, must be later.
                            result.Add(versionKeyName + "  " + name);
                        else
                        {
                            if (sp != "" && install == "1")
                            {
                                result.Add(versionKeyName + "  " + name + "  SP" + sp);
                            }

                        }
                        if (name != "")
                        {
                            continue;
                        }
                        foreach (string subKeyName in versionKey.GetSubKeyNames())
                        {
                            RegistryKey subKey = versionKey.OpenSubKey(subKeyName);
                            name = (string)subKey.GetValue("Version", "");
                            if (name != "")
                                sp = subKey.GetValue("SP", "").ToString();
                            install = subKey.GetValue("Install", "").ToString();
                            if (install != "")
                            {
                                if (sp != "" && install == "1")
                                {
                                    result.Add(versionKeyName + "  " + subKeyName + "  " + name + "  SP" + sp);
                                }
                                else if (install == "1")
                                {
                                    result.Add(versionKeyName + "  " + subKeyName + "  " + name);
                                }
                            }
                        }
                    }
                }
            }
            return result.ToArray();
        }

        private static string Get45or451FromRegistry()
        {
            using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
            {
                int releaseKey = Convert.ToInt32(ndpKey.GetValue("Release"));
                return CheckFor45DotVersion(releaseKey);
            }
        }

        private static string CheckFor45DotVersion(int releaseKey)
        {
            if (releaseKey >= 393295)
            {
                return "v4.6 or later";
            }
            if ((releaseKey >= 379893))
            {
                return "v4.5.2 or later";
            }
            if ((releaseKey >= 378675))
            {
                return "v4.5.1 or later";
            }
            if ((releaseKey >= 378389))
            {
                return "v4.5 or later";
            }
            // This line should never execute. A non-null release key should mean
            // that 4.5 or later is installed.
            return "No 4.5 or later version detected";
        }

    }
}
