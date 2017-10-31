using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float bonusHealth = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<HealthState>())
        {
            collision.GetComponent<HealthState>().health += bonusHealth;
            Destroy(gameObject);
        }
    }
}
