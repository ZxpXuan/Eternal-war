using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Area))]
public class AreaAttributeDrawer : PropertyDrawer
{
	SerializedProperty area;
	SerializedProperty areaSize;
	SerializedProperty casterPosition;

	readonly float lineHeight = EditorGUIUtility.singleLineHeight;
	const float toggleSize = 14f;
	const float toggleSpace = 2f;

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		// Take shortcut for values.
		if (area == null)
		{
			area = property.FindPropertyRelative("rawArea");
			areaSize = property.FindPropertyRelative("areaSize");
			casterPosition = property.FindPropertyRelative("selfPosition");
		}

		var height = lineHeight;

		if (property.isExpanded)
		{
			height += lineHeight * 3;

			if (area.isExpanded)
			{
				height += (toggleSize + toggleSpace) * (areaSize.vector2IntValue.y);
			}
		}

		return height;
	}

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		label = EditorGUI.BeginProperty(position, label, property);
		position.height = lineHeight;

		property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label);

		// If this property isn't expanded, ignore the rest of the codes.
		if (!property.isExpanded)
		{
			EditorGUI.EndProperty();
			return;
		}

		EditorGUI.indentLevel++;

		// To next line
		position.y += lineHeight;

		// Draw Caster Position field
		var prefixedPosition = EditorGUI.PrefixLabel(position, new GUIContent("Caster Position"));
		var newCasterPos = EditorGUI.Vector2IntField(prefixedPosition, "", casterPosition.vector2IntValue);

		// Making sure Caster Position is within the range of area.
		newCasterPos.x = Mathf.Clamp(newCasterPos.x, 1, areaSize.vector2IntValue.x);
		newCasterPos.y = Mathf.Clamp(newCasterPos.y, 1, areaSize.vector2IntValue.y);
		casterPosition.vector2IntValue = newCasterPos;

		// To next line
		position.y += lineHeight;

		// Draw Area Size field
		prefixedPosition = EditorGUI.PrefixLabel(position, new GUIContent("Area Size"));
		areaSize.vector2IntValue = EditorGUI.Vector2IntField(prefixedPosition, "", areaSize.vector2IntValue);

		// Resize Area of Effect
		area.arraySize = areaSize.vector2IntValue.x * areaSize.vector2IntValue.y;

		// To next line
		position.y += lineHeight;

		// Draw Area of Effect field
		area.isExpanded = EditorGUI.Foldout(position, area.isExpanded, "Area of Effect");

		// To next line
		position.y += lineHeight;

		// If Area of Effect is expanded, draw the toggle field.
		if (area.isExpanded)
		{
			EditorGUI.indentLevel++;
			DrawAreaOfEffect(position);
		}

		EditorGUI.EndProperty();
	}

	void DrawAreaOfEffect(Rect position)
	{
		var size = areaSize.vector2IntValue;
		var caster = casterPosition.vector2IntValue;
		var origPosition = position;

		position.width = toggleSize + EditorGUI.indentLevel * 14;
		position.height = toggleSize;

		for (int x = 0; x < size.x; x ++)
		{
			for (int y = 0; y < size.y; y ++)
			{
				if (x != caster.x-1 || y != caster.y-1)
				{
					area.GetArrayElementAtIndex(x * size.y + y).boolValue = EditorGUI.Toggle(position, area.GetArrayElementAtIndex(x * size.y + y).boolValue);
				}
				else
				{
					EditorGUI.BeginDisabledGroup(true);
					EditorGUI.Toggle(position, true);
					EditorGUI.EndDisabledGroup();
				}
				
				position.y += toggleSize + toggleSpace;
			}
			position.y = origPosition.y;
			position.x += toggleSize + toggleSpace;
		}
	}
}
