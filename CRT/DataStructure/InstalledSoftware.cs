namespace CRT.DataStructure
{
    class InstalledSoftware
    {
        string name;
        string registryKey;
        string version;
        string installDate;

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string RegistryKey
        {
            get
            {
                return registryKey;
            }

            set
            {
                registryKey = value;
            }
        }

        public string Version
        {
            get
            {
                return version;
            }

            set
            {
                version = value;
            }
        }

        public string InstallDate
        {
            get
            {
                return installDate;
            }

            set
            {
                installDate = value;
            }
        }
    }
}
