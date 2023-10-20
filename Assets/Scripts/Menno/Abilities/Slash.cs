using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public float destroyTime = 10f; //projectile destroy time
    public int minDamage = 15;
    public int MaxDamage = 18;


    void Update()
    {
        Destroy(gameObject, destroyTime);
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        int damage = Random.Range(minDamage, MaxDamage); //projectile damage

        //ShadowWizard takes damage
        ShadowWizard wizard = collision.collider.GetComponent<ShadowWizard>();
        if (wizard != null)
        {
            wizard.TakeDamage(damage);
        }

        //StoneCaster takes damage
        StoneCaster stone = collision.collider.GetComponent<StoneCaster>();
        if (stone != null)
        {
            stone.TakeDamage(damage);
        }

        //Necromancer taks damage
        Necromancer necro = collision.collider.GetComponent<Necromancer>();
        if (necro != null)
        {
            necro.TakeDamage(damage);
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        int damage = Random.Range(minDamage, MaxDamage); //projectile damage

        //ShadowWizard takes damage
        ShadowWizard wizard = other.GetComponent<ShadowWizard>();
        if (wizard != null)
        {
            wizard.TakeDamage(damage);
        }

        //StoneCaster takes damage
        StoneCaster stone = other.GetComponent<StoneCaster>();
        if (stone != null)
        {
            stone.TakeDamage(damage);
        }

        //Necromancer takes damage
        Necromancer necro = other.GetComponent<Necromancer>();
        if (necro != null)
        {
            necro.TakeDamage(damage);
        }

        //Summon takes damage
        Summon summon = other.GetComponent<Summon>();
        if (summon != null)
        {
            summon.TakeDamage(damage);
        }

        if (other.gameObject.tag == "Shield") Destroy(this.gameObject);
    }
}
