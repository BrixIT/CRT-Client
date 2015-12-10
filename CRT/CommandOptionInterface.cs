using System.Collections.Generic;

namespace CRT
{
    interface CommandOptionInterface
    {
        string GetLabel();
        string GetValue();
        string GetDefault();
    }
}
