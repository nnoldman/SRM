using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Symbol
    {
        public int type;
        public int start;
        public int end;
    }

    public class Feature
    {
        public int color;
        public bool blod;
        public string font;
    }

    public interface IGrammar
    {
        List<Symbol> Parse(string content);
    }


}
