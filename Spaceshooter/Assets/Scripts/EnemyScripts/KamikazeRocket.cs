using UnityEngine;
using Random = UnityEngine.Random;

public class KamikazeRocket : MonoBehaviour, Enemy
{
    [SerializeField] private int power = 20;
    [SerializeField] private float speed = 10;
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
        if (other.CompareTag("Player") || other.CompareTag("PlayerProjectile"))
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score", 0) + power);
            Destroy(gameObject);
        }
    }
}
