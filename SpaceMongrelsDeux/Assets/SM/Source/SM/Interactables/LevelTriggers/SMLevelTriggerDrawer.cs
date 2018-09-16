using UnityEditor;
using UnityEngine;

namespace SM
{
    [CustomPropertyDrawer(typeof(SMLevelTrigger))]
    public class SMLevelTriggerDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Debug.Log("property: " + property);
            EditorGUI.BeginProperty(position, label, property);
            var levelToLoadNameRect = new Rect(position.x, position.y, 30, position.height);
            EditorGUI.PropertyField(levelToLoadNameRect, property.FindPropertyRelative("levelToLoadName"), GUIContent.none);
            EditorGUI.EndProperty();
        }
    }
}
