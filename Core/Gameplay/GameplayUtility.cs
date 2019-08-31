using UnityEngine;

namespace UniToolkit.Gameplay
{
    /// <summary>
    /// Class that contains helpers functions for gameplay
    /// </summary>
    public static class GameplayUtility
    {
        /// <summary>
        /// Check a layermask to a GameObject
        /// </summary>
        /// <param name="mask">Reference LayerMask</param>
        /// <param name="Other">GameObject to check</param>
        /// <returns></returns>
        public static bool CompareLayerMask(this GameObject Other , LayerMask mask)
        {
            return (((1 << Other.layer) & mask) != 0);
        }

        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            T comp = gameObject.GetComponent<T>();
            if (comp != null )
            {
                return comp;
            }else
            {
                return gameObject.AddComponent<T>();
            }
        }

        /// <summary>
        /// Clamp this vector
        /// </summary>
        /// <param name="Min">Minimum value</param>
        /// <param name="Max">Maximun value</param>
        public static void ClampVector( this Vector3 vec , Vector3 Min , Vector3 Max)
        {
            vec.Set(Mathf.Clamp(vec.x, Min.x, Max.x), Mathf.Clamp(vec.y, Min.y, Max.y), Mathf.Clamp(vec.z,
                Min.z, Max.z)); 
        }

        /// <summary>
        /// Calculates direction to target vector based on axis normal
        /// </summary>
        /// <param name="target">Target Vector</param>
        /// <param name="Normal">Axis normal</param>
        public static Vector3 ComputeDirection(this Vector3 origin, Vector3 target , Vector3 Normal)
        {
            Vector3 dir = (target - origin);
            dir.Scale(Normal);
            return dir;
        }
    }
}