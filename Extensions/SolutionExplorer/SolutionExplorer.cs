using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeifenLuo.WinFormsUI.Docking;

[Core.ExtensionVersion(Name = "SolutionExplorer")]

public class SolutionExplorer : Extension, ATrigger.ITriggerStatic
{
    static SolutionExplorer Instance;

    protected override void OnFormClosed(System.Windows.Forms.FormClosedEventArgs e)
    {
        base.OnFormClosed(e);
        Instance = null;
    }

    [ATrigger.Receiver((int)DataType.View)]
    static void OnViewChange()
    {
        if ((Type)Center.View.Args[0] == typeof(SolutionExplorer))
        {
            if (Instance == null)
            {
                Instance = new SolutionExplorer();
                Instance.TabText = "SolutionExplorer";
                Instance.Show(Center.Container, DockState.Float);
            }
            else
            {
                Instance.Hide();
                Instance.Dispose();
                Instance = null;
            }
        }
    }
}