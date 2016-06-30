using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATrigger
{
    /// <summary>
    /// add instance receiver and emitter
    /// </summary>
    public abstract class TriggerObject
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

        public static implicit operator bool(TriggerObject obj)
        {
            return obj != null;
        }
    }
    /// <summary>
    /// add static receiver and emitter
    /// </summary>
    public interface ITriggerStatic
    {
    }
}
