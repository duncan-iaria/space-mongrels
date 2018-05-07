using UnityEngine;

namespace SM
{
    [CreateAssetMenu(menuName = "SM/Interactables/LevelTrigger", order = 100)]
    public class SMLevelTrigger : SMInteractable2
    {
        public string levelToLoad;

        public override void onEnter()
        {
            Debug.Log("Entered the Trigger");
        }
        public override void onExit()
        {
            Debug.Log("Exited the Trigger");
        }
        public override void onInteract()
        {
            Debug.Log("Interacted with Trigger: " + levelToLoad);
        }
    }
}
