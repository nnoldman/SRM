using Core;
using ScintillaNET;
using Shortcut;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;


namespace TextEditor
{

    [Core.ExtensionVersion(Name = "TextEditor")]

    public partial class TextEditor : Extension, ATrigger.IStaticEmitterContainer
    {
        public static Color GlobalBackColor = Color.FromArgb(0, 220, 230, 255);
        static List<TextEditor> mInstances = new List<TextEditor>();
        static int CreateCount = 0;

        static string NewDocuemntName
        {
            get
            {
                ++CreateCount;
                return "New" + CreateCount.ToString();
            }
        }

        public new string Text
        {
            get
            {
                return this.scintilla1.Text;
            }
            set
            {
                this.scintilla1.Text = value;
            }
        }

        protected string mFileName;

        public string FileName
        {
            get
            {
                return mFileName;
            }
            set
            {
                if (File.Exists(value))
                {
                    mFileName = value;
                    TabText = Path.GetFileName(value);
                    this.Text = File.ReadAllText(mFileName);
                }
                else
                {
                    TabText = value;
                }
            }
        }

        static ScintillaNET.Lexer GetLexer(string extension)
        {
            var option = Center.Option.GetOption<LanguageExtensionOption>();
            foreach (var item in option.LanguageNameExtensions)
            {
                var extensions = item.Value.Split(';');
                if (Array.FindIndex(extensions, (ext) => ext == extension) != -1)
                    return (ScintillaNET.Lexer)Enum.Parse(typeof(ScintillaNET.Lexer), item.Key);
                else
                    return ScintillaNET.Lexer.Null;
            }
            return ScintillaNET.Lexer.Null;
        }

        [ATrigger.Receiver((int)DataType.ActiveDocument)]
        static void OnActiveDocument()
        {
            foreach (var instance in mInstances)
            {
                string name = string.IsNullOrEmpty(instance.FileName) ? instance.TabText : instance.FileName;
                if (name == Center.ActiveDocument.value)
                {
                    instance.scintilla1.Lexer = GetLexer(Path.GetExtension(Center.ActiveDocument.value));
                    instance.Show(Center.Form.DockerContainer, DockState.Document);
                }
            }
        }

