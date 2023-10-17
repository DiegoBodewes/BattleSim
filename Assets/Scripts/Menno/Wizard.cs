using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wizard : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform enemy;

    public LayerMask whatIsGround;
    public LayerMask WhatIsEnemy;

    public float health = 50;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttack;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange;
    public float attackRange;
    public bool EnemyInSightRange;
    public bool EnemyInAttackRange;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("Enemy").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        EnemyInSightRange = Physics.CheckSphere(transform.position, sightRange, WhatIsEnemy);
        EnemyInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatIsEnemy);

        //if (!EnemyInSightRange && !EnemyInAttackRange) Patrolling();
        if (EnemyInSightRange && !EnemyInAttackRange) Chase();
        if (EnemyInSightRange && EnemyInAttackRange) Attack();
    }

    /*private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }*/

    private void Chase()
    {
        agent.SetDestination(enemy.position);
    }

    private void Attack()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(enemy);

        if (!alreadyAttacked)
        {
            //Ability
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);

            //

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttack);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamge(int damage)
    {
        health -= damage;

        if (health < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
