using System;
using System.Collections.Generic;
using System.Linq;

namespace CRT
{
    class ModuleResolver
    {
        public List<ReportingModuleInterface> GetReportingModules()
        {
            var result = new List<ReportingModuleInterface>();
            var reportingInterfaceType = typeof(ReportingModuleInterface);
            var classes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(reportingInterfaceType.IsAssignableFrom).ToList();
            classes.Remove(typeof(ReportingModuleInterface));
            foreach(var classType in classes)
            {
                result.Add((ReportingModuleInterface) Activator.CreateInstance(classType));
            }
            return result;
        }
    }
}
