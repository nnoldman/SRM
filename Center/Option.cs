using ATrigger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class FileOption : Component
    {
        public int MaxHistroyCount = 10;
        public List<string> Histroy = new List<string>();
    }
    public class SolutionOption : Component
    {
        public string LastSolutionPath;
        public List<string> IgnoreExtensions = new List<string>();
    }



    public class Option : TriggerObject
    {
        public const string FileName = "Option.json";
        public string ExtensionsPath = "Extensions";
        public string LayoutFile = "Layout.xml";

        public FileOption File = new FileOption();
        public SolutionOption Solution = new SolutionOption();
        public BuildOption BuildOption = new BuildOption();

        public List<Component> ExtensionOptions = new List<Component>();
    }
}
