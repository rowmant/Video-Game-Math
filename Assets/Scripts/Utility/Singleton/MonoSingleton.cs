using UnityEngine;

namespace Kiraio.Utility
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : Component
    {
        static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>(); // First pass
                    if (_instance == null)
                        _instance = new GameObject(typeof(T).Name, typeof(T)).GetComponent<T>(); // Second pass
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                _instance.transform.parent = null;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }
    }
}
