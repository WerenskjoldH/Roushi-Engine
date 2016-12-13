using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Roushi
{
    class ChaseAI : Part
    {
        float speed;
        public ChaseAI()
        {

        }

        public override void Initialize()
        {
            speed = 1.8f;
        }



        public override void CleanUp()
        {
            

            base.CleanUp();
        }


        private Entity target;
        public override void Update(GameTime gameTime)
        {
            if (entity.Has<TransformPart>() && target != null)
            {
                TransformPart selfMovement = entity.Get<TransformPart>();
                TransformPart targetMovement = target.Get<TransformPart>();

                entity.Get<TransformPart>().GetPositionX += speed * ((float)Math.Cos(Math.Atan2(targetMovement.GetPositionY - selfMovement.GetPositionY, targetMovement.GetPositionX - selfMovement.GetPositionX)));
                entity.Get<TransformPart>().GetPositionY += speed * ((float)Math.Sin(Math.Atan2(targetMovement.GetPositionY - selfMovement.GetPositionY, targetMovement.GetPositionX - selfMovement.GetPositionX)));


            }
            else if (target == null)
                target = GetAttachedEntity().entityManager.GetByUniqueID("target");



            base.Update(gameTime);
        }

        private float CalculateDistance(Entity self, Entity target)
        {
            TransformPart selfTf = self.Get<TransformPart>();
            TransformPart targetTf = target.Get<TransformPart>();
            return (float)Math.Sqrt((double)(Math.Pow((targetTf.GetPositionX - selfTf.GetPositionX), 2) - (Math.Pow((targetTf.GetPositionY - selfTf.GetPositionY), 2))));
        }
    }
}
