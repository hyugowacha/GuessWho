using System;

namespace ZL.Unity.IO
{
    [Serializable]

    public sealed class StringPrefs : SerializablePlayerPrefs<string>
    {
        public StringPrefs(string key, string value) : base(key, value) { }

        public override void LoadValue()
        {
            Value = GetString(key);
        }

        public override void SaveValue()
        {
            SetString(key, value);
        }
    }
}