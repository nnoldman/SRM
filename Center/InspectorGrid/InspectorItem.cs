using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace Core
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InspectorType : Attribute
    {
        public Type TargetType;
        public InspectorType(Type type)
        {
            this.TargetType = type;
        }
    }
    public class InspectorItem : UserControl
    {
        public object Target;
        public FieldInfo Field;

        public virtual void OnInit()
        {

        }

        public static InspectorItem Create(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                type = typeof(List<>);
            }
            else
            {
                if (type.BaseType == typeof(Enum))
                    type = typeof(Enum);
            }

            Type inspectorItemType;

            if (InspectorItemCreators.TryGetValue(type, out inspectorItemType))
            {
                InspectorItem item = (InspectorItem)inspectorItemType.GetConstructor(Type.EmptyTypes).Invoke(null);
                return item;
            }
            return null;
        }

        static Dictionary<Type, Type> InspectorItemCreators
        {
            get
            {
                if (mInspectorItemCreators == null)
                {
                    mInspectorItemCreators = new Dictionary<Type, Type>();

                    Assembly asm = Assembly.GetCallingAssembly();
                    
                    foreach (var type in asm.DefinedTypes)
                    {
                        Attribute attr = type.GetCustomAttribute(typeof(InspectorType));
                        if (attr != null)
                        {
                            InspectorType inspectorType = (InspectorType)attr;
                            mInspectorItemCreators.Add(inspectorType.TargetType, type);
                        }
                    }
                }
                return mInspectorItemCreators;
            }
        }

        static Dictionary<Type, Type> mInspectorItemCreators;
    }
}
