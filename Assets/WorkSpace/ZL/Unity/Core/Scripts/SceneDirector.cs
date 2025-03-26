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

    public class SceneDirector : SceneDirector<SceneDirector> { }

    public abstract class SceneDirector<T> : MonoBehaviour, ISingleton<T>

        where T : SceneDirector<T>
    {
        [Space]

        [SerializeField]

        protected CanvasGroupFader screenFader;

        [Space]

        [SerializeField]

        protected float fadeDuration = 0f;

        private int pauseCount = 0;

        private void Awake()
        {
            ISingleton<T>.TrySetInstance((T)this);
        }

        protected virtual IEnumerator Start()
        {
            FadeIn();

            yield return WaitFor.Seconds(fadeDuration);
        }

        private void OnDestroy()
        {
            ISingleton<T>.Release((T)this);
        }

        public virtual void LoadScene(string sceneName)
        {
            StartCoroutine(LoadSceneRoutine(sceneName));
        }

        protected virtual IEnumerator LoadSceneRoutine(string sceneName)
        {
            FadeOut();

            yield return WaitFor.Seconds(fadeDuration);

            SceneManager.LoadScene(sceneName);
        }

        public void FadeIn()
        {
            if (ISingleton<AudioListenerVolumeTweener>.Instance != null)
            {
                ISingleton<AudioListenerVolumeTweener>.Instance.Tween(1f, fadeDuration);
            }

            if (screenFader != null)
            {
                screenFader.SetFaded(false, fadeDuration);
            }
        }

        public void FadeOut()
        {
            if (ISingleton<AudioListenerVolumeTweener>.Instance != null)
            {
                ISingleton<AudioListenerVolumeTweener>.Instance.Tween(0f, fadeDuration);
            }

            if (screenFader != null)
            {
                screenFader.SetFaded(true, fadeDuration);
            }
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