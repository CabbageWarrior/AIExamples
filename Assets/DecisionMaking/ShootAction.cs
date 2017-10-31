﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : MonoBehaviour
{
    public GameObject bulletPrefab;
    
    public void Shoot()
    {
        var bulletGo = Instantiate(bulletPrefab);
        bulletGo.transform.position = transform.position + transform.right * .25f;
        bulletGo.transform.rotation = transform.rotation;
    }
}
