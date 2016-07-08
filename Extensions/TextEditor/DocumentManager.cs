
using Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeifenLuo.WinFormsUI.Docking;
namespace TextEditor
{
    public class DocumentManager
    {
        static DocumentManager mInstance = new DocumentManager();
        internal static DocumentManager Instance
        {
            get
            {
                if(mInstance==null)
                    mInstance = new DocumentManager();
                return mInstance;
            }
        }

        List<string> mDocuments = new List<string>();

        public List<string> Documents
        {
            get { return mDocuments; }
        }
        List<string> Histroy
        {
            get
            {
                return Center.Option.File.Histroy;
            }
        }

        int HistroyCount
        {
            get
            {
                return Center.Option.File.MaxHistroyCount;
            }
        }

        //[Watcher((int)ID.OpenDocument)]
        //public void CreateDocument()
        //{
        //    if (!mDocuments.Contains(Center.CurrentOpenDoucment.value))
        //        mDocuments.Add(Center.CurrentOpenDoucment.value);
        //}

        //[Watcher((int)ID.ChangeDocumentName)]
        //public void OnNameChanged(string oldname, string newname)
        //{
        //    mDocuments.Remove(oldname);

        //    if (!mDocuments.Contains(newname))
        //        mDocuments.Add(newname);
        //    else
        //        throw new Exception();

        //    if (Center.ActiveDocument.value == oldname)
        //        Center.ActiveDocument.value = newname;
        //}

        public void AddHistroy(string doc)
        {
            mDocuments.Add(doc);
        }

        //[Watcher((int)ID.CloseDocument)]
        //static void CloseDocument()
        //{
        //    mDocuments.Remove(Center.CurrentCloseDoucment.value);

        //    if (mDocuments.Count == 0)
        //        Center.ActiveDocument.value = string.Empty;
        //}

        //public void OnClose()
        //{
        //    foreach (var doc in mDocuments)
        //        Center.CurrentCloseDoucment.value = doc;
        //}
    }
}
