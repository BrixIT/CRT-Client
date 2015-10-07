using CRT.OutputFormatter;
using System.Collections.Generic;

namespace CRT.Module
{
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

        public List<OutputInterface> getResult()
        {
            var result = new List<OutputInterface>();
            result.Add(new Text("This is Hello World from the Product Key module"));
            return result;
        }
    }
}
