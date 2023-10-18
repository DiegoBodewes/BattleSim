using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float destroyTime = 10f;
    public int damage = 12;

    private void OnCollisionEnter(Collision collision)
    {
        Wizard wizard = collision.collider.GetComponent<Wizard>();
        if (wizard != null)
        {
            wizard.TakeDamage(damage);
        }

        Destroy(this.gameObject);
    }

    private void Update()
    {
        Destroy(gameObject, destroyTime);
    }
}
