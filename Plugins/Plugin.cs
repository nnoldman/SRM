﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PluginVersion:Attribute
    {
        public string Name;
        public int Version = 1;
    }

    public interface Plugin
    {
        bool OnInit();
        bool OnExit();
    }
}
