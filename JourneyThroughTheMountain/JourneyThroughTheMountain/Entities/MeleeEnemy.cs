using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;
using TileEngine;

namespace JourneyThroughTheMountain.Entities
{
    public class MeleeEnemy : GameObject, IGameObjectWithHealth, IGameObjectWithDamage
    {
        public int MaxHealth = 5;
        private Vector2 Fallspeed = new Vector2(0, 20);
        private float WalkSpeed = 30.0f;
        private bool FacingLeft = true;
        public bool Dead = false;
        public System.Timers.Timer AttackTimer = new System.Timers.Timer(5000);
        public bool CanAttack = true;
        public bool Attacking = false;
        public bool Move = true;
        public string newAnimation;
        public int Damage { get; set; }

        public Segment Raycast
        {
            get
            {
                Vector2 seg = new Vector2(1, 0.1f) * frameWidth;
                return FacingLeft ? new Segment(WorldLocation, Vector2.Add(WorldLocation, new Vector2(seg.X - frameWidth, seg.Y))) : new Segment(WorldLocation, Vector2.Add(WorldLocation, new Vector2(seg.X + frameWidth, seg.Y)));
            }
        }

        public MeleeEnemy(ContentManager content, int cellX, int cellY)
        {
            animations.Add("idle", new AnimationStrip(content.Load<Texture2D>(@"Enemy/Snowy/Snowy_idle"), 48, "idle"));
            animations.Add("Walking", new AnimationStrip(content.Load<Texture2D>(@"Enemy/Snowy/Snowy_walk"), 48, "Walking"));
            animations.Add("Hurt", new AnimationStrip(content.Load<Texture2D>(@"Enemy/Snowy/Snowy_hurt"), 48, "Hurt"));
            animations.Add("Die", new AnimationStrip(content.Load<Texture2D>(@"Enemy/Snowy/Snowy_death"), 48, "Die"));
            animations.Add("Attack", new AnimationStrip(content.Load<Texture2D>(@"Enemy/Snowy/Snowy_attack"), 48, "Attack"));

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

            _boundingboxes.Add(new BoundingBox(new Vector2(0, 0), 48, 48));

            AttackTimer.Elapsed += SetAttack;

            WorldLocation = new Vector2(cellX * TileMap.TileWidth, cellY * TileMap.TileHeight);

            codeBasedBlocks = true;
            Health = 4;
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
            if (!enabled)
            {
                return;
            }

            Vector2 oldlocation = worldLocation;

            if (!Dead && Move)
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

                if (animations[currentAnimation].FinishedPlaying)
                {
                    newAnimation = "Walking";
                }
                if (Attacking)
                {
                    newAnimation = "Attack";
                }
            }
            else
            {
                //Enabled = false;
                velocity = Vector2.Zero;
            }

            base.Update(gameTime);
            if (!Dead)
            {
                if (Move)
                {
                    if (oldlocation == WorldLocation)
                    {
                        FacingLeft = !FacingLeft;
                    }
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
            if (!enabled && animations[currentAnimation].FinishedPlaying)
            {
                return;
            }
            spriteBatch.DrawLine(Raycast.P1, Raycast.P2, Color.Red, 5, 1.25f);
            base.Draw(spriteBatch);
        }

        private void SetAttack(object sender, System.Timers.ElapsedEventArgs e)
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

                    }
                    KnockBack();
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

        public void Revive()
        {
            health = MaxHealth;
            _boundingboxes.Add(new BoundingBox(new Vector2(0, 0), 52, 48));
            Move = true;
            Dead = false;
            Enabled = true;
            PlayAnimation("Walking");
        }

        public void Kill()
        {
            PlayAnimation("Die");
            velocity.X = 0;
            _boundingboxes.Clear();
            _triggerboxes.Clear();
            Dead = true;


        }
    }
}
