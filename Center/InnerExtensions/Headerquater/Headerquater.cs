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
        internal static Headerquater Instance;

        List<HeaderquaterLinkerIn> mLinkerIns = new List<HeaderquaterLinkerIn>();
        List<HeaderquaterLinkerOut> mLinkerOuts = new List<HeaderquaterLinkerOut>();

        public Headerquater()
        {
            Instance = this;
            InitializeComponent();
            InitList(listBox1, string.Empty);
        }

        public void InitList(ListBox view, string except)
        {
            view.Items.Clear();

            var components = Bus.Components;

            foreach(var com in components)
            {
                if (!string.IsNullOrEmpty(except) && except == com.Key.ManifestModule.Name)
                    continue;
                view.Items.Add(com.Key.ManifestModule.Name);
            }
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

        public string InputAsm
        {
            get
            {
                var com = Bus.Components.Keys.First((item) => item.ManifestModule.Name == this.listBox2.SelectedItem.ToString());
                return com.FullName;
            }
        }

        public string OututASM
        {
            get
            {
                var com = Bus.Components.Keys.First((item) => item.ManifestModule.Name == this.listBox1.SelectedItem.ToString());
                return com.FullName;
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedItem == null)
                return;
            InitList(this.listBox2, this.listBox1.SelectedItem.ToString());
            InitOutPorts();
        }

        void InitOutPorts()
        {
            var com = Bus.Components.Keys.First((item) => item.ManifestModule.Name == this.listBox1.SelectedItem.ToString());

            Constructure constructure;

            if(Bus.Components.TryGetValue(com, out constructure))
            {
                mLinkerOuts.ForEach(i => this.outpanel.Controls.Remove(i));
                mLinkerOuts.Clear();

                foreach(var outputPort in constructure.Outputs)
                {
                    HeaderquaterLinkerOut linker = new HeaderquaterLinkerOut();
                    linker.Title = outputPort.field.Name;
                    this.outpanel.Controls.Add(linker);
                    linker.Dock = DockStyle.Top;

                    mLinkerOuts.Add(linker);
                }
            }
        }
        void InitInputPorts()
        {
            var com = Bus.Components.Keys.First((item) => item.ManifestModule.Name == this.listBox2.SelectedItem.ToString());

            Constructure constructure;

            if (Bus.Components.TryGetValue(com, out constructure))
            {
                mLinkerIns.ForEach(i => this.inpanel.Controls.Remove(i));
                mLinkerIns.Clear();

                foreach (var inputPort in constructure.Inputs)
                {
                    HeaderquaterLinkerIn linker = new HeaderquaterLinkerIn();
                    linker.Title = inputPort.field.Name;
                    this.inpanel.Controls.Add(linker);
                    linker.Dock = DockStyle.Top;
                    mLinkerIns.Add(linker);
                }
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitInputPorts();
        }
        Pen mOutPen = new Pen(Color.Green);
        private void splitContainer1_Paint(object sender, PaintEventArgs e)
        {
            mOutPen.Width = 4;
            e.Graphics.Clear(Color.FromArgb(200, 200, 200, 200));
            Point p0 = new Point(e.ClipRectangle.X, 10);
            Point p1 = new Point(e.ClipRectangle.Right, 10);
            e.Graphics.DrawLine(mOutPen,p0,p1);
        }
    }
}
//protected override void OnPaint(PaintEventArgs e)
//{
//    // If there is an image and it has a location, 
//    // paint it when the Form is repainted.
//    base.OnPaint(e);
//    if (this.picture != null && this.pictureLocation != Point.Empty)
//    {
//        e.Graphics.DrawImage(this.picture, this.pictureLocation);
//    }
//}

//private void Form1_DragDrop(object sender, DragEventArgs e)
//{
//    // Handle FileDrop data.
//    if (e.Data.GetDataPresent(DataFormats.FileDrop))
//    {
//        // Assign the file names to a string array, in 
//        // case the user has selected multiple files.
//        string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
//        try
//        {
//            // Assign the first image to the picture variable.
//            this.picture = Image.FromFile(files[0]);
//            // Set the picture location equal to the drop point.
//            this.pictureLocation = this.PointToClient(new Point(e.X, e.Y));
//        }
//        catch (Exception ex)
//        {
//            MessageBox.Show(ex.Message);
//            return;
//        }
//    }

//    // Handle Bitmap data.
//    if (e.Data.GetDataPresent(DataFormats.Bitmap))
//    {
//        try
//        {
//            // Create an Image and assign it to the picture variable.
//            this.picture = (Image)e.Data.GetData(DataFormats.Bitmap);
//            // Set the picture location equal to the drop point.
//            this.pictureLocation = this.PointToClient(new Point(e.X, e.Y));
//        }
//        catch (Exception ex)
//        {
//            MessageBox.Show(ex.Message);
//            return;
//        }
//    }
//    // Force the form to be redrawn with the image.
//    this.Invalidate();
//}

//private void Form1_DragEnter(object sender, DragEventArgs e)
//{
//    // If the data is a file or a bitmap, display the copy cursor.
//    if (e.Data.GetDataPresent(DataFormats.Bitmap) ||
//       e.Data.GetDataPresent(DataFormats.FileDrop))
//    {
//        e.Effect = DragDropEffects.Copy;
//    }
//    else
//    {
//        e.Effect = DragDropEffects.None;
//    }
//}