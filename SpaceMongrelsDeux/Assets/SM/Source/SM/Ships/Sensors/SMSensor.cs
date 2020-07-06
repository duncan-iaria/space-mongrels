using UnityEngine;

namespace SM
{
  [CreateAssetMenu(menuName = "SM/Ships/Sensor", order = 99)]
  public class SMSensor : Sensor
  {
    protected SMSensorController sensorController;
    public override void initialize(GameObject tObject)
    {
      SMSensorController tempSensorController = tObject.GetComponent<SMSensorController>();
      if (tempSensorController != null)
      {
        sensorController = tempSensorController;
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

    // TODO clean this all up (use the sensor range for ranges (maybe / 2), etc)
    public override CollisionDirection checkForCollisions(Transform sourceTransform)
    {
      Vector3 leftAngleDirection = Quaternion.AngleAxis(sensorController.collisionCheckSweepAngle, Vector3.forward) * sourceTransform.up;
      Vector3 rightAngleDirection = Quaternion.AngleAxis(-sensorController.collisionCheckSweepAngle, Vector3.forward) * sourceTransform.up;
      RaycastHit2D hitR = Physics2D.Raycast(sourceTransform.position, rightAngleDirection, 5f, layerMask);
      RaycastHit2D hitL = Physics2D.Raycast(sourceTransform.position, leftAngleDirection, 5f, layerMask);

      Debug.DrawRay(sourceTransform.position, leftAngleDirection * 5f, Color.red);
      Debug.DrawRay(sourceTransform.position, rightAngleDirection * 5f, Color.blue);


      //a general hit check, and checks to see if it's the player
      if (hitR.collider != null || hitL.collider != null)
      {
        //check what kind of hit we've got
        //hit both
        if (hitR.collider != null && hitL.collider != null)
        {
          Debug.Log("Hit BOTH!!!- REVERSE");
          return CollisionDirection.Front;
          // reverse();
          //Debug.Log( hitR.collider + " " + hitL.collider );
          //Debug.Log( "Avoid MIDDLE!" );
        }
        //hit right
        else if (hitR.collider != null)
        {
          if (hitR.collider.CompareTag("Player"))
          {
            // pawn.controller.engageTarget = hitR.collider.transform;
            // pawn.controller.currentState.toEngageState();
            // return;
          }
          // checkLeft();
          Debug.Log("Hit Left");
          return CollisionDirection.Left;
        }
        //hit left
        else if (hitL.collider != null)
        {
          if (hitL.collider.CompareTag("Player"))
          {
            // pawn.controller.engageTarget = hitL.collider.transform;
            // pawn.controller.currentState.toEngageState();
            // return;
          }
          // checkRight();
          Debug.Log("Hit Right");
          return CollisionDirection.Right;
        }

        //turn on avoidance mode
        // pawn.engageAvoidanceMode();
      }
      //no hit
      else
      {
        // Hit nothing
        return CollisionDirection.None;
        //start moving again
        // pawn.resetSpeed();
      }

      return CollisionDirection.None;
    }
  }
}