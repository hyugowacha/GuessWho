using Photon.Pun;

using UnityEngine;

using ZL.Unity.IO;

namespace ZL.Unity.Server.Photon
{
    [AddComponentMenu("ZL/Server/Photon/Photon Player Manager (Singleton)")]

    public class PhotonPlayerManager : PhotonPlayerManager<PhotonPlayerManager> { }

    [DisallowMultipleComponent]

    public abstract class PhotonPlayerManager<T> :
        
        MonoBehaviourPunCallbacks, ISingleton<T>

        where T : PhotonPlayerManager<T>
    {
        [Space]

        [SerializeField]

        private StringPref nicknamePref = new("Nickname", string.Empty);

        public string Nickname => nicknamePref.Value;

        [Space]

        [SerializeField]

        private GameObject playerPrefab;

        [Space]

        [SerializeField]

        private Transform[] playerSpawnPoint;

        private void Awake()
        {
            ISingleton<T>.TrySetInstance((T)this);

            nicknamePref.ActionOnValueChanged += (value) =>
            {
                PhotonNetwork.NickName = value;
            };

            nicknamePref.TryLoadValue();
        }

        private void OnDestroy()
        {
            ISingleton<T>.Release((T)this);
        }

        public bool TrySetNickname(string nickname, out NicknameValidationException exception)
        {
            if (nickname.IsValidNickname(out exception) == false)
            {
                return false;
            }

            nicknamePref.SaveValue(nickname);

            return true;
        }

        public void Spawn(Vector3 position, Quaternion rotation)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, position, rotation);
        }

        public void SpawnRandom()
        {
            int randomPoint = Random.Range(0, playerSpawnPoint.Length);

            Spawn(playerSpawnPoint[randomPoint].position, Quaternion.identity);
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