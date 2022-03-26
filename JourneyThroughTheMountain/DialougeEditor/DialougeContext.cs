using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace DialougeEditor
{
    class DialougeContext : INodesContext
    {


        public NodeVisual CurrentProcessingNode { get; set; }

        public event Action<string, NodeVisual, FeedBackType, object, bool> FeedbackInfo;

        [Node("Test", "Testing", "In-Development", "Just a Test Node", false)]
        public void InputString(string InValue, out string outValue)
        {
            outValue = InValue;
            
        }


        [Node("Dialogue Node 1 response", "Dialouge", "Story", "", customEditor:typeof(DialougeNode1))]
        public void DialougeNode1(out DialogueChoiceObject OutValue1,DialogueChoiceObject choiceinput = default(DialogueChoiceObject), string inputValue = "")
        {

            DialogueObject dialogueObject = new DialogueObject(CurrentProcessingNode.CustomEditor, inputValue,CurrentProcessingNode.GetGUID(),
                choiceinput.ChoiceGUID.ToString());

            OutValue1 = dialogueObject.Choices[0];

        }


        [Node("Dialogue Node 2 responses", "Dialouge", "Story", "", customEditor: typeof(DialougeNode2))]
        public void DialougeNode2(out DialogueChoiceObject OutValue1,out DialogueChoiceObject OutValue2, DialogueChoiceObject choiceinput = default(DialogueChoiceObject), string inputValue = "")
        {



            DialogueObject dialogueObject = new DialogueObject(CurrentProcessingNode.CustomEditor, inputValue, CurrentProcessingNode.GetGUID(),
              choiceinput.ChoiceGUID.ToString());

            //dialogueObject.Choices = new DialogueChoiceObject[]
            //{
            //  new DialogueChoiceObject(  CurrentProcessingNode.CustomEditor.Controls["TextBox1"].Text),
            //   new DialogueChoiceObject( CurrentProcessingNode.CustomEditor.Controls["TextBox2"].Text)
            //};

            OutValue1 =  dialogueObject.Choices[0];
            OutValue2 =  dialogueObject.Choices[1];

        }


        [Node("Dialogue Node 3 responses", "Dialouge", "Story", "", customEditor: typeof(DialougeNode3))]
        public void DialougeNode3(out DialogueChoiceObject OutValue1, out DialogueChoiceObject OutValue2, out DialogueChoiceObject OutValue3, DialogueChoiceObject choiceinput = default(DialogueChoiceObject), string inputValue = "")
        {

            DialogueObject dialogueObject = new DialogueObject(CurrentProcessingNode.CustomEditor, inputValue, CurrentProcessingNode.GetGUID(),
             choiceinput.ChoiceGUID.ToString());

            //dialogueObject.Choices = new DialogueChoiceObject[]
            //{
            //   new DialogueChoiceObject( CurrentProcessingNode.CustomEditor.Controls["TextBox1"].Text),
            //    new DialogueChoiceObject(CurrentProcessingNode.CustomEditor.Controls["TextBox2"].Text),
            //     new DialogueChoiceObject(CurrentProcessingNode.CustomEditor.Controls["TextBox3"].Text)
            //};//LOOK AT GAME DEV TV AND HOW THEY DO IT

            OutValue1 = dialogueObject.Choices[0];
            OutValue2 = dialogueObject.Choices[1];
            OutValue3 = dialogueObject.Choices[2];

        }

        [Node("Starter Node", "Basic","Helper", "Starts Execution", true, true)]
        public void StarterNode()
        {
            
        }

        [Node("Liner Speak Node","Dialouge", "Story", "", customEditor:typeof(SpeakNode))]
        public void SpeakNode(ref Dictionary<Guid, LinearStroyObject> Log, out Dictionary<Guid, LinearStroyObject> OutLog)
        {
            LinearStroyObject o = new LinearStroyObject(CurrentProcessingNode.CustomEditor.Controls.OfType<RadioButton>().First().Checked, CurrentProcessingNode.CustomEditor.Controls.OfType<RichTextBox>().First().Text);

            Log.Add(Guid.Parse(CurrentProcessingNode.GetGUID()), o);
            OutLog = Log;
        }


        [Node("XML start Story Node", "Dialouge", "Story", "")]
        public void XMLExportNodestart(out Dictionary<Guid, LinearStroyObject> Log)
        {
            Log = new Dictionary<Guid, LinearStroyObject>();
            
        }

       [Node("XML End Story", "Dialouge", "Story", "")]
       public void XMLEndSotry(ref Dictionary<Guid,LinearStroyObject> Log)
       {
            string xmlstring;
            DataContractSerializer serializer = new DataContractSerializer(typeof(Dictionary<Guid, LinearStroyObject>));
            using(SaveFileDialog SFD = new SaveFileDialog())
            {
                if (SFD.ShowDialog() == DialogResult.OK)
                {
                    System.IO.StreamWriter sw = System.IO.File.CreateText(Path.GetFullPath(SFD.FileName) + ".xml");
                    
                    using(XmlTextWriter writer = new XmlTextWriter(sw))
                    {
                        writer.Formatting = Formatting.Indented;
                        serializer.WriteObject(writer, Log);
                        writer.Flush();
                        xmlstring = sw.ToString();
                    }

                

                    sw.Close();
                    sw.Dispose();
                }
            }
       }


        //ref Dictionary<string, string> DictonaryIn,DialougeChoiceNode choicein, out Dictionary<string,string> DictonaryOut

    }
}
