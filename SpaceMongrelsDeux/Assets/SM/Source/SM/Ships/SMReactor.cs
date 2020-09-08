using UnityEngine;
using System.Collections;

namespace SM
{
  public abstract class SMReactor : ScriptableObject
  {
    public float moveSpeed;
    public float horizontalDampening;
    public float rotationSpeed;
    // rotational slow down when player is going forward - 0 if this engine isn't affected by forward thrust
    public float rotationalDampeningUnderThrust;
    public float boostSpeed;
    public float boostCooldown;
    public float thrustSpeed; // 0 if no thrust is possible

    public abstract void initialize(GameObject tObject);
    public abstract void boost(Rigidbody2D tRigidbody, float tBoostSpeed);
  }
}