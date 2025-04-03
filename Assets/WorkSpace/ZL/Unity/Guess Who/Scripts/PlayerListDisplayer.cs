using Photon.Pun;

using Photon.Realtime;

using UnityEngine;

using ZL.Unity.Server.Photon;

namespace ZL.Unity.GuessWho
{
    [AddComponentMenu("ZL/Guess Who/Player List Displayer")]

    [DisallowMultipleComponent]

    public sealed class PlayerListDisplayer : PhotonPlayerListDisplayer<PlayerListItem>
    {
        public override void Add(Player player)
        {
            playerListItemPool.TryGenerate(player.ActorNumber, out var item);

            item.transform.SetAsLastSibling();

            item.MasterClientIcon.SetActive(player.IsMasterClient);
            
            item.NicknameText.text = player.NickName;

            item.SetActive(true);
        }

        public void ReplaceMasterClientIcon()
        {
            int actorNumber = PhotonNetwork.MasterClient.ActorNumber;

            var item = playerListItemPool[actorNumber];

            item.transform.SetAsFirstSibling();

            item.MasterClientIcon.SetActive(true);
        }
    }
}