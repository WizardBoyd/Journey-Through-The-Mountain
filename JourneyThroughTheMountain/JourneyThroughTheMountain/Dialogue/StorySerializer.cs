using CommonClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace JourneyThroughTheMountain.Dialogue
{
    public class StorySerializer
    {
        public Dictionary<Guid, LinearStroyObject> Deserialize(String input)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(Dictionary<Guid, LinearStroyObject>));
            using (StringReader sr = new StringReader(input))
            {
                using (XmlReader xr = XmlReader.Create(sr))
                {
                    return (Dictionary<Guid, LinearStroyObject>)serializer.ReadObject(xr);
            }
            }
   
        }

        //Could add a method to serilize but why bother
    }
}
