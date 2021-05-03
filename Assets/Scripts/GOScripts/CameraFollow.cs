using UnityEngine;

namespace Assets.Scripts.GOScripts
{
    /// <summary>
    /// Скрипт движения камеры
    /// </summary>
    public class CameraFollow : MonoBehaviour
    {
        public Transform Target;

        public float ZOffset;
        void Update()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, ZOffset + Target.position.z);
        }
    }
}
