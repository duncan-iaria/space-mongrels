using UnityEngine;
using UnityEngine.Events;
using SNDL;

namespace SM
{
    [CreateAssetMenu(menuName = "SM/Interactables/LevelTrigger", order = 100)]
    public class SMLevelTrigger : SMInteractable
    {
        public bool isLoadByIndex = false;
        public int levelToLoadIndex;
        public string levelToLoadName;

        public override void onEnter() { }
        public override void onExit() { }
        public override void onDeselect() { }

        public override void onInteract()
        {
            Debug.Log("level trigger");
            SMGame tempGame = Game.GetGame<SMGame>();
            if (isLoadByIndex)
            {
                tempGame.onLoadLevel(levelToLoadIndex);
            }
            else
            {
                Debug.Log("level load by name");
                tempGame.onLoadLevelByName(levelToLoadName);
            }
        }
    }
}
