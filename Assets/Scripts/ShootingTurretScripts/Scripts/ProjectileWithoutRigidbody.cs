using UnityEngine;

public class ProjectileWithoutRigidbody : MonoBehaviour
{
    [SerializeField] private float projectileSpeed = 15f;
    public float damage; // Damage value to be set by the turret

    private void Update()
    {
        transform.Translate(new Vector3(0f, 0f, projectileSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision detected with: {collision.collider.name}");

        Enemy enemy = collision.collider.GetComponent<Enemy>();
        if (enemy != null)
        {
            Debug.Log($"Bullet collided with enemy: {collision.collider.name}, dealing {damage} damage");
            enemy.health -= damage; // Apply damage
            Destroy(gameObject);    // Destroy the bullet
        }
    }

}

