using UnityEngine;
using UnityEditor;
using UniToolkit.Security;

namespace UniToolkitEditor
{
    [CustomPropertyDrawer(typeof(SafeInt))]
    public class SafeIntDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var offset = property.FindPropertyRelative("offset");
            var value = property.FindPropertyRelative("value");

            int targetval = value.intValue - offset.intValue;

            EditorGUI.BeginProperty(position, label, property);

            targetval = EditorGUI.IntField(position, property.displayName, targetval);

            if (GUI.changed)
            {
                var rand = new System.Random(new System.Random(946426189).Next());

                offset.intValue = rand.Next(-1000, 1000);

                value.intValue = targetval + offset.intValue;
            }

            EditorGUI.EndProperty();
        }
    }
}