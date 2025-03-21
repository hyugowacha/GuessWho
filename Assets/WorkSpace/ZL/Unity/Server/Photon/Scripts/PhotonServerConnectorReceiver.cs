using UnityEngine;

namespace ZL.Unity.Server.Photon
{
    [AddComponentMenu("ZL/Server/Photon/Photon Server Connector Receiver")]

    [DisallowMultipleComponent]

    public sealed class PhotonServerConnectorReceiver : SingletonReceiver<PhotonServerConnector>
    {
        public void TryConnect()
        {
            Instance.TryConnect();
        }
    }
}