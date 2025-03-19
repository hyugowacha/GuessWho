using System;

using System.Collections;

using System.Collections.Generic;

using UnityEngine;

namespace ZL.Unity.Collections
{
    [Serializable]

    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField]

        protected List<SerializableKeyValuePair<TKey, TValue>> elements = new();

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (elements != null)
            {
                Clear();

                foreach (var element in elements)
                {
                    TryAdd(element.Key, element.Value);
                }
            }

#if !UNITY_EDITOR

            elements = null;

#endif
        }

#if UNITY_EDITOR

        public new void Add(TKey key, TValue value)
        {
            base.Add(key, value);

            elements.Add(new(key, value));
        }

#endif
    }

    [Serializable]

    public class SerializableDictionary<TKey, TValue, TKeyValueContainer> : IEnumerable<TKeyValueContainer>, ISerializationCallbackReceiver

        where TKeyValueContainer : IKeyValueContainer<TKey, TValue>
    {
        [SerializeField]

        private List<TKeyValueContainer> elements = new();

        private readonly Dictionary<TKey, TKeyValueContainer> @base = new();

        public TValue this[TKey key]
        {
            get => @base[key].Value;

            set => @base[key].Value = value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<TKeyValueContainer> GetEnumerator()
        {
            return @base.Values.GetEnumerator();
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (elements != null)
            {
                @base.Clear();

                foreach (var element in elements)
                {
                    if (element != null)
                    {
                        @base.TryAdd(element.Key, element);
                    }
                }
            }

#if !UNITY_EDITOR

            elements = null;

#endif
        }

        public void Add(TKeyValueContainer element)
        {
            @base.Add(element.Key, element);

#if UNITY_EDITOR

            elements.Add(element);

#endif
        }
        public void Clear()
        {
            @base.Clear();

#if UNITY_EDITOR

            elements.Clear();

#endif
        }
    }
}