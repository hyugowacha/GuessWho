using TMPro;

using UnityEngine;

using ZL.Unity.IO;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Frame Rate Displayer (Singleton)")]

    [DisallowMultipleComponent]

    public sealed class FrameRateDisplayer
        
        : MonoBehaviour, ISingleton<FrameRateDisplayer>
    {
        [Space]

        [SerializeField]

        private TextMeshProUGUI text = null;

        [Space]

        [SerializeField]

        private string format = "{0:0.0} ms ({1:0} fps)";

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [PropertyField]

        [Button(nameof(Load))]

        [Button(nameof(Save))]

        private BoolPref displayFrameRatePref = new("Display Frame Rate", true);

        private float time = 0f;

        private float ms;

        private float fps;

#if UNITY_EDITOR

        private void OnValidate()
        {
            if (Application.isPlaying == true)
            {
                displayFrameRatePref.Value = displayFrameRatePref.Value;
            }
        }

#endif

        private void Awake()
        {
            ISingleton<FrameRateDisplayer>.TrySetInstance(this);

            displayFrameRatePref.ActionOnValueChanged += (value) =>
            {
                gameObject.SetActive(value);
            };

            displayFrameRatePref.TryLoadValue();
        }

        private void Update()
        {
            time += (Time.unscaledDeltaTime - time) * 0.1f;

            ms = 1000f * time;

            fps = 1f / time;

            text.text = string.Format(format, ms, fps);
        }

        private void OnDestroy()
        {
            ISingleton<FrameRateDisplayer>.Release(this);
        }

        public void Load()
        {
            displayFrameRatePref.LoadValue();
        }

        public void Save()
        {
            displayFrameRatePref.SaveValue();
        }
    }
}