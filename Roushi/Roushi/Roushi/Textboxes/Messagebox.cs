using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections;

namespace Roushi
{
    class Messagebox : Dialog
    {
        Texture2D messageBoxTexture, blinkerTexture;
        Textbox textBox;
        Vector2 position;
        Vector2 size;
        Color color;
        int buffer = 20;
        float blinkerAlpha;

        Texture2D finalRender;

        public Boolean DrawBackground;
        public Boolean ShouldDraw;

        public Messagebox(string text, Vector2 pos, string fontName, Color fontColor, DialogLocation DialogLocation, string backgroundName)
        {
            size = new Vector2(400, 100);
            if (backgroundName == "default")
                messageBoxTexture = TextboxFactory.DefaultTextbox(new Vector2(400, 100), Color.White);
            else
                messageBoxTexture = ScreenManager.Instance.Content.Load<Texture2D>(backgroundName);

            position = new Vector2(pos.X - messageBoxTexture.Width/2, pos.Y - messageBoxTexture.Height/2);

            blinkerTexture = ScreenManager.Instance.Content.Load<Texture2D>("Textbox/blinker");
            textBox = new Textbox(new Rectangle((int)position.X + buffer, (int)position.Y + buffer, (int)size.X - buffer, (int)size.Y - buffer), fontName, text, fontColor);
            DrawBackground = true;
            ShouldDraw = true;
            dialogLocation = DialogLocation;
            blinkerAlpha = 0f;
        }

        public Messagebox(string text, Vector2 pos, string fontName, Color fontColor, Color Color, DialogLocation DialogLocation, string backgroundName)
        {
            size = new Vector2(400, 100);
            if (backgroundName == "default")
                messageBoxTexture = TextboxFactory.DefaultTextbox(new Vector2(400, 100), Color.White);
            else
                messageBoxTexture = ScreenManager.Instance.Content.Load<Texture2D>(backgroundName);
            color = Color;
            position = new Vector2(pos.X - messageBoxTexture.Width / 2, pos.Y - messageBoxTexture.Height / 2);

            blinkerTexture = ScreenManager.Instance.Content.Load<Texture2D>("Textbox/blinker");
            textBox = new Textbox(new Rectangle((int)position.X + buffer, (int)position.Y + buffer, (int)size.X - buffer, (int)size.Y - buffer), fontName, text, fontColor);
            DrawBackground = true;
            ShouldDraw = true;
            dialogLocation = DialogLocation;
            blinkerAlpha = 0f;
        }

        public Boolean IsDoneDrawing()
        {
            return textBox.doneDrawing;
        }


        public override void Initialize()
        {
            
            base.Initialize();
        }

        public override void UnloadContent()
        {
            messageBoxTexture.Dispose();
        }

        public void Reset()
        {
            textBox.Reset();
            Render();
            blinkerAlpha = 0f;
        }

        public override void Update(GameTime gameTime)
        {
            if (ShouldDraw)
            {
                if (!textBox.doneDrawing)
                {
                    textBox.Update();
                    Render();
                }
                else
                    blinkerAlpha = (float)(.5 * Math.Sin(gameTime.TotalGameTime.TotalMilliseconds / 200) + .5);


            }
        }

        

        private void Render()
        {
            RenderTarget2D RT = new RenderTarget2D(ScreenManager.Instance.GraphicsDevice, (int)ScreenManager.Instance.Dimensions.X, (int)ScreenManager.Instance.Dimensions.Y);

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(RT);
            ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);
            ScreenManager.Instance.SpriteBatch.Begin();

            if(DrawBackground)
                ScreenManager.Instance.SpriteBatch.Draw(messageBoxTexture, position, color);
            textBox.Draw(ScreenManager.Instance.SpriteBatch);

            ScreenManager.Instance.SpriteBatch.End();

            finalRender = RT;

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(messageBoxTexture, position, Color.White);
            //textBox.Draw(ScreenManager.Instance.SpriteBatch);
            if (finalRender != null && ShouldDraw)
            {
                spriteBatch.Draw(finalRender, Vector2.Zero, new Rectangle(0, 0, (int)finalRender.Width, (int)finalRender.Height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                if (textBox.doneDrawing && ShouldDraw)
                    ScreenManager.Instance.SpriteBatch.Draw(blinkerTexture, new Vector2(position.X + messageBoxTexture.Width - 20, position.Y + messageBoxTexture.Height - 20), new Rectangle(0, 0, (int)blinkerTexture.Width, (int)blinkerTexture.Height), new Color(255, 255, 255, blinkerAlpha), 0f, new Vector2((float)blinkerTexture.Width / 2, (float)blinkerTexture.Height / 2), 1f, SpriteEffects.None, 0f);
            
            }
        }

    }
}
