using UnityEngine;

namespace Star_Movement
{
    public class StarMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.left * (movementSpeed * Time.deltaTime));
        }
    }
}
