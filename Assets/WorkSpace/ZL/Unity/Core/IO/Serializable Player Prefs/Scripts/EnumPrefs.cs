using System;

using ZL.CS;

namespace ZL.Unity.IO
{
    [Serializable]

    public sealed class EnumPrefs<TEnum> : SerializablePlayerPrefs<TEnum>

        where TEnum : Enum
    {
        public EnumPrefs(string key, TEnum value) : base(key, value) { }

        public override void LoadValue()
        {
            Value = GetInt(key).ToEnum<TEnum>();
        }

        public override void SaveValue()
        {
            SetInt(key, value.ToInt());
        }
    }
}