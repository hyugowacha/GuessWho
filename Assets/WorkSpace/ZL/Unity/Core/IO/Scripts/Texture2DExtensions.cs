using UnityEngine;

using ZL.Unity.IO;

namespace ZL.Unity
{
    public static partial class Texture2DExtensions
    {
        public static byte[] EncodeTo(this Texture2D instance, ImageFileFormat imageFileFormat)
        {
            return imageFileFormat switch
            {
                ImageFileFormat.EXR => instance.EncodeToEXR(),

                ImageFileFormat.JPG => instance.EncodeToJPG(),

                ImageFileFormat.PNG => instance.EncodeToPNG(),

                ImageFileFormat.TGA => instance.EncodeToTGA(),

                _ => null
            };
        }
    }
}