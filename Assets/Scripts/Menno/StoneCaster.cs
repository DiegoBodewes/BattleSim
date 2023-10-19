using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StoneCaster : MonoBehaviour
{
    public bool isTeam1 = true;

    public NavMeshAgent agent;

    public Transform enemy;
    public Transform ally;

    public LayerMask whatIsGround;
    public LayerMask WhatIsEnemy;
    public LayerMask WhatIsAlly;

    [Header("Options")]
    public float health = 50;

    public float enemySightRange;
    public float timeBetweenAttack;
    public float attackRange;

    public float shieldRange;
    public float timeBetweenShield;

    //Abilities
    [Header("Abilities")]
    bool alreadyAttacked;
    bool alreadyShield;

    public GameObject ProjectilePrefab;
    public GameObject ShieldPrefab;

    public Transform firePoint;

    //States
    [Header("States")]
    public bool EnemyInSightRange;
    public bool EnemyInAttackRange;
    public bool AllyInShieldRange;

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
            ally = GameObject.FindWithTag("Team1").transform;
        }
        else if (isTeam1 == false)
        {
            enemy = GameObject.FindWithTag("Team1").transform;
            ally = GameObject.FindWithTag("Team2").transform;
        }

        //Check for sight and attack range
        EnemyInSightRange = Physics.CheckSphere(transform.position, enemySightRange, WhatIsEnemy);
        EnemyInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatIsEnemy);

        AllyInShieldRange = Physics.CheckSphere(transform.position, shieldRange, WhatIsAlly);

        if (EnemyInSightRange && !EnemyInAttackRange) Chase();
        if (EnemyInSightRange && EnemyInAttackRange) Attack();
        if (AllyInShieldRange) Attack();
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

            //instantiate an projectile
            GameObject projectile = (GameObject)Instantiate(ProjectilePrefab, firePoint.transform.position, Quaternion.identity);

            //compute the projectile direction towards the enemy
            Vector3 direction = enemy.transform.position - projectile.transform.position;

            //Set the projectile direction
            projectile.GetComponent<Projectile>().SetDirection(direction);

            //

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttack);
        }
    }

    private void Shield()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(ally);

        if (!alreadyShield)
        {
            //Ability
            

            //

            alreadyAttacked = true;
            Invoke(nameof(ResetShield), timeBetweenShield);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void ResetShield()
    {
        alreadyShield = false;
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
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, shieldRange);
    }
}
