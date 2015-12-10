using CRT.CommandOption;
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
            var options = new List<CommandOptionInterface>();
            options.Add(new Checkbox("Disable java updater adware", true));
            options.Add(new Checkbox("Disable adobe reader updater adware", true));
            return options;
        }

        public bool isSupportedPlatform()
        {
            throw new NotImplementedException();
        }
    }
}
