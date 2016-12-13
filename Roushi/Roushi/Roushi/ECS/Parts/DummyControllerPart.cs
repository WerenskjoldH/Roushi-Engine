using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Roushi
{
    class DummyControllerPart : Part
    {
        public DummyControllerPart()
        {

        }

        public override void Initialize()
        {

        }



        public override void CleanUp()
        {
            

            base.CleanUp();
        }


        private Entity target;
        Vector2 prevMovement;
        public override void Update(GameTime gameTime)
        {
            if (entity.Has<TransformPart>() && target != null)
            {
                TransformPart selfMovement = entity.Get<TransformPart>();
                TransformPart targetMovement = target.Get<TransformPart>();

                if (targetMovement.GetPosition != prevMovement)
                {
                    if (targetMovement.GetPosition.X < selfMovement.GetPosition.X)
                        selfMovement.GetPositionX -= 50 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    else if (targetMovement.GetPosition.X > selfMovement.GetPosition.X)
                        selfMovement.GetPositionX += 50 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (targetMovement.GetPosition.Y < selfMovement.GetPosition.Y)
                        selfMovement.GetPositionY -= 50 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    else if (targetMovement.GetPosition.Y > selfMovement.GetPosition.Y)
                        selfMovement.GetPositionY += 50 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                prevMovement = targetMovement.GetPosition;
            }
            else if (target == null)
                target = GetAttachedEntity().entityManager.GetByUniqueID("player");



            base.Update(gameTime);
        }

    }
}
