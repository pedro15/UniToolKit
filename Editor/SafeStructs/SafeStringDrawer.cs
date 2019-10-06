using UnityEngine;
using UnityEditor;
using UniToolkit.Security;

namespace UniToolkitEditor
{
    [CustomPropertyDrawer(typeof(SafeString))]
    public class SafeStringDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var str = property.FindPropertyRelative("str");

            var off = property.FindPropertyRelative("offset");

            if (GUI.changed || off.intValue == 0)
            {
                off.intValue = Random.Range(-1000, 1000);
            }

            EditorGUI.BeginProperty(position, label, property);

            string estring = EditorGUI.TextField(position, property.displayName,
                SafeString.UnSecureText(str.stringValue, off.intValue));

            str.stringValue = SafeString.SecureText(estring, off.intValue);

            EditorGUI.EndProperty();
        }
    }
}