using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using JourneyThroughTheMountain.Input;
using JourneyThroughTheMountain.GameObjects;
using JourneyThroughTheMountain.Sound;

namespace JourneyThroughTheMountain.GameStates
{
    public abstract class BaseGameState
    {
        private const string FallbackTexture = "Empty";
        private const String FallBackSong = "EmptySong";

        protected bool _degub = false;
        protected ContentManager _contentManager;
        protected int _viewportHeight;
        protected int _viewportWidth;
        protected SoundManager _soundManager = new SoundManager();

        private readonly List<BaseGameObject> _GameObjects = new List<BaseGameObject>();

        protected InputManager InputManager { get; set; }

        public void Initialize(ContentManager contentManager, int ViewportWidth, int ViewportHeight)
        {
            _contentManager = contentManager;
            _viewportHeight = ViewportHeight;
            _viewportWidth = ViewportWidth;

            SetupInputManager();
        }

        public abstract void LoadContent();

        public void UnloadContent()
        {
            _contentManager.Unload();
        }

        public abstract void UpdateGameState(GameTime time);

        public void Update(GameTime Time)
        {
           
            UpdateGameState(Time);
            _soundManager.PlaySoundTrack();
        }

        protected Texture2D LoadTexture(String textureName)
        {
            return _contentManager.Load<Texture2D>(textureName);
        }

        protected SoundEffect LoadSound(string soundname)
        {
            return _contentManager.Load<SoundEffect>(soundname);
        }

        public abstract void HandleInput(GameTime time);

        public event EventHandler<BaseGameState> OnStateSwitched;
        public event EventHandler<BaseGameStateEvent> OnEventNotification;

        protected abstract void SetupInputManager();

        protected void SwitchState(BaseGameState gameState)
        {
            OnStateSwitched?.Invoke(this, gameState);
        }

        public void NotifyEvent(BaseGameStateEvent gameEvent)
        {
            OnEventNotification?.Invoke(this, gameEvent);
            foreach (var item in _GameObjects)
            {
                //NotifyGameObjects This Way
            }

            _soundManager.OnNotify(gameEvent);
        }

        protected void AddGameObject(BaseGameObject GObject)
        {
            _GameObjects.Add(GObject);
        }

        protected void RemoveGameObject(BaseGameObject gameObject)
        {
            _GameObjects.Remove(gameObject);
        }

        public virtual void Render(SpriteBatch spriteBatch)
        {
            foreach (var GObject in _GameObjects)
            {
                if (_degub)
                {
                    //RenderDebugStuff
                }

                GObject.Render(spriteBatch);
            }
        }

    }
}
