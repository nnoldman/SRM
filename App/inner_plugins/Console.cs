using Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Plugins.PluginVersion(Name = "Console", Version = 1)]

public class Console : Docker, Plugins.Plugin
{
    public bool OnInit()
    {
        throw new Exception("The method or operation is not implemented.");
    }

    public bool OnExit()
    {
        throw new Exception("The method or operation is not implemented.");
    }
}
