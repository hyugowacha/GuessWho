using System.Collections;

using UnityEngine;

using ZL.Unity.Server.Photon;

namespace ZL.Unity.GuessWho
{
    [AddComponentMenu("ZL/Guess Who/Title Scene Director")]

    public sealed class TitleSceneDirector : PhotonSceneDirector
    {
        protected override IEnumerator Start()
        {
            yield return base.Start();

            ISingleton<PhotonServerManager>.Instance.ConnectToMaster();
        }
    }
}