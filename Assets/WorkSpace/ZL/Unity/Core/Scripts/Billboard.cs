using UnityEngine;

namespace ZL.Unity
{
    [DisallowMultipleComponent]

    public sealed class Billboard : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private bool freezeX = false;

        [SerializeField]

        private bool freezeY = false;

        [SerializeField]

        private bool freezeZ = false;

        private Transform targetTransform = null;

        private void Awake()
        {
            targetTransform = Camera.main.transform;
        }

        private void FixedUpdate()
        {
            var v = transform.eulerAngles;

            transform.LookAt(transform.position + targetTransform.transform.rotation * Vector3.back, targetTransform.transform.rotation * Vector3.up);

            if (freezeX == false)
            {
                v.x = transform.eulerAngles.x;
            }

            if (freezeY == false)
            {
                v.y = transform.eulerAngles.y;
            }

            if (freezeZ == false)
            {
                v.z = transform.eulerAngles.z;
            }

            transform.rotation = Quaternion.Euler(v);
        }
    }
}