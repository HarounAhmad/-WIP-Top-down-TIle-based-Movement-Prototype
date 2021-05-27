using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;

        // Start is called before the first frame update
        private void Start()
        {
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            transform.position = target.position;
        }
    }
}