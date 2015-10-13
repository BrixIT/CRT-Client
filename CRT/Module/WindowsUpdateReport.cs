using System;
using System.Collections.Generic;
using WUApiLib;

namespace CRT.Module
{
    class WindowsUpdateReport : ReportingModuleInterface
    {
        public string ModuleName
        {
            get
            {
                return "Windows Update";
            }
        }

        public List<OutputInterface> getResult()
        {
            var result = new List<OutputInterface>();

            // http://www.nullskull.com/a/1592/install-windows-updates-using-c--wuapi.aspx
            UpdateSession uSession = new UpdateSession();
            IUpdateSearcher uSearcher = uSession.CreateUpdateSearcher();
            ISearchResult uResult = uSearcher.Search("IsInstalled=0 and Type = 'Software'");
            return result;
        }

        public bool isSupportedPlatform()
        {
            // Disabled because this module isn't done yet.
            return false;
        }
    }
}
