using System.Collections;

using UnityEngine;

using UnityEngine.SceneManagement;

using ZL.Unity.Collections;

using ZL.Unity.Tweeners;

using ZL.Unity.UI;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/Scene Director")]

    [DisallowMultipleComponent]

    public sealed class SceneDirector : SceneDirector<SceneDirector> { }

    public abstract class SceneDirector<T> : MonoBehaviour, ISingleton<T>

        where T : SceneDirector<T>
    {
        [Space]

        [SerializeField]

        private CanvasGroupFader screenFader;

        [Space]

        [SerializeField]

        private float startDelay = 0.5f;

        [SerializeField]

        private float fadeDuration = 1f;

        private int pauseCount = 0;

        private void Awake()
        {
            ISingleton<T>.TrySetInstance((T)this);
        }

        protected virtual IEnumerator Start()
        {
            yield return WaitFor.Seconds(startDelay);

            if (ISingleton<AudioListenerVolumeTweener>.Instance != null)
            {
                ISingleton<AudioListenerVolumeTweener>.Instance.Tween(1f, fadeDuration);
            }

            if (screenFader != null)
            {
                screenFader.SetFaded(false, fadeDuration);
            }

            yield return WaitFor.Seconds(fadeDuration);
        }

        private void OnDestroy()
        {
            ISingleton<T>.Release((T)this);
        }

        public virtual void EndScene() { }

        public void LoadScene(string scaneName)
        {
            StartCoroutine(LoadSceneRoutine(scaneName));
        }

        private IEnumerator LoadSceneRoutine(string scaneName)
        {
            if (ISingleton<AudioListenerVolumeTweener>.Instance != null)
            {
                ISingleton<AudioListenerVolumeTweener>.Instance.Tween(0f, fadeDuration);
            }

            if (screenFader != null)
            {
                screenFader.SetFaded(true, fadeDuration);
            }

            yield return WaitFor.Seconds(fadeDuration);

            SceneManager.LoadScene(scaneName);
        }

        public void Pause()
        {
            ++pauseCount;

            Time.timeScale = 0f;
        }

        public void Resume()
        {
            if (--pauseCount <= 0)
            {
                pauseCount = 0;

                Time.timeScale = 1f;
            }
        }

        public void Quit()
        {
            FixedApplication.Quit();
        }
    }
}