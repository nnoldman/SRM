using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    [AddOption("Base")]
    public class BaseOption : Component
    {
        public const string OptionsFloder = "Options";
        public const string ExtensionsPath = "Extensions";
        public string LayoutFile = "Layout.xml";
    }
}
