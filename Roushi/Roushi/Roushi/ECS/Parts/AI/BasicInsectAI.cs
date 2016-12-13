using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Collections;

namespace Roushi
{
    class BasicInsectAI : Part
    {
         float speed;
        public BasicInsectAI()
        {

        }

        public override void Initialize()
        {
            speed = 2f;
        }



        public override void CleanUp()
        {
            

            base.CleanUp();
        }

        public override void Update(GameTime gameTime)
        {
            ArrayList targets = new ArrayList();
            float totalForceRatio = 0;

            foreach (Entity e in entity.entityManager.GetByCategory("humanoid"))
            {
                if (e.Has<TransformPart>() && CalculateDistance(entity, e) < 50 && entity != e)
                {
                    targets.Add(e);
                    totalForceRatio += (float)(1 / (CalculateDistance(entity, e)));
                }
            }


            //ArrayList forces = new ArrayList();
            Vector2 final = new Vector2();


            final.Y += .3f*(float)Math.Sin(.9f * gameTime.TotalGameTime.TotalSeconds);

            entity.Get<TransformPart>().GetPositionX += (-final.X);
            entity.Get<TransformPart>().GetPositionY += (-final.Y);

            base.Update(gameTime);
        }

        private float CalculateDistance(Entity self, Entity target)
        {
            TransformPart selfTf = self.Get<TransformPart>();
            TransformPart targetTf = target.Get<TransformPart>();
            return Math.Abs((float)Math.Sqrt((double)(Math.Pow((targetTf.GetPositionX - selfTf.GetPositionX), 2)+(Math.Pow((targetTf.GetPositionY - selfTf.GetPositionY), 2)))));
        }
    }
}
