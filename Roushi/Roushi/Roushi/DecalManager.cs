using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Roushi
{

    struct Decal
    {
        public Vector2 position;
        public Vector2 scale;
        public Texture2D texture;
        public float opacity;
        public float rotation;
        public Color color;

        public Decal(string textureName, Vector2 pos, Vector2 scal, float opac, float rot)
        {
            position = pos;
            scale = scal;
            texture = ScreenManager.Instance.Content.Load<Texture2D>(textureName);
            opacity = opac;
            rotation = rot;
            color = Color.White;
        }
    }

    public class DecalManager
    {
        private static int MAX_DECALS = 1000;
        ArrayList decals;

        public DecalManager()
        {
            decals = new ArrayList();
        }

        //public void AddDecal(Decal decal)
        //{
        //    if((decals.Count + 1) >= MAX_DECALS)
        //        decals.RemoveAt(0);
        //    decals.Add(decal);
        //}

        public void AddDecal(string textureName, Vector2 pos, Vector2 scal, float opac, float rot)
        {
            if ((decals.Count + 1) >= MAX_DECALS)
                decals.RemoveAt(0);
            decals.Add(new Decal(textureName, pos, scal, opac, rot));
            UpdateTexture();
        }

        public void AddDecal(string textureName, Vector2 pos)
        {
            if((decals.Count + 1) >= MAX_DECALS)
                decals.RemoveAt(0);
            decals.Add(new Decal(textureName, pos, Vector2.One, 1f, 0f));
            UpdateTexture();
        }

        //Get around to this sometime
        //public void AddRandomizedDecal(string textureName, Vector2 pos)
        //{
        //}
        RenderTarget2D RT;
        public void UpdateTexture()
        {
            RT = new RenderTarget2D(ScreenManager.Instance.GraphicsDevice, (int)ScreenManager.Instance.Dimensions.X, (int)ScreenManager.Instance.Dimensions.Y);

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(RT);
            ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);
            ScreenManager.Instance.SpriteBatch.Begin();

            foreach (Decal d in decals)
            {
                ScreenManager.Instance.SpriteBatch.Draw(d.texture, d.position, new Rectangle(0, 0, (int)d.texture.Width, (int)d.texture.Height), d.color, d.rotation, new Vector2(d.texture.Width / 2, d.texture.Height / 2), d.scale, SpriteEffects.None, 0);
            }

            ScreenManager.Instance.SpriteBatch.End();
            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);
        }
        //public void Update()
        //{

        //}

        public void Draw(SpriteBatch spriteBatch)
        {
            if (RT != null) 
                spriteBatch.Draw(RT, Vector2.Zero, new Rectangle(0, 0, (int)RT.Width, (int)RT.Height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
        
    }
}
