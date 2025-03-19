using System.Collections.Generic;

namespace ZL.Unity.Collections
{
    public static class LinkedListExtentions
    {
        public static T PopFirst<T>(this LinkedList<T> instance)
        {
            T value = instance.First.Value;

            instance.RemoveFirst();

            return value;
        }

        public static T PopLast<T>(this LinkedList<T> instance)
        {
            T value = instance.Last.Value;

            instance.RemoveLast();

            return value;
        }
    }
}