using UnityEngine;
using System.Reflection;

namespace UniToolkit.Utility
{
    /// <summary>
    /// Singleton Desing pattern implementation
    /// </summary>
    /// <typeparam name="T">Class</typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T eInstance;

        protected virtual bool Persistent
        {
            get { return false; }
        }

        public static T Instance
        {
            get
            {
                if (!eInstance)
                {
                    eInstance = FindObjectOfType<T>();

                    if (!eInstance)
                    {
                        SingletonPrefabAttribute m_prefab = typeof(T).GetCustomAttribute<SingletonPrefabAttribute>(true);
                        if (m_prefab != null && !string.IsNullOrEmpty(m_prefab.PrefabPath))
                        {
                            eInstance = Instantiate(Resources.Load<GameObject>(m_prefab.PrefabPath)).GetComponent<T>();
                        }
                    }
                }
                return eInstance;
            }
        }

        protected virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }

            if (Persistent)
            {
                DontDestroyOnLoad(gameObject);
            }
            eInstance = GetComponent<T>();
        }

    }
}