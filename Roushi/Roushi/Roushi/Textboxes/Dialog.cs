using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Roushi
{
    public class Dialog
    {
        public Boolean isAlive;
        public DialogLocation dialogLocation;
        public virtual void Initialize() { isAlive = true; } // triggers before content is loaded so if you want to change how the screen is created you can modify it here before anything happens

        public virtual void LoadContent()
        {
            Initialize();
        }

        public virtual void UnloadContent()
        {      }

        public virtual void Update(GameTime gameTime)
        {      }

        public virtual void Draw(SpriteBatch spriteBatch)
        {      }

    }
}
