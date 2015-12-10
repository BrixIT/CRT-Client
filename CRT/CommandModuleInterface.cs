using System.Collections.Generic;

namespace CRT
{
    interface CommandModuleInterface : ModuleInterface
    {
        List<CommandOptionInterface> GetOptions();
        List<OutputInterface> Execute(List<CommandOptionInterface> options);
    }
}
