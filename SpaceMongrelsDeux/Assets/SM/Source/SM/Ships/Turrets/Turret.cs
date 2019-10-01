using UnityEngine;

namespace SM
{
  [CreateAssetMenu(menuName = "SM/Ships/Turret", order = 98)]
  public class Turret : SMTurret
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
          SMProjectile tempProjectile = Instantiate(projectile, turretController.transform.position, turretController.transform.rotation);
          tempProjectile.source = turretController.transform.parent.gameObject;
        }
      }

      turretController.nextFireTime = Time.time + reloadTime;
    }
  }
}