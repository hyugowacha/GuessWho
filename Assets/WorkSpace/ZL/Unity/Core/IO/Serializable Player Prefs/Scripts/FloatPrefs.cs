using System;

namespace ZL.Unity.IO
{
    [Serializable]

    public sealed class FloatPrefs : SerializablePlayerPrefs<float>
    {
        public FloatPrefs(string key, float value) : base(key, value) { }

        public override void LoadValue()
        {
            Value = GetFloat(key);
        }

        public override void SaveValue()
        {
            SetFloat(key, value);
        }
    }
}