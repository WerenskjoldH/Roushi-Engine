using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Roushi
{
    class TalkToPart : Part
    {

        Messagebox messageBox;
        public TalkToPart(string Text, string fontType, Vector2 boxPosition, Color textColor, Color bgColor, bool drawBackground, string backgroundName)
        {
            messageBox = new Messagebox(Text, boxPosition, fontType, textColor, bgColor, DialogLocation.Foreground, backgroundName);
            messageBox.DrawBackground = drawBackground;
            messageBox.ShouldDraw = false;
        }

        public override void Initialize()
        {
            entity.Get<CollisionPart>().Collided += new CollisionEventHandler(StartTalk);
            entity.Get<CollisionPart>().Colliding += new CollisionEventHandler(Talk);
            entity.Get<CollisionPart>().EndedCollision += new CollisionEventHandler(EndTalk);
        }

        void StartTalk(Entity target)
        {
            messageBox.Reset();
        }

        void EndTalk(Entity target)
        {
            if (target.uniqueID == "player")
            {
                messageBox.ShouldDraw = false;
                messageBox.Reset();
                target.Get<AccessoryPart>().FindAccessory("talkBubble").visible = false;
            }
        }

        private void Talk(Entity target)
        {
            if (target.uniqueID == "player")
            {
                if (messageBox.ShouldDraw)
                    target.Get<AccessoryPart>().FindAccessory("talkBubble").visible = true;
                else
                    target.Get<AccessoryPart>().FindAccessory("talkBubble").visible = false;

                if (InputManager.Instance.KeyPressed(Keys.J) && target.Get<PlayerControllerPart>().CurrentAction.Interruptible)
                {
                    if (messageBox.IsDoneDrawing())
                        messageBox.Reset();
                    messageBox.ShouldDraw = true;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            messageBox.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            messageBox.Draw(spriteBatch);
        }

    }
}
