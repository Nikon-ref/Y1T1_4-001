using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float rateOfFire = 1f;
    [SerializeField] Transform gunPoint;
    [SerializeField] float bulletDamage = 10f; // Damage dealt by the bullet

    private void Start()
    {
        if (gunPoint == null)
            gunPoint = GetComponentInChildren<GunPoint>().transform;
    }

    public float GetRateOfFire()
    {
        return rateOfFire;
    }

    public void Fire()
    {
        GameObject bullet = Instantiate(projectilePrefab, gunPoint.position, transform.rotation);
        ProjectileWithoutRigidbody bulletScript = bullet.GetComponent<ProjectileWithoutRigidbody>();
        if (bulletScript != null)
        {
            bulletScript.damage = bulletDamage; // Assign the damage
        }
    }
}
