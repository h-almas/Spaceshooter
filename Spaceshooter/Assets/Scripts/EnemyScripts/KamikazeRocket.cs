using UnityEngine;
using Random = UnityEngine.Random;

public class KamikazeRocket : MonoBehaviour, Enemy
{
    [SerializeField] private int power = 20;
    [SerializeField] private float speed = 10;
    [SerializeField] private int hp = 1;
    private bool dead = false;
    [SerializeField] private GameObject explosionPrefab;
    
    void Start()
    {
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(.05f, .95f), 1.25f, 10) );
        transform.position = pos;
    }

    void Update()
    {
        transform.Translate(Vector3.down * (speed * Time.deltaTime), Space.World);
        
    }

    public int GetPower()
    {
        return power;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!dead && other.CompareTag("PlayerProjectile") || other.CompareTag("Player"))
        {
            hp--;
            if (hp == 0)
            {
                dead = true;
                Instantiate(explosionPrefab, transform.position, transform.rotation);
                Player.Score += power;
                Destroy(gameObject);
            }
        }
    }
}
