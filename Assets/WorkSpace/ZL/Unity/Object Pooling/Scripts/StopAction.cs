using UnityEngine;

namespace ZL.Unity.Pooling
{
    public abstract class StopAction<T> : MonoBehaviour

        where T : Component
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnly(true)]

        [GetComponent]

        protected T target;

        public T Target => target;

        public abstract bool IsStoped { get; }

        private void LateUpdate()
        {
            if (IsStoped == true)
            {
                gameObject.SetActive(false);
            }
        }
    }
}