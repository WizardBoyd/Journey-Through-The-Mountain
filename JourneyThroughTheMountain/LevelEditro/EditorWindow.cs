using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Forms.Controls;
using TileEngine;

namespace LevelEditro
{
    public class EditorWindow : MonoGameControl
    {
        public static System.Windows.Forms.Form Appform;


        public int DrawLayer = 0;
        public int DrawTile;
        public bool EditingCode = false;
        public string CurrentCodeValue = "";
        public string HoverCodeValue = "";

        public MouseState lastMouseState;
        System.Windows.Forms.VScrollBar vScroll;
        System.Windows.Forms.HScrollBar hScroll;
        System.Windows.Forms.ListBox DebugBox;

        public EditorWindow()
        {
            
        }

        protected override void Initialize()
        {
            base.Initialize();
            vScroll = (System.Windows.Forms.VScrollBar)Appform.Controls["vScrollBar1"];
            hScroll = (System.Windows.Forms.HScrollBar)Appform.Controls["hScrollBar1"];
            DebugBox = (System.Windows.Forms.ListBox)Appform.Controls["LstDebugBox"];

            Appform.SizeChanged += MainForm_SizeChanged;


            LoadContent();
            

        }

        void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (Editor != null)
            {
                
                Editor.graphics.Viewport = new Microsoft.Xna.Framework.Graphics.Viewport(0, 0, this.Width, this.Height);
                Camera.ViewPortWidth = this.Width;
                Camera.ViewPortHeight = this.Height;
            }

        }

        protected override void Draw()
        {
            base.Draw();

            GraphicsDevice.Clear(Color.CornflowerBlue);

            Editor.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            TileMap.Draw(Editor.spriteBatch);
            Editor.spriteBatch.End();

            
        }

        protected virtual void LoadContent()
        {
            Camera.ViewPortWidth = this.Width;
            Camera.ViewPortHeight = this.Height;
            Camera.WorldRectangle = new Rectangle(0, 0, TileMap.TileWidth * TileMap.MapWidth, TileMap.TileHeight * TileMap.MapHeight);

            TileMap.Initialize(Editor.Content.Load<Texture2D>(@"PlatformTiles"));
            //TileMap.spriteFont = Editor.Content.Load<SpriteFont>(@"Pericles7");

            lastMouseState = Mouse.GetState();

        }

        protected override void Update(GameTime gameTime)
        {
            Camera.Position = new Vector2(hScroll.Value, vScroll.Value);

            MouseState ms = Mouse.GetState();
            //IntPtr myhandle = Handle;
            //IntPtr handle = Mouse.WindowHandle;

            DebugBox.Items.Add($"{ms.RightButton == ButtonState.Pressed}, {ms.LeftButton == ButtonState.Pressed}");

            if ((ms.X > 0) && (ms.Y > 0) &&
                (ms.X < Camera.ViewPortWidth) &&
                (ms.Y < Camera.ViewPortHeight))
            {
                Vector2 mouseLoc = Camera.ScreenToWorld(new Vector2(ms.X, ms.Y));

                if (Camera.WorldRectangle.Contains((int)mouseLoc.X, (int)mouseLoc.Y))
                {
                    if (ms.LeftButton == ButtonState.Pressed)
                    {
                       
                        TileMap.SetTileAtCell(TileMap.GetCellByPixelX((int)mouseLoc.X),
                            TileMap.GetCellByPixelY((int)mouseLoc.Y),
                            DrawLayer,
                            DrawTile);
                    }

                    if ((ms.RightButton == ButtonState.Pressed) && (lastMouseState.RightButton == ButtonState.Released))
                    {
                        if (EditingCode)
                        {
                            TileMap.GetMapSquareAtCell(TileMap.GetCellByPixelX((int)mouseLoc.X),
                                TileMap.GetCellByPixelY((int)mouseLoc.Y)).CodeValue = CurrentCodeValue;
                        }
                        else
                        {
                            TileMap.GetMapSquareAtCell(TileMap.GetCellByPixelX((int)mouseLoc.X),
                                TileMap.GetCellByPixelY((int)mouseLoc.Y)).TogglePassable();
                        }
                    }
                    HoverCodeValue = TileMap.GetMapSquareAtCell(TileMap.GetCellByPixelX((int)mouseLoc.X), TileMap.GetCellByPixelY((int)mouseLoc.Y)).CodeValue;
                }


                lastMouseState = ms;

                base.Update(gameTime);
            }




        }
    }
}
