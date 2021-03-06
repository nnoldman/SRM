﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public enum LaunchMode
    {
        Lanunch,
        Attach,
    }

    public enum BuildMode
    {
        Debug,
        Release,
    }

 

    public class LaunchParams
    {
        public string WorkPath;
        public LaunchMode Mode;
        public string Args;
    }

    [AddOption("Base")]
    public class BuildOption : Component
    {
        public string CurrentBuilderName;
        public BuildMode CurrentMode;
        public List<Builder> Builders = new List<Builder>();

        public Builder CurrentBuilder
        {
            get
            {
                return Builders.Find((b) => b.Name == CurrentBuilderName);
            }
        }
    }
}
