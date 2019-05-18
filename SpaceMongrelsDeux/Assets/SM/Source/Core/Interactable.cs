using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SNDL
{
    public abstract class Interactable : ScriptableObject
    {
        public abstract void onEnter();
        public abstract void onExit();
        public abstract void onInteract();
        public abstract void onDeselect();
    }
}
