using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Roushi
{
    public enum ParticleType { None, Enemy, Bullet, IgnoreGravity }
    
    public struct ParticleState
    {
        public Vector2 Velocity;
        public ParticleType Type;
        public float LengthMultiplier;

        public static void UpdateParticle(ParticleManager<ParticleState>.Particle particle)
        {
            var vel = particle.State.Velocity;

            particle.Position += vel;
            particle.Orientation = (float)Math.Atan2((double)vel.Y,(double)vel.X);

            float speed = vel.Length();
            float alpha = Math.Min(1, Math.Min(particle.PercentLife * 2, speed * 1f));
            alpha *= alpha;

            particle.Tint.A = (byte)(255 * alpha);

            particle.Scale = particle.State.LengthMultiplier * Math.Min(Math.Min(1f, 0.2f * speed + 0.1f), alpha);


            //denormalized floats cause significant performance issues
            if (Math.Abs(vel.X) + Math.Abs(vel.Y) < 0.00000000001f)
                vel = Vector2.Zero;

            vel *= 0.97f; // slows down particles overtime
            particle.State.Velocity = vel;
        }
    }
}

static class ColorUtil
{
    public static Color HSVToColor(float h, float s, float v)
    {
        if (h == 0 && s == 0)
            return new Color(v, v, v);

        float c = s * v;
        float x = c * (1 - Math.Abs(h % 2 - 1));
        float m = v - c;

        if (h < 1) return new Color(c + m, x + m, m);
        else if (h < 2) return new Color(x + m, c + m, m);
        else if (h < 3) return new Color(m, c + m, x + m);
        else if (h < 4) return new Color(m, x + m, c + m);
        else if (h < 5) return new Color(x + m, m, c + m);
        else return new Color(c + m, m, x + m);
    }
}