//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using System;
//using TileEngine;


//namespace LevelEditro
//{
//    //public class Game1 : Game
//    //{
//    //    private GraphicsDeviceManager _graphics;
//    //    private SpriteBatch _spriteBatch;

//    //    IntPtr DrawSurface;
//    //    System.Windows.Forms.Form ParentForm;
//    //    System.Windows.Forms.PictureBox PictureBox;
//    //    System.Windows.Forms.Control GameForm;
//    //    IntPtr other;
//    //    IntPtr test;
//    //    GameWindow NewWindow;



//    //    public int DrawLayer = 0;
//    //    public int DrawTile = 0;
//    //    public bool EditingCode = false;
//    //    public string CurrentCodeValue = "";
//    //    public string HoverCodeValue = "";

//    //    public MouseState lastMouseState;
//    //    System.Windows.Forms.VScrollBar vScroll;
//    //    System.Windows.Forms.HScrollBar hScroll;
//    //    System.Windows.Forms.ListBox DebugBox;

//    //    public Game1(IntPtr drawSurface,
//    //        System.Windows.Forms.Form parentForm,
//    //        System.Windows.Forms.PictureBox surfacePictureBox)
//    //    {
//    //        _graphics = new GraphicsDeviceManager(this);
//    //        Content.RootDirectory = "Content";
//    //        //IsMouseVisible = true;
            
//    //        this.DrawSurface = drawSurface;
//    //        this.ParentForm = parentForm;
//    //        this.PictureBox = surfacePictureBox;

//    //        _graphics.PreparingDeviceSettings += new EventHandler<PreparingDeviceSettingsEventArgs>(graphics_PreparingDeviceSettings);//Do something here
//    //         test = this.ParentForm.Handle;
//    //         other = Mouse.WindowHandle;
//    //        Mouse.WindowHandle = drawSurface;
           
           

//    //        GameForm = System.Windows.Forms.Control.FromHandle(this.Window.Handle);
//    //        GameForm.VisibleChanged += new EventHandler(gameForm_VisibleChanged);
//    //        GameForm.SizeChanged += new EventHandler(pictureBox_SizeChanged);

//    //        vScroll = (System.Windows.Forms.VScrollBar)parentForm.Controls["vScrollBar1"];
//    //        hScroll = (System.Windows.Forms.HScrollBar)parentForm.Controls["hScrollBar1"];
//    //        DebugBox = (System.Windows.Forms.ListBox)parentForm.Controls["LstDebugBox"];

//    //        NewWindow = GameWindow.Create(this, PictureBox.Width, PictureBox.Height);
//    //    }

//    //    private void gameForm_VisibleChanged(object sender, EventArgs e)
//    //    {
//    //        if (GameForm.Visible == true)
//    //        {
//    //           GameForm.Visible = false;
//    //        }
//    //    }

//    //    void pictureBox_SizeChanged(object sender, EventArgs e)
//    //    {
//    //        if (ParentForm.WindowState != System.Windows.Forms.FormWindowState.Minimized)
//    //        {
//    //            _graphics.PreferredBackBufferWidth = PictureBox.Width;
//    //            _graphics.PreferredBackBufferHeight = PictureBox.Height;
//    //            Camera.ViewPortWidth = PictureBox.Width;
//    //            Camera.ViewPortHeight = PictureBox.Height;

//    //            _graphics.ApplyChanges();
//    //        }
//    //    }

//    //    void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
//    //    {
//    //        e.GraphicsDeviceInformation.PresentationParameters.DeviceWindowHandle = DrawSurface;
//    //    }

//    //    protected override void Initialize()
//    //    {
//    //        // TODO: Add your initialization logic here

//    //        base.Initialize();
//    //    }

//    //    protected override void LoadContent()
//    //    {
//    //        _spriteBatch = new SpriteBatch(GraphicsDevice);

//    //        // TODO: use this.Content to load your game content here
//    //        Camera.ViewPortWidth = PictureBox.Width;
//    //        Camera.ViewPortHeight = PictureBox.Height;
//    //        Camera.WorldRectangle = new Rectangle(0, 0, TileMap.TileWidth * TileMap.MapWidth, TileMap.TileHeight * TileMap.MapHeight);

//    //        TileMap.Initialize(Content.Load<Texture2D>(@"CastleTilemap"));
//    //        TileMap.spriteFont = Content.Load<SpriteFont>(@"Pericles7");

//    //        lastMouseState = Mouse.GetState();

//    //        pictureBox_SizeChanged(null, null);
//    //    }

//    //    protected override void Update(GameTime gameTime)
//    //    {
//    //        Camera.Position = new Vector2(hScroll.Value, vScroll.Value);
//    //        IntPtr other = Mouse.WindowHandle;
//    //        bool active = this.IsActive;
//    //        MouseState ms = Mouse.GetState(Window);
//    //        int test = Mouse.GetState().X;

//    //        System.Diagnostics.Debug.WriteLine($"{ms.X} : {ms.Y}");


//    //        DebugBox.Items.Add($" MS.x: {test}");
//    //       // DebugBox.Items.Add($" MS.y: {ms.Y}");

//    //        if ((ms.X > 0) && (ms.Y > 0) &&
//    //            (ms.X < Camera.ViewPortWidth) &&
//    //            (ms.Y < Camera.ViewPortHeight))
//    //        {
//    //            Vector2 mouseLoc = Camera.ScreenToWorld(new Vector2(ms.X, ms.Y));

//    //            if (Camera.WorldRectangle.Contains((int)mouseLoc.X, (int)mouseLoc.Y))
//    //            {
//    //                if (ms.LeftButton == ButtonState.Pressed)
//    //                {
//    //                    TileMap.SetTileAtCell(TileMap.GetCellByPixelX((int)mouseLoc.X),
//    //                        TileMap.GetCellByPixelY((int)mouseLoc.Y),
//    //                        DrawLayer,
//    //                        DrawTile);
//    //                }

//    //                if ((ms.RightButton == ButtonState.Pressed) && (lastMouseState.RightButton == ButtonState.Released))
//    //                {
//    //                    if (EditingCode)
//    //                    {
//    //                        TileMap.GetMapSquareAtCell(TileMap.GetCellByPixelX((int)mouseLoc.X),
//    //                            TileMap.GetCellByPixelY((int)mouseLoc.Y)).CodeValue = CurrentCodeValue;
//    //                    }
//    //                    else
//    //                    {
//    //                        TileMap.GetMapSquareAtCell(TileMap.GetCellByPixelX((int)mouseLoc.X),
//    //                            TileMap.GetCellByPixelY((int)mouseLoc.Y)).TogglePassable();
//    //                    }
//    //                }
//    //                HoverCodeValue = TileMap.GetMapSquareAtCell(TileMap.GetCellByPixelX((int)mouseLoc.X), TileMap.GetCellByPixelY((int)mouseLoc.Y)).CodeValue;
//    //            }
//    //        }

//    //        lastMouseState = ms;

//    //        base.Update(gameTime);
//    //    }

//    //    protected override void Draw(GameTime gameTime)
//    //    {
//    //        GraphicsDevice.Clear(Color.CornflowerBlue);

//    //        // TODO: Add your drawing code here

//    //        _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
//    //        TileMap.Draw(_spriteBatch);
//    //        _spriteBatch.End();

//    //        base.Draw(gameTime);
//    //    }
//    }
//}
