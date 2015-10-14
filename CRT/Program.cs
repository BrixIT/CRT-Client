using CRT.Helper;
using System;
using System.Collections.Generic;
using Zeroconf;

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

                // The PC ID should be unique for the hardare of the computer
                var pcId = Identification.GetMotherBoardID();
                Console.WriteLine("PC ID: " + pcId + "\n");

                Console.WriteLine(" c. Connect to central CRT server");
                Console.WriteLine(" r. Go to the report menu");
                Console.WriteLine(" t. Go to the task menu");
                Console.WriteLine(" q. Quit");
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.C:
                        Connect();
                        Console.Clear();
                        break;

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
            foreach (string choise in choises)
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

        static async void Connect()
        {
            Console.Clear();
            Console.WriteLine("---=[ Central server ]=---\n");
            Console.WriteLine("Searching for central server with ZeroConf...");
            IReadOnlyList<IZeroconfHost> results = await
                    ZeroconfResolver.ResolveAsync("_crt._tcp.local.");

            Console.Clear();
            Console.WriteLine("---=[ Central server ]=---\n");
            if (results.Count == 0)
            {
                Console.WriteLine("Could not locate a master server");
                Console.ReadLine();
            }
            else if (results.Count == 1)
            {
                Connect(results[0]);
            }
            else
            {
                int index = 1;
                foreach (var host in results)
                {
                    Console.Write(index.ToString() + ". " + host.IPAddress + " (" + host.DisplayName + ")");
                }
                Console.WriteLine("\nEnter number to continue: ");
                var choise = Console.ReadKey();
                int serverIndex = Int32.Parse(choise.KeyChar.ToString());
                Connect(results[serverIndex]);
            }
            Console.ReadLine();
        }

        static void Connect(IZeroconfHost host)
        {
            var services = host.Services;
            if (services.ContainsKey("_crt._tcp.local."))
            {
                IService service = services["_crt._tcp.local."];
                Connect(host.IPAddress, service.Port);
            }
            else
            {
                Console.WriteLine("No _crt._tcp.local. record found for host " + host.IPAddress);
            }
        }

        static void Connect(string ip, int port)
        {
            RemoteControl remote = new RemoteControl(ip, port);
        }

    }
}
