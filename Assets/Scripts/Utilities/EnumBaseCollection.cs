using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnityUtility
{
	[Serializable]
	public abstract class EnumBaseCollection
	{
		public abstract void Init();
	}

	/// <summary>
	/// Collectaion based on enum values
	/// </summary>
	/// <typeparam name="E"></typeparam>
	/// <typeparam name="V"></typeparam>
	/// TODO: Figure out how to make a serializable Dictionary
	[Serializable]
	public abstract class EnumBasedCollection<E, V> : EnumBaseCollection, ISerializationCallbackReceiver, IEnumerable<V> where E : struct, IConvertible
	{
		[NonSerialized]
		protected Dictionary<E, V> dict = new Dictionary<E, V>();

		[SerializeField]
		protected List<E> keys = new List<E>();

		[SerializeField]
		protected List<V> vals = new List<V>();

		public Type EnumType { get; private set; }

		public EnumBasedCollection()
		{
			if (!typeof(E).IsEnum)
				throw new ArgumentException(typeof(E).Name + " is not a Enum type!");
			EnumType = typeof(E);
			var eValues = Enum.GetValues(EnumType);
			int enumNum = eValues.Length;

			dict = new Dictionary<E, V>();

			for (int i = 0; i < enumNum; i++)
			{
				dict.Add((E)eValues.GetValue(i), default(V));
				keys.Add((E)eValues.GetValue(i));
				vals.Add(default(V));
			}
		}

		public EnumBasedCollection(EnumBasedCollection<E, V> collection)
		{
			var length = collection.keys.Count;

			dict = new Dictionary<E, V>();

			for (int i = 0; i < length; i++)
			{
				dict.Add(collection.keys[i], collection.vals[i]);
				keys.Add(collection.keys[i]);
				vals.Add(collection.vals[i]);
			}
		}

		/// <summary>
		/// Clear all contains
		/// </summary>
		public void Clear()
		{
			dict.Clear();
			keys.Clear();
			vals.Clear();
		}

		/// <summary>
		/// Reset all the values to default values
		/// </summary>
		public void Reset()
		{
			var eValues = Enum.GetValues(EnumType);

			Clear();

			for (int i = 0; i < eValues.Length; i++)
			{
				dict.Add((E)eValues.GetValue(i), default(V));
				keys.Add((E)eValues.GetValue(i));
				vals.Add(default(V));
			}
		}

		/// <summary>
		/// Initialise the object if haven't initialise it before.
		/// </summary>
		public override void Init()
		{
			if (keys != null && keys.Count > 0) return;

			Debug.Log("Clear");
			Clear();
			var eValues = Enum.GetValues(typeof(E));
			int enumNum = eValues.Length;

			dict = new Dictionary<E, V>();

			for (int i = 0; i < enumNum; i++)
			{
				dict.Add((E)eValues.GetValue(i), default(V));
				keys.Add((E)eValues.GetValue(i));
				vals.Add(default(V));
			}
		}

		public IEnumerator<V> GetEnumerator()
		{
			return ((IEnumerable<V>)vals).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<V>)vals).GetEnumerator();
		}

		public virtual V this[E key]
		{
			get
			{
				Init();
				return dict[key];
			}

			set
			{
				Init();
				dict[key] = value;
			}
		}

		////// Unity Serialization //////

		public void OnBeforeSerialize()
		{
			var values = Enum.GetValues(EnumType);
			if (dict == null)
			{
				dict = new Dictionary<E, V>();
				for (int i = 0; i < values.Length; i++)
				{
					dict[(E)values.GetValue(i)] = default(V);
				}
			}

			while (keys.Count < values.Length) keys.Add(default(E));
			while (vals.Count < values.Length) vals.Add(default(V));
			
			for (int i = 0; i < values.Length; i++)
			{
				keys[i] = ((E)values.GetValue(i));
				vals[i] = (dict[keys[i]]);
			}
		}

		public void OnAfterDeserialize()
		{
			if (dict == null) dict = new Dictionary<E, V>();
			for (int i = 0; i < keys.Count; i++)
			{
				dict[keys[i]] = vals[i];
			}
		}
	}
}
