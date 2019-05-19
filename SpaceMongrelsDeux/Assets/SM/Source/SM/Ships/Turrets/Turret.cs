using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SM
{
    [CreateAssetMenu(menuName = "SM/Ships/Turret", order = 98)]
    public class Turret : SMTurret
    {
        protected SMSensorController sensorController;
        public override void initialize(SMTurretController tTurretController)
        {
            SMTurretController tempTurretController = tTurretController.GetComponent<SMTurretController>();
            if(tempTurretController != null)
            {
                turretController = tempTurretController;
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

        }
    }
}