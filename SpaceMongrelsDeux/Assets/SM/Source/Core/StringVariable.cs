using UnityEngine;

namespace SNDL
{
  [CreateAssetMenu]
  public class StringVariable : ScriptableObject, ISerializationCallbackReceiver
  {
    public string initialValue;
    public string value
    {
      get { return runtimeValue; }
      set { runtimeValue = value; }
    }

    private string runtimeValue;

    public void OnAfterDeserialize()
    {
      runtimeValue = initialValue;
    }

    public void OnBeforeSerialize() { }
  }
}
