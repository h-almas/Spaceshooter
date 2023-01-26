using UnityEngine;

namespace Star_Movement
{
    public class StarMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.left * (movementSpeed * Time.deltaTime));
        }
    }
}
