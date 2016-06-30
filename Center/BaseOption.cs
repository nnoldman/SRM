using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class BaseOption : Component
    {
        public List<int> sums;
        public string FileName = "Option.json";
        public string ExtensionsPath = "Extensions";
        public string LayoutFile = "Layout.xml";
    }
}
