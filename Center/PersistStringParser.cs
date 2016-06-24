using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Core
{
    public class PersistStringParser
    {
        public string this[string key]
        {
            get
            {
                return mContents[key];
            }
        }

        Dictionary<string, string> mContents = new Dictionary<string, string>();

        public bool Parse(string content)
        {
            string[] keyValues = content.Split(';');
            if (keyValues != null)
            {
                foreach (var kv in keyValues)
                {
                    int eqpos = kv.IndexOf('=');
                    if (eqpos == -1)
                        return false;
                    mContents.Add(kv.Substring(0, eqpos), kv.Substring(eqpos + 1));
                }
            }
            return true;
        }
    }
}
