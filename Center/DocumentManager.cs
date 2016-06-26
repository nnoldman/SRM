using Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeifenLuo.WinFormsUI.Docking;

public class DocumentManager : ATrigger.TriggerObject
{
    string mActiveDocument;
    List<string> mDocuments = new List<string>();

    List<string> Histroy
    {
        get
        {
            return Center.Option.File.Histroy;
        }
    }

    public string ActiveDocument
    {
        get
        {
            return mActiveDocument;
        }
    }

    int HistroyCount
    {
        get
        {
            return Center.Option.File.MaxHistroyCount;
        }
    }

    [ATrigger.Receiver((int)DataType.OpenDocument)]
    public void CreateDocument()
    {
        if (!mDocuments.Contains(Center.CurrentOpenDoucment.value))
            mDocuments.Add(Center.CurrentOpenDoucment.value);
    }

    [ATrigger.Receiver((int)DataType.ChangeDocumentName)]
    public void OnNameChanged()
    {
        string oldname = Center.OnChangeDocumentName.Arg<string>(0);
        string newname = Center.OnChangeDocumentName.Arg<string>(1);

        mDocuments.Remove(oldname);

        if (!mDocuments.Contains(newname))
            mDocuments.Add(newname);
        else
            throw new Exception();

        if (mActiveDocument == oldname)
            mActiveDocument = newname;
    }

    public void AddHistroy(string doc)
    {
        mDocuments.Add(doc);
    }

    public void CloseDocument()
    {
        mDocuments.Remove(Center.CurrentCloseDoucment.value);

        if (mDocuments.Count == 0)
            mActiveDocument = string.Empty;
    }

    public void OnClose()
    {
        foreach (var doc in mDocuments)
            Center.CurrentCloseDoucment.value = doc;
    }

    public void Active(string name)
    {
        if (mDocuments.Contains(name))
            mActiveDocument = name;
    }
}
