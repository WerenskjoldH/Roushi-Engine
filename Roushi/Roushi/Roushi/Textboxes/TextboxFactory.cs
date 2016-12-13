using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections;
namespace Roushi
{
    class TextboxFactory
    {
        private static Texture2D CreateRectangle(Vector2 size, Color color)
        {

            Texture2D texture = ScreenManager.Instance.Content.Load<Texture2D>("Particles/blackPixel");
            RenderTarget2D RT = new RenderTarget2D(ScreenManager.Instance.GraphicsDevice, (int)size.X, (int)size.Y);

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(RT);
            ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);
            ScreenManager.Instance.SpriteBatch.Begin();

            ScreenManager.Instance.SpriteBatch.Draw(texture, Vector2.Zero, new Rectangle(0, 0, (int)texture.Width, (int)texture.Height), color, 0f, Vector2.Zero, new Vector2(10, 10), SpriteEffects.None, 0);

            ScreenManager.Instance.SpriteBatch.End();

            texture = RT;

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);

            return texture;
        }

        public static Texture2D DefaultTextbox(Vector2 size, Color color)
        {
            int lineThickness = 4;

            Texture2D pixelTexture = ScreenManager.Instance.Content.Load<Texture2D>("Particles/blackPixel");
            //Texture2D peg = CreateRectangle(new Vector2(20, 20), color);
            RenderTarget2D RT = new RenderTarget2D(ScreenManager.Instance.GraphicsDevice, (int)size.X+lineThickness, (int)size.Y+lineThickness);

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(RT);
            ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);
            ScreenManager.Instance.SpriteBatch.Begin();

            //Border Lines
            ScreenManager.Instance.SpriteBatch.Draw(pixelTexture, new Vector2(0, 0), new Rectangle(0, 0, (int)pixelTexture.Width, (int)pixelTexture.Height), color, 0f, Vector2.Zero, new Vector2(size.X , lineThickness), SpriteEffects.None, 0);
            ScreenManager.Instance.SpriteBatch.Draw(pixelTexture, new Vector2(0, size.Y), new Rectangle(0, 0, (int)pixelTexture.Width, (int)pixelTexture.Height), color, 0f, Vector2.Zero, new Vector2(size.X , lineThickness), SpriteEffects.None, 0);
            ScreenManager.Instance.SpriteBatch.Draw(pixelTexture, new Vector2(0, 0), new Rectangle(0, 0, (int)pixelTexture.Width, (int)pixelTexture.Height), color, 0f, Vector2.Zero, new Vector2(lineThickness , size.Y), SpriteEffects.None, 0);
            ScreenManager.Instance.SpriteBatch.Draw(pixelTexture, new Vector2(size.X, 0), new Rectangle(0, 0, (int)pixelTexture.Width, (int)pixelTexture.Height), color, 0f, Vector2.Zero, new Vector2(lineThickness , size.Y+lineThickness), SpriteEffects.None, 0);
            
            //Background
            ScreenManager.Instance.SpriteBatch.Draw(pixelTexture, new Vector2(0,  0), new Rectangle(0, 0, (int)pixelTexture.Width, (int)pixelTexture.Height), new Color(color.R, color.G, color.B, .2f), 0f, Vector2.Zero, new Vector2(size.X , size.Y ), SpriteEffects.None, 0);

            //Corner Squares
            //ScreenManager.Instance.SpriteBatch.Draw(peg, new Vector2(0, 0), new Rectangle(0, 0, (int)pixelTexture.Width, (int)pixelTexture.Height), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0); // TL
            //ScreenManager.Instance.SpriteBatch.Draw(peg, new Vector2(size.X - /2, 0), new Rectangle(0, 0, (int)pixelTexture.Width, (int)pixelTexture.Height), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0); // TR
            //ScreenManager.Instance.SpriteBatch.Draw(peg, new Vector2(0, size.Y - /2), new Rectangle(0, 0, (int)pixelTexture.Width, (int)pixelTexture.Height), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0); // BL
            //ScreenManager.Instance.SpriteBatch.Draw(peg, new Vector2(size.X - /2, size.Y - /2), new Rectangle(0, 0, (int)pixelTexture.Width, (int)pixelTexture.Height), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0); // BR

            ScreenManager.Instance.SpriteBatch.End();

            Texture2D texture = RT;

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);

            return texture;
        }

    }
}
