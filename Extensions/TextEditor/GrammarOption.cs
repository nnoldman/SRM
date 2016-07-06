using Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor
{
    public class GrammarStyle
    {
        public Color ForeColor = Color.Black;
        public Color BackColor = Color.FromArgb(255, 220, 230, 255);
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
        public Dictionary<string, string> LanguageNameExtensions;
        public LanguageExtensionOption()
        {
            LanguageNameExtensions = new Dictionary<string, string>()
            {
                ["Cpp"] = ".h;.cpp;.cc",
                ["C"] = ".c",
                ["Lua"] = ".lua;.bytes",
                ["Xml"] = ".xml",
                ["Csharp"] = ".cs",
                ["Python"] = ".py",
            };
        }
    }
}
