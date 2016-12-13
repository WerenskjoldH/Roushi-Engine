using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Roushi
{
    class TransformPart : Part
    {
        private Vector2 position;
        private float positionZ;

        public Vector2 GetPosition
        { get { return position; } set { position = value; } }
        public float GetPositionZ
        { get { return positionZ; } set { positionZ = value; } } // distance off of ground plane

        public float GetPositionX
        { get { return position.X; } set { position.X = value; } }

        public float GetPositionY
        { get { return position.Y; } set { position.Y = value; } }

        public TransformPart(Vector2 position)
        {
            this.position = position;
            positionZ = 0f;
        }

        public TransformPart(Vector2 position, float height)
        {
            this.position = position;
            positionZ = height;
        }

        public TransformPart()
        {
            this.position = Vector2.Zero;
            this.positionZ = 0f;
        }
    }
}
