using CommonClasses;
using JourneyThroughTheMountain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace JourneyThroughTheMountain
{
    public class CollidedEvents : BaseGameStateEvent
    {
        

        public class DialogueEvent : CollidedEvents
        {
            Timer switchchattimer;
            public List<LinearStroyObject> linearStroyObjects = new List<LinearStroyObject>();
            int MaxDialouges;
            int current;
            NPC person1;
            GameObject person2;


            public DialogueEvent()
            {
                switchchattimer = new Timer(5000);
                switchchattimer.Elapsed += switchDialouge;
                
                
            }

            public void StartDialouge(NPC p1, GameObject P2)
            {
                person1 = p1;
                person2 = P2;
                foreach (KeyValuePair<Guid, LinearStroyObject> item in p1.Dialogue)
                {
                    linearStroyObjects.Add(item.Value);
                }
                MaxDialouges = linearStroyObjects.Count;
                current = 0;
                switchchattimer.Start();

            }

            private void switchDialouge(object source,ElapsedEventArgs e)
            {
                if (current < MaxDialouges)
                {
                    if (linearStroyObjects[current].AIspeaking)
                    {
                        LevelManager.Talker = person1;
                    }
                    else
                    {
                        LevelManager.Talker = person2;
                    }
                    LevelManager.Text = linearStroyObjects[current].Text;
                    current++;
                    switchchattimer.Start();
                }
                
            }

           
        }

    }
}
