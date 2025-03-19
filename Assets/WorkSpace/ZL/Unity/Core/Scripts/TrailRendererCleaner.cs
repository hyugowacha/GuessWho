using UnityEngine;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/Trail Renderer Cleaner")]

    [DisallowMultipleComponent]

    [RequireComponent(typeof(TrailRenderer))]

    public sealed class TrailRendererCleaner : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnly(true)]

        [GetComponent]

        private TrailRenderer trailRenderer;

        private void OnDisable()
        {
            trailRenderer.Clear();
        }
    }
}