using UnityEngine;

using UnityEngine.EventSystems;

namespace ZL.Unity
{
    public static class PointerEventDataExtensions
    {
        public static Vector2 LocalPosition(this PointerEventData instance, RectTransform target)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(target, instance.position, instance.pressEventCamera, out var localPosition);

            return localPosition;
        }
    }
}