using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityUtility
{
	/// <summary>
	/// Serializable dictionary
	/// </summary>
	[Serializable]
	public class SDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
	{
		[SerializeField]
		List<TKey> keys = new List<TKey>();

		[SerializeField]
		List<TValue> values = new List<TValue>();

		public void OnAfterDeserialize()
		{
			Clear();

			if (keys.Count != values.Count)
			{
				Debug.LogWarning("the number of keys doesn't match the number of values!");
				if (keys.Count > values.Count)
				{
					keys.RemoveRange(values.Count, keys.Count - values.Count);
				}

				if (keys.Count < values.Count)
				{
					values.RemoveRange(keys.Count, values.Count - keys.Count);
				}
			}

			for (int i = 0; i < keys.Count; i++)
			{
				Add(keys[i], values[i]);
			}
		}

		public void OnBeforeSerialize()
		{
			keys = Keys.ToList();
			values = Values.ToList();
		}
	}
}
