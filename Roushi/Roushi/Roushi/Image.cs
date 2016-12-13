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
    public class Image
    {
        public float Alpha;
        public string Text, FontName, Path;
        public Vector2 Position, Scale;
        public Rectangle SourceRect;
        public Rectangle CurrentBounds;
        public Color Color;
        public bool IsActive;
        public Boolean TopLeftOrigin;
        public float ZDepth;

        public Texture2D Texture;
        Vector2 origin;
        ContentManager content;
        RenderTarget2D renderTarget;
        SpriteFont font;
        Dictionary<string, ImageEffect> effectList;
        public string Effects;

        public FadeEffect FadeEffect;
        public SpriteSheetEffect SpriteSheetEffect;

        void SetEffect<T>(ref T effect)
        {
            if (effect == null)
                effect = (T)Activator.CreateInstance(typeof(T));
            else
            {
                (effect as ImageEffect).IsActive = true;
                var obj = this;
                (effect as ImageEffect).LoadContent(ref obj);
            }

            effectList.Add(effect.GetType().ToString().Replace("Roushi.", ""), (effect as ImageEffect));
        }

        private void ActivateEffect(string effect)
        {
            if (effectList.ContainsKey(effect))
            {
                effectList[effect].IsActive = true;
                var obj = this;
                effectList[effect].LoadContent(ref obj);
            }
        }

        private void DeactivateEffect(string effect)
        {
            if (effectList.ContainsKey(effect))
            {
                effectList[effect].IsActive = false;
                effectList[effect].UnloadContent();
            }
        }

        public Image(float zDepth)
        {
            Path = Text = Effects = string.Empty;
            FontName = "Fonts/BitFont";
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Alpha = 1.0f;
            Color = Color.White;
            SourceRect = Rectangle.Empty;
            CurrentBounds = Rectangle.Empty;
            effectList = new Dictionary<string,ImageEffect>();
            TopLeftOrigin = false;
            ZDepth = zDepth;
        }

        public Image(int zDepth)
        {
            Path = Text = Effects = string.Empty;
            FontName = "Fonts/BitFont";
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Alpha = 1.0f;
            Color = Color.White;
            SourceRect = Rectangle.Empty;
            CurrentBounds = Rectangle.Empty;
            effectList = new Dictionary<string, ImageEffect>();
            TopLeftOrigin = false;
        }


        public void LoadContent()
        {
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            if(Path != String.Empty)
                 Texture = content.Load<Texture2D>(Path);

            font = content.Load<SpriteFont>(FontName);

            Vector2 dimensions = Vector2.Zero;
            if(Texture != null)
                dimensions.X += Texture.Width;
            dimensions.X += font.MeasureString(Text).X;

            if(Texture != null)
                dimensions.Y = Math.Max(Texture.Height, (int)font.MeasureString(Text).Y);
            else
                dimensions.Y = (int)font.MeasureString(Text).Y;

            if(SourceRect == Rectangle.Empty)
                SourceRect = new Rectangle(0,0, (int)dimensions.X, (int)dimensions.Y);

            renderTarget = new RenderTarget2D(ScreenManager.Instance.GraphicsDevice,
                (int)dimensions.X, (int)dimensions.Y);
            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(renderTarget);
            ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);
            ScreenManager.Instance.SpriteBatch.Begin();
            if (Texture != null)
                ScreenManager.Instance.SpriteBatch.Draw(Texture, Vector2.Zero, Color.White);
            ScreenManager.Instance.SpriteBatch.DrawString(font, Text, Vector2.Zero, Color.White);
            ScreenManager.Instance.SpriteBatch.End();

            Texture = renderTarget;

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);

            SetEffect<FadeEffect>(ref FadeEffect);
            SetEffect<SpriteSheetEffect>(ref SpriteSheetEffect);

            if (Effects != String.Empty)
            {
                string[] split = Effects.Split(':');
                foreach (string item in split)
                    ActivateEffect(item);
            }
        }

        public void UnloadContent()
        {
            content.Unload();
            foreach (var effect in effectList)
                DeactivateEffect(effect.Key);
        }

        public void Update(GameTime gameTime)
        {
            if (Texture == null)
            {
                if(!TopLeftOrigin)
                    CurrentBounds = new Rectangle((int)(Position.X - (((int)font.MeasureString(Text).X * Scale.X) / 2)), (int)(Position.Y - (((int)font.MeasureString(Text).Y * Scale.Y) / 2)), (int)font.MeasureString(Text).X, (int)font.MeasureString(Text).Y);
                else
                    CurrentBounds = new Rectangle((int)(Position.X), (int)(Position.Y), (int)font.MeasureString(Text).X, (int)font.MeasureString(Text).Y);
            }
            else
            {
                if (!TopLeftOrigin)
                    CurrentBounds = new Rectangle((int)(Position.X - (((int)SourceRect.Width * Scale.X) / 2)), (int)(Position.Y - (((int)SourceRect.Height * Scale.Y) / 2)), (int)((int)SourceRect.Width * Scale.X), (int)((int)SourceRect.Height * Scale.Y));
                else
                    CurrentBounds = new Rectangle((int)Position.X, (int)Position.Y, (int)SourceRect.Width, (int)SourceRect.Height);
            }

            foreach (var effect in effectList)
            {
                if(effect.Value.IsActive)
                    effect.Value.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!TopLeftOrigin)
                origin = new Vector2((int)SourceRect.Width / 2, (int)SourceRect.Height / 2);
            else
                origin = Vector2.Zero;

            spriteBatch.Draw(Texture, Position, SourceRect, Color * Alpha, 0.0f, origin, Scale, SpriteEffects.None, ZDepth);
        }

    }
}
