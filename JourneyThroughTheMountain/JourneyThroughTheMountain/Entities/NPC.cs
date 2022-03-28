using CommonClasses;
using JourneyThroughTheMountain.Dialogue;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace JourneyThroughTheMountain.Entities
{
    public class NPC: GameObject
    {
        public bool Speaker;

        public Dictionary<Guid, LinearStroyObject> Dialogue = null;

        public EventBox AssociatedEvent;

        public float Speakingspeed = 10f;
        public Vector2 fallSpeed = new Vector2(0, 20);
        public float WalkSpeed = 0.0f;
        private bool facingleft = true;


        public NPC(ContentManager content, string AnimationImage, int x, int y, string XMLTextLocation = null)//Need to add Event as well
        {
            if (XMLTextLocation != null)
            {
                StorySerializer ser = new StorySerializer();
                string path = content.RootDirectory + @"/Dialogue/" + XMLTextLocation + ".xml";
                string XMLInputDatat = System.IO.File.ReadAllText(path);
                Dialogue = ser.Deserialize(XMLInputDatat);
            }

            frameWidth = 48;
            frameHeight = 48;

            animations.Add("idle", new AnimationStrip(content.Load<Texture2D>(@"Animations/NPC/" + AnimationImage), 48, "idle"));

            animations["idle"].LoopAnimation = true;

            _boundingboxes.Add(new BoundingBox(new Vector2(0, 0), 30, 46));

            drawDepth = 0.825f;
            enabled = true;
            WorldLocation = new Vector2(x, y);
            PlayAnimation("idle");
            //NEED TO ADD TALKING AND EVENT TRIGGER
        }

        private void AssociateEvent(Rectangle rec)
        {
            if (TileEngine.TileMap.CellCodeValue(rec.X, rec.Y).StartsWith("E_Talk"))
            {
                //AssociatedEvent = new EventBox(rec, new CollidedEvents.DialogueEvent(Dialogue));
            }
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 oldLocation = worldLocation;

            velocity = new Vector2(0, velocity.Y);

            Vector2 direction = new Vector2(1, 0);
            flipped = true;

            if (facingleft)
            {
                direction = new Vector2(-1, 0);
                flipped = false;
            }
            direction *= WalkSpeed;
            velocity += direction;
            velocity += fallSpeed;

            base.Update(gameTime);
        }


    }
}
