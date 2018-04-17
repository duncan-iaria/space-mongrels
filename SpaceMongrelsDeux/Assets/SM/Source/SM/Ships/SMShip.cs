using UnityEngine;
using System.Collections;

namespace SM
{
    public abstract class SMShip : ScriptableObject
    {
        public string shipName;
        public int maxHealth;

        public abstract void initialize(GameObject tObject);
    }
}