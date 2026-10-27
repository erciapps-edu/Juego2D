using UnityEditor;
using UnityEngine;

namespace ErciDemo.scripts
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform player;
        void LateUpdate()
        {
            transform.position = new Vector3(
                player.position.x,
                player.position.y,
                transform.position.z
                );
        }
    }
}