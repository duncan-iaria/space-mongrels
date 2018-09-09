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
                selectedTargetIndex = -1;
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
            if( selectedTargetIndex >= 0 )
            {                
                ITargetable currentSelectedTarget = targetList[selectedTargetIndex];
                int removedTargetIndex = targetList.IndexOf( tTarget );
                targetList.Remove(tTarget);

                if( removedTargetIndex == selectedTargetIndex )
                {
                    unsetTargetReticule();
                } 
                else
                {
                    // update the selectedTargetIndex to the new index position of the selectedTarget
                    selectedTargetIndex = targetList.IndexOf( currentSelectedTarget );
                }
            }
            else 
            {
                targetList.Remove(tTarget);
            }
        }

        public override bool sortTargets(Vector3 tSensorOrigin)
        {
            if (targetList.Count > 0)
            {
                targetList.Sort(delegate (ITargetable targetA, ITargetable targetB)
                {
                    return Vector2.Distance(tSensorOrigin, targetA.getTransform().position)
                    .CompareTo(Vector2.Distance(tSensorOrigin, targetB.getTransform().position));
                });
                return true;
            }
            else return false;
        }

        public override void selectNextTarget(Vector3 tSensorOrigin)
        {
            if (sortTargets(tSensorOrigin))
            {
                // increment the selected target and set the reticule
                selectedTargetIndex = selectedTargetIndex + 1 > targetList.Count - 1 ? 0 : selectedTargetIndex + 1;
                setTargetReticule(targetList[selectedTargetIndex]);
            }
        }

        public override void selectPreviousTarget(Vector3 tSensorOrigin)
        {
            if (sortTargets(tSensorOrigin))
            {
                // decrement the selected target and set the reticule
                selectedTargetIndex = selectedTargetIndex - 1 < 0 ? targetList.Count - 1 : selectedTargetIndex - 1;
                setTargetReticule(targetList[selectedTargetIndex]);
            }
        }

        protected void setTargetReticule(ITargetable tTarget)
        {
            targetReticule.SetActive( true );
            tTarget.setSelected(targetReticule);
        }

        protected void unsetTargetReticule()
        {
            selectedTargetIndex = -1;
            targetReticule.SetActive( false );    
        }

        // simple utility to print the target list to the console
        private void logTargetList()
        {
            targetList.ForEach(delegate (ITargetable tTarget)
            {
                Debug.Log(tTarget.getTransform().gameObject);
            });
        }
    }
}
