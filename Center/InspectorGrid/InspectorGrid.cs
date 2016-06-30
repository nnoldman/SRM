using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Core
{
    public partial class InspectorGrid : UserControl
    {
        List<InspectorSection> mChildren = new List<InspectorSection>();

        public InspectorGrid()
        {
            InitializeComponent();
        }

        Object mSelectedObject;

        public Core.Object SelectedObject
        {
            get { return mSelectedObject; }
            
            set
            {
                if (this.mSelectedObject != value)
                {
                    this.mSelectedObject = value;

                    ForceUpdate();
                }
            }
        }

        void ForceUpdate()
        {
            if(!mSelectedObject)
                return;

            this.Controls.Clear();
            this.mChildren.Clear();

            foreach (var com in mSelectedObject.Components)
            {
                InspectorSection section = new InspectorSection();
                this.Controls.Add(section);
                section.Title = com.GetType().Name;
                section.Dock = DockStyle.Top;
                section.Target = com;
                section.Width = this.Width;
                mChildren.Add(section);
                section.Show();
            }
        }
    }
}
