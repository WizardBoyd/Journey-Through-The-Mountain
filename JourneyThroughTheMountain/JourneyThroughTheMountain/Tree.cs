using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TileEngine;

namespace JourneyThroughTheMountain
{
    public class Tree : GameObject
    {

        public Tree(ContentManager Content, int cellX, int cellY)
        {
            worldLocation.X = TileMap.TileWidth * cellX;
            worldLocation.Y = TileMap.TileHeight * cellY;

            

            string[] treePaths = Directory.GetFiles(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) +"\\Content\\Objects\\Trees");

            System.Random rnd = new Random();

            string[] TreeItem = treePaths[rnd.Next(treePaths.Length)].Split("Content\\");
            string TreeItemsplitted = TreeItem[TreeItem.Length - 1].Replace(".xnb", "");

            Texture2D Tree = Content.Load<Texture2D>(TreeItemsplitted);

            frameWidth = Tree.Width;
            frameHeight = Tree.Height;

            animations.Add("Tree",
               new AnimationStrip(
                   Tree,
                   frameWidth,
                   "idle"));

            
            drawDepth = 0.55f;
            

            enabled = true;
            PlayAnimation("Tree");

        }

    }
}
