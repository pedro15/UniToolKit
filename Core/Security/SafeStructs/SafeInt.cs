using System;

namespace UniToolkit.Security
{
    [Serializable]
    public struct SafeInt
    {
        [UnityEngine.SerializeField, UnityEngine.HideInInspector]
        private int offset;
        [UnityEngine.SerializeField, UnityEngine.HideInInspector]
        private int value;

        Random rand;

        public SafeInt(int value = 0)
        {
            rand = new Random(new Random(946426189).Next());
            offset = rand.Next(-1000, +1000);
            this.value = value + offset;
        }

        private int GetValue()
        {
            return value - offset;
        }

        public void Dispose()
        {
            offset = 0;
            value = 0;
        }

        public override string ToString()
        {
            return GetValue().ToString();
        }

        public static SafeInt operator +(SafeInt f1, SafeInt f2)
        {
            return new SafeInt(f1.GetValue() + f2.GetValue());
        }

        public static SafeInt operator -(SafeInt f1, SafeInt f2)
        {
            return new SafeInt(f1.GetValue() - f2.GetValue());
        }

        public static SafeInt operator *(SafeInt f1, SafeInt f2)
        {
            return new SafeInt(f1.GetValue() * f2.GetValue());
        }

        public static SafeInt operator /(SafeInt f1, SafeInt f2)
        {
            return new SafeInt(f1.GetValue() / f2.GetValue());
        }

        public static implicit operator SafeInt (int ii)
        {
            return new SafeInt(ii);
        }

        public static implicit operator SafeInt(float ii)
        {
            return new SafeInt(UnityEngine.Mathf.RoundToInt(ii));
        }

        public static implicit operator int (SafeInt safeint)
        {
            return safeint.GetValue();
        }

    }
}
