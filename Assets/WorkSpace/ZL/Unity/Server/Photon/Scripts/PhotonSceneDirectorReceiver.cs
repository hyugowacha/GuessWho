using UnityEngine;

namespace ZL.Unity.Server.Photon
{
    [AddComponentMenu("ZL/Server/Photon/Photon Scene Director Receiver")]

    public sealed class PhotonSceneDirectorReceiver : PhotonSceneDirectorReceiver<PhotonSceneDirector> { }

    public abstract class PhotonSceneDirectorReceiver<T> : SceneDirectorReceiver<T>

        where T : PhotonSceneDirector<T>
    {
        public void RPCLoadScene(string sceneName)
        {
            Instance.RPCLoadScene(sceneName);
        }
    }
}