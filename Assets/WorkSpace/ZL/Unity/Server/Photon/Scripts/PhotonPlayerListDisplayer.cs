using Photon.Pun;

using Photon.Realtime;

using UnityEngine;

using ZL.Unity.Pooling;

using ZL.Unity.Collections;

namespace ZL.Unity.Server.Photon
{
    [AddComponentMenu("ZL/Server/Photon/Photon Player List Displayer")]

    public sealed class PhotonPlayerListDisplayer :
        
        PhotonPlayerListDisplayer<PhotonPlayerListItem>
    {

    }

    [DisallowMultipleComponent]

    public abstract class PhotonPlayerListDisplayer<TPlayerListItem> : MonoBehaviour

        where TPlayerListItem : Component, IKeyValueContainer<int, TPlayerListItem>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnlyWhenPlayMode]

        protected ManagedObjectPool<int, TPlayerListItem> playerListItemPool;

        public void Refresh()
        {
            Debug.Log(1);
            
            playerListItemPool.CollectAll();

            foreach (var player in PhotonNetwork.PlayerList)
            {
                Add(player);
            }
        }

        public virtual void Add(Player player)
        {
            playerListItemPool.TryGenerate(player.ActorNumber, out var item);

            item.transform.SetAsLastSibling();

            item.SetActive(true);
        }

        public void Remove(Player player)
        {
            playerListItemPool[player.ActorNumber].SetActive(false);
        }
    }
}