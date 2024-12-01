using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] float turretRange = 13f;
    [SerializeField] float turretRotationSpeed = 5f;

    [SerializeField] string targetTag = "Player"; // Tag of the object to target
    private Transform targetTransform;  // Generic target
    private Gun currentGun;
    private float fireRate;
    private float fireRateDelta;

    private void Start()
    {
        currentGun = GetComponentInChildren<Gun>();
        fireRate = currentGun.GetRateOfFire();

        FindTarget(); // Attempt to find a target at the start
    }

    private void Update()
    {
        if (targetTransform == null)
        {
            FindTarget(); // Continuously check if a target exists
            return;       // Skip the rest of the Update if no target
        }

        Vector3 targetGroundPos = new Vector3(targetTransform.position.x, transform.position.y, targetTransform.position.z);

        // Check if target is not in range
        if (Vector3.Distance(transform.position, targetGroundPos) > turretRange)
        {
            return; // Do nothing because the target is out of range
        }

        // TARGET IN RANGE

        // Rotate Turret towards the target
        Vector3 targetDirection = targetGroundPos - transform.position;
        float turretRotationStep = turretRotationSpeed * Time.deltaTime;
        Vector3 newLookDirection = Vector3.RotateTowards(transform.forward, targetDirection, turretRotationStep, 0f);
        transform.rotation = Quaternion.LookRotation(newLookDirection);

        // Fire at the target
        fireRateDelta -= Time.deltaTime;
        if (fireRateDelta <= 0)
        {
            currentGun.Fire();
            fireRateDelta = fireRate;
        }
    }

    private void FindTarget()
    {
        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);
        if (targetObject != null)
        {
            targetTransform = targetObject.transform;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, turretRange); // Show the turret's range
    }
}


