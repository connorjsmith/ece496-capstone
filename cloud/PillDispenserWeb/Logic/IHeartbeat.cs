using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PillDispenserWeb.Logic
{
    public interface IHeartbeat
    {
        void AddHeartbeat(string deviceId);
    }
}
