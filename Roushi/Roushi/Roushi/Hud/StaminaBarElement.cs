using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Roushi
{
    class StaminaBarElement : HudElement
    {
        Texture2D staminaBlockTxt;
        StatsPart playerStats;
        Vector2 startingPos;
        int xOffset;

        public StaminaBarElement(Entity e)
        {
            staminaBlockTxt = ScreenManager.Instance.Content.Load<Texture2D>("Hud/healthBlock");
            playerStats = e.Get<StatsPart>();
            startingPos = new Vector2(25, 45);
            xOffset = 15;
        }

        public override void Initialize()
        {


        }

        public override void Update(GameTime gameTime)
        {


        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < playerStats.GetMaxStamina; x++)
            {
                if (x < playerStats.GetStamina)
                {
                    spriteBatch.Draw(staminaBlockTxt, new Vector2((float)startingPos.X + (xOffset * x), startingPos.Y), new Rectangle(0, 0, staminaBlockTxt.Width, staminaBlockTxt.Height), Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                }
                else
                {
                    spriteBatch.Draw(staminaBlockTxt, new Vector2((float)startingPos.X + (xOffset * x), startingPos.Y), new Rectangle(0, 0, staminaBlockTxt.Width, staminaBlockTxt.Height), Color.Green, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                }

            }

        }
    }
}