        [ATrigger.Receiver((int)DataType.ExtensionsLoaded)]
        public void OnExtensionLoaded()
        {
            throw new Exception();
        }
        void Cut()
        {
            this.scintilla1.Cut();
        }
        void InitStyles()
        {
            {
                DefaultOption option = Center.Option.GetOption<DefaultOption>();
                SetStyle(Style.Default, option.Default);
                this.scintilla1.StyleClearAll();
            }
            {
                CppOption option = Center.Option.GetComponentFromChildren<CppOption>();

                SetStyle(Style.Cpp.Character, option.Character);
                SetStyle(Style.Cpp.Comment, option.Comment);
                SetStyle(Style.Cpp.CommentDoc, option.CommentDoc);
                SetStyle(Style.Cpp.CommentDocKeyword, option.CommentDocKeyword);
                SetStyle(Style.Cpp.CommentDocKeywordError, option.CommentDocKeywordError);
                SetStyle(Style.Cpp.CommentLine, option.CommentLine);
                SetStyle(Style.Cpp.CommentLineDoc, option.CommentLineDoc);
                SetStyle(Style.Cpp.Default, option.Default);
                SetStyle(Style.Cpp.EscapeSequence, option.EscapeSequence);
                SetStyle(Style.Cpp.GlobalClass, option.GlobalClass);
                SetStyle(Style.Cpp.HashQuotedString, option.HashQuotedString);
                SetStyle(Style.Cpp.Identifier, option.Identifier);
                SetStyle(Style.Cpp.Number, option.Number);
                SetStyle(Style.Cpp.Preprocessor, option.Preprocessor);
                SetStyle(Style.Cpp.PreprocessorComment, option.PreprocessorComment);
                SetStyle(Style.Cpp.PreprocessorCommentDoc, option.PreprocessorCommentDoc);
                SetStyle(Style.Cpp.Regex, option.Regex);
                SetStyle(Style.Cpp.String, option.String);
                SetStyle(Style.Cpp.StringEol, option.StringEol);
                SetStyle(Style.Cpp.StringRaw, option.StringRaw);
                SetStyle(Style.Cpp.TaskMarker, option.TaskMarker);
                SetStyle(Style.Cpp.TripleVerbatim, option.TripleVerbatim);
                SetStyle(Style.Cpp.UserLiteral, option.UserLiteral);
                SetStyle(Style.Cpp.Uuid, option.Uuid);
                SetStyle(Style.Cpp.Verbatim, option.Verbatim);
                SetStyle(Style.Cpp.Word, option.Word);
                SetStyle(Style.Cpp.Word2, option.Word2);
            }


        }
        public TextEditor()
        {
            InitializeComponent();
            ATrigger.DataCenter.AddInstance(this);

            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;

            this.scintilla1.StyleResetDefault();
            this.scintilla1.SetWhitespaceBackColor(true, GlobalBackColor);
            this.scintilla1.SetWhitespaceForeColor(true, GlobalBackColor);

            this.scintilla1.EdgeColor = GlobalBackColor;
            this.scintilla1.EdgeMode = ScintillaNET.EdgeMode.Background;
            this.scintilla1.CaretLineBackColor = GlobalBackColor;
            this.scintilla1.AdditionalCaretForeColor = GlobalBackColor;
            this.scintilla1.AdditionalCaretsVisible = false;

            this.scintilla1.Margins[1].Type = ScintillaNET.MarginType.Number;
            this.scintilla1.Margins[1].Width = 60;

            InitStyles();

            this.scintilla1.BufferedDraw = true;
            this.scintilla1.Lexer = Lexer.Cpp;
            this.scintilla1.CharAdded += CharAdded;
            this.scintilla1.AutoCCharDeleted += OnAutoCCharDeleted;

            this.scintilla1.AutoCIgnoreCase = true;

            //this.scintilla1.SetProperty("fold", "1");
            //this.scintilla1.SetProperty("fold.compact", "1");

            // Configure a margin to display folding symbols
            this.scintilla1.Margins[2].Type = MarginType.Symbol;
            this.scintilla1.Margins[2].Mask = Marker.MaskAll;
            this.scintilla1.Margins[2].Sensitive = true;
            this.scintilla1.Margins[2].Width = 16;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                this.scintilla1.Markers[i].SetForeColor(SystemColors.ControlLightLight);
                this.scintilla1.Markers[i].SetBackColor(SystemColors.ControlDark);
            }

            // Configure folding markers with respective symbols
            this.scintilla1.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
            this.scintilla1.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
            this.scintilla1.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
            this.scintilla1.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            this.scintilla1.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
            this.scintilla1.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            this.scintilla1.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Enable automatic folding
            this.scintilla1.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);

