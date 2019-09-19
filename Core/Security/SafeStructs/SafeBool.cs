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
            rand = new Random((int)DateTime.Now.Ticks);
            offset = rand.Next(-1000, +1000);
            this.value = (value ? 1 : 0) + offset;
        }

        private bool GetValue()
        {
            return (value - offset) >= 1;
        }

        public static implicit operator bool (SafeBool sb)
        {
            return sb.GetValue();
        }

        public static implicit operator SafeBool (bool val)
        {
            return new SafeBool(val);
        }

        public override string ToString()
        {
            return GetValue().ToString();
        }

        public override bool Equals(object obj)
        {
            return GetValue().Equals(obj);
        }

        public override int GetHashCode()
        {
            return GetValue().GetHashCode();
        }

    }
}