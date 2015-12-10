using Newtonsoft.Json;

namespace CRT
{
    interface CommandOptionInterface
    {
        string FormatConsole();
        void FormatJson(ref JsonWriter writer);
    }
}
