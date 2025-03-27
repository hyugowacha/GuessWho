using Photon.Pun;

using UnityEngine;

namespace ZL.Unity.Server.Photon
{
    [AddComponentMenu("ZL/Server/Photon/Photon Scene Director (Singleton)")]

    public class PhotonSceneDirector : PhotonSceneDirector<PhotonSceneDirector> { }

    [RequireComponent(typeof(PhotonView))]

    public abstract class PhotonSceneDirector<T> : SceneDirector<T>
        
        where T : PhotonSceneDirector<T>
    {
        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Toggle(true)]

        private PhotonView photonView;

        public void RPCLoadScene(string sceneName)
        {
            photonView.RPC("LoadScene", RpcTarget.All, sceneName);
        }

        [PunRPC]

        public override void LoadScene(string sceneName)
        {
            base.LoadScene(sceneName);
        }
    }
}