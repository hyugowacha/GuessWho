using System.Collections;

using UnityEngine;

using ZL.Unity.Server.Photon;

namespace ZL.Unity.GuessWho
{
    [AddComponentMenu("ZL/GuessWho/TitleSceneDirector")]

    public sealed class TitleSceneDirector : PhotonSceneDirector
    {
        protected override IEnumerator Start()
        {
            yield return base.Start();

            ISingleton<PhotonServerManager>.Instance.ConnectToMaster();
        }
    }
}