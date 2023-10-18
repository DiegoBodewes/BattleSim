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

    [Header("Options")]
    public float health = 50;
    public float timeBetweenAttack;
    public float sightRange;
    public float attackRange;

    //Attacking
    [Header("Attacking")]
    bool alreadyAttacked;
    public GameObject Abbility1;
    public Transform firePoint;

    //States
    [Header("States")]
    public bool EnemyInSightRange;
    public bool EnemyInAttackRange;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        enemy = GameObject.FindWithTag("Enemy").transform;

        //Check for sight and attack range
        EnemyInSightRange = Physics.CheckSphere(transform.position, sightRange, WhatIsEnemy);
        EnemyInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatIsEnemy);

        if (EnemyInSightRange && !EnemyInAttackRange) Chase();
        if (EnemyInSightRange && EnemyInAttackRange) Attack();
    }

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
            Rigidbody rb = Instantiate(Abbility1, firePoint.transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            Vector3 dir = enemy.transform.position - this.transform.position;
            rb.AddForce(dir.normalized * 32f, ForceMode.Impulse);

            //

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttack);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
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
