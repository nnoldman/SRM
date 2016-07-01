using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

    public delegate Image LoadBuildImage();

    [AttributeUsage(AttributeTargets.Method)]
    public class AddMenuButton : Attribute
    {
        public string Text;
        public Type LoaderType;

        public AddMenuButton(string str, Type loaderType = null)
        {
            this.Text = str;
            this.LoaderType = loaderType;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class AddShortCut : Attribute
    {
        public ShortCutIndex Index;
        public Shortcut.Modifiers DefaultModifiers;
        public Keys DefaultKey;
        public string Text;

        public AddShortCut(ShortCutIndex index, Shortcut.Modifiers defaultModifiers, Keys defaultKey, string text = "")
        {
            this.Index = index;
            this.DefaultModifiers = defaultModifiers;
            this.DefaultKey = defaultKey;
            this.Text = text;
        }
    }

    public class Component : BoolObject
    {

    }
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AddOption : Attribute
    {
        public string Cate;

        public AddOption(string cate)
        {
            this.Cate = cate;
        }
    }
}
