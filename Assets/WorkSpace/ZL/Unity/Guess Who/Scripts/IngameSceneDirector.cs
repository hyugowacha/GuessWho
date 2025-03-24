using Photon.Pun;

using System.Collections;

using UnityEngine;

using ZL.Unity.Server.Photon;

namespace ZL.Unity.GuessWho
{
    [AddComponentMenu("ZL/Guess Who/Ingame Scene Director")]

    [DisallowMultipleComponent]

    public sealed class IngameSceneDirector : PhotonSceneDirector<IngameSceneDirector>
    {
        protected override IEnumerator Start()
        {
            ISingleton<PhotonServerManager>.Instance.TryConnectToMaster();

            yield return base.Start();
        }

        public void RoomTest()
        {
            PhotonNetwork.JoinRandomOrCreateRoom();
        }
    }
}