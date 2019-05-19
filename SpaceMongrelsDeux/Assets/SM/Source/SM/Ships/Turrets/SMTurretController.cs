using System.Collections;
using System.Collections.Generic;
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

        void Start()
        {
            initTurretData();
        }

        void initTurretData()
        {
            if(turret != null)
            {
                turret.initialize(this);
            }
        }

        void Update()
        {
            if(isWeaponsFree && sensor.currentTarget != null)
            {
                turret.rotateTowardTarget(sensor.currentTarget.getTransform());
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