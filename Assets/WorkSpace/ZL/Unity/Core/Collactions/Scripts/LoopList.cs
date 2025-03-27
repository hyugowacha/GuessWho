using System.Collections.Generic;

using System;

using UnityEngine;

namespace ZL.Unity.Collections
{
    [Serializable]

    public sealed class LoopList<T>
    {
        [SerializeField]

        private int index = 0;

        public int Index
        {
            get => index;

            set
            {
                index = value;

                if (index.IsOutOfRange(0, list.Count) == true)
                {
                    return;
                }

                switch (loopType)
                {
                    case LoopType.Clamp:

                        index = index.Clamp(0, list.Count);

                        break;

                    case LoopType.Repeat:

                        index = index.Repeat(0, list.Count);

                        break;

                    case LoopType.PingPong:

                        index = index.PingPong(0, list.Count);

                        break;
                }
            }
        }

        [SerializeField]

        private LoopType loopType = LoopType.None;

        [Space]

        [SerializeField]

        private List<T> list = new();

        public bool TryGetCurrent(out T result)
        {
            if (list.Count == 0)
            {
                result = default;

                return false;
            }

            result = list[index];

            return true;
        }

        public enum LoopType
        {
            None,

            Clamp,

            Repeat,

            PingPong,
        }
    }
}