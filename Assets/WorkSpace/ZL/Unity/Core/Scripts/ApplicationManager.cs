using UnityEngine;

using ZL.Unity.IO;

namespace ZL.Unity
{
    [CreateAfterSceneLoad]

    public class ApplicationManager : MonoBehaviour
    {
        public static ApplicationManager Instance { get; private set; }

        [Space]

        [SerializeField]

        private bool runInBackground = true;

        [Space]

        [SerializeField]

        private IntPrefs targetFrameRate = new("Target Frame Rate", 60);

        private void Awake()
        {
            Instance = this;

            Application.runInBackground = runInBackground;

            targetFrameRate.TryLoadValue();

            Application.targetFrameRate = targetFrameRate.Value;
        }

        private void OnValidate()
        {
            Application.runInBackground = runInBackground;
        }

        public static void TargetFrameRate(int value)
        {
            Instance.targetFrameRate.Value = value;

            Application.targetFrameRate = value;
        }
    }
}