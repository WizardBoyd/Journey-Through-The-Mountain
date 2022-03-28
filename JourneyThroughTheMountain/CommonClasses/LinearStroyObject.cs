using System;
using System.Runtime.Serialization;

namespace CommonClasses
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
