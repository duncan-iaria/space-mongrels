﻿using UnityEngine;

namespace SM
{
  public class SMSensorController : MonoBehaviour
  {
    public SMSensor sensor;
    public ITargetable currentTarget = null;

    [HideInInspector]
    public float range, scanSpeed, sortRate, collisionCheckSweepAngle = 12f;

    [HideInInspector]
    public CircleCollider2D sensorCollider;

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
      currentTarget = sensor.selectNextTarget(this.transform.position);
    }

    public void selectPreviousTarget()
    {
      currentTarget = sensor.selectPreviousTarget(this.transform.position);
    }

    public void clearAllTargets()
    {
      sensor.clearTargetList();
    }

    public CollisionDirection checkForCollisions()
    {
      return sensor.checkForCollisions(this.transform);
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

    public void OnDisable()
    {
      sensor.clearTargetList();
    }
  }
}