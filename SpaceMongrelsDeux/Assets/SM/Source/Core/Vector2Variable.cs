using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SNDL
{
    [CreateAssetMenu]
    public class Vector2Variable : ScriptableObject, ISerializationCallbackReceiver
    {
        public Vector2 initialValue;

        [HideInInspector]
        public Vector2 value
        {
            get { return runtimeValue; }
            set { runtimeValue = value; }
        }
        private Vector2 runtimeValue;

        public void OnAfterDeserialize()
        {
            runtimeValue = initialValue;
        }

        public void OnBeforeSerialize() { }
    }
}
