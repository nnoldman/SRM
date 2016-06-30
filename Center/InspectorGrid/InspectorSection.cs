using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace Core
{
    public partial class InspectorSection : UserControl
    {
        int mOrignalHeight = 0;

        public Component Target
        {
            get { return mTarget; }
            set
            {
                mTarget = value;
                UpdatePropertices();
            }
        }

        Component mTarget;
        bool mExpend = true;

        public bool Expend
        {
            get { return mExpend; }
            set { mExpend = value; }
        }

        public string Title {
            get { return this.TitleButton.Text; }
            set { this.TitleButton.Text = value; }
        }

        List<InspectorItem> mChildren = new List<InspectorItem>();

        public InspectorSection()
        {
            InitializeComponent();
        }

        void UpdatePropertices()
        {
            mChildren.Clear();
            this.Controls.Clear();
            this.Controls.Add(this.TitleButton);

            FieldInfo[] fields = mTarget.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);

            int h = 0;

            foreach (var field in fields)
            {
                InspectorItem item = InspectorItem.Create(field.FieldType);
                if (item == null)
                    continue;
                this.Controls.Add(item);
                mChildren.Add(item);
                item.Location = new Point(0, h + this.TitleButton.Height);
                item.Target = this.Target;
                item.Width = this.Width;
                item.Dock = DockStyle.Bottom;
                item.Field = field;
                item.OnInit();
                h += item.Height;
                item.Show();
            }

            mOrignalHeight = this.TitleButton.Height + h;
            this.Size = new Size(this.Width, mOrignalHeight);
        }

        private void TitleButton_Click_1(object sender, EventArgs e)
        {
            Expend = !Expend;

            if (Expend)
            {
                mChildren.ForEach((item) => item.Visible = true);
                this.Size = new Size(this.Width, mOrignalHeight);
            }
            else
            {
                mChildren.ForEach((item) => item.Visible = false);
                this.Size = this.TitleButton.Size;
            }
        }
    }
}
