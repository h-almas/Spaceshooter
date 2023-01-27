using UnityEngine;

public class WeaponCannon : Weapon
{
    public override void Shoot()
    {
        if (timeSinceLastShot >= timeBetweenShots)
        {
            if (Input.GetKey(KeyCode.Space)){
                // TODO: Should be more ball like but this will do for now
                GameObject projectile = Instantiate(projectilePrefab, weaponTransform.position, Quaternion.identity);
                projectile.transform.localScale *= 8; 
                timeSinceLastShot = 0;
            }
        }
        else
        {
            timeSinceLastShot += Time.deltaTime;
        }
    }
}
