using System;
using System.Collections.Generic;

namespace CRT.Module
{
    /// <summary>
    /// Disables installation of adware with the installers/updaters of common runtimes.
    /// 
    /// Supports:
    /// - Java updater
    /// - Adobe reader updater
    /// 
    /// </summary>
    class RuntimeAdware : CommandModuleInterface
    {
        public string ModuleName
        {
            get
            {
                return "Disable runtime updater adware";
            }
        }

        public List<OutputInterface> Execute(List<CommandOptionInterface> options)
        {
            throw new NotImplementedException();
        }

        public List<CommandOptionInterface> GetOptions()
        {
            throw new NotImplementedException();
        }

        public bool isSupportedPlatform()
        {
            throw new NotImplementedException();
        }
    }
}
