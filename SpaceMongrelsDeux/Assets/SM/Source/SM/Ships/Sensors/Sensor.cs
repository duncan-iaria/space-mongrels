using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SM
{
  [CreateAssetMenu(menuName = "SM/Ships/Sensor", order = 99)]
  public class Sensor : SMSensor
  {
    protected SMSensorController sensorController;
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
      logTargetList();
    }

    public override void removeTarget(ITargetable tTarget)
    {
      if (selectedTargetIndex >= 0)
      {
        ITargetable currentSelectedTarget = targetList[selectedTargetIndex];
        int removedTargetIndex = targetList.IndexOf(tTarget);
        targetList.Remove(tTarget);

        if (removedTargetIndex == selectedTargetIndex)
        {
          unsetTargetReticule();
        }
        else
        {
          // update the selectedTargetIndex to the new index position of the selectedTarget
          selectedTargetIndex = targetList.IndexOf(currentSelectedTarget);
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

    public override ITargetable selectNextTarget(Vector3 tSensorOrigin)
    {
      if (sortTargets(tSensorOrigin))
      {
        // increment the selected target and set the reticule
        selectedTargetIndex = selectedTargetIndex + 1 > targetList.Count - 1 ? 0 : selectedTargetIndex + 1;

        ITargetable tempTarget = targetList[selectedTargetIndex];
        setTargetReticule(tempTarget);
        return tempTarget;
      }

      return null;
    }

    public override ITargetable selectPreviousTarget(Vector3 tSensorOrigin)
    {
      if (sortTargets(tSensorOrigin))
      {
        // decrement the selected target and set the reticule
        selectedTargetIndex = selectedTargetIndex - 1 < 0 ? targetList.Count - 1 : selectedTargetIndex - 1;

        ITargetable tempTarget = targetList[selectedTargetIndex];
        setTargetReticule(tempTarget);
        return tempTarget;
      }

      return null;
    }

    protected void setTargetReticule(ITargetable tTarget)
    {
      targetReticule.SetActive(true);
      SMTargetReticule tempReticule = targetReticule.GetComponent<SMTargetReticule>();

      if (tempReticule == null)
      {
        Debug.LogWarning("No reticule script on this target reticule!");
        return;
      }

      // TODO Should we do this here? or should we have the ITargetable take care of this
      // In it's setSelected method?

      tempReticule.setNewTarget(tTarget.getTransform());
      // tTarget.setSelected(targetReticule);
    }

    protected void unsetTargetReticule()
    {
      selectedTargetIndex = -1;
      targetReticule.SetActive(false);
    }

    public override void clearTargetList()
    {
      targetList.Clear();
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