using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Projectile : MonoBehaviour
{
    public float destroyTime = 10f; //projectile destroy time
    public int minDamage = 15;
    public int MaxDamage = 18;

    public float speed = 5f; //projectile speed
    Vector3 _direction; //the direction of the projectile
    bool isReady; //to know when the projectile direction is set

    void Awake()
    {
        isReady = false;
    }

    public void SetDirection(Vector3 direction)
    {
        //set the direction normalized, to get an unit vector
        _direction = direction.normalized;

        isReady = true; //set flag to true
    }

    void Update()
    {
        if (isReady)
        {
            //get the bullet's current position
            Vector3 position = this.transform.position;

            //compute the bullet's new position
            position += _direction * speed * Time.deltaTime;

            //update the bullet's position
            this.transform.position = position;
        }

        Destroy(gameObject, destroyTime);
    }

    private void OnCollisionEnter(Collision collision)
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

        //Summon takes damage
        Summon summon = collision.collider.GetComponent<Summon>();
        if (summon != null)
        {
            summon.TakeDamage(damage);
        }

        if (collision.gameObject.tag == "Team1") Destroy(this.gameObject);
        if (collision.gameObject.tag == "Team2") Destroy(this.gameObject);
        
    }

   private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shield") Destroy(this.gameObject);
    }
}

