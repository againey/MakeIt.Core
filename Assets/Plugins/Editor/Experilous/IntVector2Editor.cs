/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using UnityEditor;

namespace Experilous.Topological
{
	[CustomPropertyDrawer(typeof(IntVector2))]
	public class IntVector2Editor : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			float spacing = 2f;
			float labelWidth = EditorStyles.label.CalcSize(new GUIContent("M")).x;
			float fullFieldWidth = (position.width + spacing) / 2f;
			float inputFieldWidth = fullFieldWidth - labelWidth - spacing;
			
			float offset = position.x;
			GUI.Label(new Rect(offset, position.yMin, labelWidth, position.height), "X");
			property.FindPropertyRelative("x").intValue = EditorGUI.IntField(
				new Rect(offset + labelWidth, position.yMin, inputFieldWidth, position.height),
				property.FindPropertyRelative("x").intValue);

			offset = position.xMax - fullFieldWidth + spacing;
			GUI.Label(new Rect(offset, position.yMin, labelWidth, position.height), "Y");
			property.FindPropertyRelative("y").intValue = EditorGUI.IntField(
				new Rect(offset + labelWidth, position.yMin, inputFieldWidth, position.height),
				property.FindPropertyRelative("y").intValue);

			EditorGUI.EndProperty();
		}
	}
}
