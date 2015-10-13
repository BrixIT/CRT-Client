using CRT.DataStructure;
using Microsoft.Win32;
using System;
using System.Collections.Generic;

namespace CRT.Helper
{
    class InstalledSoftwareHelper
    {
        public static Dictionary<string, InstalledSoftware> GetInstalledSoftware()
        {
            string uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            string uninstallKeyWoW = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
            var result = new Dictionary<string, InstalledSoftware>();
            var LocalMachine32 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            var LocalMachine64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);

            using (RegistryKey rk = LocalMachine32.OpenSubKey(uninstallKey))
            {
                InstalledSoftwareHelper.getInstalledSoftwareFromKey(rk, ref result);
            }
            using (RegistryKey rk = LocalMachine32.OpenSubKey(uninstallKeyWoW))
            {
                InstalledSoftwareHelper.getInstalledSoftwareFromKey(rk, ref result);
            }
            using (RegistryKey rk = LocalMachine64.OpenSubKey(uninstallKey))
            {
                InstalledSoftwareHelper.getInstalledSoftwareFromKey(rk, ref result);
            }
            using (RegistryKey rk = LocalMachine64.OpenSubKey(uninstallKeyWoW))
            {
                InstalledSoftwareHelper.getInstalledSoftwareFromKey(rk, ref result);
            }

            return result;
        }

        private static void getInstalledSoftwareFromKey(RegistryKey rk, ref Dictionary<string, InstalledSoftware> softwareList)
        {
            foreach (string skName in rk.GetSubKeyNames())
            {
                using (RegistryKey sk = rk.OpenSubKey(skName))
                {
                    if (!softwareList.ContainsKey(skName))
                    {
                        try
                        {
                            var item = new InstalledSoftware();
                            item.Name = (string)sk.GetValue("DisplayName");
                            item.RegistryKey = skName;
                            item.Version = (string)sk.GetValue("DisplayVersion");
                            item.InstallDate = (string)sk.GetValue("InstallDate");
                            softwareList.Add(skName, item);
                        }
                        catch (Exception ex)
                        { }
                    }
                }
            }
        }
    }
}
