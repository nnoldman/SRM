using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diagrament
{
    public partial class Canvas : UserControl
    {
        List<StringRectNode> mNodes = new List<StringRectNode>();
        List<NodeConnection> mConnections = new List<NodeConnection>();

        public Canvas()
        {
            InitializeComponent();

            var node1 = AddNode("PPP");
            var node2 = AddNode("PPP2");
            node2.Position = new Point(100, 60);
            var con = new NodeConnection();
            con.From = node1;
            con.To = node2;
            mNodes.Add(node1);
            mNodes.Add(node2);
            mConnections.Add(con);
        }

        public StringRectNode AddNode(string content)
        {
            var node = new StringRectNode();
            node.Content = content;
            return node;
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.FromArgb(200, 200, 200));

            foreach (var node in mNodes)
                node.Draw(e.Graphics);

            foreach (var con in mConnections)
                con.Draw(e.Graphics);
        }
    }
}
