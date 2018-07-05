using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SM
{
    [CreateAssetMenu(menuName = "SM/Ships/Sensor", order = 99)]
    public class Sensor : SMSensor
    {
        public override void initialize(GameObject tObject)
        {
            SMSensorController tempSensorController = tObject.GetComponent<SMSensorController>();
            if (tempSensorController != null)
            {
                tempSensorController.range = range;
                tempSensorController.sensorCollider.radius = range;
                tempSensorController.scanSpeed = scanSpeed;
                tempSensorController.sortRate = sortRate;
                targetReticule = Instantiate(targetReticuleSource, new Vector3(), Quaternion.identity);
            }
        }

        public override void addTarget(ITargetable tTarget)
        {
            targetList.Add(tTarget);
        }

        public override void removeTarget(ITargetable tTarget)
        {
            targetList.Remove(tTarget);
        }

        public override bool sortTargets()
        {
            if (targetList.Count > 0)
            {
                //SORT THE TARGETS
                // Debug.Log("WE've GOT TARGETs: " + targetList[0]);
                // Debug.Log("WE've GOT TARGETs: " + targetList.Count);
                return true;
            }
            else return false;
        }

        public override void selectNextTarget()
        {
            if (sortTargets())
            {
                int nextTargetIndex;
                nextTargetIndex = selectedTargetIndex + 1 > targetList.Count - 1 ? 0 : selectedTargetIndex + 1;
                setTargetReticule(targetList[nextTargetIndex]);
            }
        }

        public override void selectPreviousTarget()
        {
            if (sortTargets())
            {
                int nextTargetIndex;
                nextTargetIndex = selectedTargetIndex - 1 < targetList.Count ? targetList.Count - 1 : selectedTargetIndex - 1;
                setTargetReticule(targetList[nextTargetIndex]);
            }
        }

        protected void setTargetReticule(ITargetable tTarget)
        {
            tTarget.setSelected(targetReticule);
            // Transform tTargetTransform = tTarget.GameObject.Transform; 
            //targetReticule
        }
    }
}
