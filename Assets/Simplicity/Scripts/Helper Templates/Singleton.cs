using UnityEngine;

namespace Simplicity
{

    /// <summary>
    /// Singleton Helper class
    /// Inherit from Singleton and use the class as T
    /// </summary>
    /// <typeparam name="T">
    /// Class name that will be the singleton.
    /// T will be a MonoBehaviour.
    /// </typeparam>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<T>();

                if (FindObjectsOfType<T>().Length > 1)
                {
                    Debug.LogWarning("There is more than one " + typeof(T).GetType().ToString());
                    return null;
                }

                return _instance as T;
            }
        }

        public bool destroyOnLoad;

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                if (!destroyOnLoad)
                    DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }
    }
}