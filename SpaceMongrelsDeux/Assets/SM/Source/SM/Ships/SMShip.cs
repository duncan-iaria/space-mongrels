using UnityEngine;
using System.Collections;

namespace SM
{
    public abstract class SMShip : ScriptableObject
    {
        public string shipName;
        public int maxHealth;
        public float collisionModifier = 1;

        public abstract void initialize(GameObject tObject);
        public abstract void takeDamage(GameObject tObject, int tAmt);
        public abstract void onCollision(GameObject tObject, Collision2D tCollision);
        public abstract void onDeath(GameObject tObject);
    }
}