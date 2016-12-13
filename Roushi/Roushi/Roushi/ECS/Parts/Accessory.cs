using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Roushi
{
    class Accessory
    {
        public Boolean visible;
        public string name;
        Texture2D texture;
        Vector2 offset;
        Color color;
        float rotation;

        public Accessory(string Name, Vector2 Offset, Color Color, float Rotation)
        {
            texture = ScreenManager.Instance.Content.Load<Texture2D>("Accessories/" + Name);
            name = Name;
            offset = Offset;
            color = Color;
            rotation = Rotation;
            visible = false;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            if(visible)
                spriteBatch.Draw(texture, position + offset, new Rectangle(0, 0, texture.Width, texture.Height), color, rotation, new Vector2(texture.Width / 2, texture.Height), 1f, SpriteEffects.None, 0);
        }

    }
}
