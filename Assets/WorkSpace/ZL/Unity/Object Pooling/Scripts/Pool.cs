using System.Collections.Generic;

using ZL.Unity.Collections;

namespace ZL.Unity.Pooling
{
    public abstract class Pool<T>

        where T : class
    {
        private readonly LinkedList<T> stock = new();

        public virtual T Generate()
        {
            if (stock.Count == 0)
            {
                return Clone();
            }

            return stock.PopLast();
        }

        public abstract T Clone();

        public virtual void Collect(T value)
        {
            stock.AddLast(value);
        }

        public void Release(T value)
        {
            stock.Remove(value);
        }
    }
}