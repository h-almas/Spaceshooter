using UnityEngine;

public class WeaponSpread : Weapon
{

    
    
    public override void Shoot()
    {
        if (timeSinceLastShot >= timeBetweenShots)
        {
            if (Input.GetKey(KeyCode.Space)){
                Instantiate(projectilePrefab, weaponTransform.position, Quaternion.identity);
                Instantiate(projectilePrefab, weaponTransform.position, Quaternion.Euler(0,0,30));
                Instantiate(projectilePrefab, weaponTransform.position, Quaternion.Euler(0,0,-30));
                timeSinceLastShot = 0;
            }
        }
        else
        {
            timeSinceLastShot += Time.deltaTime;
        }
    }
}
