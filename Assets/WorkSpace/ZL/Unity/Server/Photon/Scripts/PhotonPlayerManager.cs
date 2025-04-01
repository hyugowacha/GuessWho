using Photon.Pun;

using UnityEngine;

using ZL.Unity.IO;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/Server/Photon Player Manager (Singleton)")]

    [DisallowMultipleComponent]

    public sealed class PhotonPlayerManager :
        
        MonoBehaviour, ISingleton<PhotonPlayerManager>
    {
        [Space]

        [SerializeField]

        private StringPref nicknamePref = new("Nickname", string.Empty);

        public string Nickname => nicknamePref.Value;

        private void Awake()
        {
            ISingleton<PhotonPlayerManager>.TrySetInstance(this);

            nicknamePref.ActionOnValueChanged += (value) =>
            {
                PhotonNetwork.NickName = value;
            };

            nicknamePref.TryLoadValue();
        }
        private void OnDestroy()
        {
            ISingleton<PhotonPlayerManager>.Release(this);
        }

        public bool TrySetNickname(string nickname, out NicknameValidationException exception)
        {
            if (nickname.IsValidNickname(out exception) == false)
            {
                FixedDebug.LogWarning($"Try Set Nickname failed: {exception}");

                return false;
            }

            nicknamePref.SaveValue(nickname);

            return true;
        }
    }

    public enum NicknameValidationException
    {
        None,

        NullOrEmpty,
    }

    public static partial class StringExtensions
    {
        public static bool IsValidNickname(this string instance)
        {
            return IsValidNickname(instance, out var exception);
        }

        public static bool IsValidNickname(this string instance, out NicknameValidationException exception)
        {
            if (instance.IsNullOrEmpty() == true)
            {
                exception = NicknameValidationException.NullOrEmpty;

                return false;
            }

            exception = NicknameValidationException.None;

            return true;
        }
    }
}