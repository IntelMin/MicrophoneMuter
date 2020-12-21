using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicMuter
{
    class MAudioDevice
    {
        public String name;
        public bool bMuted;
        public MAudioDevice(String name = "", bool bMuted = false)
        {
            this.name = name;
            this.bMuted = bMuted;
        }
    }
}
