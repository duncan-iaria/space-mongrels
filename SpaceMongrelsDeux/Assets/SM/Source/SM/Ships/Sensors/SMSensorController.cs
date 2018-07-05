using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SM
{
    public class SMSensorController : MonoBehaviour
    {
        public SMSensor sensor;
        public CircleCollider2D sensorCollider;
        public List<ITargetable> targetList = new List<ITargetable>();

        [HideInInspector]
        public float range, scanSpeed, sortRate;

        private int selectedIndex = 0;

        void Start()
        {
            sensorCollider = gameObject.GetComponent<CircleCollider2D>();
            if (sensorCollider != null)
            {
                initializeSensorData();
            }
            else
            {
                Debug.LogWarning("No Sensor Collider Assigned to Sensor");
            }
        }

        void initializeSensorData()
        {
            if (sensor != null)
            {
                sensor.initialize(gameObject);
            }
        }

        public void selectNextTarget()
        {
            sensor.selectNextTarget();
        }

        public void selectPreviousTarget()
        {
            sensor.selectPreviousTarget();
        }

        protected virtual void OnTriggerEnter2D(Collider2D tCollider)
        {
            ITargetable tempTarget = tCollider.GetComponent<ITargetable>();
            if (tempTarget != null)
            {
                sensor.addTarget(tempTarget);
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D tCollider)
        {
            ITargetable tempTarget = tCollider.GetComponent<ITargetable>();
            if (tempTarget != null)
            {
                sensor.removeTarget(tempTarget);
            }
        }

    }
}
