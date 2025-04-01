using TMPro;

using UnityEngine;

namespace ZL.Unity.Server.Photon
{
    [AddComponentMenu("ZL/Server/Photon/Photon Player List Item")]

    [DisallowMultipleComponent]

    public sealed class PhotonPlayerListItem : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        private TextMeshProUGUI nicknameText;

        public void Initialize(string nickname)
        {
            nicknameText.text = nickname;
        }
    }
}