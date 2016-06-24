using ATrigger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class GrammarOption
    {
        public List<string> extensions;
        public Dictionary<int, Feature> features;
        public IGrammar grammarInterface;
    }

    public class FileOption:TriggerObject
    {
        public int MaxHistroyCount = 10;

        [DataEntity((int)DataType.FileHistroyChange)]
        public AList<string> Histroy = new AList<string>();
    }

    public class Option : TriggerObject
    {
        static Option mInstance;
        public const string FileName = "Option.json";
        public static Option Main
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new Option();
                }
                return mInstance;
            }
            set
            {
                mInstance = value;
            }
        }

        public string LayoutFile = "Layout.xml";
        public FileOption File = new FileOption();
    }
}
