using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] protected float damage;

    void Update()
    {
        float amtToMove = projectileSpeed * Time.deltaTime;
        transform.Translate(Vector3.up * amtToMove);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().DealDamage(damage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("EnemyProjectile"))
        {
            Destroy(gameObject);
        }
    }
}
