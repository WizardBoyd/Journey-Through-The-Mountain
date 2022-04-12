using CommonClasses;
using JourneyThroughTheMountain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace JourneyThroughTheMountain.Dialogue
{
    public class Dialouge
    {
        Timer switchchattimer;
        public List<LinearStroyObject> linearStroyObjects = new List<LinearStroyObject>();
        int MaxDialouges;
        int current;
        NPC person1;
        Player person2;

        public Dialouge()
        {
            switchchattimer = new Timer(2000);
            switchchattimer.Elapsed += switchDialouge;


        }
        public void StartDialouge(NPC p1, Player P2)
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

        private void switchDialouge(object source, ElapsedEventArgs e)
        {
            if (current < MaxDialouges)
            {
                if (linearStroyObjects[current].AIspeaking)
                {
                    LevelManager.TalkSFXPlayNPC = true;
                    LevelManager.Talker = person1;
                }
                else
                {
                    LevelManager.TalkSFXPlay = true;
                    LevelManager.Talker = person2;

                }
                LevelManager.Text = linearStroyObjects[current].Text;
                current++;
                //switchchattimer.Start();
            }
            else
            {
                LevelManager.Text = " ";
                person2.Talking = false;
                switchchattimer.Stop();
            }

        }

    }
}
