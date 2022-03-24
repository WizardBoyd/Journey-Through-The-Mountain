
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

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
                    context.FeedbackInfo -= ContextOnFeedbackInfo;
                }
                context = value;
                if (context != null)
                {
                    context.FeedbackInfo += ContextOnFeedbackInfo;
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
                var methods = Context.GetType().GetMethods();
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

                        ChangeSelectedNodesColor();

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
                foreach (var node in nodes.OrderBy(x => x.nodeAttribute.Path))
                {
                    AddToMenu(context.Items, node, node.nodeAttribute.Path, (s, ev) => {

                        var tag = (s as ToolStripMenuItem).Tag as NodeToken;

                        var nv = new NodeVisual();
                        nv.X = lastMouseLocation.X;
                        nv.Y = lastMouseLocation.Y;
                        nv.Type = node.method;
                        nv.Callable = node.nodeAttribute.IsCallable;
                        nv.Name = node.nodeAttribute.Name;
                        nv.Order = graph.Nodes.Count;
                        nv.ExecInit = node.nodeAttribute.IsExecutionInitiator;
                        nv.XmlExportName = node.nodeAttribute.XmlExportName;
                        nv.CustomWidth = node.nodeAttribute.Width;
                        nv.CustomHeight = node.nodeAttribute.Height;

                        if (node.nodeAttribute.CustomEditor != null)
                        {
                            Control ctrl = null;
                            nv.CustomEditor = ctrl = Activator.CreateInstance(node.nodeAttribute.CustomEditor) as Control;
                            if (ctrl != null)
                            {
                                ctrl.Tag = nv;
                                Controls.Add(ctrl);
                            }
                            nv.LayoutEditor();
                        }
                        graph.Nodes.Add(nv);
                        Refresh();
                        needRepaint = true;
                    
                    });
                }
                context.Show(MousePosition);
            }
        }

        private void ChangeSelectedNodesColor()
        {
            ColorDialog cd = new ColorDialog();
            cd.FullOpen = true;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                foreach (var n in graph.Nodes.Where(x => x.IsSlected))
                {
                    n.NodeColor = cd.Color;
                }
            }
            Refresh();
            needRepaint = true;
        }

        private void DuplicateSelectedNodes()
        {
            var cloned = new List<NodeVisual>();
            foreach (var n in graph.Nodes.Where(x => x.IsSlected))
            {
                int count = graph.Nodes.Count(x => x.IsSlected);
                var ms = new MemoryStream();
                var bw = new BinaryWriter(ms);

                SerializeNode(bw, n);

                ms.Seek(0, SeekOrigin.Begin);

                var br = new BinaryReader(ms);
                var clone = DeserializeNode(br);
                clone.X += 40;
                clone.Y += 40;
                clone.GUID = Guid.NewGuid().ToString();
                cloned.Add(clone);
                br.Dispose();
                bw.Dispose();
                ms.Dispose();
            }
            graph.Nodes.ForEach(x => x.IsSlected = false);
            cloned.ForEach(x => x.IsSlected = true);
            cloned.Where(x => x.CustomEditor != null).ToList().ForEach(x => x.CustomEditor.BringToFront());
            graph.Nodes.AddRange(cloned);
            Invalidate();
        }

        private void DeleteSelectedNodes()
        {
            if (graph.Nodes.Exists(x => x.IsSlected))
            {
                foreach (var n in graph.Nodes.Where(x => x.IsSlected))
                {
                    Controls.Remove(n.CustomEditor);
                    graph.Connections.RemoveAll(x => x.OutputNode == n || x.InputNode == n);
                }
                graph.Nodes.RemoveAll(x => graph.Nodes.Where(n => n.IsSlected).Contains(x));
            }
            Invalidate();
        }

        public void Execute(NodeVisual node = null)
        {
            var nodeQueue = new Queue<NodeVisual>();
            nodeQueue.Enqueue(node);

            while (nodeQueue.Count > 0)
            {
                if (breakExecution)
                {
                    breakExecution = false;
                    ExecutionStack.Clear();
                    return;
                }

                var init = nodeQueue.Dequeue() ?? graph.Nodes.FirstOrDefault(x => x.ExecInit);
                if (init != null)
                {
                    init.Feedback = FeedBackType.Debug;

                    Resolve(init);
                    init.Execute(Context);

                    var connection = graph.Connections.FirstOrDefault(x => x.OutputNode == init && x.IsExecution && x.OutputSocket.Value != null
                    && (x.OutputSocket.Value as ExecutionPath).IsSignaled);
                    if (connection == null)
                    {
                        connection = graph.Connections.FirstOrDefault(x => x.OutputNode == init && x.IsExecution && x.OutputSocket.IsMainExecution);

                    }
                    else
                    {
                        ExecutionStack.Push(init);
                    }
                    if (connection != null)
                    {
                        connection.InputNode.IsBackExecuted = false;
                        nodeQueue.Enqueue(connection.InputNode);
                    }
                    else
                    {
                        if (ExecutionStack.Count > 0)
                        {
                            var back = ExecutionStack.Pop();
                            back.IsBackExecuted = true;
                            Execute(back);
                        }
                    }
                }
            }
        }

        public List<NodeVisual> Getnodes(params string[] nodeNames)
        {
            var nodes = graph.Nodes.Where(x => nodeNames.Contains(x.Name));
            return nodes.ToList();
        }

        public bool HasImpact(NodeVisual startNode, NodeVisual endNode)
        {
            var connections = graph.Connections.Where(x => x.OutputNode == startNode && !x.IsExecution);
            foreach (var connection in connections)
            {
                if (connection.InputNode == endNode)
                {
                    return true;
                }
                bool nextImpact = HasImpact(connection.InputNode, endNode);
                if (nextImpact)
                {
                    return true;
                }
            }
            return false;
        }

        public void ExecuteResolving(params string[] nodeNames)
        {
            var nodes = graph.Nodes.Where(x => nodeNames.Contains(x.Name));

            foreach (var node in nodes)
            {
                ExecuteResolvingInternal(node);
            }
        }

        private void ExecuteResolvingInternal(NodeVisual node)
        {
            var icontext = (node.getNodeContext() as DynamicNodeContext);
            foreach (var input in node.GetInputs())
            {
                var connection =
                    graph.Connections.FirstOrDefault(x => x.InputNode == node && x.InputSocketName == input.Name);

                if (connection != null)
                {
                    Resolve(connection.OutputNode);

                    connection.OutputNode.Execute(Context);

                    ExecuteResolvingInternal(connection.OutputNode);

                    var ocontext = (connection.OutputNode.getNodeContext() as DynamicNodeContext);
                    icontext[connection.InputSocketName] = ocontext[connection.OutputSocketName];
                }
            }
        }

        private void Resolve(NodeVisual node)
        {
            var icontext = (node.getNodeContext() as DynamicNodeContext);
            foreach (var input in node.GetInputs())
            {
                var connection = GetConnection(node.GUID + input.Name);

                if (connection != null)
                {
                    Resolve(connection.OutputNode);
                    if (!connection.OutputNode.Callable)
                    {
                        connection.OutputNode.Execute(Context);
                    }
                    var ocontext = (connection.OutputNode.getNodeContext() as DynamicNodeContext);
                    icontext[connection.InputSocketName] = ocontext[connection.OutputSocketName];
                }
            }
        }

        private NodeConnection GetConnection(string v)
        {
            if (rebuildConnectionDictionary)
            {
                rebuildConnectionDictionary = false;
                connectionDictionary.Clear();
                foreach (var conn in graph.Connections)
                {
                    connectionDictionary.Add(conn.InputNode.GUID + conn.InputSocketName, conn);
                }
            }
            NodeConnection nc = null;
            if (connectionDictionary.TryGetValue(v,out nc))
            {
                return nc;
            }
            return null;
        }

        public string ExportToXml()
        {
            var xml = new XmlDocument();

            XmlElement el = (XmlElement)xml.AppendChild(xml.CreateElement("NodeGrap"));
            el.SetAttribute("Created", DateTime.Now.ToString());
            var nodes = el.AppendChild(xml.CreateElement("Nodes"));
            foreach (var node in graph.Nodes)
            {
                var xmlNode = (XmlElement)nodes.AppendChild(xml.CreateElement("Node"));
                xmlNode.SetAttribute("Name", node.XmlExportName);
                xmlNode.SetAttribute("Id", node.GetGUID());
                var xmlContext = (XmlElement)xmlNode.AppendChild(xml.CreateElement("Context"));
                var context = node.getNodeContext() as DynamicNodeContext;
                foreach (var kv in context)
                {
                    var ce = (XmlElement)xmlContext.AppendChild(xml.CreateElement("ContextMember"));
                    ce.SetAttribute("Name", kv);
                    ce.SetAttribute("Value", Convert.ToString(context[kv] ?? ""));
                    ce.SetAttribute("Type", context[kv] == null ? "" : context[kv].GetType().FullName);
                }
            }
            var connections = el.AppendChild(xml.CreateElement("Connections"));
            foreach (var conn in graph.Connections)
            {
                var xmlconn = (XmlElement)nodes.AppendChild(xml.CreateElement("Connection"));
                xmlconn.SetAttribute("OutputNodeID", conn.OutputNode.GetGUID());
                xmlconn.SetAttribute("OutputNodeSocket", conn.OutputSocketName);
                xmlconn.SetAttribute("InputNodeID", conn.InputNode.GetGUID());
                xmlconn.SetAttribute("InputNodeSocket", conn.InputSocketName);
            }
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace
            };
            using(XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                xml.Save(writer);
            }
            return sb.ToString();
        }

        public byte[] Serialize()
        {
            using(var bw = new BinaryWriter(new MemoryStream()))
            {
                bw.Write("NodeSystemP"); //recognation string
                bw.Write(1000); //version
                bw.Write(graph.Nodes.Count);
                foreach (var Node in graph.Nodes)
                {
                    SerializeNode(bw, Node);
                }
                bw.Write(graph.Connections.Count);
                foreach (var connection in graph.Connections)
                {
                    bw.Write(connection.OutputNode.GUID);
                    bw.Write(connection.OutputSocketName);

                    bw.Write(connection.InputNode.GUID);
                    bw.Write(connection.InputSocketName);
                    bw.Write(0); //additional data size

                }
                bw.Write(0);// addtional size per graph
                return (bw.BaseStream as MemoryStream).ToArray();
            }
        }

        private static void SerializeNode(BinaryWriter bw, NodeVisual node)
        {
            bw.Write(node.GUID);
            bw.Write(node.X);
            bw.Write(node.Y);
            bw.Write(node.Callable);
            bw.Write(node.ExecInit);
            bw.Write(node.Name);
            bw.Write(node.Order);
            if (node.CustomEditor == null)
            {
                bw.Write("");
                bw.Write("");
            }
            else
            {
                bw.Write(node.CustomEditor.GetType().Assembly.GetName().Name);
                bw.Write(node.CustomEditor.GetType().FullName);
            }
            bw.Write(node.Type.Name);
            var context = (node.getNodeContext() as DynamicNodeContext).Serialize();
            bw.Write(context.Length);
            bw.Write(context);
            bw.Write(8); //additonal data size for nodes
            bw.Write(node.Tag);
            bw.Write(node.NodeColor.ToArgb());
        }

        public void Deserialize(byte[] data)
        {
            using (var br = new BinaryReader(new MemoryStream(data)))
            {
                var ident = br.ReadString();
                if (ident != "NodeSystemP")
                {
                    return;
                }
                rebuildConnectionDictionary = true;
                graph.Connections.Clear();
                graph.Nodes.Clear();
                Controls.Clear();

                var version = br.ReadInt32();
                int nodeCount = br.ReadInt32();
                for (int i = 0; i < nodeCount; i++)
                {
                    var nv = DeserializeNode(br);

                    graph.Nodes.Add(nv);
                }

                var connectionsCount = br.ReadInt32();
                for (int i = 0; i < connectionsCount; i++)
                {
                    var con = new NodeConnection();
                    var og = br.ReadString();
                    con.OutputNode = graph.Nodes.FirstOrDefault(x => x.GUID == og);
                    con.OutputSocketName = br.ReadString();
                    var ig = br.ReadString();
                    con.InputNode = graph.Nodes.FirstOrDefault(x => x.GUID == ig);
                    con.InputSocketName = br.ReadString();
                    br.ReadBytes(br.ReadInt32()); // read additonal data

                    graph.Connections.Add(con);
                    rebuildConnectionDictionary = true;
                }
                br.ReadBytes(br.ReadInt32()); //read more data
            }
            Refresh();
        }

        private NodeVisual DeserializeNode(BinaryReader br)
        {
            var nv = new NodeVisual();
            nv.GUID = br.ReadString();
            nv.X = br.ReadSingle();
            nv.Y = br.ReadSingle();
            nv.Callable = br.ReadBoolean();
            nv.ExecInit = br.ReadBoolean();
            nv.Name = br.ReadString();
            nv.Order = br.ReadInt32();

            var customEditorAssembly = br.ReadString();
            var customEditor = br.ReadString();
            nv.Type = Context.GetType().GetMethod(br.ReadString());
            var attribute = nv.Type.GetCustomAttributes(typeof(NodeAttribute), false).Cast<NodeAttribute>().FirstOrDefault();

            if (attribute != null)
            {
                nv.CustomWidth = attribute.Width;
                nv.CustomHeight = attribute.Height;
            }
            (nv.getNodeContext() as DynamicNodeContext).Deserialize(br.ReadBytes(br.ReadInt32()));
            var additional = br.ReadInt32(); //read additional data
            if (additional >= 4)
            {
                nv.Tag = br.ReadInt32();
                if (additional >= 8)
                {
                    nv.NodeColor = Color.FromArgb(br.ReadInt32());
                }
            }
            if (additional > 8)
            {
                br.ReadBytes(additional - 8);
            }

            if (customEditor != "")
            {
                nv.CustomEditor = Activator.CreateInstance(AppDomain.CurrentDomain, customEditorAssembly, customEditor).Unwrap() as Control;

                Control ctrl = nv.CustomEditor;
                if (ctrl != null)
                {
                    ctrl.Tag = nv;
                    Controls.Add(ctrl);
                }
                nv.LayoutEditor();
            }
            return nv;
        }

        public void Clear()
        {
            graph.Nodes.Clear();
            graph.Connections.Clear();
            Controls.Clear();
            Refresh();
            rebuildConnectionDictionary = true;
        }
    }
}
