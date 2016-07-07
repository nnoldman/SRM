using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Core
{
    public partial class Headerquater : Extension
    {
        static Headerquater Instance;

        public Headerquater()
        {
            Instance = this;
            InitializeComponent();
        }

        [AddMenu("Headerquater(&H)")]
        static void OnOpenView()
        {
            if (Instance == null)
            {
                CreateInstance();
            }
            else
            {
                Instance.Hide();
                Instance.Dispose();
                Instance = null;
            }
        }

        static void CreateInstance()
        {
            Instance = new Headerquater();
            Instance.TabText = "Headerquater";
            Instance.Show(Center.Form.DockerContainer, DockState.Float);
        }

        protected override void OnFormClosed(System.Windows.Forms.FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Instance = null;
        }
    }
}
