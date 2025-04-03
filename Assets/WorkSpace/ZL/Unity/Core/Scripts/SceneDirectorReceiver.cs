using UnityEngine;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/Scene Director Receiver")]

    public sealed class SceneDirectorReceiver : SceneDirectorReceiver<SceneDirector>
    {

    }

    public abstract class SceneDirectorReceiver<T> : SingletonReceiver<T>

        where T : SceneDirector<T>
    {
        public void LoadScene(string scaneName)
        {
            Instance.LoadScene(scaneName);
        }

        public void Pause()
        {
            Instance.Pause();
        }

        public void Resume()
        {
            Instance.Resume();
        }

        public void Quit()
        {
            Instance.Quit();
        }
    }
}