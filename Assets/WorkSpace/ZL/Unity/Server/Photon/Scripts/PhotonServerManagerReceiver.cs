using UnityEngine;

namespace ZL.Unity.Server.Photon
{
    [AddComponentMenu("ZL/Server/Photon/Photon Server Manager Receiver")]

    [DisallowMultipleComponent]

    public sealed class PhotonServerManagerReceiver : SingletonReceiver<PhotonServerManager>
    {
        public void ConnectToMaster()
        {
            Instance.ConnectToMaster();
        }
    }
}