using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Follow : MonoBehaviour
{
    public float speed;
    public Transform target;
    public float minimumDistance;

    public GameObject projectile;
    public float timeBetweenShots;
    private float nextShotTime;
    
    void Update()
    {   
        if(Time.time > nextShotTime)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            nextShotTime = Time.time + timeBetweenShots;
        }

        if (Vector3.Distance(transform.position, target.position) > minimumDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
