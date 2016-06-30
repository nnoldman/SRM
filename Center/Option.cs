using ATrigger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    [CategoryAttribute("FileOption")]
    public class FileOption : Component
    {
        public int MaxHistroyCount = 10;
        public List<string> Histroy = new List<string>();
    }
    [CategoryAttribute("SolutionOption")]
    public class SolutionOption : Component
    {
        public string LastSolutionPath;
        public List<string> IgnoreExtensions = new List<string>();
    }

    public class Option : Object
    {
        public Option()
        {
            this.AddComponent<BaseOption>();
            this.AddComponent<FileOption>();
            this.AddComponent<SolutionOption>();
            this.AddComponent<BuildOption>();
        }

        public BaseOption Base { get { return this.GetComponent<BaseOption>(); } }
        public FileOption File { get { return this.GetComponent<FileOption>(); } }
        public SolutionOption Solution { get { return this.GetComponent<SolutionOption>(); } }
        public BuildOption BuildOption { get { return this.GetComponent<BuildOption>(); } }
    }
}
