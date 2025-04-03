using Photon.Pun;

using UnityEngine;

using ZL.Unity.IO;

namespace ZL.Unity.Server.Photon
{
    [AddComponentMenu("ZL/Server/Photon Player Manager (Singleton)")]

    [DisallowMultipleComponent]

    public sealed class PhotonPlayerManager
        
        : MonoBehaviour, ISingleton<PhotonPlayerManager>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnlyWhenPlayMode]

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
}