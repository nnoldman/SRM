using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Object : ATrigger.TriggerObject
    {
        public Object()
        {
            ATrigger.DataCenter.AddInstance(this);
        }
        ~Object()
        {
            ATrigger.DataCenter.RemoveInstance(this);
        }

        public List<Component> Components { get { 
            return mComponents;
        } }

        internal List<Component> mComponents = new List<Component>();

        public T AddComponent<T>() where T : Component, new()
        {
            T com = new T();
            mComponents.Add(com);
            return com;
        }

        public void RemoveComponent<T>() where T : Component, new()
        {
            mComponents.RemoveAll((item) => item.GetType() == typeof(T));
        }

        public T GetComponent<T>() where T : Component, new()
        {
            foreach (var com in mComponents)
            {
                if (com.GetType() == typeof(T))
                    return (T)com;
            }
            return null;
        }
    }
}
