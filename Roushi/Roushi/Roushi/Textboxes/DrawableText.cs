using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Roushi
{
    class DrawableText : Dialog
    {
        string text;
        SpriteFont spriteFont;
        Vector2 position;
        Color color;

        public DrawableText(string Text, string FontName, Vector2 Position, Color Color, DialogLocation dl)
        {
            text = Text;
            spriteFont = ScreenManager.Instance.Content.Load<SpriteFont>("Fonts/" + FontName);
            position = new Vector2(Position.X - (int)spriteFont.MeasureString(text).X / 2, Position.Y - (int)spriteFont.MeasureString(text).Y / 2);
            color = Color;
            dialogLocation = dl;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, text, position, color);
        }
    }
}
