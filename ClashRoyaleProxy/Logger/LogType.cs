using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyaleProxy
{
    enum LogType
    {
        INFO, // A default text (i.e. "Proxy started")
        WARNING, // A warning (i.e. 2 running proxys)
        PACKET, // A packet (i.e. OwnHomeData)
        EXCEPTION // An exception (i.e. NullReferenceException)
    }
}
