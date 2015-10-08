namespace CRT
{
    class Program
    {
        static void Main(string[] args)
        {
            var resolver = new ModuleResolver();
            var reportingModules = resolver.GetReportingModules();
            foreach(var reportingModule in reportingModules)
            {
                System.Console.WriteLine(reportingModule.ModuleName);
                var resultParts = reportingModule.getResult();
                foreach(var result in resultParts)
                {
                    System.Console.WriteLine(result.FormatConsole());
                }
            }
            System.Console.ReadLine();
        }
    }
}
