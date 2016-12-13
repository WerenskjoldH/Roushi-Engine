//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;

//namespace Roushi
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            Entity villager = createVillager();
//            Entity monster = createMonster(villager);

//            while (true)
//            {
//                villager.Update(1);
//                monster.Update(1);
//                Thread.Sleep(1000);
//            }
//        }

//        public static Entity createMonster(Entity target)
//        {
//            Entity monster = new Entity();
//            monster.Attach(new StatsPart(100, 10));
//            //monster.Attach(new FlyingPart(20));
//            monster.Attach(new SpellsPart(5));
//            monster.Attach(new MonsterControllerPart(target));
//            monster.Initialize();
//            return monster;
//        }

//        public static Entity createVillager()
//        {
//            Entity villager = new Entity();
//            villager.Attach(new StatsPart(50, 0));
//            villager.Initialize();
//            return villager;
//        }
//    }
//}
