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
        public List<ITargetable> targetList = new List<ITargetable>();

        protected int selectedTargetIndex = 0;
        protected GameObject targetReticule;

        public abstract void initialize(GameObject tObject);
        public abstract void addTarget(ITargetable tTarget);
        public abstract void removeTarget(ITargetable tTarget);
        public abstract bool sortTargets();
        public abstract void selectNextTarget();
        public abstract void selectPreviousTarget();
    }
}
