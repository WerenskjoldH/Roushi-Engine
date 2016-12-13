using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Collections;

namespace Roushi
{
    class AccessoryPart : Part
    {
        private ArrayList accessories;
        public AccessoryPart()
        {
            accessories = new ArrayList();
        }

        public void AddAccessory(string name, Vector2 Offset, Color Color, float Rotation)
        {
            for (int x = 0; x < accessories.Count; x++)
            {
                Accessory a = (Accessory)accessories[x];
                if (a.name == name)
                {
                    return;
                }
            }
            accessories.Add(new Accessory(name, Offset, Color, Rotation));
        }

        public void RemoveAccessory(string name)
        {
            for (int x = 0; x < accessories.Count; x++)
            {
                Accessory a = (Accessory)accessories[x];
                if (a.name == name)
                {
                    accessories.RemoveAt(x);
                    return;
                }
            }
            throw new InvalidOperationException("Accessory " + name + " could not be found.");
        }

        public Accessory FindAccessory(string name)
        {
            for (int x = 0; x < accessories.Count; x++)
            {
                Accessory a = (Accessory)accessories[x];
                if (a.name == name)
                {
                    return a;
                }
            }
            throw new InvalidOperationException("Accessory " + name + " could not be found.");
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            foreach (Accessory a in accessories)
            {
                a.Draw(spriteBatch, entity.Get<TransformPart>().GetPosition);
            }
        }
    }
}
