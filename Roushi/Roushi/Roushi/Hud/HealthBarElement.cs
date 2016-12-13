using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Roushi
{
    class HealthBarElement : HudElement
    {
        Texture2D healthBlockTxt;
        StatsPart playerStats;
        Vector2 startingPos;
        int xOffset;

        public HealthBarElement(Entity e)
        {
            healthBlockTxt = ScreenManager.Instance.Content.Load<Texture2D>("Hud/healthBlock");
            playerStats = e.Get<StatsPart>();
            startingPos = new Vector2(25, 25);
            xOffset = 15;
        }

        public override void Initialize()
        {


        }

        public override void Update(GameTime gameTime)
        {


        }
        // y = startingPos.Y + (float)Math.Sin(x)
        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < playerStats.GetMaxHealth; x++)
            {
                if (x <= playerStats.GetHealth)
                {
                    spriteBatch.Draw(healthBlockTxt, new Vector2((float)startingPos.X + (xOffset * x), startingPos.Y), new Rectangle(0, 0, healthBlockTxt.Width, healthBlockTxt.Height), Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                }
                else
                {
                    spriteBatch.Draw(healthBlockTxt, new Vector2((float)startingPos.X + (xOffset * x), startingPos.Y), new Rectangle(0, 0, healthBlockTxt.Width, healthBlockTxt.Height), new Color(255, 255, 255, .2f), 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                }

            }

        }
    }
}
