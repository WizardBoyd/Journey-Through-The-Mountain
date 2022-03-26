using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DialougeEditor
{
    [Serializable]
    public class DialogueChoiceObject: ISerializable
    {
        public Guid PreviousDialouge;
        public string Text;
        public Guid ChoiceGUID;


        public DialogueChoiceObject(string _text, string GUID)
        {
            PreviousDialouge = Guid.Parse(GUID);
            ChoiceGUID = Guid.NewGuid();
            Text = _text;
        }

        public DialogueChoiceObject()
        {
            PreviousDialouge = Guid.Empty;
            Text = "";
            ChoiceGUID = Guid.Empty;
        }

        private DialogueChoiceObject(SerializationInfo info, StreamingContext ctx)
        {

        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Text", Text);
        }
    }
}
