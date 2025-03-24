using System.Collections;

using UnityEngine;

using ZL.Unity.Server.Photon;

namespace ZL.Unity.PhotonLobbyDemo
{
    [AddComponentMenu("")]

    public sealed class PhotonLobbyDemoSceneDirector : PhotonSceneDirector
    {
        protected override IEnumerator Start()
        {
            yield return base.Start();

            ISingleton<PhotonServerConnector>.Instance.TryConnect();
        }
    }
}