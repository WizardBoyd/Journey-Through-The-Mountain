using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common_Classes
{
    [DataContract]
    public class LinearStroyObject
    {
        [DataMember]
        public bool AIspeaking { get; set; }
        [DataMember]
        public string Text { get; set; }

        public LinearStroyObject(bool _aispeaking, string _text)
        {
            AIspeaking = _aispeaking;
            Text = _text;
        }
    }
}
