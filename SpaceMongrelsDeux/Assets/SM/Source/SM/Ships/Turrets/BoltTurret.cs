using UnityEngine;

namespace SM
{
  [CreateAssetMenu(menuName = "SM/Ships/Turret", order = 98)]
  public class BoltTurret : Turret
  {
    public override void initialize(SMTurretController tTurretController)
    {
      if (tTurretController != null)
      {
        turretController = tTurretController;
      }
    }

    public override void rotateTowardTarget(Transform tTarget)
    {
      Transform tempTurretTransform = turretController.transform;
      Vector3 vectorToward = tTarget.position - turretController.transform.position;
      float angle = Mathf.Atan2(vectorToward.y, vectorToward.x) * Mathf.Rad2Deg - 90;
      Quaternion nextAngle = Quaternion.AngleAxis(angle, Vector3.forward);
      tempTurretTransform.rotation = Quaternion.Slerp(tempTurretTransform.rotation, nextAngle, Time.deltaTime * rotationSpeed);
    }

    public override void fireWeapon(Transform tTarget)
    {
      if (!turretController.IsFireEligible)
      {
        return;
      }

      Vector3 forward = turretController.transform.up;
      Vector3 toOther = (tTarget.position - turretController.transform.position).normalized;

      float tempDotProduct = Vector3.Dot(forward, toOther);

      if (tempDotProduct > firingArc)
      {
        if (projectile != null)
        {
          Quaternion tempRotation = turretController.transform.rotation * calcAccuracyDeviation();
          SMProjectile tempProjectile = Instantiate(projectile, turretController.CurrentFiringPosition.position, tempRotation);
          tempProjectile.source = turretController.transform.parent.gameObject;
        }
      }

      turretController.nextFireTime = Time.time + reloadTime;
    }

    //generates random rotations for projectiles based on turret's accuracy
    public virtual Quaternion calcAccuracyDeviation()
    {
      // conversion to inverse of accuracy rating
      // so with an acc of .7 you'd have .3
      float tempAccuracy = 1 - accuracy;

      // generates a random number between a negative value of the calculated number and the normal number
      // this is to get a spread ( -.3 to .3 ), then mult that by 10
      float randomAccuracy = Random.Range(-tempAccuracy, tempAccuracy) * 10;

      // save that number as a quaternion effecting only the Z axis, and return it
      Quaternion tempDeviation = Quaternion.Euler(0f, 0f, randomAccuracy);

      return tempDeviation;
    }
  }
}