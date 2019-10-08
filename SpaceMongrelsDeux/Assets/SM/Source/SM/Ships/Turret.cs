using UnityEngine;

namespace SM
{
  public abstract class Turret : ScriptableObject
  {
    public string turretName;
    public SMProjectile projectile;
    public int damageMultiplier = 1;
    public float reloadTime;

    [Range(0, 2)]
    public float rotationSpeed;


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