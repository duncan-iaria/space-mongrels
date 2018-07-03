using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SM
{
    public abstract class SMInteractable : ScriptableObject
    {
        public abstract void onEnter();
        public abstract void onExit();
        public abstract void onInteract();
        public abstract void onDeselect();
    }
}
