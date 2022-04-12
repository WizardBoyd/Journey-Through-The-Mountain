using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace JourneyThroughTheMountain
{
    public class EventBox : GameObject
    {
        public event EventHandler<BoundingBox> CollidedWith;

        public bool Triggered = false;

        public override List<BoundingBox> TriggerBoxes
        {
            get
            {
                return _triggerboxes;
            }
            set
            {
                _triggerboxes = value;
            }
        }

        public override List<BoundingBox> BoundingBoxes
        {
            get
            {
                return null;
            }
            set
            {
                _boundingboxes = null;
            }
        }

        public CollidedEvents EventType;

        public string NPCName;
        public Entities.NPC AssociatedNPC = null;
        public GameObject Triggerer = null;

        public EventBox(Rectangle rectangle, int Eventtype, string AssociatedNPCName = null)
        {
            _triggerboxes.Add(new BoundingBox(new Vector2(rectangle.X, rectangle.Y), rectangle.Width, rectangle.Height));
            WorldLocation = new Vector2(rectangle.X, rectangle.Y);
            NPCName = AssociatedNPCName;
            switch (Eventtype)
            {
                case 0:
                    EventType = new CollidedEvents.DialogueEvent();
                    break;
                case 1:
                    EventType = new CollidedEvents.PrayEvent();
                    break;
            }
        }

        public override void OnNotify(BaseGameStateEvent Event)
        {
            if (!Triggered)
            {
                switch (Event)
                {

                    case CollidedEvents.PrayEvent m:
                        LevelManager.Display_EButton = true;
                        break;
                }
            }
        
        }
    }
}
