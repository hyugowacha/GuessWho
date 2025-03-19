using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity
{
	[DisallowMultipleComponent]

	public sealed class Invoker : MonoBehaviour
	{
        [Space]

		[SerializeField]

		private float time = 0f;

        public float Time
        {
            get => time;

            set => time = value;
        }

        [Space]

        [SerializeField]

        private UnityEvent eventOnTime;

        public UnityEvent EventOnTime => eventOnTime;

        private void OnEnable()
        {
            Invoke(nameOfOnTime, time);
        }

        private void OnDisable()
        {
            CancelInvoke(nameOfOnTime);
        }

        private const string nameOfOnTime = nameof(OnTime);

        private void OnTime()
        {
            eventOnTime.Invoke();
        }
	}
}