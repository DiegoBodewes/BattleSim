using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShadowWizard : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject enemyRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeeEnemy;
    public bool inRadius;

    public NavMeshAgent shadowWizard;

    public float distance = 0f;

    //attack
    public GameObject firePoint;
    public GameObject projectile;

    public float fireRate = 5;
    private float nextTimeToFire = 0;

    private void Start()
    {
        enemyRef = GameObject.FindGameObjectWithTag("Enemy");
        StartCoroutine(FOVRoutine());
    }



    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeeEnemy = true;
                else
                    canSeeEnemy = false;

            }
            else
                canSeeEnemy = false;
        }
        else if (canSeeEnemy)
            canSeeEnemy = false;

        if (canSeeEnemy)
        {
            //shadowWizard.SetDestination(enemyRef.transform.position);

            //use abbility
            Projectile();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //ability

        //health = 0 destroy
    }

    void Projectile()
    {
        if (canSeeEnemy && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Instantiate(projectile, firePoint.transform.position, firePoint.transform.rotation, firePoint.transform);
        }

          //projectileRig = projectile.GetComponent<Rigidbody>();
        //projectileRig.AddForce(projectileRig.transform.forward);
    }

}
