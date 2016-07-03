using Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor
{
    public class AColor
    {
        public byte A;
        public byte R;
        public byte G;
        public byte B;

        public static AColor Black = new AColor() { A = 255, R = 0, G = 0, B = 0 };

        public static implicit operator Color(AColor c)
        {
            return Color.FromArgb(c.A, c.R, c.G, c.B);
        }
    }
    public class GrammarStyle
    {
        public AColor ForeColor = AColor.Black;
        public AColor BackColor = new AColor() { A = 255, R = 220, G = 230, B = 255 };
        public string Font = "courier new";
        public int Weight = 14;
        public int Size = 14;
    }

    [AddOption("GrammarHightLight")]
    public class DefaultOption : Component
    {
        public GrammarStyle Default = new GrammarStyle();
    }

    [AddOption("GrammarHightLight")]
    public class CppOption : Component
    {
        public GrammarStyle Character = new GrammarStyle();
        public GrammarStyle Comment = new GrammarStyle();
        public GrammarStyle CommentDoc = new GrammarStyle();
        public GrammarStyle CommentDocKeyword = new GrammarStyle();
        public GrammarStyle CommentDocKeywordError = new GrammarStyle();
        public GrammarStyle CommentLine = new GrammarStyle();
        public GrammarStyle CommentLineDoc = new GrammarStyle();
        public GrammarStyle Default = new GrammarStyle();
        public GrammarStyle EscapeSequence = new GrammarStyle();
        public GrammarStyle GlobalClass = new GrammarStyle();
        public GrammarStyle HashQuotedString = new GrammarStyle();
        public GrammarStyle Identifier = new GrammarStyle();
        public GrammarStyle Number = new GrammarStyle();
        public GrammarStyle Preprocessor = new GrammarStyle();
        public GrammarStyle PreprocessorComment = new GrammarStyle();
        public GrammarStyle PreprocessorCommentDoc = new GrammarStyle();
        public GrammarStyle Regex = new GrammarStyle();
        public GrammarStyle String = new GrammarStyle();
        public GrammarStyle StringEol = new GrammarStyle();
        public GrammarStyle StringRaw = new GrammarStyle();
        public GrammarStyle TaskMarker = new GrammarStyle();
        public GrammarStyle TripleVerbatim = new GrammarStyle();
        public GrammarStyle UserLiteral = new GrammarStyle();
        public GrammarStyle Uuid = new GrammarStyle();
        public GrammarStyle Verbatim = new GrammarStyle();
        public GrammarStyle Word = new GrammarStyle();
        public GrammarStyle Word2 = new GrammarStyle();
    }

    [AddOption("GrammarHightLight")]
    public class CSharpOption : Component
    {
        public GrammarStyle Default = new GrammarStyle();
        public GrammarStyle Comment = new GrammarStyle();
        public GrammarStyle CommentLine = new GrammarStyle();
        public GrammarStyle Number = new GrammarStyle();
        public GrammarStyle Word = new GrammarStyle();
        public GrammarStyle Word2 = new GrammarStyle();
        public GrammarStyle String = new GrammarStyle();
        public GrammarStyle Character = new GrammarStyle();
        public GrammarStyle StringEol = new GrammarStyle();
        public GrammarStyle Operator = new GrammarStyle();
        public GrammarStyle Preprocessor = new GrammarStyle();
        public GrammarStyle UserLiteral = new GrammarStyle();
        public GrammarStyle Identifier = new GrammarStyle();
    }
    [AddOption("GrammarHightLight")]
    public class COption : Component
    {
        public GrammarStyle Default = new GrammarStyle();
        public GrammarStyle Comment = new GrammarStyle();
        public GrammarStyle CommentLine = new GrammarStyle();
        public GrammarStyle Number = new GrammarStyle();
        public GrammarStyle Word = new GrammarStyle();
        public GrammarStyle Word2 = new GrammarStyle();
        public GrammarStyle String = new GrammarStyle();
        public GrammarStyle Character = new GrammarStyle();
        public GrammarStyle StringEol = new GrammarStyle();
        public GrammarStyle Operator = new GrammarStyle();
        public GrammarStyle Preprocessor = new GrammarStyle();
        public GrammarStyle UserLiteral = new GrammarStyle();
        public GrammarStyle Identifier = new GrammarStyle();
    }

    [AddOption("GrammarHightLight")]
    public class XMLOption : Component
    {
        public GrammarStyle Default = new GrammarStyle();
        public GrammarStyle Comment = new GrammarStyle();
        public GrammarStyle CommentLine = new GrammarStyle();
        public GrammarStyle Number = new GrammarStyle();
        public GrammarStyle Word = new GrammarStyle();
        public GrammarStyle Word2 = new GrammarStyle();
        public GrammarStyle String = new GrammarStyle();
        public GrammarStyle Character = new GrammarStyle();
        public GrammarStyle StringEol = new GrammarStyle();
        public GrammarStyle Operator = new GrammarStyle();
        public GrammarStyle Preprocessor = new GrammarStyle();
        public GrammarStyle UserLiteral = new GrammarStyle();
        public GrammarStyle Identifier = new GrammarStyle();
    }

    [AddOption("LanguageExtension")]
    public class LanguageExtensionOption : Component
    {
        public string Cpp = ".h;.cpp;.cc";
        public string C = ".c";
        public string Lua = ".lua;.bytes";
        public string Xml = ".xml";
        public string Csharp = ".cs";
        public string Python = ".py";
    }
}
