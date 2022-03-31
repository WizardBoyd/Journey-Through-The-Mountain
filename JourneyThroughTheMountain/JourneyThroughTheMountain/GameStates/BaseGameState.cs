using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace JourneyThroughTheMountain.GameStates
{
    public abstract class BaseGameState
    {

        private readonly List<GameObject> _GameObjects = new List<GameObject>();

        public abstract void LoadContent(ContentManager contentManager);

        public abstract void UnloadContent(ContentManager contentManager);

        public abstract void HandleInput();

        public event EventHandler<BaseGameState> OnStateSwitched;

        protected void SwitchState(BaseGameState gameState)
        {
            OnStateSwitched?.Invoke(this, gameState);
        }

        protected void AddGameObject(GameObject GObject)
        {
            _GameObjects.Add(GObject);
        }

        public void Render(SpriteBatch spriteBatch)
        {
            foreach (var GObject in _GameObjects)
            {
                GObject.Draw(spriteBatch);
            }
        }

    }
}
