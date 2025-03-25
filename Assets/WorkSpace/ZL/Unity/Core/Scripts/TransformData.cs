using UnityEngine;

namespace ZL.Unity
{
    public sealed class TransformData
    {
        public Vector3 position;

        public Quaternion rotation;

        public Vector3 localScale;

        public TransformData()
        {
            Set(Vector3.zero, Quaternion.identity, Vector3.one);
        }

        public TransformData(in Vector3 position)
        {
            Set(position, Quaternion.identity, Vector3.one);
        }

        public TransformData(in Pose pose)
        {
            Set(pose.position, pose.rotation, Vector3.one);
        }

        public void Set(Transform transform)
        {
            Set(transform.position, transform.rotation, transform.localScale);
        }

        public void Set(in Vector3 position, in Quaternion rotation, in Vector3 localScale)
        {
            this.position = position;

            this.rotation = rotation;

            this.localScale = localScale;
        }
    }
}