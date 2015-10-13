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
    }
}
