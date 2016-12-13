using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Roushi
{
    public enum DepthLayer
    {
        Overlay,
        Background,
        Stage,
        Foreground
    }

    class DrawablePart : Part
    {
        DepthLayer depthLayer;
        string path;
        Vector2 frameSize;
        Vector2 scale;
        Texture2D texture;
        Rectangle sourceRectangle;
        Color defaultColor;
        Color color;
        float alpha;
        float rotation;
        ContentManager content;

        public Texture2D GetTexture
        {
            get { return texture; }
        }

        public DepthLayer DepthLayer
        {
            get { return depthLayer; }
            protected set { depthLayer = value; }
        }

        public Vector2 FrameSize
        {
            get { return frameSize; }
            set { frameSize = value; }
        }

        public Vector2 Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public Rectangle SourceRectangle
        {
            get { return sourceRectangle; }
            set { sourceRectangle = value; }
        }

        public float Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public Color DefaultColor // Change this to effect what the base color of the monster will be
        {
            get { return defaultColor; }
            set { defaultColor = value; }
        }

        public float GetRotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public DrawablePart(string path, Vector2 frameSize, DepthLayer depthlayer)
        {
            this.path = path;
            this.frameSize = frameSize;
            color = Color.White;
            defaultColor = color;
            alpha = 1f;
            sourceRectangle = new Rectangle(0, 0, (int)frameSize.X, (int)frameSize.Y);
            scale = Vector2.One;
            depthLayer = depthlayer;
            rotation = 0;
        }

        public Rectangle GetBoundingBox()
        {
            Rectangle temp = new Rectangle((int)(entity.Get<TransformPart>().GetPositionX), (int)entity.Get<TransformPart>().GetPositionY,
                (int)(entity.Get<DrawablePart>().SourceRectangle.Width * entity.Get<DrawablePart>().Scale.X), (int)(entity.Get<DrawablePart>().SourceRectangle.Height * entity.Get<DrawablePart>().Scale.Y));

            temp.X = temp.X - (temp.Width / 2); // center up that bounding box
            temp.Y = temp.Y - (temp.Height); // yeah yeah center that shit up

            return temp;
        }

        public override void Initialize()
        {
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            if (path != String.Empty)
                texture = content.Load<Texture2D>(path);

            //font = content.Load<SpriteFont>(FontName);

            //Vector2 dimensions = Vector2.Zero;
            //if (texture != null)
            //    dimensions.X += Texture.Width;
            //dimensions.X += font.MeasureString(Text).X;

            //if (texture != null)
            //    dimensions.Y = Math.Max(Texture.Height, font.MeasureString(Text).Y);
            //else
            //    dimensions.Y = font.MeasureString(Text).Y;

            //if (SourceRect == Rectangle.Empty)
            //    SourceRect = new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y);

            //renderTarget = new RenderTarget2D(ScreenManager.Instance.GraphicsDevice,
            //    (int)dimensions.X, (int)dimensions.Y);
            //ScreenManager.Instance.GraphicsDevice.SetRenderTarget(renderTarget);
            //ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);
            //ScreenManager.Instance.SpriteBatch.Begin();
            //if (texture != null)
            //    ScreenManager.Instance.SpriteBatch.Draw(texture, Vector2.Zero, Color.White);
            //ScreenManager.Instance.SpriteBatch.DrawString(font, Text, Vector2.Zero, Color.White);
            //ScreenManager.Instance.SpriteBatch.End();

            //texture = renderTarget;

            //ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);

            //SetEffect<FadeEffect>(ref FadeEffect);
            //SetEffect<SpriteSheetEffect>(ref SpriteSheetEffect);

            //if (Effects != String.Empty)
            //{
            //    string[] split = Effects.Split(':');
            //    foreach (string item in split)
            //        ActivateEffect(item);
            //}
            base.Initialize();
        }

        public override void CleanUp()
        {
            content.Unload();
            base.CleanUp();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, entity.Get<TransformPart>().GetPosition, sourceRectangle, color * alpha, rotation, new Vector2(frameSize.X / 2, frameSize.Y), scale, SpriteEffects.None, 0f);
        }
    }
}
