using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace UnityUtility
{
	/// <summary>
	/// An GameObject that will live through the whole game process
	/// </summary>
	public class GlobalObject : MonoBehaviour
	{
		/// <summary>
        /// For quick search components on thie object
		/// </summary>
		private Dictionary<Type, Component> _components = new Dictionary<Type, Component>();

		private static GlobalObject _instance;

		private static Dictionary<Type, Component> Components
		{
			get { return Instance._components; }
		}

		/// <summary>
		/// Intacnce of GlobalObject
		/// </summary>
		public static GlobalObject Instance
		{
			get
			{
				if (_instance != null) return _instance;
				_instance = FindObjectOfType<GlobalObject>();

				if (_instance != null) return _instance;

				_instance = new GameObject("GlobalObject").AddComponent<GlobalObject>();
				
				return _instance;
			}
		}

		private static GameObject _deactivedObject;

        /// <summary>
        /// An object that will stay deactived through out the whole game process
        /// </summary>
		public static GameObject DeactivedObject
		{
			get
			{
				if (_deactivedObject == null)
				{
					_deactivedObject = new GameObject("HidenObject");
					_deactivedObject.transform.SetParent(Instance.transform);
					_deactivedObject.SetActive(false);
				}

				return _deactivedObject;
			}
		}

		/// <summary>
		/// Add a component on GlobalObject
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public static T AddComponent<T>() where T : Component
		{
			var type = typeof(T);
			Component comp;

			if (Components.TryGetValue(type, out comp))
			{
				Debug.LogWarning("GlobalObject already contained a component of type " + type.ToString());
				return (T)comp;
			}

			comp = Instance.gameObject.AddComponent<T>();
			Components.Add(type, comp);
			return (T)comp;
		}

        /// <summary>
        /// Get a component from GlobalObject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public new static T GetComponent<T>() where T : Component
		{
			var type = typeof(T);
			Component comp;

			// If cannot find one in components 
			if (!Components.TryGetValue(type, out comp))
			{
				comp = Instance.gameObject.GetComponent<T>();
				if (comp != null)
					Components.Add(type, comp);
			}

			return (T)comp;
		}

        /// <summary>
        /// Get a component from GlobalObject, create one if that componenet isn't existed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static T GetOrAddComponent<T>() where T : Component
		{
			var obj = GetComponent<T>();
			if (obj == null)
				obj = AddComponent<T>();
			return obj;
		}

        /// <summary>
        /// Remove a component from GlobalObject, return false if that componenet isn't existed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool RemoveComponent<T>() where T : Component
		{
			var type = typeof(T);
			Component comp;
			if (!Components.TryGetValue(type, out comp)) return false;

			Destroy(comp);
			Components.Remove(type);
			
			return true;
		}

		/// <summary>
		/// 保存玩家数据到硬盘中
		/// </summary>
		private static void SaveState()
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream saveFile = File.Create(Application.persistentDataPath + "/.gb");

			formatter.Serialize(saveFile, Instance);

			saveFile.Close();
		}

		/// <summary>
		/// 从硬盘中读取数据
		/// </summary>
		private static void LoadState()
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream saveFile;

			try
			{
				saveFile = File.Open(Application.persistentDataPath + "/.gb", FileMode.Open);
			}
			catch (FileNotFoundException)
			{
				return;
			}

			try
			{
				_instance = (GlobalObject)formatter.Deserialize(saveFile);
			}
			catch (SerializationException)
			{
				Debug.LogWarning("读取到旧版本存档，旧版本存档将会被覆盖");
			}

			saveFile.Close();
		}

		private void Awake()
		{
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

			DontDestroyOnLoad(this.gameObject);
		}
	}
}
