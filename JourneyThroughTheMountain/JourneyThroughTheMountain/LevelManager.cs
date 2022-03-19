using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using TileEngine;

namespace JourneyThroughTheMountain
{
    public static class LevelManager
    {
        #region Declarations
        private static ContentManager Content;
        private static Player player;
        private static int currentLevel;
        private static Vector2 respawnLocation;

        private static List<Coin> coins = new List<Coin>();
        private static List<Enemy> enemies = new List<Enemy>();
        private static List<Tree> Trees = new List<Tree>();
        private static List<GameTile> Tiles = new List<GameTile>();
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
            Player gamePlayer)
        {
            Content = content;
            player = gamePlayer;
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
        #endregion

        #region Public Methods
        public static void LoadLevel(int levelNumber)
        {
            TileMap.LoadMap((System.IO.FileStream)TitleContainer.OpenStream(@"Content/Maps/MAP" + levelNumber.ToString().PadLeft(3, '0') + ".MAP"));

            coins.Clear();
            enemies.Clear();


            for (int x = 0; x < TileMap.MapWidth; x++)
            {
                for (int y = 0; y < TileMap.MapHeight; y++)
                {
                    if (TileMap.CellCodeValue(x, y) == "START")
                    {
                        player.WorldLocation = new Vector2(
                            x * TileMap.TileWidth,
                            y * TileMap.TileHeight);

                    }

                    if (TileMap.CellCodeValue(x, y) == "GEM")
                    {
                        coins.Add(new Coin(Content, x, y));
                    }

                    if (TileMap.CellCodeValue(x, y) == "ENEMY")
                    {
                        enemies.Add(new Enemy(Content, x, y));
                    }

                    if (TileMap.CellCodeValue(x,y) == "TREE")
                    {
                        Trees.Add(new Tree(Content, x, y));
                    }

                    if (!TileMap.CellIsPassable(x,y))
                    {
                        Tiles.Add(new GameTile(TileMap.CellWorldRectangle(x, y))); 
                    }

                    
                }

               
            }

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

                DetectCollisions();

            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (Coin coin in coins)
                coin.Draw(spriteBatch);

            foreach (Enemy enemy in enemies)
                enemy.Draw(spriteBatch);

            foreach (Tree tree in Trees)
            {
                tree.Draw(spriteBatch);
            }

            foreach (GameTile tile in Tiles)
            {
                tile.Draw(spriteBatch);
            }

        }

        private static void DetectCollisions()
        {
            AABBCollisionDetector<GameTile, Player> Tile_Player_CollisionDetector = new AABBCollisionDetector<GameTile, Player>(Tiles);

            Tile_Player_CollisionDetector.DetectTriggers(player, (Tile, P) =>
            {
                if (P.TakeDamageOnLand)
                {
                    //Have the player take Damage
                    //MAKE A LINE SEGMENT FOR WHEN YOU ARE IN THE AIR AND MONITER IT THEN OR SOMETHING LIKE THAT
                    
                }
            });
        }

        #endregion



    }
}
