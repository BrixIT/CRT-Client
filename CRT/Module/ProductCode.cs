using CRT.OutputFormatter;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Management;

namespace CRT.Module
{
    /// <summary>
    /// Module for retrieving product keys.
    /// 
    /// Supports:
    /// - Windows xp - 10
    /// 
    /// ToDo:
    /// - Office
    /// </summary>
    class ProductCode : ReportingModuleInterface
    {
        private string moduleName = "Product Key";

        public string ModuleName
        {
            get
            {
                return moduleName;
            }
        }

        public bool isSupportedPlatform()
        {
            // This one can run on any windows version
            return true;
        }

        public List<OutputInterface> getResult()
        {
            var result = new List<OutputInterface>();
            var resultTable = new Table();
            resultTable.SetHeaders(new string[]{"Product name", "Key"});
            resultTable.AddRow(new string[] { this.GetOSFriendlyName(), this.GetWindowsProductKey()});
            result.Add(resultTable);
            return result;
        }

        private string GetOSFriendlyName()
        {
            string result = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem");
            foreach (ManagementObject os in searcher.Get())
            {
                result = os["Caption"].ToString();
                break;
            }
            return result;
        }

        private string GetWindowsProductKey()
        {
            var key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
                                          RegistryView.Registry64);
            const string keyPath = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion";
            var subKey = key.OpenSubKey(keyPath);
            var digitalProductId = (byte[])subKey.GetValue("DigitalProductId");

            var isWin8OrUp =
                (Environment.OSVersion.Version.Major == 6 && System.Environment.OSVersion.Version.Minor >= 2)
                ||
                (Environment.OSVersion.Version.Major > 6);

            var productKey = isWin8OrUp ? this.DecodeProductKeyWin8AndUp(digitalProductId) : this.DecodeProductKey(digitalProductId);
            return productKey;
        }

        private string DecodeProductKey(byte[] digitalProductId)
        {
            const int keyStartIndex = 52;
            const int keyEndIndex = keyStartIndex + 15;
            var digits = new[]
            {
                'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'M', 'P', 'Q', 'R',
                'T', 'V', 'W', 'X', 'Y', '2', '3', '4', '6', '7', '8', '9',
            };
            const int decodeLength = 29;
            const int decodeStringLength = 15;
            var decodedChars = new char[decodeLength];
            var hexPid = new ArrayList();
            for (var i = keyStartIndex; i <= keyEndIndex; i++)
            {
                hexPid.Add(digitalProductId[i]);
            }
            for (var i = decodeLength - 1; i >= 0; i--)
            {
                // Every sixth char is a separator.
                if ((i + 1) % 6 == 0)
                {
                    decodedChars[i] = '-';
                }
                else
                {
                    // Do the actual decoding.
                    var digitMapIndex = 0;
                    for (var j = decodeStringLength - 1; j >= 0; j--)
                    {
                        var byteValue = (digitMapIndex << 8) | (byte)hexPid[j];
                        hexPid[j] = (byte)(byteValue / 24);
                        digitMapIndex = byteValue % 24;
                        decodedChars[i] = digits[digitMapIndex];
                    }
                }
            }
            return new string(decodedChars);
        }


        private string DecodeProductKeyWin8AndUp(byte[] digitalProductId)
        {
            var key = String.Empty;
            const int keyOffset = 52;
            var isWin8 = (byte)((digitalProductId[66] / 6) & 1);
            digitalProductId[66] = (byte)((digitalProductId[66] & 0xf7) | (isWin8 & 2) * 4);

            // Possible alpha-numeric characters in product key.
            const string digits = "BCDFGHJKMPQRTVWXY2346789";
            int last = 0;
            for (var i = 24; i >= 0; i--)
            {
                var current = 0;
                for (var j = 14; j >= 0; j--)
                {
                    current = current * 256;
                    current = digitalProductId[j + keyOffset] + current;
                    digitalProductId[j + keyOffset] = (byte)(current / 24);
                    current = current % 24;
                    last = current;
                }
                key = digits[current] + key;
            }
            var keypart1 = key.Substring(1, last);
            const string insert = "N";
            key = key.Substring(1).Replace(keypart1, keypart1 + insert);
            if (last == 0)
                key = insert + key;
            for (var i = 5; i < key.Length; i += 6)
            {
                key = key.Insert(i, "-");
            }
            return key;
        }
    }
}
