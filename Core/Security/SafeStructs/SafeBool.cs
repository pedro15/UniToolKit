using System;

namespace UniToolkit.Security
{
    [Serializable]
    public struct SafeBool
    {
        [UnityEngine.SerializeField, UnityEngine.HideInInspector]
        private float offset;
        [UnityEngine.SerializeField, UnityEngine.HideInInspector]
        private float value;

        private Random rand;

        public SafeBool(bool value)
        {
            rand = new Random(new Random(746876180).Next());
            offset = rand.Next(-1000, +1000);
            this.value = value ? 1 : 0 + offset;
        }

        public override string ToString()
        {
            return GetValue().ToString();
        }

        private bool GetValue()
        {
            return (value - offset) >= 1;
        }

        public static implicit operator SafeBool (bool bb)
        {
            return new SafeBool(bb);
        }

        public static implicit operator bool(SafeBool sb)
        {
            return sb.GetValue();
        }
    }
}