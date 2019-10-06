using UnityEngine;
using UnityEditor;
using UniToolkit.Security;

namespace UniToolkitEditor
{
    [CustomPropertyDrawer(typeof(SafeBool))]
    public class SafeBoolDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var offset = property.FindPropertyRelative("offset");
            var value = property.FindPropertyRelative("value");

            float targetval = value.floatValue - offset.floatValue;

            EditorGUI.BeginProperty(position, label, property);

            targetval = EditorGUI.Toggle(position, property.displayName, targetval > 0 ? true : false) ? 1 : 0;

            if (GUI.changed)
            {
                var rand = new System.Random(new System.Random(946426189).Next());

                offset.floatValue = rand.Next(-1000, 1000);

                value.floatValue = targetval + offset.floatValue;
            }

            EditorGUI.EndProperty();
        }
    }
}