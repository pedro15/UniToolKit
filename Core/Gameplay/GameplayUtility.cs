using System.Collections;
using System.Collections.Generic;
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

        /// <summary>
        /// Get's angle between two points and it's normal
        /// </summary>
        /// <param name="a">Point A</param>
        /// <param name="b">Point B</param>
        /// <param name="Normal">Normal</param>
        /// <returns>Angle</returns>
        public static float GetAngle(Vector3 a, Vector3 b, Vector3 Normal)
        {
            Vector3 Direction = b - a;
            float angle = Vector3.Angle(Direction, Normal);
            return angle;
        }

        public static T GetAddComponent<T>(this GameObject gameObject) where T : Component
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
        public static void Clamp( this Vector3 vec , Vector3 Min , Vector3 Max)
        {
            vec.Set(Mathf.Clamp(vec.x, Min.x, Max.x), Mathf.Clamp(vec.y, Min.y, Max.y), Mathf.Clamp(vec.z,
                Min.z, Max.z)); 
        }
    }
}