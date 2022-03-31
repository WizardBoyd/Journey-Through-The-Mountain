using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using TileEngine;

namespace JourneyThroughTheMountain.Entities
{
    class Enemy : GameObject, IGameObjectWithHealth, IGameObjectWithDamage
    {

        private Vector2 Fallspeed = new Vector2(0, 20);
        private float WalkSpeed = 60.0f;
        private bool FacingLeft = true;
        public bool Dead = false;
       public System.Timers.Timer AttackTimer = new System.Timers.Timer(5000);
        public bool CanAttack = true;
        private bool Attacking = false;


        public Segment Raycast
        {
            get
            {
                Vector2 seg = new Vector2(1,0.2f) * frameWidth;
                return FacingLeft ? new Segment(WorldLocation, Vector2.Add(WorldLocation, new Vector2(seg.X - frameWidth, seg.Y))) : new Segment(WorldLocation, Vector2.Add(WorldLocation, new Vector2(seg.X + frameWidth, seg.Y)));
            }
        }

        public int Damage { get; set; }

        public Enemy(ContentManager content, int cellX, int cellY)
        {
            animations.Add("idle", new AnimationStrip(content.Load<Texture2D>(@"Enemy/Frost_golem/Frost_golem_idle"), 48, "idle"));
            animations.Add("Walking", new AnimationStrip(content.Load<Texture2D>(@"Enemy/Frost_golem/Frost_golem_walk"), 48, "Walking"));
            animations.Add("Hurt", new AnimationStrip(content.Load<Texture2D>(@"Enemy/Frost_golem/Frost_golem_hurt"), 48, "Hurt"));
            animations.Add("Die", new AnimationStrip(content.Load<Texture2D>(@"Enemy/Frost_golem/Frost_golem_death"), 48, "Die"));
            animations.Add("Attack", new AnimationStrip(content.Load<Texture2D>(@"Enemy/Frost_golem/Frost_golem_attack"), 48, "Attack"));

            animations["Die"].LoopAnimation = false;
            animations["Hurt"].LoopAnimation = false;
            animations["idle"].LoopAnimation = true;
            animations["Walking"].FrameLength = 0.1f;
            animations["Walking"].LoopAnimation = true;
            animations["Attack"].LoopAnimation = false;

            animations["Attack"].FrameLength = 0.2f;
            animations["Die"].FrameLength = 0.3f;

            frameWidth = 48;
            frameHeight = 48;

            _boundingboxes.Add(new BoundingBox(new Vector2(0, 0), 52, 48));

            AttackTimer.Elapsed += SetAttack;

            WorldLocation = new Vector2(cellX * TileMap.TileWidth, cellY * TileMap.TileHeight);

            codeBasedBlocks = true;
            Health = 15;
            Damage = 2;
            enabled = true;
            KnockbackTimer.Elapsed += (object sender, System.Timers.ElapsedEventArgs e) =>
            {
                Knockback = false;
            };
            PlayAnimation("Walking");


        }

        public override void Update(GameTime gameTime)
        {
            Vector2 oldlocation = worldLocation;

            if (!Dead)
            {
                velocity = new Vector2(0, velocity.Y);

                Vector2 direction = new Vector2(1, 0);
                flipped = true;

                if (FacingLeft)
                {
                    direction = new Vector2(-1, 0);
                    flipped = false;
                }

                direction *= WalkSpeed;
                velocity += direction;
                velocity += Fallspeed;
            }
            else
            {
                //Enabled = false;
            }

            base.Update(gameTime);
            if (!Dead)
            {
                if (oldlocation == WorldLocation)
                {
                    FacingLeft = !FacingLeft;
                }
          
            }
            else
            {

                if (animations[currentAnimation].FinishedPlaying)
                {
                    Enabled = false;
                    AttackTimer.Elapsed -= SetAttack;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawLine(Raycast.P1, Raycast.P2, Color.Red, 5, 1.25f);
            base.Draw(spriteBatch);
        }

        private void SetAttack(object sender,System.Timers.ElapsedEventArgs e)
        {
            CanAttack = true;
        }

        public override void OnNotify(BaseGameStateEvent Event)
        {
            switch (Event)
            {
                case GameplayEvents.DamageDealt m:
                    TakeDamage(m.Damage);
                    if (health < 0)
                    {
                        Kill();
                        KnockBack();
                    }
                    break;
                default:
                    break;
            }
        }

        public void TakeDamage(IGameObjectWithDamage o)
        {
            Health -= o.Damage;
            PlayAnimation("Hurt");
           
        }

        public void FireWeapon()
        {
            PlayAnimation("Attack");
        }

        public void TakeDamage(int Amount)
        {
            Health -= Amount;
            PlayAnimation("Hurt");
            KnockBack();
        }

        public void Kill()
        {
            PlayAnimation("Die");
            velocity.X = 0;

            Dead = true;

        }
    }
}