            mInstances.Add(this);
        }

        void SetStyle(int index, GrammarStyle grammar)
        {
            this.scintilla1.Styles[index].ForeColor = grammar.ForeColor;
            this.scintilla1.Styles[index].BackColor = grammar.BackColor;
            this.scintilla1.Styles[index].Font = grammar.Font;
            this.scintilla1.Styles[index].Weight = grammar.Weight;
            this.scintilla1.Styles[index].Size = grammar.Size;
        }

        //[AddMenu("View(&V)/GrammarHighLight")]
        //static void OnOpenView()
        //{
        //    Center.SelectObject.value=
        //}

        [AddMenu("View(&V)/TextEditor")]
        static void OnOpenView()
        {
            if (mInstances.Count == 0)
                Center.CurrentOpenDoucment.value = NewDocuemntName;
        }

        [ATrigger.Receiver((int)DataType.LayoutEnd)]
        static void OnLayoutEnd()
        {
            var histroy = new List<string>();
            foreach (var instance in mInstances)
            {
                string name = string.IsNullOrEmpty(instance.FileName) ? instance.TabText : instance.FileName;
                Center.DocumentManager.AddHistroy(name);
            }

            mFileMenuInitItemCount = FileToolStripMenuItem.DropDownItems.Count;

            LoadHistry(Center.Option.File.Histroy);
        }

        [InputListener(InnerIndex = (int)ID.OpenDocument)]
        static public void CreateDocument()
        {
            TextEditor instance = new TextEditor();
            if (!File.Exists(PortHub.OpenDocument.Value))
                instance.TabText = PortHub.OpenDocument.Value;
            else
                instance.FileName = PortHub.OpenDocument.Value;
            instance.Show(Center.Form.DockerContainer, DockState.Document);

            //TextEditor instance = new TextEditor();
            //if (!File.Exists(Center.CurrentOpenDoucment.value))
            //    instance.TabText = Center.CurrentOpenDoucment.value;
            //else
            //    instance.FileName = Center.CurrentOpenDoucment.value;
            //instance.Show(Center.Form.DockerContainer, DockState.Document);
        }

        //[InputListener(InnerIndex = (int)ID.NameChanged)]
        //void OnNameChangedd()
        //{
        //    string oldname= ProtHub.OnNameChanged.Value["OldName"];
        //    string newname = ProtHub.OnNameChanged.Value["NewName"];

        //    if (oldname == this.TabText || oldname == this.FileName)
        //        this.FileName = newname;
        //}

        [ATrigger.Receiver((int)DataType.ChangeDocumentName)]
        public void OnNameChaned(string oldname, string newname)
        {
            if (oldname == this.TabText || oldname == this.FileName)
                this.FileName = newname;
        }

        static ToolStripMenuItem FileToolStripMenuItem
        {
            get
            {
                foreach (var item in Center.Form.MainMenu.Items)
                {
                    ToolStripMenuItem menu = (ToolStripMenuItem)item;
                    if (menu.Text == "File(&F)")
                    {
                        mFileToolStripMenuItem = menu;
                        break;
                    }
                }
                return mFileToolStripMenuItem;
            }
        }
        static int mFileMenuInitItemCount = 0;
        static ToolStripMenuItem mFileToolStripMenuItem;

        static void LoadHistry(List<string> histroy)
        {
            while (FileToolStripMenuItem.DropDownItems.Count > mFileMenuInitItemCount)
                FileToolStripMenuItem.DropDownItems.RemoveAt(FileToolStripMenuItem.DropDownItems.Count - 1);

            if (histroy.Count == 0)
                return;

            FileToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
            FileToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem("Histroy"));

            for (int i = 0; i < histroy.Count; ++i)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = (i + 1).ToString() + " " + histroy[i];
                item.Click += OpenHistroyFile;
                FileToolStripMenuItem.DropDownItems.Add(item);
            }
        }

        [ATrigger.Receiver((int)DataType.OpenDocument)]
        static void OnDocumentOpen()
        {
            if (Center.Option.File.Histroy.Remove(Center.CurrentOpenDoucment.value))
                LoadHistry(Center.Option.File.Histroy);
        }

        [ATrigger.Receiver((int)DataType.CloseDocument)]
        static void OnDocumentClose()
        {
            while (Center.Option.File.Histroy.Count >= Center.Option.File.MaxHistroyCount)
                Center.Option.File.Histroy.RemoveAt(Center.Option.File.Histroy.Count - 1);
            Center.Option.File.Histroy.Insert(0, Center.CurrentCloseDoucment.value);
            LoadHistry(Center.Option.File.Histroy);
        }
        static void OpenHistroyFile(object sender, EventArgs e)
        {
            int pos = sender.ToString().IndexOf(' ');
            if (pos != -1)
                Center.CurrentOpenDoucment.value = sender.ToString().Substring(pos + 1);
            else
                throw new Exception();
        }
        static string GetTextFromInstances(string name)
        {
            foreach (var inst in mInstances)
            {
                if (inst.TabText == name || inst.FileName == name)
                {
                    return inst.Text;
                }
            }
            return string.Empty;
        }


        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
                Center.CurrentOpenDoucment.value = file;
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }

        [AddShortCut(ShortCutIndex.SaveDocument, Modifiers.Control, Keys.S, "SaveFile")]
        static void SaveFile()
        {
            if (!string.IsNullOrEmpty(Center.ActiveDocument.value))
            {
                string text = GetTextFromInstances(Center.ActiveDocument.value);

                if (File.Exists(Center.ActiveDocument.value))
                {
                    File.WriteAllText(Center.ActiveDocument.value, text);
                }
                else
                {
                    SaveFileDialog dlg = new SaveFileDialog();
                    var res = dlg.ShowDialog();

                    if (res == DialogResult.OK)
                    {
                        File.WriteAllText(dlg.FileName, text);

                        Center.OnChangeDocumentName.Trigger(Center.ActiveDocument.value, dlg.FileName);
                    }
                }
            }
        }


        SortedSet<string> GetCacheWords(int startPos, int len)
        {
            char ch = '\0';
            SortedSet<string> words = new SortedSet<string>();
            string text = Text;
            int allLen = text.Length;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < allLen; ++i)
            {
                ch = text[i];

                if (i == startPos)
                {
                    goto MakeWord;
                }
                else if (i > startPos && i <= startPos + len)
                {
                    continue;
                }
                else if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9') || ch == '_')
                {
                    sb.Append(ch);
                    continue;
                }
            MakeWord:
                if (sb.Length > 0)
                {
                    string word = sb.ToString();
                    if (!words.Contains(word))
                        words.Add(word);
                    sb.Clear();
                }
            }
            return words;
        }
        bool Match(string word, string target)
        {
            if (target.Length > word.Length)
                return false;

            int len = Math.Min(word.Length, target.Length);

            char ch1, ch2;

            for (int i = 0; i < len; ++i)
            {
                ch1 = word[i];
                ch2 = target[i];
                if (ch1 == ch2 || ch1 + 32 == ch2 || ch1 == ch2 + 32)
                    continue;
                else
                    return false;
            }
            return true;
        }
        string GetAutoList(string content, int startPos, int len)
        {
            var Words = GetCacheWords(startPos, len);
            StringBuilder sb = new StringBuilder();
            foreach (var word in Words)
            {
                if (Match(word, content))
                {
                    sb.Append(word);
                    sb.Append(' ');
                }
            }
            if (sb.Length > 0)
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
        void TryAutoComplete()
        {
            // Find the word start
            var currentPos = this.scintilla1.CurrentPosition;
            var wordStartPos = this.scintilla1.WordStartPosition(currentPos, true);
            // Display the autocompletion list
            var lenEntered = currentPos - wordStartPos;
            string content = Text.Substring(wordStartPos, lenEntered);
            if (lenEntered > 0)
            {
                //this.scintilla1.AutoCShow(lenEntered, "abstract as base break case catch checked continue default delegate do else event explicit extern false finally fixed for foreach goto if implicit in interface internal is lock namespace new null object operator out override params private protected public readonly ref return sealed sizeof stackalloc switch this throw true try typeof unchecked unsafe using virtual while");
                this.scintilla1.AutoCShow(lenEntered, GetAutoList(content, wordStartPos, lenEntered));
            }
        }
        private void OnAutoCCharDeleted(object sender, EventArgs e)
        {
            TryAutoComplete();
        }
        private void CharAdded(object sender, CharAddedEventArgs e)
        {
            char pair = '\0';

            switch (e.Char)
            {
                case '{': pair = '}'; break;
                case '(': pair = ')'; break;
                case '[': pair = ']'; break;
            }

            if (pair != '\0')
            {
                this.scintilla1.InsertText(this.scintilla1.CurrentPosition, new string(pair, 1));
                return;
            }
            TryAutoComplete();
        }

        public override void OnDBClickNC()
        {
            Center.CurrentOpenDoucment.value = NewDocuemntName;
        }

        static void CloseAll()
        {
            foreach (var instance in mInstances)
            {
                Center.CurrentCloseDoucment.value = instance.FileName;
                instance.Hide();
                instance.Dispose();
            }
            mInstances.Clear();
        }

        private void TextEditor_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            ATrigger.DataCenter.RemoveInstance(this);

            this.scintilla1.ClearRegisteredImages();
            mInstances.Remove(this);
            if (!string.IsNullOrEmpty(this.FileName))
                Center.CurrentCloseDoucment.value = this.FileName;
        }

        private void closeAllToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CloseAll();
        }
        protected override string GetPersistString()
        {
            return string.Format("Type={0};TabText={1}", GetType().Name, FileName);
        }
        public override bool LoadFromPersistString(Core.PersistStringParser parser)
        {
            FileName = parser["TabText"];
            return !string.IsNullOrEmpty(FileName);
        }

        private void scintilla1_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
                e.Effect = DragDropEffects.All;
        }

        private void scintilla1_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
                Center.CurrentOpenDoucment.value = file;
        }

        private void TextEditor_VisibleChanged(object sender, EventArgs e)
        {
            TextEditor editor = (TextEditor)sender;
            if (editor.Visible)
                Center.ActiveDocument.value = string.IsNullOrEmpty(editor.FileName) ? editor.TabText : editor.FileName;
        }

        private void openInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Center.ActiveDocument.value))
                Shell.OpenFloder(Center.ActiveDocument.value);
        }
    }
}
