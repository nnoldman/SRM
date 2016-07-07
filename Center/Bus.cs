using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class Bus
    {
        static Dictionary<Port, Listeners> mPortListeners = new Dictionary<Port, Listeners>();

        public static void Input(Port p)
        {
            Listeners listeners;

            if(mPortListeners.TryGetValue(p,out listeners))
            {
                foreach (var listener in listeners)
                    listener.SetValue(p.RawValue);
            }
        }
    }
}
