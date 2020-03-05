/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using UnityEditor;

namespace MakeIt.Numerics
{
	/// <summary>
	/// Property drawer for editing instances of <see cref="IntVector3"/> within the Unity Editor inspector pane.
	/// </summary>
	[CustomPropertyDrawer(typeof(IntVector3))]
	public class IntVector3Editor : PropertyDrawer
	{
		/// <summary>
		/// Draws the GUI for the <see cref="IntVector3"/> property.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the property GUI.</param>
		/// <param name="property">The SerializedProperty to make the custom GUI for.</param>
		/// <param name="label">The label of this property.</param>
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			float spacing = 2f;
			float labelWidth = EditorStyles.label.CalcSize(new GUIContent("M")).x;
			float fullFieldWidth = (position.width + spacing) / 3f;
			float inputFieldWidth = fullFieldWidth - labelWidth - spacing;
			
			float offset = position.x;
			GUI.Label(new Rect(offset, position.yMin, labelWidth, position.height), "X");
			property.FindPropertyRelative("x").intValue = EditorGUI.IntField(
				new Rect(offset + labelWidth, position.yMin, inputFieldWidth, position.height),
				property.FindPropertyRelative("x").intValue);

			offset = position.x + fullFieldWidth;
			GUI.Label(new Rect(offset, position.yMin, labelWidth, position.height), "Y");
			property.FindPropertyRelative("y").intValue = EditorGUI.IntField(
				new Rect(offset + labelWidth, position.yMin, inputFieldWidth, position.height),
				property.FindPropertyRelative("y").intValue);

			offset = position.xMax - fullFieldWidth + spacing;
			GUI.Label(new Rect(offset, position.yMin, labelWidth, position.height), "Z");
			property.FindPropertyRelative("z").intValue = EditorGUI.IntField(
				new Rect(offset + labelWidth, position.yMin, inputFieldWidth, position.height),
				property.FindPropertyRelative("z").intValue);

			EditorGUI.EndProperty();
		}
	}
}
