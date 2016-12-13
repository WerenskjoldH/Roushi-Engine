using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roushi
{
    class StatsPart : Part
    {
        private float health;
        private float maxHealth;
        private float stamina;
        private float maxStamina;

        public StatsPart(float health, float stamina)
        {
            this.health = health;
            this.maxHealth = health;
            this.stamina = stamina;
            this.maxStamina = stamina;
        }

        public float GetHealth
        { get { return health; }
        set { maxHealth = value; } }

        public float GetMaxHealth
        { 
            get { return maxHealth; } 
            set { maxHealth = value; }
        }

        public float GetStamina
        {
            get { return stamina; }
            set { stamina = value; }
        }

        public float GetMaxStamina
        {
            get { return maxStamina; }
            set { maxStamina = value; }
        }

        public void setHealth(float hp)
        {
            if (hp > maxHealth)
                this.health = maxHealth;
            else
                this.health = hp;
        }


    }
}
