using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Animator animator;

    public bool dash;
    public bool attack;
    public bool canDamage;
    private bool block = false;
    public bool covering = false;
    private bool running = false;
    
    public bool kick = false;

   


    public float kickCoolDown;
    public float coolDown;
    public float speed = 5f;
    public float turnSmoothTime = 0.1f;

    private Vector3 moveDirection;
    private Vector3 direction;

    float turnSmoothVelocity;

    public static CharacterMovement instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    void Update()
    {


        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");


        direction = new Vector3(horizontal, 0, vertical).normalized;

        Cover();
        Movement();
        Combat();
        Dash();
        Kick();


        #region Cool Down
        if (!attack)
        {
            coolDown += 1 * Time.deltaTime;
        }
        else
        {
            coolDown = 0;
        }
        
        if (!kick)
        {
            kickCoolDown += 1 * Time.deltaTime;
        }
        else
        {
            kickCoolDown = 0;
        }

        #endregion

        #region UpdateAnimatorVariables
        animator.SetFloat("Speed", direction.magnitude * speed);
        animator.SetFloat("Horizontal", direction.x * speed);
        animator.SetFloat("Vertical", direction.z * speed);
        animator.SetBool("Covering", covering);
        animator.SetBool("Attack", attack);
        animator.SetBool("Dash", dash);
        #endregion




    }



    public void Cover()
    {
        if (Input.GetMouseButton(1) && !attack)
        {
            speed = Mathf.Lerp(speed, 2, 1f * Time.deltaTime); covering = true;
        }

        else
        {
            covering = false;
        }
    }

    public void Movement()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !covering && !attack) { speed = Mathf.Lerp(speed, 8, 4f * Time.deltaTime); running = true; }
        else { running = false; }

        if (!covering && !running) { speed = Mathf.Lerp(speed, 4, 3f * Time.deltaTime); }




        if (direction.magnitude > 0.1f)
        {


            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float camMode = covering && !dash ? cam.eulerAngles.y : targetAngle;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, camMode, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            if (!attack && !block)
            {
                controller.Move(moveDirection.normalized * speed * Time.deltaTime);
            }



        }
    }


    public void Combat()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            attack = true;

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            attack = false;
        }


    }


    public void Dash()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            dash = true;
            attack = false;
        }
        else
        {
            dash = false;
        }

    }

    public void Kick()
    {
        if (Input.GetKeyDown(KeyCode.Q)&& kickCoolDown>3f)
        {
            kick = true;
            animator.SetTrigger("Kick");
        }
       
        
        
    }

    public void AttackAnimationEvent()
    {
        if (!Input.GetKey(KeyCode.Space))
        {
            attack = false;
        }
    }






    public void AttackStarted()
    {
        canDamage = true;
    }

    public void AttackEnded()
    {
        canDamage = false;
    }





}
