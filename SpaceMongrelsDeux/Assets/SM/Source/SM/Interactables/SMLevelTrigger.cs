using UnityEngine;
using UnityEngine.Events;
using SNDL;

namespace SM
{
    [CreateAssetMenu(menuName = "SM/Interactables/LevelTrigger", order = 100)]
    public class SMLevelTrigger : SMInteractable
    {
        public int levelToLoadIndex;

        public override void onEnter() { }
        public override void onExit() { }
        public override void onDeselect() { }

        public override void onInteract()
        {
            SMGame tempGame = Game.GetGame<SMGame>();
            tempGame.onLoadLevel(levelToLoadIndex);
        }
    }
}
