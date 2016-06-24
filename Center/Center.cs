using ATrigger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeifenLuo.WinFormsUI.Docking;

namespace Core
{
    public class Center:TriggerObject
    {
        public static DockPanel Container;

        [DataEntity((int)DataType.ApplicationExit)]
        public static Signal OnExit = new Signal();
    }
}
