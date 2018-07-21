using UnityEngine;

namespace UnityUtility
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T _instance;
        protected static T Instance
        {
            get
            {
                if (_instance) return _instance;
                _instance = FindObjectOfType<T>();

                if (_instance) return _instance;
                _instance = GlobalObject.GetOrAddComponent<T>();
                return _instance;
            }
        }
    }
}
