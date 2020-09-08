using UnityEngine;
using System.Collections;
using SM;


[CreateAssetMenu(menuName = "SM/Ships/Reactor", order = 100)]
public class Reactor : SMReactor
{
  public override void initialize(GameObject tObject)
  {
    SMPawnShip tempShip = tObject.GetComponent<SMPawnShip>();
    if (tempShip != null)
    {
      tempShip.moveSpeed = moveSpeed;
      tempShip.horizontalDampening = horizontalDampening;
      tempShip.rotationSpeed = rotationSpeed;
      tempShip.boostSpeed = boostSpeed;
      tempShip.boostCooldown = boostCooldown;
      tempShip.thrustSpeed = thrustSpeed;
      tempShip.rotationalDampeningUnderThrust = rotationalDampeningUnderThrust;
    }
    else
    {
      Debug.LogWarning("No ship was found on the GameObject - Have you set up your ship correctly?");
    }
  }

  public override void boost(Rigidbody2D tRigidbody, float tBoostSpeed)
  {
    if (tRigidbody)
    {
      tRigidbody.AddRelativeForce(Vector2.up * tBoostSpeed, ForceMode2D.Impulse);
    }
  }
}