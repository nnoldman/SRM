using ATrigger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    [AddOption("Base")]
    public class FileOption : Component
    {
        public int MaxHistroyCount = 10;
        public List<string> Histroy = new List<string>();
    }
    [AddOption("Base")]
    public class SolutionOption : Component
    {
        public string LastSolutionPath;
        public List<string> IgnoreExtensions = new List<string>();
    }

    public class Option : Object
    {
        public BaseOption Base { get { return this.GetComponentFromChildren<BaseOption>(); } }
        public FileOption File { get { return this.GetComponentFromChildren<FileOption>(); } }
        public SolutionOption Solution { get { return this.GetComponentFromChildren<SolutionOption>(); } }
        public BuildOption BuildOption { get { return this.GetComponentFromChildren<BuildOption>(); } }

        public T GetOption<T>() where T : Component, new()
        {
            T com = GetComponentFromChildren<T>();
            
            if (!com)
            {
                var tp = typeof(T).GetType();
                var attrs = tp.GetCustomAttributes(typeof(AddOption), true);
                AddOption attr = (AddOption)attrs[0];
                
                Core.Object child = FindChildByName(attr.Cate);
                
                if (!child)
                {
                    child = new Object();
                    child.Name = attr.Cate;
                }
                com = child.AddComponent<T>();
            }
            return com;
        }
    }
}
