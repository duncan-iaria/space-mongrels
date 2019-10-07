using UnityEngine;

public class SMTargetReticule : MonoBehaviour
{
  public Transform currentTarget;

  void LateUpdate()
  {
    if (currentTarget != null)
    {
      this.transform.SetPositionAndRotation(currentTarget.transform.position, Quaternion.identity);
    }
    else
    {
      onNoTargetAvailable();
    }
  }

  public void setNewTarget(Transform tTarget)
  {
    this.enabled = true;
    currentTarget = tTarget;

    // Animation Effects
  }

  private void onNoTargetAvailable()
  {
    this.enabled = false;
    currentTarget = null;
  }
}
