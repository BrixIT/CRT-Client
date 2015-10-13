using System;
using System.Collections.Generic;

namespace CRT
{
    class Program
    {
        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("---=[ Computer Repair Toolkit ]=---\n");
                Console.WriteLine(" r. Go to the report menu");
                Console.WriteLine(" t. Go to the task menu");
                Console.WriteLine(" q. Quit");
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.R:
                        Reporting();
                        break;

                    case ConsoleKey.T:
                        throw new NotImplementedException();

                    case ConsoleKey.Q:
                        running = false;
                        break;
                }
            }
        }

        static void Reporting()
        {
            Console.Clear();
            Console.WriteLine("---=[ Reporting ]=---\n");
            Console.WriteLine("Enter the number of the reports to run, seperate with comma\n");

            var resolver = new ModuleResolver();
            var reportingModules = resolver.GetReportingModules();
            var moduleIndex = new Dictionary<int, ReportingModuleInterface>();
            var i = 1;
            foreach (var reportingModule in reportingModules)
            {
                Console.Write(i);
                Console.Write(". ");
                Console.WriteLine(reportingModule.ModuleName);
                moduleIndex.Add(i, reportingModule);
                i++;
            }
            Console.WriteLine();
            Console.Write("Enter numbers: ");
            var numbers = Console.ReadLine();
            var choises = numbers.Replace(" ", "").Split(",;".ToCharArray());
            Console.Clear();
            foreach(string choise in choises)
            {
                var moduleId = Int32.Parse(choise);
                ReportingModuleInterface reportingModule = moduleIndex[moduleId];
                Console.WriteLine("---=[ " + reportingModule.ModuleName + " ]=---");
                var resultParts = reportingModule.getResult();
                foreach (var result in resultParts)
                {
                    Console.WriteLine(result.FormatConsole());
                }
            }
            Console.ReadLine();

        }
    }
}
