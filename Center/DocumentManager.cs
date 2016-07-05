using ATrigger;
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

    [ATrigger.Receiver((int)DataType.OpenDocument)]
    public void CreateDocument()
    {
        if (!mDocuments.Contains(Center.CurrentOpenDoucment.value))
            mDocuments.Add(Center.CurrentOpenDoucment.value);
    }

    [ATrigger.Receiver((int)DataType.ChangeDocumentName)]
    public void OnNameChanged(string oldname, string newname)
    {
        mDocuments.Remove(oldname);

        if (!mDocuments.Contains(newname))
            mDocuments.Add(newname);
        else
            throw new Exception();

        if (Center.ActiveDocument.value == oldname)
            Center.ActiveDocument.value = newname;
    }

    public void AddHistroy(string doc)
    {
        mDocuments.Add(doc);
    }

    [ATrigger.Receiver((int)DataType.CloseDocument)]
    public void CloseDocument()
    {
        mDocuments.Remove(Center.CurrentCloseDoucment.value);

        if (mDocuments.Count == 0)
            Center.ActiveDocument.value = string.Empty;
    }

    public void OnClose()
    {
        foreach (var doc in mDocuments)
            Center.CurrentCloseDoucment.value = doc;
    }
}
