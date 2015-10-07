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
            }
        }
    }
}
