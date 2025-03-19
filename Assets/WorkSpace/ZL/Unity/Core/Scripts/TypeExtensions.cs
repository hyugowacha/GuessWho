using System;

using UnityEngine;

public static class TypeExtensions
{
    public static bool IsInheritGeneric(this Type instance, out Type result)
    {
        Type componentType = typeof(Component);

        while (instance != componentType)
        {
            if (instance.IsGenericType == true)
            {
                result = instance;

                return true;
            }

            instance = instance.BaseType;
        }

        result = null;

        return false;
    }
}