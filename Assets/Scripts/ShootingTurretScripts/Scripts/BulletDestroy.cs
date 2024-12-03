using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float shrinkRate = 0.1f; // How quickly the bullet shrinks
    [SerializeField] private float minSize = 0.1f; // The minimum size the bullet will shrink to
    [SerializeField] private float lifeTime = 5f; // Time after which the bullet disappears
    //private bool isShrinking = false;

    private void Start()
    {
        // Start shrinking the bullet gradually over time
        StartCoroutine(ShrinkBullet());
        // Destroy the bullet after a certain lifetime, even if it hasn't collided
        Destroy(gameObject, lifeTime);
    }
    private void Update()
    {
        // Optionally, can have a shrinking effect here if needed
    }

    private IEnumerator ShrinkBullet()
    {
        while (transform.localScale.x > minSize)
        {
            transform.localScale -= new Vector3(shrinkRate, shrinkRate, shrinkRate) * Time.deltaTime;
            yield return null;
        }
        // Once the bullet reaches its minimum size, destroy it
        Destroy(gameObject);
    }

    // Collision detection to destroy the bullet upon hitting something
    private void OnCollisionEnter(Collision collision)
    {
        // Destroy the bullet immediately upon collision which I assume only works with rigid body bullets which has a script for it but needs to be manually activated
        //currently doesnt work with rigid body + rigid body script for bullets
        Destroy(gameObject);
    }
}

