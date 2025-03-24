using System.Collections;

using UnityEngine;

using ZL.Unity.Server.Photon;

namespace ZL.Unity.GuessWho
{
    [AddComponentMenu("ZL/Guess Who/Title Scene Diretor")]

    public sealed class TitleSceneDiretor : PhotonSceneDirector
    {
        protected override IEnumerator Start()
        {
            yield return base.Start();

            ISingleton<PhotonServerManager>.Instance.TryConnectToMaster();
        }
    }
}