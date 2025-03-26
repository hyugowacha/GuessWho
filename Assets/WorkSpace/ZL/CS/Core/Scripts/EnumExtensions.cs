using System;

namespace ZL.CS
{
    public static partial class EnumExtensions
    {
        private struct EnumUnion<T>

            where T : Enum
        {
            public T enumValue;

            public int intValue;
        }

        public static int ToInt<T>(this T instance)

            where T : Enum
        {
            EnumUnion<T> enumUnion = new()
            {
                enumValue = instance,
            };

            unsafe
            {
                int* pointer = &enumUnion.intValue;

                pointer -= 1;

                return *pointer;
            }
        }

        public static T ToEnum<T>(this int instance)

            where T : Enum
        {
            EnumUnion<T> enumUnion = new();

            unsafe
            {
                int* pointer = &enumUnion.intValue;

                pointer -= 1;

                *pointer = instance;
            }

            return enumUnion.enumValue;
        }

        public static TTo ToEnum<TFrom, TTo>(this TFrom instance)

            where TFrom : Enum

            where TTo : Enum
        {
            return instance.ToInt().ToEnum<TTo>();
        }
    }
}