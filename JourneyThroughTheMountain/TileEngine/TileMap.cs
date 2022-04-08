﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TileEngine
{
    public static class TileMap
    {
        #region Declarations
        public const int TileWidth = 32;
        public const int TileHeight = 32;
        public const int MapWidth = 50;
        public const int MapHeight = 50;
        public const int MapLayers = 3;
        private const int skyTile = 0;

        static private MapSquare[,] mapCells =
            new MapSquare[MapWidth, MapHeight];

        public static bool EditorMode = false;

        public static SpriteFont spriteFont;
        static private Texture2D tileSheet;
        #endregion

        #region Initialization
        static public void Initialize(Texture2D tileTexture)
        {
            tileSheet = tileTexture;

            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    for (int z = 0; z < MapLayers; z++)
                    {
                        mapCells[x, y] = new MapSquare(skyTile, 0, 0, "", true);
                    }
                }
            }
        }
        #endregion

        #region Tile and Tile Sheet Handling
        public static int TilesPerRow
        {
            get { return tileSheet.Width / TileWidth; }
        }

        public static Rectangle TileSourceRectangle(int tileIndex)
        {
            return new Rectangle(
                (tileIndex % TilesPerRow) * TileWidth,
                (tileIndex / TilesPerRow) * TileHeight,
                TileWidth,
                TileHeight);
        }
        #endregion

        #region Information about Map Cells
        static public int GetCellByPixelX(int pixelX)
        {
            return pixelX / TileWidth;
        }

        static public int GetCellByPixelY(int pixelY)
        {
            return pixelY / TileHeight;
        }

        static public int GetPixelByCellY(int CellY)
        {
            return CellY * TileHeight;
        }

        static public int GetPixelByCellX(int CellX)
        {
            return CellX * TileWidth;
        }

        static public Vector2 GetCellByPixel(Vector2 pixelLocation)
        {
            return new Vector2(
                GetCellByPixelX((int)pixelLocation.X),
                GetCellByPixelY((int)pixelLocation.Y));
        }

        static public Vector2 GetPixelByCell(Vector2 cellLocation)
        {
            return new Vector2(
                GetPixelByCellX((int)cellLocation.X),
                GetPixelByCellY((int)cellLocation.Y));
        }

        static public Vector2 GetCellCenter(int cellX, int cellY)
        {
            return new Vector2(
                (cellX * TileWidth) + (TileWidth / 2),
                (cellY * TileHeight) + (TileHeight / 2));
        }

        static public Vector2 GetCellCenter(Vector2 cell)
        {
            return GetCellCenter(
                (int)cell.X,
                (int)cell.Y);
        }

        static public Rectangle CellWorldRectangle(int cellX, int cellY)//GETS THE RECTANGLE IN WORLD IN PIXELS
        {
            return new Rectangle(
                cellX * TileWidth,
                cellY * TileHeight,
                TileWidth,
                TileHeight);
        }

        static public Rectangle CellWorldRectangle(Vector2 cell)
        {
            return CellWorldRectangle(
                (int)cell.X,
                (int)cell.Y);
        }

        /// <summary>
        /// Should take the cell numbers and convert it into a rectangle for pixel world
        /// </summary>
        /// <param name="CellX"></param>
        /// <param name="CellY"></param>
        /// <returns></returns>
        static public Rectangle PixelWorldRectangle(int CellX, int CellY)
        {
            return new Rectangle(
                (int)(CellX / TileWidth),
                (int)(CellY / TileHeight),
                TileWidth,
                TileHeight);
        }

        static public Rectangle CellScreenRectangle(int cellX, int cellY)//TAKES IN CELL NUMBERS
        {
            return Camera.WorldToScreen(CellWorldRectangle(cellX, cellY));
        }

        static public Rectangle CellSreenRectangle(Vector2 cell)
        {
            return CellScreenRectangle((int)cell.X, (int)cell.Y);
        }

        static public bool CellIsPassable(int cellX, int cellY)
        {
            MapSquare square = GetMapSquareAtCell(cellX, cellY);

            if (square == null)
                return false;
            else
                return square.Passable;
        }

        static public bool CellIsPassable(Vector2 cell)
        {
            return CellIsPassable((int)cell.X, (int)cell.Y);
        }

        static public bool CellIsPassableByPixel(Vector2 pixelLocation)
        {
            return CellIsPassable(
                GetCellByPixelX((int)pixelLocation.X),
                GetCellByPixelY((int)pixelLocation.Y));
        }

        static public string CellCodeValue(int cellX, int cellY)
        {
            MapSquare square = GetMapSquareAtCell(cellX, cellY);

            if (square == null)
                return "";
            else
                return square.CodeValue;
        }

        static public string CellCodeValue(Vector2 cell)
        {
            return CellCodeValue((int)cell.X, (int)cell.Y);
        }



        //private static IEnumerable<T[]> Filter<T>(T[,] source, Func<T[], bool> predicate)
        //{
        //    for (int i = 0; i < source.GetLength(0); i++)
        //    {
        //        T[] values = new T[source.GetLength(1)];
        //        for (int j = 0; j < values.Length; j++)
        //        {
        //            values[j] = source[i, j];
        //        }
        //        if (predicate(values))
        //        {
        //            yield return values;
        //        }
        //    }
        //}
        #endregion

        #region Information about MapSquare objects
        static public MapSquare GetMapSquareAtCell(int tileX, int tileY)
        {
            if ((tileX >= 0) && (tileX < MapWidth) &&
                (tileY >= 0) && (tileY < MapHeight))
            {
                return mapCells[tileX, tileY];
            }
            else
            {
                return null;
            }
        }

        static public void SetMapSquareAtCell(
            int tileX,
            int tileY,
            MapSquare tile)
        {
            if ((tileX >= 0) && (tileX < MapWidth) &&
                (tileY >= 0) && (tileY < MapHeight))
            {
                mapCells[tileX, tileY] = tile;
            }
        }

        static public void SetTileAtCell(
            int tileX,
            int tileY,
            int layer,
            int tileIndex)
        {
            if ((tileX >= 0) && (tileX < MapWidth) &&
                (tileY >= 0) && (tileY < MapHeight))
            {
                mapCells[tileX, tileY].LayerTiles[layer] = tileIndex;
            }
        }

        static public MapSquare GetMapSquareAtPixel(int pixelX, int pixelY)
        {
            return GetMapSquareAtCell(
                GetCellByPixelX(pixelX),
                GetCellByPixelY(pixelY));
        }

        static public MapSquare GetMapSquareAtPixel(Vector2 pixelLocation)
        {
            return GetMapSquareAtPixel(
                (int)pixelLocation.X,
                (int)pixelLocation.Y);
        }

        #endregion

        #region Loading and Saving Maps
        public static void SaveMap(FileStream fileStream)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fileStream, mapCells);
            fileStream.Close();
        }

        public static void LoadMap(FileStream fileStream)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                mapCells = (MapSquare[,])formatter.Deserialize(fileStream);
                mapCells = ResizeArray<MapSquare>(mapCells, MapWidth, MapHeight);
                for (int x = 0; x < mapCells.GetLength(0); x++)
                {
                    for (int y = 0; y < mapCells.GetLength(1); y++)
                    {
                        if (mapCells[x,y] == null)
                        {
                            mapCells[x, y] = new MapSquare(skyTile, 0, 0, "", true);
                        }
                    }
                }
                fileStream.Close();
            }
            catch
            {
                ClearMap();
            }
        }

        public static void ClearMap()
        {
            for (int x = 0; x < MapWidth; x++)
                for (int y = 0; y < MapHeight; y++)
                    for (int z = 0; z < MapLayers; z++)
                    {
                        mapCells[x, y] = new MapSquare(0, 0, 0, "", true);
                    }
        }
        #endregion

        #region Drawing
        static public void Draw(SpriteBatch spriteBatch)
        {
            int startX = GetCellByPixelX((int)Camera.Position.X);
            int endX = GetCellByPixelX((int)Camera.Position.X +
                  Camera.ViewPortWidth);

            int startY = GetCellByPixelY((int)Camera.Position.Y);//THE START's GET THE WHOLE BOX TO RENDER AKA TOP BOTTOM RIGHT AND LEFT
            int endY = GetCellByPixelY((int)Camera.Position.Y +
                      Camera.ViewPortHeight);

            for (int x = startX; x <= endX; x++)
                for (int y = startY; y <= endY; y++)
                {
                    for (int z = 0; z < MapLayers; z++)
                    {
                        if ((x >= 0) && (y >= 0) &&
                            (x < MapWidth) && (y < MapHeight))
                        {

                                spriteBatch.Draw(
                             tileSheet,
                             CellScreenRectangle(x, y),
                             TileSourceRectangle(mapCells[x, y].LayerTiles[z]),
                             Color.White,
                             0.0f,
                             Vector2.Zero,
                             SpriteEffects.None,
                             1f - ((float)z * 0.1f));
                            
                        }
                    }

                    if (EditorMode)
                    {
                        DrawEditModeItems(spriteBatch, x, y);
                    }

                }
        }

        public static void DrawEditModeItems(
            SpriteBatch spriteBatch,
            int x,
            int y)
        {
            if ((x < 0) || (x >= MapWidth) ||
                (y < 0) || (y >= MapHeight))
                return;

            if (!CellIsPassable(x, y))
            {
                spriteBatch.Draw(
                                tileSheet,
                                CellScreenRectangle(x, y),
                                TileSourceRectangle(1),
                                new Color(255, 0, 0, 80),
                                0.0f,
                                Vector2.Zero,
                                SpriteEffects.None,
                                0.0f);
            }

            if (mapCells[x, y].CodeValue != "")
            {
                Rectangle screenRect = CellScreenRectangle(x, y);

                spriteBatch.DrawString(
                    spriteFont,
                    mapCells[x, y].CodeValue,
                    new Vector2(screenRect.X, screenRect.Y),
                    Color.White,
                    0.0f,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    0.0f);
            }
        }
        #endregion

        #region Helper
       static T[,] ResizeArray<T>(T[,] original, int rows, int columns)
        {
            var newArray = new T[rows, columns];
            int minrows = Math.Min(rows, original.GetLength(0));
            int mincols = Math.Min(columns, original.GetLength(1));

            for (int i = 0; i < minrows; i++)
            {
                for (int j = 0; j < mincols; j++)
                {
                    newArray[i, j] = original[i, j];
                }
            }
            return newArray;
        }
        #endregion




    }
}
