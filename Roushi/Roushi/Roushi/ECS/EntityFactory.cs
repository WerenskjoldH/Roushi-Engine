using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Roushi
{
    class EntityFactory
    {
        public static Entity CreatePlayer(int health, int stamina, int speed,  Vector2 position, string spriteName, Vector2 frameSize)
        {
            Entity player = new Entity();
            player.Attach(new StatsPart(health, stamina));
            player.Attach(new TransformPart(position));
            player.Attach(new PlayerControllerPart(speed));
            player.Attach(new DrawablePart(spriteName, frameSize, DepthLayer.Stage));
            player.Attach(new CollisionPart());
            player.Attach(new AccessoryPart());
            player.Get<AccessoryPart>().AddAccessory("talkBubble", new Vector2(player.Get<DrawablePart>().FrameSize.X, -player.Get<DrawablePart>().GetBoundingBox().Height), Color.White, 0f);
            player.uniqueID = "player";
            player.categoryID = "humanoid";
            //player.Attach(new AnimationPart());
            //Animation animation = new Animation("walk_down", 0, 3, 0, 10);
            //player.Get<AnimationPart>().CreateAnimation(animation);

            return player;
        }

        public static Entity CreateTestSnowman(Vector2 position)
        {
            Entity snowman = new Entity();
            
            snowman.Attach(new TransformPart(position));
            snowman.Attach(new DrawablePart("gameObjects/testsnowman", new Vector2(64, 64), DepthLayer.Stage));
            snowman.Attach(new CollisionPart());
            snowman.categoryID = "humanoid";

            return snowman;
        }

        public static Entity PluckablePlant( Vector2 position, string spriteName, Vector2 frameSize)
        {
            Entity dummy = new Entity();
            dummy.Attach(new AnimationPart());
            dummy.Get<AnimationPart>().CreateAnimation(new Animation("plucked", 0, 3, 0, 2));
            dummy.Attach(new TransformPart(position));
            dummy.Attach(new DrawablePart(spriteName, frameSize, DepthLayer.Stage));
            dummy.Attach(new CollisionPart());
            dummy.Attach(new BurningPart());
            dummy.Attach(new PluckablePart());
            dummy.Get<DrawablePart>().Scale = new Vector2(4, 4);
            dummy.categoryID = "grass";

            return dummy;
        }

        public static Entity ChasingBaddie(Vector2 position, string spriteName, Vector2 frameSize)
        {
            Entity dummy = new Entity();
            dummy.categoryID = "humanoid";
            dummy.Attach(new TransformPart(position));
            dummy.Attach(new DrawablePart(spriteName, frameSize, DepthLayer.Stage));
            dummy.Attach(new CollisionPart());
            dummy.Attach(new BurningPart());
            dummy.Attach(new ChaseAI());

            dummy.Get<DrawablePart>().Color = Color.Red;

            return dummy;
        }

        public static Entity FleeingDummy(Vector2 position, string spriteName, Vector2 frameSize)
        {
            Entity dummy = new Entity();
            dummy.categoryID = "humanoid";
            dummy.uniqueID = "target";
            dummy.Attach(new TransformPart(position));
            dummy.Attach(new DrawablePart(spriteName, frameSize, DepthLayer.Stage));
            dummy.Attach(new CollisionPart());
            dummy.Attach(new FleeAI());

            dummy.Get<DrawablePart>().Color = Color.Green;

            return dummy;
        }

        public static Entity KindSign(string text, string fontType, Vector2 position, Vector2 textboxPosition, Color textColor, Color bgColor, Boolean drawBackground, string backgroundName)
        {
            Entity dummy = new Entity();

            dummy.uniqueID = "sign";
            dummy.Attach(new TransformPart(position));
            dummy.Attach(new DrawablePart("GameObjects/scratchedSign", new Vector2(44, 58), DepthLayer.Stage));
            dummy.Attach(new CollisionPart());
            dummy.Get<CollisionPart>().OverrideCollisionBox(new Rectangle((int)position.X - dummy.Get<DrawablePart>().GetBoundingBox().Width/2, (int)position.Y, dummy.Get<DrawablePart>().GetBoundingBox().Width, dummy.Get<DrawablePart>().GetBoundingBox().Height/2));
            dummy.Attach(new TalkToPart(text, fontType, textboxPosition, textColor, bgColor, drawBackground, backgroundName));

            return dummy;
        }

        public static Entity CreateSprite(Vector2 position)
        {
            Entity dummy = new Entity();
            dummy.categoryID = "insect";
            dummy.uniqueID = "sprite";
            dummy.Attach(new TransformPart(position));
            dummy.Attach(new DrawablePart("NPC/Insect/Sprite/sprite", new Vector2(32, 32), DepthLayer.Stage));
            dummy.Attach(new CollisionPart());
            dummy.Attach(new BasicInsectAI());
            //dummy.Attach(new CrushablePart());

            return dummy;
        }

    }
}
