using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.AI;
using Unity.Mathematics;

public class Enemy : MonoBehaviour
{
    public CharacterMovement controller_cs;
    public EnemySpawner enemySpawner_cs;


    private NavMeshAgent navMeshAgent;
    private GameObject target;
    private Transform shield;
    private Animator animator;

    public float shieldHealth = 3f;
    public float distance;
    public float attackRange = 2f;
    public float coolDown;

    public bool isShieldActive = true;
    public bool attack;
    public bool cover=false;
    public bool blocked=false;
    public bool damaged;

    public float health = 100;

    private bool canDamage;
    public bool dead = false;



    void Start()
    {

        controller_cs = GameObject.FindWithTag("Player").GetComponent<CharacterMovement>();
        enemySpawner_cs = GameObject.Find("Enemy Spawner").GetComponent<EnemySpawner>();
        target = GameObject.FindWithTag("Player");
        //shield = transform.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(2);
        

        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
    }

    
    void Update()
    {

        canDamage = controller_cs.canDamage;
        coolDown = controller_cs.coolDown;

        BehaviourTree();
        Death();

        
    

        animator.SetBool("Attack", attack);
        animator.SetBool("Cover", cover);

        animator.SetFloat("Speed",navMeshAgent.speed);
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword" && controller_cs.attack == true && canDamage )
        {
            if (!cover)
            {
                health -= 35;
                attack = false;
                animator.SetTrigger("Damaged");
            }
            else
            {
                if (shieldHealth > 0)
                {
                    shieldHealth -= 1;
                    animator.SetTrigger("Blocked");
                }
                else
                {
                    isShieldActive = false;
                   // shield.gameObject.SetActive(false);
                }
 
            }
        }
    }


    public void BehaviourTree()
    {

        

        distance = Mathf.Abs((transform.position - target.transform.position).magnitude);
       
      
            
        
        if (distance > 10f)
        {
            navMeshAgent.speed = 3.5f;
        }
        else if (distance > attackRange && distance<10f)
        {
            navMeshAgent.speed = 2f;
        }
        


        if (!attack )
        {
           navMeshAgent.SetDestination(target.transform.position);
            if(distance<=attackRange)
            {
                if (coolDown > 2f)
                {
                    attack = true;
                    cover = false;
                }
                else
                {
                    cover = true;
                }
            }
        }
        
    }

    public void Death() 
    {
        dead = health <= 0 ? true : false;

        if (dead)
        {
            enemySpawner_cs.isDead = true;
            Destroy(this.gameObject);
        }
        if (health <= 40)
        {
            navMeshAgent.speed = 2f;

        }
    }



    public void AttackAnimationControl()
    {
        if (distance > attackRange)
        {
            attack = false;
        }
    }


    
}
