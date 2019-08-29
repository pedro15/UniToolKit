using System;

namespace UniToolkit.Utility
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SingletonPrefabAttribute : Attribute
    {
        public readonly string PrefabPath;
        /// <summary>
        /// Singleton Prefab
        /// </summary>
        /// <param name="PathToResources">Prefab path relative to Resources folder</param>
        public SingletonPrefabAttribute(string PathToResources)
        {
            PrefabPath = PathToResources;
        }
    }
}