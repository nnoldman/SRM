﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BoolObject
{
    public static implicit operator bool(BoolObject obj)
    {
        return obj != null;
    }
}
