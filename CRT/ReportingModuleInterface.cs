using System.Collections.Generic;

namespace CRT
{
    interface ReportingModuleInterface : ModuleInterface
    {
        List<OutputInterface> getResult();
    }
}
