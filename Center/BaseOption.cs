using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    [AddOption]
    public class BaseOption : Component
    {
        public const string ExtensionsPath = "Extensions";
        public string FileName = "Option.json";
        public string LayoutFile = "Layout.xml";
    }
}
