using Photon.Pun;

using System.Collections;

using UnityEngine;

using ZL.Unity.Server.Photon;

namespace ZL.Unity.GuessWho
{
    [AddComponentMenu("ZL/Guess Who/Title Scene Director (Singleton)")]

    public sealed class TitleSceneDirector : PhotonSceneDirector
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Alias("Click SFX Clip")]

        private AudioClip clickSFXClip;

        [SerializeField]

        private AudioSource audioSource;

        protected override IEnumerator Start()
        {
            yield return base.Start();

            if (PhotonNetwork.IsConnected == false)
            {
                ISingleton<PhotonServerManager>.Instance.ConnectToMaster();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) == true)
            {
                audioSource.PlayOneShot(clickSFXClip);
            }
        }
    }
}