using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace DialougeEditor
{
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class DialougeString: ISerializable 
    {
        public string Dialouge;

        private DialougeString(SerializationInfo info, StreamingContext ctx)
        {
            Dialouge = info.GetString("Dialouge");

        }

        public DialougeString()
        {

        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public void GetObjectData(SerializationInfo info, StreamingContext ctx)
        {
            info.AddValue("Dialouge", Dialouge);
        }
    }
}
