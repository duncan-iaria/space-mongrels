using UnityEngine;

namespace SNDL
{
  [CreateAssetMenu]
  public class FloatVariable : ScriptableObject, ISerializationCallbackReceiver
  {
    public float initialValue;
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
