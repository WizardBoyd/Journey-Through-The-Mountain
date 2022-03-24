using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialougeEditor
{
    [ToolboxBitmap(typeof(DialougeEditor), "nodeed")]
    public partial class DialougeEditor : UserControl
    {
        internal class NodeToken
        {
            public MethodInfo method;
            public NodeAttribute nodeAttribute;
        }

        private NodesGraph graph = new NodesGraph();
        private bool needRepaint = true;
        private Timer timer = new Timer();
        private bool mdown;
        private Point lastmpos;
        private SocketVisual dragsocket;
        private NodeVisual dragSocketNode;
        private PointF dragConnetctionBegin;
        private PointF dragConnetctionEnd;
        private Stack<NodeVisual> ExecutionStack = new Stack<NodeVisual>();
        private bool rebuildConnectionDictionary = true;
        private Dictionary<string, NodeConnection> connectionDictionary = new Dictionary<string, NodeConnection>();

        public INodesContext Context
        {
            get { return context; }
            set
            {
                if (context != null)
                {
                    context.FeedbackInfo -= contexton
                }
                context = value;
                if (context != null)
                {
                    context.FeedbackInfo += co
                }
            }
        }

        public event Action<object> OnNodeContextSelected = delegate { };

        public event Action<string> OnNodeHint = delegate { };

        public event Action<RectangleF> onShowLocation = delegate { };

        private readonly Dictionary<ToolStripMenuItem, int> allContextItems = new Dictionary<ToolStripMenuItem, int>();

        private Point lastMouseLocation;

        private Point autoScroll;

        private PointF selectionStart;

        private PointF selectionEnd;

        private INodesContext context;

        private bool breakExecution = false;

        public DialougeEditor()
        {
            InitializeComponent();
            timer.Interval = 30;
            timer.Tick += TimerOnTick;
            timer.Start();
            KeyDown += OnKeyDown;
            SetStyle(ControlStyles.Selectable, true);
        }

        private void ContextOnFeedbackInfo(string message, NodeVisual nodeVisual, FeedBackType type, object tag, bool breakExecution)
        {
            this.breakExecution = breakExecution;
            if (breakExecution)
            {
                nodeVisual.Feedback = type;
                OnNodeHint(message);
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 7)
            {
                return;
            }

            base.WndProc(ref m);
        }

        private void OnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.KeyCode == Keys.Delete)
            {
                //delete selected nodes
            }
        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            if (DesignMode) return;
            if (needRepaint)
            {
                Invalidate();
            }

        }

        private void NodesControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;

            graph.Draw(e.Graphics, PointToClient(MousePosition), MouseButtons);

            if (dragsocket != null)
            {
                var pen = new Pen(Color.Black, 2);
                NodesGraph.DrawConnection(e.Graphics, pen, dragConnetctionBegin, dragConnetctionEnd);
            }

            if (selectionStart != PointF.Empty)
            {
                var rect = Rectangle.Round(MakeRect(selectionStart, selectionEnd));
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(50, Color.CornflowerBlue)), rect);
                e.Graphics.DrawRectangle(new Pen(Color.DodgerBlue), rect);
            }

            needRepaint = false;
        }

        private static RectangleF MakeRect(PointF a, PointF b)
        {
            var x1 = a.X;
            var x2 = b.X;
            var y1 = a.Y;
            var y2 = b.Y;
            return new RectangleF(Math.Min(x1, x2), Math.Min(y1, y2), Math.Abs(x2 - x1), Math.Abs(y2 - y1));
        }

        private void NodesControl_MouseMove(object sender, MouseEventArgs e)
        {
            var em = PointToScreen(e.Location);
            if (selectionStart != PointF.Empty)
            {
                selectionEnd = e.Location;
            }
            if (mdown)
            {
                foreach (var node in graph.Nodes.Where(x => x.IsSlected))
                {
                    node.X += em.X - lastmpos.X;
                    node.Y += em.Y - lastmpos.Y;
                    node.DiscardCache();
                    node.LayoutEditor();
                }
                if (graph.Nodes.Exists(x=>x.IsSlected))
                {
                    var n = graph.Nodes.FirstOrDefault(x => x.IsSlected);
                    var bound = new RectangleF(new PointF(n.X, n.Y), n.GetNodeBounds());
                    foreach (var node in graph.Nodes.Where(x=>x.IsSlected))
                    {
                        bound = RectangleF.Union(bound, new RectangleF(new PointF(node.X, node.Y), node.GetNodeBounds()));

                    }
                    onShowLocation(bound);
                }
                Invalidate();

                if (dragsocket != null)
                {
                    var center = new PointF(dragsocket.X + dragsocket.Width / 2f, dragsocket.Y + dragsocket.Height / 2f);
                    if (dragsocket.Input)
                    {
                        dragConnetctionBegin.X += em.X - lastmpos.X;
                        dragConnetctionBegin.Y += em.Y - lastmpos.Y;
                        dragConnetctionEnd = center;
                        onShowLocation(new RectangleF(dragConnetctionBegin, new SizeF(10, 10)));
                    }
                    else
                    {
                        dragConnetctionBegin = center;
                        dragConnetctionEnd.X += em.X - lastmpos.X;
                        dragConnetctionEnd.Y += em.Y - lastmpos.Y;
                        onShowLocation(new RectangleF(dragConnetctionEnd, new SizeF(10, 10)));
                    }
                }
                lastmpos = em;
            }
            needRepaint = true;
        }

        private void NodesControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                selectionStart = PointF.Empty;

                Focus();

                if ((ModifierKeys & Keys.Shift) != Keys.Shift)
                {
                    graph.Nodes.ForEach(x => x.IsSlected = false);
                }

                var node =
                    graph.Nodes.OrderBy(x => x.Order).FirstOrDefault(
                        x => new RectangleF(new PointF(x.X, x.Y), x.GetHeaderSize()).Contains(e.Location));

                if (node != null && !mdown)
                {
                    node.IsSlected = true;

                    node.Order = graph.Nodes.Min(x => x.Order) - 1;
                    if (node.CustomEditor != null)
                    {
                        node.CustomEditor.BringToFront();
                    }
                    mdown = true;
                    lastmpos = PointToScreen(e.Location);

                    Refresh();
                }

                if (node == null && !mdown)
                {
                    var nodeWhole =
                        graph.Nodes.OrderBy(x => x.Order).FirstOrDefault(
                            x => new RectangleF(new PointF(x.X, x.Y), x.GetNodeBounds()).Contains(e.Location));
                    if (nodeWhole != null)
                    {
                        node = nodeWhole;
                        var socket = nodeWhole.GetSockets().FirstOrDefault(x => x.GetBounds().Contains(e.Location));
                        if (socket != null)
                        {
                            if ((ModifierKeys & Keys.Control) == Keys.Control)
                            {
                                var connection =
                                    graph.Connections.FirstOrDefault(
                                        x => x.InputNode == nodeWhole && x.InputSocketName == socket.Name);

                                if (connection != null)
                                {
                                    dragsocket =
                                        connection.OutputNode.GetSockets()
                                        .FirstOrDefault(x => x.Name == connection.OutputSocketName);
                                    dragSocketNode = connection.OutputNode;
                                }
                                else
                                {
                                    connection =
                                        graph.Connections.FirstOrDefault(
                                            x => x.OutputNode == nodeWhole && x.OutputSocketName == socket.Name);
                                    if (connection != null)
                                    {
                                        dragsocket =
                                            connection.InputNode.GetSockets().FirstOrDefault(x => x.Name == connection.InputSocketName);
                                        dragSocketNode = connection.InputNode;
                                    }

                                }

                                graph.Connections.Remove(connection);
                                rebuildConnectionDictionary = true;
                            }
                            else
                            {
                                dragsocket = socket;
                                dragSocketNode = nodeWhole;
                            }
                            dragConnetctionBegin = e.Location;
                            dragConnetctionEnd = e.Location;
                            mdown = true;
                            lastmpos = PointToScreen(e.Location);
                        }
                    }
                    else
                    {
                        selectionStart = selectionEnd = e.Location;
                    }
                }
                if (node != null)
                {
                    OnNodeContextSelected(node.getNodeContext());
                }
            }
            needRepaint = true;
        }

        private bool IsConnectable(SocketVisual a, SocketVisual b)
        {
            var input = a.Input ? a : b;
            var output = a.Input ? b : a;
            var otype = Type.GetType(output.Type.FullName.Replace("&", ""), AssemblyResolver, TypeResolver);
            var itype = Type.GetType(input.Type.FullName.Replace("&", ""), AssemblyResolver, TypeResolver);
            if (otype == null || itype == null)
            {
                return false;
            }
            var allow = otype == itype || otype.IsSubclassOf(itype);
            return allow;
        }

        private Type TypeResolver(Assembly assembly, string name, bool inh)
        {
            if (assembly == null)
            {
                assembly = ResolveAssembly(name);
            }
            if (assembly == null) return null;
            return assembly.GetType(name);
        }

        private Assembly ResolveAssembly(string fullTypeName)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(x => x.GetTypes().Any(o => o.FullName == fullTypeName));
        }

        private Assembly AssemblyResolver(AssemblyName assemblyName)
        {
            return AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.FullName == assemblyName.FullName);
        }

        private void Nodes_control_MouseUp(object sender, MouseEventArgs e)
        {
            if (selectionStart != PointF.Empty)
            {
                var rect = MakeRect(selectionStart, selectionEnd);
                graph.Nodes.ForEach(x =>
                x.IsSlected = rect.Contains(new RectangleF(new PointF(x.X, x.Y), x.GetNodeBounds())));
                selectionStart = PointF.Empty;
            }

            if (dragsocket != null)
            {
                var nodeWhole = graph.Nodes.OrderBy(x => x.Order).FirstOrDefault(
                    x => new RectangleF(new PointF(x.X, x.Y), x.GetNodeBounds()).Contains(e.Location));
                if (nodeWhole != null)
                {
                    var socket = nodeWhole.GetSockets().FirstOrDefault(x => x.GetBounds().Contains(e.Location));
                    if (socket != null)
                    {
                        if (IsConnectable(dragsocket,socket) && dragsocket.Input != socket.Input)
                        {
                            var nc = new NodeConnection();
                            if (!dragsocket.Input)
                            {
                                nc.OutputNode = dragSocketNode;
                                nc.OutputSocketName = dragsocket.Name;
                                nc.InputNode = nodeWhole;
                                nc.InputSocketName = socket.Name;
                            }
                            else
                            {
                                nc.InputNode = dragSocketNode;
                                nc.InputSocketName = dragsocket.Name;
                                nc.OutputNode = nodeWhole;
                                nc.OutputSocketName = socket.Name;
                            }

                            graph.Connections.RemoveAll(x => x.InputNode == nc.InputNode && x.InputSocketName == nc.InputSocketName);

                            graph.Connections.Add(nc);
                            rebuildConnectionDictionary = true;
                        }
                    }
                }
            }
            dragsocket = null;
            mdown = false;
            needRepaint = true;
        }

        private void AddToMenu(ToolStripItemCollection items, NodeToken token, string path, EventHandler click)
        {
            var pathParts = path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var first = pathParts.FirstOrDefault();
            ToolStripMenuItem item = null;
            if (!items.ContainsKey(first))
            {
                item = new ToolStripMenuItem(first);
                item.Name = first;
                item.Tag = token;
                items.Add(item);
            }
            else
            {
                item = items[first] as ToolStripMenuItem;
            }

            var next = string.Join("/", pathParts.Skip(1));
            if (!string.IsNullOrEmpty(next))
            {
                item.MouseEnter += (sender, args) => OnNodeHint("");
                AddToMenu(item.DropDownItems, token, next, click);
            }
            else
            {
                item.Click += click;
                item.Click += (sender, args) =>
                {
                    var i = allContextItems.Keys.FirstOrDefault(x => x.Name == item.Name);
                    allContextItems[i]++;
                };

                item.MouseEnter += (sender, args) => OnNodeHint(token.nodeAttribute.Description ?? ""); //LOOOK OUT HERE
                if (!allContextItems.Keys.Any(x => x.Name == item.Name))
                {
                    allContextItems.Add(item, 0);
                }
            }
        }

        private void NodesControl_MouseClick(object sender, MouseEventArgs e)
        {
            lastMouseLocation = e.Location;

            if (context == null)
            {
                return;
            }

            if (e.Button == MouseButtons.Right)
            {
                var methods = context.GetType().GetMethods();
                var nodes =
                    methods.Select(
                        x => new NodeToken()
                        {
                            method = x,
                            nodeAttribute = x.GetCustomAttributes(typeof(NodeAttribute), false).Cast<NodeAttribute>().FirstOrDefault()
                        }).Where(x => x.nodeAttribute != null);

                var context = new ContextMenuStrip();
                if (graph.Nodes.Exists(x => x.IsSlected))
                {
                    context.Items.Add("Delete Node(s)", null, ((o, args) =>
                    {
                        DeleteSelectedNodes();
                    }));
                    context.Items.Add("Duplicate Node(s)", null, ((o, args) =>
                    {
                        DuplicateSelectedNodes();
                    }));

                    context.Items.Add("Change colour ...", null, ((o, args) =>
                    {

                        ChangeSelectedNodesColour();

                    }));

                    if (graph.Nodes.Count(x=> x.IsSlected) == 2)
                    {
                        var sel = graph.Nodes.Where(x => x.IsSlected).ToArray();
                        context.Items.Add("Check Impact", null, ((o, args) =>
                        {
                            if (HasImpact(sel[0], sel[1])|| HasImpact(sel[1], sel[0]))
                            {
                                MessageBox.Show("One Node Has impact on other.", "Impact Detected.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("These Nodes do not impact themselves.", "No Impact.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }));

                    }
                    context.Items.Add(new ToolStripSeparator());

                }
                if (allContextItems.Values.Any(x=> x > 0))
                {
                    var handy = allContextItems.Where(x => x.Value > 0 && !string.IsNullOrEmpty(((x.Key.Tag) as NodeToken).nodeAttribute.Menu)).OrderByDescending(
                        x => x.Value).Take(8);

                    foreach (var kv in handy)
                    {
                        context.Items.Add(kv.Key);
                    }
                    context.Items.Add(new ToolStripSeparator());
                }
                foreach (var node in nodes.OrderBy(x => x.Attribute.Path))
                {

                }
            }
        }
    }
}
