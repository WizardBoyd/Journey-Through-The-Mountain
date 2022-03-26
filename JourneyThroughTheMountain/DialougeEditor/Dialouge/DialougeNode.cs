using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialougeEditor
{

    [TypeConverter(typeof(DynamicNodeContextConverter))]
    public class DialougeNode1 : Dialouge.Dialouge1Response,IZoomable
    {

        public float zoom = 1f;
        public float Zoom
        {
            get { return zoom; }
            set
            {
                zoom = value;
            }
        }

        public DialougeNode1()
        {
            Invalidated += UpdateNodeContext;



        }

        ~DialougeNode1()
        {
            Invalidated -= UpdateNodeContext;
          
        }

        

        private void UpdateNodeContext(object sender,EventArgs e)
        {
            var node = (Tag as NodeVisual);
            if (node != null)
            {
                dynamic context = node.getNodeContext();
                //ADD context.What I want to store
            }
        }

    }

    [TypeConverter(typeof(DynamicNodeContextConverter))]
    public class DialougeNode2 : Dialouge.Dialouge2Response, IZoomable
    {

        public float zoom = 1f;
        public float Zoom
        {
            get { return zoom; }
            set
            {
                zoom = value;
            }
        }

        public DialougeNode2()
        {
            Invalidated += UpdateNodeContext;



        }

        ~DialougeNode2()
        {
            Invalidated -= UpdateNodeContext;

        }



        private void UpdateNodeContext(object sender, EventArgs e)
        {
            var node = (Tag as NodeVisual);
            if (node != null)
            {
                dynamic context = node.getNodeContext();
                //ADD context.What I want to store
            }
        }

    }

    [TypeConverter(typeof(DynamicNodeContextConverter))]
    public class DialougeNode3 : Dialouge.Dialouge3Response, IZoomable
    {

        public float zoom = 1f;
        public float Zoom
        {
            get { return zoom; }
            set
            {
                zoom = value;
            }
        }

        public DialougeNode3()
        {
            Invalidated += UpdateNodeContext;



        }

        ~DialougeNode3()
        {
            Invalidated -= UpdateNodeContext;

        }



        private void UpdateNodeContext(object sender, EventArgs e)
        {
            var node = (Tag as NodeVisual);
            if (node != null)
            {
                dynamic context = node.getNodeContext();
                //ADD context.What I want to store
            }
        }

    }

    [TypeConverter(typeof(DynamicNodeContextConverter))]
    public class SpeakNode : Dialouge.SpeakControl, IZoomable
    {
        
        public float zoom = 1f;
        public float Zoom
        {
            get { return zoom; }
            set
            {
                zoom = value;
            }
        }

        public SpeakNode()
        {
            Invalidated += UpdateNodeContext;
        }

        ~SpeakNode()
        {
            Invalidated -= UpdateNodeContext;
        }

        private void UpdateNodeContext(object sender, EventArgs e)
        {
            var node = (Tag as NodeVisual);
            if (node != null)
            {
                dynamic context = node.getNodeContext();
                //ADD context.What I want to store
            }
        }
    }


}
