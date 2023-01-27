using System;
using UnityEngine;

public class WeaponDefault : Weapon
{

    public override void Shoot()
    {
        if (timeSinceLastShot >= timeBetweenShots)
        {
            if (Input.GetKey(KeyCode.Space)){
                Instantiate(projectilePrefab, weaponTransform.position, Quaternion.identity);
                timeSinceLastShot = 0;
            }
        }
        else
        {
            timeSinceLastShot += Time.deltaTime;
        }
    }
}
