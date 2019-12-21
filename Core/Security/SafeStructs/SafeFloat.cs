using System;

namespace UniToolkit.Security
{
    [Serializable]
    public struct SafeFloat
    {
        [UnityEngine.SerializeField, UnityEngine.HideInInspector]
        private float offset;
        [UnityEngine.SerializeField, UnityEngine.HideInInspector]
        private float value;

        private Random rand;

        public SafeFloat(float value = 0f)
        {
            rand = new Random(new Random(746876180).Next());
            offset = rand.Next(-1000, +1000);
            this.value = value + offset;
        }

        private float GetValue()
        {
            return value - offset;
        }

        public void Dispose()
        {
            offset = 0;
            value = 0;
        }

        public static SafeFloat operator +(SafeFloat v1, SafeFloat v2)
        {
            return new SafeFloat(v1.GetValue() + v2.GetValue());
        }

        public static SafeFloat operator -(SafeFloat v1, SafeFloat v2)
        {
            return new SafeFloat(v1.GetValue() - v2.GetValue());
        }

        public static SafeFloat operator *(SafeFloat v1, SafeFloat v2)
        {
            return new SafeFloat(v1.GetValue() * v2.GetValue());
        }

        public static SafeFloat operator /(SafeFloat v1, SafeFloat v2)
        {
            return new SafeFloat(v1.GetValue() / v2.GetValue());
        }

        public static implicit operator SafeFloat(float fl)
        {
            return new SafeFloat(fl);
        }

        public static implicit operator SafeFloat(int ii)
        {
            return new SafeFloat(ii);
        }

        public static implicit operator float(SafeFloat sf)
        {
            return sf.GetValue();
        }
    }
}
