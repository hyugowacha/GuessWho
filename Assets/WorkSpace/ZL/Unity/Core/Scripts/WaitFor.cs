using System.Collections.Generic;

using UnityEngine;

namespace ZL.Unity.Collections
{
	public static class WaitFor
	{
        private static readonly WaitForEndOfFrame endOfFrame = new();

        private static readonly WaitForFixedUpdate fixedUpdate = new();

        private static readonly Dictionary<float, WaitForSeconds> secondsDictionary = new(new FloatComparer());

        private static readonly Dictionary<float, WaitForSecondsRealtime> secondsRealtimeDictionary = new(new FloatComparer());

        public static WaitForEndOfFrame EndOfFrame()
        {
            return endOfFrame;
        }

        public static WaitForFixedUpdate FixedUpdate()
        {
            return fixedUpdate;
        }

        public static WaitForSeconds Seconds(float seconds)
        {
            if (secondsDictionary.TryGetValue(seconds, out var waitForSeconds) == false)
            {
                secondsDictionary.Add(seconds, waitForSeconds = new(seconds));
            }

            return waitForSeconds;
        }

        public static WaitForSecondsRealtime SecondsRealtime(float seconds)
        {
            if (secondsRealtimeDictionary.TryGetValue(seconds, out var waitForSecondsRealtime) == false)
            {
                secondsRealtimeDictionary.Add(seconds, waitForSecondsRealtime = new(seconds));
            }

            return waitForSecondsRealtime;
        }

        private class FloatComparer : IEqualityComparer<float>
        {
            bool IEqualityComparer<float>.Equals(float x, float y)
            {
                return x == y;
            }

            int IEqualityComparer<float>.GetHashCode(float obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}