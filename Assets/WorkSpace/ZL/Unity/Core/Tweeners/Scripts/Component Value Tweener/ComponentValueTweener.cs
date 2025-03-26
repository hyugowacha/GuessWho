using DG.Tweening.Core;

using DG.Tweening.Plugins.Options;

using UnityEngine;

namespace ZL.Unity.Tweeners
{
    public abstract class ComponentValueTweener<TValueTweener, T1, T2, TPlugOptions> : MonoBehaviour

        where TValueTweener : ValueTweener<T1, T2, TPlugOptions>

        where TPlugOptions : struct, IPlugOptions
    {
        [Space]

        [SerializeField]

        protected TValueTweener tweener;

        public TValueTweener Tweener => tweener;

        public TweenerCore<T1, T2, TPlugOptions> Current => tweener.Current;

        protected abstract T1 Value { get; set; }

        protected virtual void Awake()
        {
            tweener.Getter = () => Value;

            tweener.Setter = value => Value = value;
        }

        public TweenerCore<T1, T2, TPlugOptions> Tween(T2 endValue)
        {
            return tweener.Tween(endValue);
        }

        public TweenerCore<T1, T2, TPlugOptions> Tween(T2 endValue, float duration)
        {
            return tweener.Tween(endValue, duration);
        }
    }
}