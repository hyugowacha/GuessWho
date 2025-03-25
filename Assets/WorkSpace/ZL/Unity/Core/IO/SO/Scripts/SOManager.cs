#if UNITY_EDITOR

using System.IO;

using UnityEditor;

using UnityEngine;

namespace ZL.Unity.IO
{
    public static partial class SOManager
    {
        public static T Load<T>(string directoryPath, string assetPath)

            where T : ScriptableObject
        {
            var asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);

            if (asset == null)
            {
                asset = ScriptableObject.CreateInstance<T>();

                if (Directory.Exists(directoryPath) == false)
                {
                    Directory.CreateDirectory(directoryPath);
                }

                AssetDatabase.CreateAsset(asset, assetPath);

                AssetDatabase.SaveAssets();

                AssetDatabase.Refresh();
            }

            return asset;
        }
    }
}

#endif