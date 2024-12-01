using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float rateOfFire = 1f;

    [SerializeField] Transform gunPoint;    //Optional


    private void Start()
    {
        if(gunPoint == null)
            gunPoint = GetComponentInChildren<GunPoint>().transform;
    }
    public float GetRateOfFire()
    {
        return rateOfFire;   
    }

    public void Fire()
    {
        Instantiate(projectile, gunPoint.position, transform.rotation);     
                    //can use transform.position instead of gunPoint.position
                    //if this script is attached directly to a gunpoint
    }
}