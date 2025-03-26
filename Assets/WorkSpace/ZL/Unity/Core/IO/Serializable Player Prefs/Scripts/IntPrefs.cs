using System;

namespace ZL.Unity.IO
{
    [Serializable]

    public sealed class IntPrefs : SerializablePlayerPrefs<int>
    {
        public IntPrefs(string key, int value) : base(key, value) { }

        public override void LoadValue()
        {
            Value = GetInt(key);
        }

        public override void SaveValue()
        {
            SetInt(key, value);
        }
    }
}