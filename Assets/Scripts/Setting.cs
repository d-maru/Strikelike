using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Setting : MonoBehaviour
{

}
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(SerializeField))]
public class FieldNameDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (!property.isArray)  //arrayÇ≈É_ÉÅÇ»óùóRÇÕímÇÁÇÒ
        {
            label.text = property.displayName;
        }

        EditorGUI.PropertyField(position, property, label, true);
    }
}
#endif

