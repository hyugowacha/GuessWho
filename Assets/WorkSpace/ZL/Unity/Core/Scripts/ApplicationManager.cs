using UnityEngine;

using ZL.Unity.IO;

namespace ZL.Unity
{
    [CreateAfterSceneLoad]

    public class ApplicationManager : MonoBehaviour, IMonoSingleton<ApplicationManager>
    {
        [Space]

        [SerializeField]

        private bool runInBackground = true;

        [Space]

        [SerializeField]

        private IntPref targetFrameRatePref = new("Target Frame Rate", 60);

        private void OnValidate()
        {
            Application.runInBackground = runInBackground;
        }

        private void Awake()
        {
            if (ISingleton<ApplicationManager>.TrySetInstance(this) == false)
            {
                return;
            }

            Application.runInBackground = runInBackground;

            targetFrameRatePref.ActionOnValueChanged += (value) =>
            {
                Application.targetFrameRate = value;
            };

            targetFrameRatePref.TryLoadValue();

            Application.targetFrameRate = targetFrameRatePref.Value;
        }

        private void OnDestroy()
        {
            ISingleton<ApplicationManager>.Release(this);
        }
    }
}