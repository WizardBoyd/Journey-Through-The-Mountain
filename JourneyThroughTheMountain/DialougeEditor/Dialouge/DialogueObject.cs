using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialougeEditor
{
    [Serializable]
    public class DialogueObject: ISerializable
    {

        public Guid previousChoiceGUID;
        public Guid NodeGUID;
        public string Text;
        public List<DialogueChoiceObject> Choices;

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            
        }

        public DialogueObject(Control control, string _text, string GUID, string _pGUID)
        {
            Text = _text;
            NodeGUID = Guid.Parse(GUID);
            previousChoiceGUID = Guid.Parse(_pGUID);
            Choices = new List<DialogueChoiceObject>();

            foreach (var ctrl in control.Controls.OfType<RichTextBox>())
            {
                Choices.Add(new DialogueChoiceObject(ctrl.Text, NodeGUID.ToString()));
            }
        }

        private DialogueObject(SerializationInfo info, StreamingContext ctx)
        {

        }
    }
}
