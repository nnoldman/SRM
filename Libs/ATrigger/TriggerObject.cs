using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATrigger
{
    public class TriggerObject : Signal
    {
        public TriggerObject()
        {
            DataCenter.AddEntityWithInstance(this);
            DataCenter.AddReceiversWithInstance(this);
        }
        ~TriggerObject()
        {
            DataCenter.RemoveEntityWithInstance(this);
            DataCenter.RemoveReceiversWithInstance(this);
        }
    }
}
