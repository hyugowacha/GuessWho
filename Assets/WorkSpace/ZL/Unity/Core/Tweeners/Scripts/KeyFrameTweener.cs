using DG.Tweening;

using DG.Tweening.Core;

using DG.Tweening.Plugins.Options;

using System;

using System.Collections.Generic;

using UnityEngine;

namespace ZL.Unity.Tweeners
{
    [Serializable]

    public sealed class KeyFrames<T> : List<T>
    {
        [Space]

        [SerializeField]

        private int index = 0;

        public int Index
        {
            get => index;

            set
            {
                
            }
        }

        private int delta = 0;

        public T Current()
        {
            return this[index];
        }

        public T Current(int index)
        {
            Index = index;

            return this[Index];
        }

        public T Next()
        {
            ++delta;

            Index = delta;

            return this[index];
        }

        public T Prev()
        {
            return this[--Index];
        }
    }

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

        private float duration;

        [Space]

        [SerializeField]

        protected KeyFrames<T2> keyFrames;

        private void OnValidate()
        {
            if (keyFrames.Count != 0)
            {
                SetKeyFrame(keyFrames.Index);
            }
        }

        [Space]

        [SerializeField]

        private bool playOnStart = false;

        [SerializeField]

        [UsingCustomProperty]

        [ToggleIf("loop", false)]

        private LoopType loopType = LoopType.Restart;

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

        protected virtual TweenerCore<T1, T2, TPlugOptions> TweenKeyFrame()
        {
            return componentTweener.Tween(keyFrames.Current(), duration);
        }
    }
}