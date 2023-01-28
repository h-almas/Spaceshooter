using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected Transform weaponTransform;
    protected GameObject projectilePrefab;
    [SerializeField] protected float timeBetweenShots;
    protected float timeSinceLastShot;
    
    public virtual void SetTransform(Transform transform)
    {
        weaponTransform = transform;
    }

    public virtual void SetProjectile(GameObject projectilePrefab)
    {
        this.projectilePrefab = projectilePrefab;
    }

    public virtual void DoCleanUp(){}
    
    public abstract void Shoot();
}
