using UnityEditor;
using UnityEngine;

namespace SNDL
{
    [CustomEditor(typeof(GameEvent))]
    public class EventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            GameEvent tempEvent = target as GameEvent;
            if (GUILayout.Button("Raise Event"))
            {
                tempEvent.raise();
            }
        }
    }
}