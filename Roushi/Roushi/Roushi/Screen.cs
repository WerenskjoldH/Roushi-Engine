using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

/**
 * By: Hunter Werenskjold
 */

namespace Roushi
{
    public class Screen
    {
        protected ContentManager content;

        public Type Type;

        public Color backgroundColor;

        public Screen()
        {
            Type = this.GetType();
            backgroundColor = Color.Black;
        }

        public virtual void Initialize() { } // triggers before content is loaded so if you want to change how the screen is created you can modify it here before anything happens

        public virtual void LoadContent()
        {
            Initialize();
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content"); // instantiate the content variable so you can use the content pipeline in any screen
            ScreenManager.Instance.BackgroundColor = backgroundColor;
        }

        public virtual void UnloadContent()
        {
            ScreenManager.Instance.Camera.Focus = null;
            content.Unload();
        }

        public virtual void Update(GameTime gameTime)
        {
            InputManager.Instance.Update(); // update the input manager
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
