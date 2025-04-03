using System.Collections;

using TMPro;

using UnityEngine;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/UI/Forced TMP Mesh Updater")]

    [DisallowMultipleComponent]

    [RequireComponent(typeof(TextMeshProUGUI))]

    public sealed class ForcedTMPMeshUpdater : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        [PropertyField]

        [ReadOnly(false)]

        [Button(nameof(ForceMeshUpdate))]

        private TextMeshProUGUI text;

        private void OnEnable()
        {
            ForceMeshUpdate();
        }

        public void ForceMeshUpdate()
        {
            StartCoroutine(ForceMeshUpdateRoutine());
        }

        private IEnumerator ForceMeshUpdateRoutine()
        {
            yield return null;

            text.ForceMeshUpdate();
        }
    }
}