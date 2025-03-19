using System.Collections;

using UnityEngine;

using ZL.Unity.Server.Photon;

namespace ZL.Unity.PhotonServerManagerDemo
{
    [AddComponentMenu("")]

    [DisallowMultipleComponent]

    public sealed class PhotonServerManagerDemoSceneDirector :
        
        SceneDirector<PhotonServerManagerDemoSceneDirector>
    {
        protected override IEnumerator Start()
        {
            yield return base.Start();

            ISingleton<PhotonServerConnector>.Instance.TryConnect();
        }
    }
}