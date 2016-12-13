using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Collections;

namespace Roushi
{
    class FleeAI : Part
    {
        float speed;
        public FleeAI()
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
                if (e.Has<TransformPart>() && CalculateDistance(entity, e) < 400 && entity != e)
                {
                    targets.Add(e);
                    totalForceRatio += (float)(1 / (CalculateDistance(entity, e)));
                }
            }


            ArrayList forces = new ArrayList();
            Vector2 final = new Vector2();
            for (int x = 0; x < targets.Count; x++)
            {
                Entity target = (Entity)targets[x];
                TransformPart selfTf = entity.Get<TransformPart>();
                TransformPart targetTf = target.Get<TransformPart>();

                Vector2 force = new Vector2();
                double angle = (float)Math.Atan2((double)(targetTf.GetPositionY - selfTf.GetPositionY), (double)(targetTf.GetPositionX - selfTf.GetPositionX));
                float distance = CalculateDistance(target, entity);

                force.X = (speed * ((1 / distance) / totalForceRatio)) * (float)Math.Cos(angle);
                force.Y = (speed * ((1 / distance) / totalForceRatio)) * (float)Math.Sin(angle);

                forces.Add(force);
                final += force;
            }


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
