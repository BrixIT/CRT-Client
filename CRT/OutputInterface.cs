using Newtonsoft.Json;

namespace CRT
{
    interface OutputInterface
    {
        string FormatConsole();
        void FormatJson(ref JsonWriter writer);
    }
}
