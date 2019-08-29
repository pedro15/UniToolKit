using UnityEngine;

namespace UniToolkit.Utility
{
    /// <summary>
    /// Singleton Desing pattern implementation
    /// </summary>
    /// <typeparam name="T">Class</typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T eInstance;

        [SerializeField,Tooltip("This GameObject should be persistent between scenes ?")]
        private bool Persistent = false;
        
        protected virtual void Awake()
        {
            eInstance = null;
            if (Persistent)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        
        public static T Instance
        {
            get
            {
                if (!eInstance)
                {
                    eInstance = FindObjectOfType<T>();
                }
                return eInstance;
            }
        }
    }
}