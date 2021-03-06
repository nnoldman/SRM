﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    internal class WatcherValue
    {
        internal object instance;
        internal MethodInfo method;
        internal int innerPort;
    }

    internal class InPortValue
    {
        internal FieldInfo field;
        internal InputPortDesc desc;
        internal Port port;
    }

    internal class OutPortValue
    {
        internal FieldInfo field;
        internal OutputPortDesc desc;
        internal Port port;
    }

    internal class Constructure
    {
        internal List<InPortValue> Inputs = new List<InPortValue>();
        internal List<OutPortValue> Outputs = new List<OutPortValue>();
        internal List<WatcherValue> InnerWatchers = new List<WatcherValue>();
    }
}
