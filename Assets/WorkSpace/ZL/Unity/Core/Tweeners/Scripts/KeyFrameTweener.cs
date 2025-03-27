using DG.Tweening.Plugins.Options;

using UnityEngine;

using ZL.Unity.Collections;

namespace ZL.Unity.Tweeners
{
    public abstract class KeyFrameTweener<TComponentTweener, TValueTweener, T1, T2, TPlugOptions> : MonoBehaviour

        where TComponentTweener : ComponentValueTweener<TValueTweener, T1, T2, TPlugOptions>

        where TValueTweener : ValueTweener<T1, T2, TPlugOptions>

        where TPlugOptions : struct, IPlugOptions
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnly(true)]

        [GetComponent]

        protected TComponentTweener componentTweener;

        [Space]

        [SerializeField]

        protected LoopList<T2> keyFrames;

        private void Awake()
        {
            SetKeyFrame(keyFrames.Index);
        }

        public abstract void SetKeyFrame(int index);

        public void TweenKeyFrameNext()
        {
            ++keyFrames.Index;

            TweenKeyFrame();
        }

        public void TweenKeyFramePrev()
        {
            --keyFrames.Index;

            TweenKeyFrame();
        }

        public void TweenKeyFrame(int index)
        {
            keyFrames.Index = index;

            TweenKeyFrame();
        }

        protected virtual void TweenKeyFrame()
        {
            if (keyFrames.TryGetCurrent(out T2 result) == true)
            {
                componentTweener.Tween(result);
            }
        }
    }
}