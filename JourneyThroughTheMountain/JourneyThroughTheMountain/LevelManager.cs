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
        private static int currentLevel;

        private static Vector2 respawlocation;

        #endregion

        #region Properties
        public static int CurrentLevel
        {
            get { return currentLevel;}
        }

        public static Vector2 respawnLocation
        {
            get { return respawlocation; }
            set { respawlocation = value; }
        }
        #endregion

        #region Initialize

        public static void Initialize(ContentManager contentManager)
        {
            Content = contentManager;
        }

        #endregion

        #region Public Methods
        public static void LoadLevel(int levelnumber)
        {
            TileMap.LoadMap((System.IO.FileStream)TitleContainer.OpenStream("@Content/Maps/MAP" + levelnumber.ToString().PadLeft(3, '0') + ".MAP"));

            for (int x = 0; x < TileMap.MapWidth; x++)
            {
                for (int y = 0; y < TileMap.MapHeight; y++)
                {
                    if (TileMap.CellCodeValue(x,y) == "START")
                    {
                        //Player
                    }
                }
            }

            currentLevel = levelnumber;
            //respawn
        }

        public static void ReloadLevel()
        {
            
        }
        #endregion


    }
}