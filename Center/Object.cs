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
        public string Name;

        public Object()
        {
            ATrigger.DataCenter.AddInstance(this);
        }
        ~Object()
        {
            ATrigger.DataCenter.RemoveInstance(this);
        }

        public List<Object> Children { get { return mChildren; } }
        public List<Component> Components { get { return mComponents; } }

        internal List<Component> mComponents = new List<Component>();
        internal List<Object> mChildren = new List<Object>();

        public T AddComponent<T>() where T : Component, new()
        {
            T com = new T();
            mComponents.Add(com);
            return com;
        }
        public Object FindChildByName(string name)
        {
            return mChildren.Find((item) => item.Name == name);
        }
        public Component AddComponent(Type type)
        {
            Component com = (Component)type.GetConstructor(Type.EmptyTypes).Invoke(null);
            mComponents.Add(com);
            return com;
        }

        public void RemoveComponent<T>() where T : Component
        {
            mComponents.RemoveAll((item) => item.GetType() == typeof(T));
        }

        public T GetComponent<T>() where T : Component
        {
            foreach (var com in mComponents)
            {
                if (com.GetType() == typeof(T))
                    return (T)com;
            }
            return null;
        }
        public T GetComponentFromChildren<T>() where T : Component
        {
            foreach (var child in mChildren)
            {
                var com = child.GetComponent<T>();
                if (com)
                    return com;
            }
            return null;
        }
    }
}
