using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ExtensionVersion:Attribute
    {
        public int MainVersion = 0;
        public int SubVersion = 1;
        public bool CanUninstall = true;

        public string Name;
        public string Authors;
        public string Country;
        public string Brief;
        public string Parent;
        public string Licence;
        public string Contact;
    }
}
