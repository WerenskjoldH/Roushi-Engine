using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Roushi
{

    class HudElement
    {
        public HudElement()
        { }

        // What happens when the part initializes
        public virtual void Initialize() { }

        // What happens when the part cleans up
        public virtual void CleanUp() { }

        // Update logic
        public virtual void Update(GameTime gameTime)
        { }

        public virtual void Draw(SpriteBatch spriteBatch)
        { }
    }

    class HudManager
    {
        ArrayList elements;

        public HudManager()
        {
            elements = new ArrayList();
        }

        public void AddElement(HudElement e)
        {
            e.Initialize();
            elements.Add(e);
        }

        public T GetElement<T>() where T : HudElement
        {
            foreach (HudElement e in elements)
            {
                if (e.GetType() == typeof(T))
                {
                    return (T)e;
                }
            }
            return null;
        }

        public void Update(GameTime gameTime)
        {
            foreach (HudElement e in elements)
            {
                e.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (HudElement e in elements)
            {
                e.Draw(spriteBatch);
            }
        }

    }
}
