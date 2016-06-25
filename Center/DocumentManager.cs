using Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeifenLuo.WinFormsUI.Docking;

public class DocumentManager
{
    static List<string> Histroy
    {
        get
        {
            return Center.Option.File.Histroy;
        }
    }

    static List<string> mActiveDocuments = new List<string>();

    static int HistroyCount
    {
        get
        {
            return Center.Option.File.MaxHistroyCount;
        }
    }

    [ATrigger.Receiver((int)DataType.OpenDocument)]
    static public void CreateDocument()
    {
        if (!mActiveDocuments.Contains(Center.CurrentOpenDoucment.value))
            mActiveDocuments.Add(Center.CurrentOpenDoucment.value);
    }

    static public void CloseDocument()
    {
        mActiveDocuments.Remove(Center.CurrentCloseDoucment.value);
    }

    public static void OnClose()
    {
        foreach (var doc in mActiveDocuments)
            Center.CurrentCloseDoucment.value = doc;
    }
}
