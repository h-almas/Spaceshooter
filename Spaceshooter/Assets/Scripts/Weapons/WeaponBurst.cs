using System.Collections;
using UnityEngine;

public class WeaponBurst : Weapon
{
    [SerializeField] private float timeBetweenBursts;
    public override void Shoot()
    {
        if (timeSinceLastShot >= timeBetweenShots)
        {
            if (Input.GetKey(KeyCode.Space)){
                StartCoroutine(ShootBurst());
                timeSinceLastShot = 0;
            }
        }
        else
        {
            timeSinceLastShot += Time.deltaTime;
        }
    }

    private IEnumerator ShootBurst()
    {
        Instantiate(projectilePrefab, weaponTransform.position, Quaternion.identity);
        yield return new WaitForSeconds(timeBetweenBursts);
        Instantiate(projectilePrefab, weaponTransform.position, Quaternion.identity);
        yield return new WaitForSeconds(timeBetweenBursts);
        Instantiate(projectilePrefab, weaponTransform.position, Quaternion.identity);
    }

}
