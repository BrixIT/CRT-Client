using Microsoft.Win32;
using System;
using System.Management;

namespace CRT.Helper
{
    class Identification
    {
        public static string GetMotherBoardID()
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystemProduct");
            ManagementObjectCollection moc = mos.Get();
            string motherBoard = "";
            foreach (ManagementObject mo in moc)
            {
                motherBoard = (string)mo["UUID"];
            }
            return motherBoard;
        }

        public static string GetWindowsInstallationId()
        {
            var localMachine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
                              RegistryView.Default);
            string keyPath = @"Software\Microsoft\Windows NT\CurrentVersion";
            string productId = (string)localMachine.OpenSubKey(keyPath).GetValue("ProductId");
            return productId;
        }
    }
}
