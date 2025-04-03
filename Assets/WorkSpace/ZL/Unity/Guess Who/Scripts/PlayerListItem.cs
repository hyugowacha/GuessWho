using TMPro;

using UnityEngine;

using UnityEngine.UI;

using ZL.Unity.Server.Photon;

namespace ZL.Unity.GuessWho
{
    [AddComponentMenu("ZL/Guess Who/Player List Item")]

    public sealed class PlayerListItem : PhotonPlayerListItem<PlayerListItem>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        private Image masterClientIcon;

        public Image MasterClientIcon => masterClientIcon;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        private TextMeshProUGUI nicknameText;

        public TextMeshProUGUI NicknameText => nicknameText;
    }
}