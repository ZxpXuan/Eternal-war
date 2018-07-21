using UnityEngine;
using UnityEditor;

namespace UnityUtility.Editor
{
	[CustomPropertyDrawer (typeof (MinMaxSlider))]
	class MinMaxSliderDrawer : PropertyDrawer {
		float m_Min;
		float m_Max;
		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {

			if (property.propertyType == SerializedPropertyType.Vector2) {
				Vector2 range = property.vector2Value;
				float min = range.x;
				float max = range.y;

				float space = 5;
				float labelWidth = position.width * 0.3f;
				float valueWidth = 50;

				float labelPos = position.x;
				float minPos = labelPos + labelWidth + space;
				float maxPos = position.x + position.width - valueWidth;

				float sliderPos = minPos + valueWidth + space;
				float sliderWidth = maxPos - sliderPos - space;

				Rect labelRect = 	new Rect(labelPos,	position.y, labelWidth,  position.height);
				Rect minRect = 		new Rect(minPos,	position.y, valueWidth,  position.height);
				Rect sliderRect = 	new Rect(sliderPos, position.y, sliderWidth, position.height);
				Rect maxRect = 		new Rect(maxPos, 	position.y, valueWidth,  position.height);

				MinMaxSlider attr = attribute as MinMaxSlider;

				EditorGUI.BeginChangeCheck ();
				EditorGUI.LabelField(labelRect, label);
				min = EditorGUI.FloatField(minRect, range.x);
				max = EditorGUI.FloatField(maxRect, range.y);
				EditorGUI.MinMaxSlider(sliderRect, ref min, ref max, attr.min, attr.max);
				if (EditorGUI.EndChangeCheck ()) {
					if (min > max && max == m_Max) min = max;
					if (max < min && min == m_Min) max = min;
					min = min<attr.min? attr.min: min;
					max = max>attr.max? attr.max: max;

					range.x = min;
					range.y = max;
					property.vector2Value = range;
					m_Max = max;
					m_Min = min;
				}
			} else {
				EditorGUI.LabelField (position, label, "Only useable with Vector2");
			}
		}
	}
}
