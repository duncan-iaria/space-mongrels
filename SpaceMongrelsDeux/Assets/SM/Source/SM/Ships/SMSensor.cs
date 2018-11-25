using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SM
{

    public abstract class SMSensor : ScriptableObject
    {
        public float range;
        public float scanSpeed;
        public float sortRate;
        public GameObject targetReticuleSource;

        // TODO this is an inheirent problem in using a SO here,
        // as it writes to a single list
        // so technically right now enemies will write to the same list
        public List<ITargetable> targetList = new List<ITargetable>();

        protected int selectedTargetIndex = -1;
        protected GameObject targetReticule;

        public abstract void initialize(GameObject tObject);
        public abstract void addTarget(ITargetable tTarget);
        public abstract void removeTarget(ITargetable tTarget);
        public abstract void clearTargetList();
        public abstract bool sortTargets(Vector3 tSensorOrigin);
        public abstract void selectNextTarget(Vector3 tSensorOrigin);
        public abstract void selectPreviousTarget(Vector3 tSensorOrigin);
    }
}
