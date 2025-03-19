using System;

namespace ZL.Unity
{
    public static class TypeExtension
    {
        public static bool IsIncludedIn(this Type instance, params Type[] types)
        {
            foreach (var type in types)
            {
                if (instance == type)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsSubclassIn(this Type instance, params Type[] types)
        {
            foreach (var type in types)
            {
                if (instance.IsSubclassOf(type) == true)
                {
                    return true;
                }
            }

            return false;
        }
    }
}