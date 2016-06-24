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
            return Option.Main.File.Histroy.value;
        }
    }

    static List<string> mActiveDocuments = new List<string>();

    static int HistroyCount
    {
        get
        {
            return Option.Main.File.MaxHistroyCount;
        }
    }
    static public void CreateDocument(string file)
    {
        if (!mActiveDocuments.Contains(file))
        {
            Option.Main.File.Histroy.Remove(file, true);
            mActiveDocuments.Add(file);
        }
    }

    static public void CloseDocument(string file)
    {
        mActiveDocuments.Remove(file);
        while (Option.Main.File.Histroy.value.Count >= Option.Main.File.MaxHistroyCount)
            Option.Main.File.Histroy.RemoveAt(0);
        Option.Main.File.Histroy.Add(file, true);
    }

    public static void OnClose()
    {
        foreach (var doc in mActiveDocuments)
            CloseDocument(doc);
    }
}
