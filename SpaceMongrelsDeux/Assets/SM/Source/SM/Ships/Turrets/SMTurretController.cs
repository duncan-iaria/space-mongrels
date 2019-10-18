using System.Collections.Generic;
using UnityEngine;

namespace SM
{
  public class SMTurretController : MonoBehaviour
  {
    public Turret turret;
    public SMSensorController sensor;

    //set by attached turret
    [HideInInspector]
    public float damage, rotationSpeed, accuracy;

    public bool isWeaponsFree = false;

    [HideInInspector]
    public float nextFireTime;
    public bool IsFireEligible
    {
      get
      {
        return Time.time >= nextFireTime ? true : false;
      }
    }

    private int currentFiringPositionIndex = 0;
    public List<Transform> firingPositions = new List<Transform>();
    public Transform firingPositionsMaster;

    public Transform CurrentFiringPosition
    {
      get
      {
        ++currentFiringPositionIndex;
        if (currentFiringPositionIndex >= 0 && currentFiringPositionIndex < firingPositions.Count)
        {
          return firingPositions[currentFiringPositionIndex];
        }
        else
        {
          currentFiringPositionIndex = 0;
          return firingPositions[currentFiringPositionIndex];
        };
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
      else Debug.LogWarning("No turret data object assigned! Your turret will have issues!");

      getFiringPositions();
    }

    private void getFiringPositions()
    {
      if (firingPositionsMaster == null)
      {
        Debug.LogWarning("No firing positions set for turret, defaulting to turret origin.");
        firingPositions.Add(this.transform);
      }
      else
      {
        Transform[] tempFiringPositions = firingPositionsMaster.GetComponentsInChildren<Transform>();
        if (tempFiringPositions.Length > 0)
        {
          foreach (Transform tempTransform in tempFiringPositions)
          {
            // For some reason the parent returns its transform as well
            // So we're throwing not adding it (if it's the parent's)
            if (tempTransform != firingPositionsMaster)
            {
              firingPositions.Add(tempTransform);
            }
          }
        }
        else
        {
          firingPositions.Add(this.transform);
        };
      }
    }

    void Update()
    {
      if (isWeaponsFree)
      {
        Transform tempTarget = sensor.currentTarget.getTransform();

        if (tempTarget == null)
        {
          return;
        }

        turret.rotateTowardTarget(tempTarget);
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