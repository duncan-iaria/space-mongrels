using UnityEngine;
using System.Collections;

namespace SM
{
    public abstract class SMReactor : ScriptableObject
    {
        public float moveSpeed;
        public float horizontalDampening;
        public float rotationSpeed;

        public abstract void initialize(GameObject tObject);
    }
}