using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialougeEditor.Dialouge
{
    [TypeConverter(typeof(DynamicNodeContextConverter))]
    public partial class Dialouge3Response : UserControl
    {
        public Dialouge3Response()
        {
            InitializeComponent();
        }
    }
}
