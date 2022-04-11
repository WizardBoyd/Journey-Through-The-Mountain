using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using TileEngine;
using JourneyThroughTheMountain.Entities;
using JourneyThroughTheMountain.Dialogue;
using CommonClasses;
using System.Reflection;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace JourneyThroughTheMountain
{
    public static class LevelManager
    {
        #region Declarations
        private static ContentManager Content;
        private static Player player;
        private static int currentLevel;
        private static Vector2 respawnLocation;

        private static SpriteFont spriteFont { get; set; }

        private static List<SnowFlakes> snowFlakes = new List<SnowFlakes>();
        private static List<Entities.Enemy> enemies = new List<Entities.Enemy>();
        private static List<Entities.MeleeEnemy> MeleeEnemies = new List<MeleeEnemy>();
        private static List<Tree> Trees = new List<Tree>();
        private static List<GameTile> Tiles = new List<GameTile>();
        private static List<EventBox> Events = new List<EventBox>();
        private static Dictionary<string, NPC> NPCs = new Dictionary<string, NPC>();

        private const string E_Button = @"UI/F_button";
     

        public static string Text;
        public static GameObject Talker;
        public static bool TalkSFXPlayNPC;
        public static bool TalkSFXPlay;
        public static bool Display_EButton;
        private static Texture2D E_button_Texture;
        #endregion

        #region Properties
        public static int CurrentLevel
        {
            get { return currentLevel; }
        }

        public static Vector2 RespawnLocation
        {
            get { return respawnLocation; }
            set { respawnLocation = value; }
        }
        #endregion

        #region Initialization
        public static void Initialize(
            ContentManager content,
            Player gamePlayer, SpriteFont Font)
        {
            Content = content;
            player = gamePlayer;
            spriteFont = Font;
            E_button_Texture = content.Load<Texture2D>(E_Button);
            
        }
        #endregion

        #region Helper Methods
        private static void checkCurrentCellCode()
        {
            string code = TileMap.CellCodeValue(
                TileMap.GetCellByPixel(player.WorldCenter));

            if (code == "DEAD")
            {
                player.Kill();
            }
        }

        private static void AssociateNPCWithEvents()
        {
            foreach (EventBox Event in Events)
            {
                if (Event.NPCName != null)
                {
                    NPC nPC;
                    if (NPCs.TryGetValue(Event.NPCName,out nPC))
                    {
                        Event.AssociatedNPC = nPC;
                    }
                }
            }
        }

        //private void CheckForNPCPlacement(int x, int y)
        //{
        //    Vector2 Cell =  new Vector2(x, y);
        //    if (TileMap.CellCodeValue(Cell).StartsWith("NPC_"))
        //    {
        //        string[] code = TileMap.CellCodeValue(Cell).Split("_");

        //        if (code.Length != 5)
        //        {
        //            return;
        //        }

        //        StorySerializer ser = new StorySerializer();
        //        string path = Content.RootDirectory + @"/Dialogue/" + code[2] + ".xml";
        //        string XMLInputDatat = System.IO.File.ReadAllText(path);
        //        Dictionary<Guid, LinearStroyObject> Dic = ser.Deserialize(XMLInputDatat);

        //        NPCs.Add(new NPC(Content, code[1], Dic, new Rectangle(int.Parse(code[3]) * TileMap.TileWidth,
        //            int.Parse(code[4]) * TileMap.TileHeight, TileMap.TileWidth, TileMap.TileHeight)));


        //    }
        //}
        #endregion

        #region Public Methods


        private static void Transition()
        {

        }

        public static void LoadLevel(int levelNumber)
        {
            TileMap.LoadMap((System.IO.FileStream)TitleContainer.OpenStream(@"Content/Maps/MAP" + levelNumber.ToString().PadLeft(3, '0') + ".MAP"));

            snowFlakes.Clear();
            enemies.Clear();
            MeleeEnemies.Clear();
            NPCs.Clear();
            Events.Clear();
            Trees.Clear();
            Tiles.Clear();




            for (int x = 0; x < TileMap.MapWidth; x++)//THESE ARE IN CELLS NOT PIXELS
            {
                for (int y = 0; y < TileMap.MapHeight; y++)
                {
                    if (TileMap.CellCodeValue(x, y) == "START")
                    {
                        player.WorldLocation = new Vector2(
                            x * TileMap.TileWidth,
                            y * TileMap.TileHeight);

                    }

                  

                    if (TileMap.CellCodeValue(x, y) == "SNOWFLAKE")
                    {
                        snowFlakes.Add(new SnowFlakes(Content, x, y));
                    }

                    if (TileMap.CellCodeValue(x, y) == "LADDER")
                    {

                    }

                    if (TileMap.CellCodeValue(x, y) == "ENEMY")
                    {
                        Entities.Enemy E = new Entities.Enemy(Content, x, y);
                        enemies.Add(E);


                    }

                    if (TileMap.CellCodeValue(x,y) == "MELEEENEMY")
                    {
                        MeleeEnemy E = new MeleeEnemy(Content, x, y);
                        MeleeEnemies.Add(E);
                    }

                    if (TileMap.CellCodeValue(x,y) == "TREE")
                    {
                        Trees.Add(new Tree(Content, x, y));
                    }

                    if (TileMap.CellCodeValue(x,y).StartsWith("NPC_"))
                    {
                        string[] code = TileMap.CellCodeValue(x, y).Split("_");
                        NPCs.Add(code[1], new NPC(Content, code[2], x * TileMap.TileWidth, y * TileMap.TileHeight, code[3]));
                    }

                    if (TileMap.CellCodeValue(x,y).StartsWith("E_"))
                    {
                        string[] code = TileMap.CellCodeValue(x, y).Split("_");
                        int ET;

                        if (int.TryParse(code[1], out ET))
                        {
                            if (code.Length <= 2)
                            {
                                Events.Add(new EventBox(new Rectangle(x * TileMap.TileWidth, y * TileMap.TileHeight, TileMap.TileWidth, TileMap.TileHeight), ET));
                            }
                            else
                            {
                                Events.Add(new EventBox(new Rectangle(x * TileMap.TileWidth, y * TileMap.TileHeight, TileMap.TileWidth, TileMap.TileHeight), ET, code[2]));
                            }
                        }

                       
                    }

                    if (!TileMap.CellIsPassable(x,y))
                    {
                        Tiles.Add(new GameTile(TileMap.CellWorldRectangle(x, y),TileMap.CellCodeValue(x,y)));
                        
                    }

                    
                }

               
            }
            AssociateNPCWithEvents();
            currentLevel = levelNumber;
            respawnLocation = player.WorldLocation;
        }

        public static void ReloadLevel()
        {
            Vector2 saveRespawn = respawnLocation;
            LoadLevel(currentLevel);

            respawnLocation = saveRespawn;
            player.WorldLocation = respawnLocation;
        }

        public static void Update(GameTime gameTime)
        {
            foreach (KeyValuePair<string, NPC> npc in NPCs)
            {
                npc.Value.Update(gameTime);
            }

            foreach (SnowFlakes item in snowFlakes)
            {
                item.Update(gameTime);
            }

            foreach (Entities.Enemy enemy in enemies)
            {
                enemy.Update(gameTime);
            }
            foreach (MeleeEnemy enemy in MeleeEnemies)
            {
                enemy.Update(gameTime);
            }

            if (!player.Dead)
            {
                checkCurrentCellCode();

                //for (int x = coins.Count - 1; x >= 0; x--)
                //{
                //    coins[x].Update(gameTime);
                //    if (player.CollisionRectangle.Intersects(
                //        coins[x].CollisionRectangle))
                //    {
                //        coins.RemoveAt(x);
                //        player.Score += 10;
                //    }
                //}

                //for (int x = Tiles.Count - 1; x >= 0; x--)
                //{
                //    if (player.CollisionRectangle.Intersects(Tiles[x].CollisionRectnagle) && player.GetVelocity().Y >= 0.5f)
                //    {
                //        player.Score += 1;//Some Reason this is moveing the sprites when i move
                //    }
                //}

                //for (int x = enemies.Count - 1; x >= 0; x--)
                //{
                //    enemies[x].Update(gameTime);
                //    if (!enemies[x].Dead)
                //    {
                //        if (player.CollisionRectangle.Intersects(
                //            enemies[x].CollisionRectangle))
                //        {
                //            if (player.WorldCenter.Y < enemies[x].WorldLocation.Y)
                //            {
                //                player.Jump();
                //                player.Score += 5;
                //                enemies[x].PlayAnimation("die");
                //                enemies[x].Dead = true; ;
                //            }
                //            else
                //            {
                //                player.Kill();
                //            }
                //        }
                //    }
                //    else
                //    {
                //        if (!enemies[x].Enabled)
                //        {
                //            enemies.RemoveAt(x);
                //        }
                //    }
                //}

                DetectCollisions(gameTime);

            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (SnowFlakes coin in snowFlakes)
                coin.Draw(spriteBatch);

            foreach (Entities.Enemy enemy in enemies)
                enemy.Draw(spriteBatch);

            foreach (MeleeEnemy enemy in MeleeEnemies)
            {
                enemy.Draw(spriteBatch);
            }

            foreach (Tree tree in Trees)
            {
                tree.Draw(spriteBatch);
            }

            foreach (GameTile tile in Tiles)
            {
                tile.Draw(spriteBatch);
            }

            foreach (KeyValuePair<string, NPC> npc in NPCs)
            {
                npc.Value.Draw(spriteBatch);
            }

            if (Text != "" && Talker != null)
            {
                Rectangle position = Camera.WorldToScreen(Talker.WorldRectangle);
                
                spriteBatch.DrawString(spriteFont, Text, new Vector2(position.X, position.Y - 50) , Color.OrangeRed);
            }

            if (Display_EButton)
            {
                Rectangle position = Camera.WorldToScreen(player.WorldRectangle);
                position.Y = position.Y - 48;
                spriteBatch.Draw(E_button_Texture, position, Color.White);
            }


        }


        private static void DetectCollisions(GameTime time)
        {
            AABBCollisionDetector<GameTile, Player> Tile_Player_CollisionDetector = new AABBCollisionDetector<GameTile, Player>(Tiles);
            AABBCollisionDetector<GameTile, Player> Tile_Player_Ladder_collison_Detector = new AABBCollisionDetector<GameTile, Player>(Tiles.Where(x => x.CodeValue == "LADDER"));
            SegmentAABBCollisionDetector<Player> Player_Land_On_Tile_Collision = new SegmentAABBCollisionDetector<Player>(player);

            AABBCollisionDetector<EventBox, Player> Event_Player_CollisionDetector = new AABBCollisionDetector<EventBox, Player>(Events);

            AABBCollisionDetector<SnowFlakes, Player> Coin_Collector = new AABBCollisionDetector<SnowFlakes, Player>(snowFlakes);
            AABBCollisionDetector<Entities.Enemy, Player> PlayerRunsIntoEnemy = new AABBCollisionDetector<Entities.Enemy, Player>(enemies);
            AABBCollisionDetector<MeleeEnemy, Player> PlayerRunsIntoMeleeEnemy = new AABBCollisionDetector<MeleeEnemy, Player>(MeleeEnemies);
            SegmentAABBCollisionDetector<Player> EnemyRaycast = new SegmentAABBCollisionDetector<Player>(player);
            SegmentAABBCollisionDetector<Player> EnemyMeleeRaycast = new SegmentAABBCollisionDetector<Player>(player);



            if (!player.onGround)
            {
                Tile_Player_CollisionDetector.DetectTriggers(player, (Tile, P) =>
                {
                    GameTile something = Tile;
                    var LandEvent = new GameplayEvents.PlayerFallDamage(P.CalculateFallDamage(time));
                    P.OnNotify(LandEvent);
                });

                //List<Segment> segs = new List<Segment>();
                //foreach (GameTile Tile in Tiles)
                //{
                //    segs.Add(Tile.GroundCollisionSegment);
                //}
                //Player_Land_On_Tile_Collision.DetectCollisions(segs, (P) =>
                //{
                //    var LandEvent = new GameplayEvents.PlayerFallDamage(P.CalculateFallDamage(time));
                //    P.OnNotify(LandEvent);
                //});
            }

            Tile_Player_Ladder_collison_Detector.DetectCollisions(player, (Tile, P) =>
            {
                if (P.Climbing)
                {
                    Vector2 Location = TileMap.GetCellByPixel(Tile.WorldLocation);
                    TileMap.GetMapSquareAtCell((int)Location.X, (int)Location.Y).Passable = true;
                    TileMap.GetMapSquareAtCell((int)Location.X - 1, (int)Location.Y).Passable = true;

                }
                else
                {
                    Vector2 Location = TileMap.GetCellByPixel(Tile.WorldLocation);
                    TileMap.GetMapSquareAtCell((int)Location.X, (int)Location.Y).Passable = false;
                    TileMap.GetMapSquareAtCell((int)Location.X - 1, (int)Location.Y).Passable = false;
                }
            });

            if (!Event_Player_CollisionDetector.DetectTriggers(player, (Event, P) =>
            {
                Event.Triggerer = P;
                Event.OnNotify(Event.EventType);

            }))
            {
                LevelManager.Talker = null;
                LevelManager.Text = null;
                LevelManager.Display_EButton = false;
            }

            Coin_Collector.DetectCollisions(player, (Snowflake, P) =>
            {
                if (Snowflake.Enabled)
                {
                    P.OnNotify(new GameplayEvents.PlayerCoinPickupEvent());
                    Snowflake.Enabled = false;
                }
              

            });

            PlayerRunsIntoEnemy.DetectCollisions(player, (enemy, P) =>
            {
                if (P.Attacking == true)
                {
                    enemy.OnNotify(new GameplayEvents.DamageDealt(P.Damage));
                    if (enemy.Dead)
                    {
                        P.OnNotify(new GameplayEvents.PlayerKilledEnemyEvent());
                       
                    }
                }
            });

            PlayerRunsIntoMeleeEnemy.DetectCollisions(player, (enemy, P) =>
            {
                if (P.Attacking == true)
                {
                    enemy.OnNotify(new GameplayEvents.DamageDealt(P.Damage));
                    if (enemy.Dead)
                    {
                        P.OnNotify(new GameplayEvents.PlayerKilledEnemyEvent());

                    }
                }
            });

            foreach (Entities.Enemy enemy in enemies)
            {
                if (EnemyRaycast.DetectCollisions(enemy.Raycast, (player) =>
                {
                    if (enemy.CanAttack && enemy.Enabled)
                    {
                        enemy.FireWeapon();
                        enemy.CanAttack = false;
                        enemy.AttackTimer.Start();

                        player.OnNotify(new GameplayEvents.DamageDealt(enemy.Damage));
                    }

                }))
                {
                    enemy.Attacking = true;
                    enemy.Move = false;
                }
                else
                {
                    enemy.Attacking = false;
                    enemy.Move = true;
                }
            }

            foreach (MeleeEnemy enemy in MeleeEnemies)
            {
                if (EnemyRaycast.DetectCollisions(enemy.Raycast, (player) =>
                {
                    if (enemy.CanAttack && enemy.Enabled)
                    {
                        enemy.FireWeapon();
                        enemy.CanAttack = false;
                        enemy.AttackTimer.Start();

                        player.OnNotify(new GameplayEvents.DamageDealt(enemy.Damage));
                    }

                }))
                {
                    enemy.Attacking = true;
                    enemy.Move = false;
                }
                else
                {
                    enemy.Attacking = false;
                    enemy.Move = true;
                }
            }





        }

        public static void ReloadEnemies()
        {
            foreach (Entities.Enemy item in enemies)
            {
                item.Kill();
                item.Revive();
            }

            foreach (MeleeEnemy item in MeleeEnemies)
            {
                item.Kill();
                item.Revive();
            }
        }

        #endregion



    }
}
