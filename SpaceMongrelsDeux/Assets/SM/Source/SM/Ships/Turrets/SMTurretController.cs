using UnityEngine;

namespace SM
{
  public class SMTurretController : MonoBehaviour
  {
    public SMTurret turret;
    public SMSensorController sensor;

    //set by attached turret
    [HideInInspector]
    public float damage, rotationSpeed, accuracy;

    public bool isWeaponsFree = false;

    public float nextFireTime;
    public bool IsFireEligible
    {
      get
      {
        return Time.time >= nextFireTime ? true : false;
      }
    }

    void Start()
    {
      initTurretData();
    }

    void initTurretData()
    {
      if (turret != null)
      {
        turret.initialize(this);
      }
    }

    void Update()
    {
      if (isWeaponsFree && sensor.currentTarget != null)
      {
        Transform tempTarget = sensor.currentTarget.getTransform();
        turret.rotateTowardTarget(tempTarget);

        //TODO implement firing behaviour HERE
        turret.fireWeapon(tempTarget);
      }
    }

    public void setWeaponsFree(bool tIsWeaponsFree)
    {
      isWeaponsFree = tIsWeaponsFree;
    }

    public void toggleWeaponsFree()
    {
      isWeaponsFree = isWeaponsFree ? false : true;
    }
  }
}