using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shootRate = 1.0f;
    
    public void Shoot()
    {
        var bulletGo = Instantiate(bulletPrefab);
        bulletGo.transform.position = transform.position + transform.right * .25f;
        bulletGo.transform.rotation = transform.rotation;
    }

    internal void StartShooting()
    {
        InvokeRepeating("Shoot", shootRate, shootRate);
    }

    internal void StopShooting()
    {
        CancelInvoke("Shoot");
    }
}
