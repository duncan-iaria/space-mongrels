using UnityEngine;

namespace SM
{
  public class SMAIRotationTest : MonoBehaviour
  {
    public Transform target;
    void Update()
    {
      float dotProd = Vector2.Dot((Vector2)transform.right, (Vector2)(target.position - transform.position).normalized);
      Debug.Log("dot prod: " + dotProd);
    }
  }
}
