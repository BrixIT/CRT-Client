using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRT
{
    class RemoteControl
    {
        public RemoteControl(string ip, int port)
        {
            Console.WriteLine(String.Format("Connecting to {0}:{1}", ip, port));
        }
    }
}
