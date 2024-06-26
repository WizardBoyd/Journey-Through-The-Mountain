﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialougeEditor
{
    public class NodeVisual
    {
        public const float NodeWidth = 140;
        public const float HeaderHeight = 20;
        public const float ComponentPadding = 2;

        /// <summary>
        /// Current Name of Node
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Current Node X position
        /// </summary>
        public float X { get; set; }
        /// <summary>
        /// Current Y position of Node
        /// </summary>
        public float Y { get; set; }
        internal MethodInfo Type { get; set; }
        internal int Order { get; set; }
        internal bool Callable { get; set; }
        internal bool ExecInit { get; set; }

        internal bool IsSlected { get; set; }
        internal FeedBackType Feedback { get; set; }
        private object nodeContext { get; set; }
        public Control CustomEditor { get; internal set; }
        internal string GUID = Guid.NewGuid().ToString();
        internal Color NodeColor = Color.Blue;
        public bool IsBackExecuted { get; internal set; }

        private SocketVisual[] socketCache;

        public Int32 Tag = 0;
        public string XmlExportName { get; internal set; }

        internal int CustomWidth = -1;
        internal int CustomHeight = -1;

        internal NodeVisual()
        {
            Feedback = FeedBackType.Debug;
        }

        public string GetGUID()
        {
            return GUID;
        }

        internal SocketVisual[] GetSockets()
        {
            if (socketCache != null)
            {
                return socketCache;
            }

            var socketList = new List<SocketVisual>();
            float curInputH = HeaderHeight + ComponentPadding;
            float curOutputH = HeaderHeight + ComponentPadding;

            var NodeWidth = GetNodeBounds().Width;

            if (Callable)
            {
                if (!ExecInit)
                {
                    socketList.Add(new SocketVisual()
                    {
                        Height = SocketVisual.SocketHeight,
                        Name = "Enter",
                        Type = typeof(ExecutionPath),
                        IsMainExecution = true,
                        Width = SocketVisual.SocketHeight,
                        X = X,
                        Y = Y + curInputH,
                        Input = true
                    });
                }
                socketList.Add(new SocketVisual()
                {
                    Height = SocketVisual.SocketHeight,
                    Name = "Exit",
                    IsMainExecution = true,
                    Type = typeof(ExecutionPath),
                    Width = SocketVisual.SocketHeight,
                    X = X + NodeWidth - SocketVisual.SocketHeight,
                    Y = Y + curOutputH
                });

                curOutputH += SocketVisual.SocketHeight + ComponentPadding;
                curInputH += SocketVisual.SocketHeight + ComponentPadding;
            }

            foreach (var input in GetInputs())
            {
                var socket = new SocketVisual();
                socket.Type = input.ParameterType;
                socket.Height = SocketVisual.SocketHeight;
                socket.Name = input.Name;
                socket.Width = SocketVisual.SocketHeight;
                socket.X = X;
                socket.Y = Y + curInputH;
                socket.Input = true;

                socketList.Add(socket);

                curInputH += SocketVisual.SocketHeight + ComponentPadding;
            }

            var ctx = getNodeContext() as DynamicNodeContext;
            foreach (var output in GetOutputs())
            {
                var socket = new SocketVisual();
                socket.Type = output.ParameterType;
                socket.Height = SocketVisual.SocketHeight;
                socket.Name = output.Name;
                socket.Width = SocketVisual.SocketHeight;
                socket.X = X + NodeWidth - SocketVisual.SocketHeight;
                socket.Y = Y + curOutputH;
                socket.Value = ctx[socket.Name];
                socketList.Add(socket);

                curOutputH += SocketVisual.SocketHeight + ComponentPadding;
            }

            socketCache = socketList.ToArray();
            return socketCache;

        }

        internal void DiscardCache()
        {
            socketCache = null;
        }

        public object getNodeContext()
        {
            const string stringTypeName = "System.String";

            if (nodeContext == null)
            {
                dynamic context = new DynamicNodeContext();

                foreach (var input in GetInputs())
                {
                    var contextName = input.Name.Replace(" ", "");
                    if (input.ParameterType.FullName.Replace("&", "") == stringTypeName)
                    {
                        context[contextName] = string.Empty;
                    }
                    else
                    {
                        try
                        {
                            context[contextName] = Activator.CreateInstance(AppDomain.CurrentDomain, input.ParameterType.Assembly.GetName().Name,
                                input.ParameterType.FullName.Replace("&", "").Replace(" ", "")).Unwrap();
                        }
                        catch (MissingMethodException ex) //for case when type does not have default constructor
                        {
                            context[contextName] = null;
                        }
                    }
                }

                foreach (var output in GetOutputs())
                {
                    var contextName = output.Name.Replace(" ", "");
                    if (output.ParameterType.FullName.Replace("&", "") == stringTypeName)
                    {
                        context[contextName] = string.Empty;
                    }
                    else
                    {
                        try
                        {
                            context[contextName] = Activator.CreateInstance(AppDomain.CurrentDomain, output.ParameterType.Assembly.GetName().Name,
                                output.ParameterType.FullName.Replace("&", "").Replace(" ", "")).Unwrap();
                        }
                        catch (MissingMethodException ex) //No default Constructor for Type
                        {

                            context[contextName] = null;
                        }
                    }
                }

                nodeContext = context;
            }
            return nodeContext;
        }

        internal ParameterInfo[] GetInputs()
        {
            return Type.GetParameters().Where(x => !x.IsOut).ToArray();
        }

        internal ParameterInfo[] GetOutputs()
        {
            return Type.GetParameters().Where(x => x.IsOut).ToArray();
        }

        public SizeF GetNodeBounds()
        {
            var csize = new SizeF();
            if (CustomEditor != null)
            {
                csize = new SizeF(CustomEditor.ClientSize.Width + 2 + 80 + SocketVisual.SocketHeight * 2, CustomEditor.ClientSize.Height
                    + HeaderHeight + 8);
                
            }

            var inputs = GetInputs().Length;
            var outputs = GetOutputs().Length;

            if (Callable)
            {
                inputs++;
                outputs++;
            }
            var h = HeaderHeight + Math.Max(inputs * (SocketVisual.SocketHeight + ComponentPadding),
                outputs * (SocketVisual.SocketHeight + ComponentPadding)) + ComponentPadding * 2f;

            csize.Width = Math.Max(csize.Width, NodeWidth);
            csize.Height = Math.Max(csize.Height, h);
            if (CustomWidth >= 0)
            {
                csize.Width = CustomWidth;
            }
            if (CustomHeight >= 0)
            {
                csize.Height = CustomHeight;
            }

            return new SizeF(csize.Width, csize.Height);
        }

        public SizeF GetHeaderSize()
        {
            return new SizeF(GetNodeBounds().Width, HeaderHeight);
        }

        public void Draw(Graphics g, Point mouseLocation, MouseButtons mousebuttons)
        {
            var rect = new RectangleF(new PointF(X, Y), GetNodeBounds());

            var feedrect = rect;
            feedrect.Inflate(10, 10);

            if (Feedback == FeedBackType.Warning)
            {
                g.DrawRectangle(new Pen(Color.Yellow, 4), Rectangle.Round(feedrect));
            }
            else if (Feedback == FeedBackType.Warning)
            {
                g.DrawRectangle(new Pen(Color.Red, 5), Rectangle.Round(feedrect));
            }

            var caption = new RectangleF(new PointF(X, Y), GetHeaderSize());
            bool mouseHoverCaption = caption.Contains(mouseLocation);

            g.FillRectangle(new SolidBrush(NodeColor), rect);

            if (IsSlected)
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(180, Color.WhiteSmoke)), rect);
                g.FillRectangle(mouseHoverCaption ? Brushes.Gold : Brushes.Goldenrod, caption);
            }
            else
            {
                g.FillRectangle(mouseHoverCaption ? Brushes.Cyan : Brushes.Aquamarine, caption);
            }
            g.DrawRectangle(Pens.Gray, Rectangle.Round(caption));
            g.DrawRectangle(Pens.Black, Rectangle.Round(rect));

            g.DrawString(Name, SystemFonts.DefaultFont, Brushes.Black, new PointF(X + 3, Y + 3));

            var sockets = GetSockets();

            foreach (var socket in sockets)
            {
                socket.Draw(g, mouseLocation, mousebuttons);
            }

        }

        internal void Execute(INodesContext context)
        {
            context.CurrentProcessingNode = this;

            var dc = (getNodeContext() as DynamicNodeContext);
            var parametersDict = Type.GetParameters().OrderBy(x => x.Position).ToDictionary(x => x.Name, x => dc[x.Name]);
            var parameters = parametersDict.Values.ToArray();

            int ndx = 0;
            Type.Invoke(context, parameters);

            foreach (var kv in parametersDict.ToArray())
            {
                parametersDict[kv.Key] = parameters[ndx];
                ndx++;
            }

            var outs = GetSockets();

            foreach (var parameter in dc.ToArray())
            {
                dc[parameter] = parametersDict[parameter];
                var o = outs.FirstOrDefault(x => x.Name == parameter);

                Debug.Assert(o != null, "Output Not Found");
                {
                    o.Value = dc[parameter];
                }
            }
        }

        internal void LayoutEditor(float zoom)
        {
            if (CustomEditor != null)
            {
                CustomEditor.Location = new Point((int)(zoom * (X + 1 + 40 + SocketVisual.SocketHeight)), (int)(zoom * (Y + HeaderHeight + 4)));
            }
          
        }
    }
}
