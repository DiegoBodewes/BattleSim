using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Summon : MonoBehaviour
{
    public bool isTeam1 = true;

    public NavMeshAgent agent;

    public Transform enemy;

    public LayerMask WhatIsEnemy;

    [Header("Options")]
    public float health = 50;

    public float enemySightRange;
    public float timeBetweenAttack;
    public float attackRange;


    //Abilities
    [Header("Abilities")]
    bool alreadyAttacked;

    public GameObject SlashPrefab;

    public Transform firePoint;

    //States
    [Header("States")]
    public bool EnemyInSightRange;
    public bool EnemyInAttackRange;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (isTeam1 == true)
        {
            WhatIsEnemy = LayerMask.GetMask("Team2");
        }
        else if (isTeam1 == false)
        {
            WhatIsEnemy = LayerMask.GetMask("Team1");
        }
    }

    private void Update()
    {
        if (isTeam1 == true)
        {
            enemy = GameObject.FindWithTag("Team2").transform;
        }
        else if (isTeam1 == false)
        {
            enemy = GameObject.FindWithTag("Team1").transform;
        }

        //Check for sight and attack range
        EnemyInSightRange = Physics.CheckSphere(transform.position, enemySightRange, WhatIsEnemy);
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
            Slash();

            //

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttack);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    void Slash()
    {
        {
            //instantiate an projectile
            GameObject projectile = (GameObject)Instantiate(SlashPrefab, firePoint.position, firePoint.rotation);
        }
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
        Gizmos.DrawWireSphere(transform.position, enemySightRange);
    }
}
