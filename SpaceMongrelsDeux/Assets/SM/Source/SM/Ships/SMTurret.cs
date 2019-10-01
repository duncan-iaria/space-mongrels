using System.Collections;
using UnityEngine;

namespace SM
{
  public abstract class SMTurret : ScriptableObject
  {
    public string turretName;
    public SMProjectile projectile;
    public int damageMultiplier = 1;
    public float rotationSpeed, reloadTime;


    [Range(0, 1)]
    public float firingArc;

    [Range(0, 100)]
    public float accuracy;

    protected SMTurretController turretController;
    protected bool isWeaponsFree = false;
    public abstract void initialize(SMTurretController tTurretController);
    public abstract void fireWeapon(Transform tTarget);
    public abstract void rotateTowardTarget(Transform tTarget);
  }
}