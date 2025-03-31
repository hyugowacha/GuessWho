using System.Text;

namespace ZL.Unity.Pooling
{
    public static class PooledStringBuilder
    {
        public static string Concat(params char[] values)
        {
            var stringBuilder = ClassPool<StringBuilder>.Generate();

            var @string = stringBuilder.Concat(values);

            ClassPool<StringBuilder>.Collect(stringBuilder.Clear());

            return @string;
        }

        public static string Concat(params string[] values)
        {
            var stringBuilder = ClassPool<StringBuilder>.Generate();

            var @string = stringBuilder.Concat(values);

            ClassPool<StringBuilder>.Collect(stringBuilder.Clear());

            return @string;
        }
    }
}