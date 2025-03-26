#if UNITY_EDITOR

using UnityEditor;

public static partial class SerializedObjectExtensions
{
    public static bool TryFindProperty(this SerializedObject instance, string propertyPath, out SerializedProperty result)
    {
        result = instance.FindProperty(propertyPath);

        return result != null;
    }
}

#endif