using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    [AttributeUsage(AttributeTargets.Method)]

    public class AddMenu : Attribute
    {
        public string Text;

        public AddMenu(string str)
        {
            this.Text = str;
        }
    }
}
