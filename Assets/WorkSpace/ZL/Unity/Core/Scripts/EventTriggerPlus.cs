using UnityEngine;

using UnityEngine.EventSystems;

using UnityEngine.Events;

namespace ZL.Unity
{
    [DisallowMultipleComponent]

    public sealed class EventTriggerPlus : EventTrigger
    {
        private readonly UnityEvent onPointerDown = new();

        private readonly UnityEvent onPointerUp = new();

        private void Awake()
        {
            Entry entry = new()
            {
                eventID = EventTriggerType.PointerDown,
            };

            entry.callback.AddListener(call => OnPointerDown());

            triggers.Add(entry);

            entry = new()
            {
                eventID = EventTriggerType.PointerUp,
            };

            entry.callback.AddListener(call => OnPointerUp());

            triggers.Add(entry);
        }

        public void AddListener(EventTriggerType eventTriggerType, UnityAction unityAction)
        {
            switch (eventTriggerType)
            {
                case EventTriggerType.PointerDown:

                    onPointerDown.AddListener(unityAction);

                    break;

                case EventTriggerType.PointerUp:

                    onPointerUp.AddListener(unityAction);

                    break;

                default:

                    break;
            }
        }

        private void OnPointerDown()
        {
            onPointerDown?.Invoke();
        }

        private void OnPointerUp()
        {
            onPointerUp?.Invoke();
        }
    }
}