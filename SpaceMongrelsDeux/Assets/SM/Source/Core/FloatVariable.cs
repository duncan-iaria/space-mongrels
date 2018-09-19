using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SNDL
{
    [CreateAssetMenu]
    public class FloatVariable : ScriptableObject, ISerializationCallbackReceiver
    {
        public float initialValue;

        [HideInInspector]
        public float value
        {
            get { return runtimeValue; }
            set { runtimeValue = value; }
        }

        private float runtimeValue;

        public void OnAfterDeserialize()
        {
            runtimeValue = initialValue;
        }

        public void OnBeforeSerialize() { }
    }
}
