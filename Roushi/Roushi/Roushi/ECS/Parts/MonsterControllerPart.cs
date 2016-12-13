//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Roushi
//{
//    class MonsterControllerPart : Part
//    {
//        private Entity target;

//        public MonsterControllerPart(Entity target)
//        {
//            this.target = target;
//        }

//        public override void Initialize()
//        {
//            Console.WriteLine("Behold me human! For I live!");

//            base.Initialize();
//        }

//        public override void CleanUp()
//        {
//            Console.WriteLine("I am now dead!");

//            base.CleanUp();
//        }



//        public override void Update(GameTime gameTime)
//        {
//            StatsPart myStatsPart = entity.Get<StatsPart>();

//            if (target.Has<StatsPart>()) // can call this way
//            {
//                StatsPart targetStatsPart = target.Get<StatsPart>();
//                target.Get<StatsPart>().setHealth(targetStatsPart.getHealth() - myStatsPart.getDamage());

//                Console.WriteLine("Monster : \"I hit you!\" Target's health is now at " + targetStatsPart.getHealth());
//            }

//            if (entity.Has(typeof(StatsPart)) && myStatsPart.getHealth() < 100) // or this way
//            {
//                entity.Get<SpellsPart>().castHeal();
//                Console.WriteLine("I healed myself! I'm now at " + myStatsPart.getHealth());
//            }

//            if (myStatsPart.getHealth() <= 0)
//                CleanUp();

//            base.Update(gameTime);
//        }
//    }
//}
