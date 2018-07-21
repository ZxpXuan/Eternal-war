using System;
using System.Reflection;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

namespace UnityUtility
{
	public static class ExtensionMethods
	{
		/// <summary>
		/// Checks whether a Unity object exists; i.e. it hasn't been destroyed
		/// </summary>
		/// <remarks>
		/// Unlike the normal null check, this will return <c>true</c> for objects
		/// that have been merely disabled.
		/// </remarks>
		/// <param name="obj"></param>
		/// <returns><c>true</c> if the object exists, otherwise <c>false</c></returns>
		public static bool IsExists(this object obj)
		{
			if (obj == null)
				return false;
			bool exists = false;
			var o = obj as UnityEngine.Object;
			if ((object)o != null)
				try
				{
					o.hideFlags.Equals(null);
					exists = true;
				}
				catch { }
			return exists;
		}

		/// <summary>
		/// Gets component from another component if it has one, and adds one if it doesn't
		/// </summary>
		/// <param name="obj"></param>
		/// <returns><c>true</c> if the object exists, otherwise <c>false</c></returns>
		public static T GetOrAddComponent<T>(this Component c) where T : Component
		{
			if (c == null) throw new ArgumentNullException("The operational component is null!");
			var part = c.GetComponent<T>();
			if (part == null)
				part = c.gameObject.AddComponent<T>();

			return part;
		}

		/// <summary>
		/// Returns the square of the given number
		/// </summary>
		/// <param name="num"></param>
		/// <returns></returns>
		public static float Square(this float num)
		{
			return num * num;
		}

		/// <summary>
		/// Clamps an angle between -180 and +180
		/// </summary>
		/// <param name="angle"></param>
		/// <returns></returns>
		public static float Clamp180(this float angle)
		{
			if (angle > 180f)
				angle -= 360f;
			else if (angle < -180f)
				angle += 360f;
			return angle;
		}

		/// <summary>
		/// Clamps an euler rotation between -180 and +180
		/// </summary>
		/// <param name="angle"></param>
		/// <returns></returns>
		public static Vector3 Clamp180(this Vector3 angle)
		{
			return new Vector3(angle.x.Clamp180(),
				angle.y.Clamp180(),
				angle.z.Clamp180());
		}

		/// <summary>
		/// Clamps an euler rotation between -180 and +180
		/// </summary>
		/// <param name="angle"></param>
		/// <returns></returns>
		public static Vector2 Clamp180(this Vector2 angle)
		{
			return new Vector2(angle.x.Clamp180(),
				angle.y.Clamp180());
		}

		public static Vector3Int ToVector3Int(this Vector3 vec)
		{
			vec.x += vec.x < 0 ? -1 : 0;
			vec.y += vec.y < 0 ? -1 : 0;
			vec.z += vec.z < 0 ? -1 : 0;
			return new Vector3Int((int)vec.x, (int)vec.y, (int)vec.z);
		}

		public static bool IsWithInBound(this Bounds self, Bounds other)
		{
			return other.Contains(self.max) && other.Contains(self.min);
		}

		// From TOMS...
		// TODO: Figure out those 'reflection' stuffs.
		/*
		static readonly char[] dotSplit = { '.' };
		static readonly Regex indexRegex = new Regex(@"data\[(\d+)\]", RegexOptions.Compiled);

		public static object GetObject(this SerializedProperty property)
		{
			object parentObject;
			return GetObject(property, out parentObject);
		}

		public static object GetObject(this SerializedProperty property, out object parentObject)
		{
			const BindingFlags bf =
				BindingFlags.Instance |
				BindingFlags.Public |
				BindingFlags.NonPublic;
			if (property == null)
				throw new ArgumentNullException();
			object obj = property.serializedObject.targetObject;
			parentObject = null;
			var pathTokens = property.propertyPath.Split(dotSplit);
			for (int i = 0; i < pathTokens.Length; i++)
			{
				parentObject = obj;
				if (obj == null)
					return null;
				var field = obj.GetType().GetField(pathTokens[i], bf);
				if (field == null)
				{
					if (pathTokens[i] != "Array")
					{
						Debug.LogError("Unable to find field " + pathTokens[i] +
									   ". Maybe it's private? (fix this)");
						return null;
					}
					var match = indexRegex.Match(pathTokens[++i]);
					if (!match.Success)
					{
						Debug.LogError("Regex was not a match: " + pathTokens[i]);
						return null;
					}
					var index = Int32.Parse(match.Groups[1].Value);
					var list = obj as IList;
					if (list == null || index < 0 || index >= list.Count)
					{
						Debug.LogError("Unable to index: " + obj);
						return null;
					}
					obj = list[index];
				}
				else
				{
					obj = field.GetValue(obj);
				}
			}
			return obj;
		}
		*/
	}
}
