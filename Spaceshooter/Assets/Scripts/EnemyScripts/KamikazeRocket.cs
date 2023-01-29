using UnityEngine;
using Random = UnityEngine.Random;

public class KamikazeRocket : Enemy
{
    [SerializeField] private float speed = 10;
    
    void Start()
    {
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(.05f, .95f), 1.25f, 10) );
        transform.position = pos;
    }

    void Update()
    {
        transform.Translate(Vector3.down * (speed * Time.deltaTime), Space.World);
    }
}
