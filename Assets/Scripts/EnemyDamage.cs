using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;



public class EnemyDamage : MonoBehaviour
{

  float health = 100f;
  float damageAmount = 15f;

  private Animator animator;
  private Transform player;

  public UnityEngine.UI.Image healthBar;


  public bool isDead;
  public bool inFightArea;

  void Start()
  {
    
    animator = this.gameObject.GetComponent<Animator>();
    player = GameObject.FindGameObjectWithTag("Player").transform;
  }

  void Update()
  {
    UpdateBar();
    LifeCheck();

    if (player != null)
    {
      Vector3 direction = player.position - transform.position; // Düşmanın karaktere doğru olan vektörü
      direction.y = 0f; // Y eksenini sıfırlayarak düşmanın y ekseninde dönmesini sağlar

      if (direction != Vector3.zero)
      {
        Quaternion rotation = Quaternion.LookRotation(direction); // Düşmanın karaktere doğru bakmasını sağlayan rotasyonu alır
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f); // Döndürme işlemi
      }
    }
  }




  public void ApplyDamage()
  {
    CharacterHealth.instance.GetDamageFunc(damageAmount);

  }

  public void GetDamage()
  {
    health -= 30f;

  }


  public void LifeCheck()
  {
    isDead = health >= 0 ? false : true;
    if (isDead)
    {
      animator.SetBool("Death", true);
    }
  }

  public void Death()
  {
    EnemySpawner.instance.DeathCounter();
    Destroy(this.gameObject);
  }

  void UpdateBar()
  {
    healthBar.rectTransform.sizeDelta = new Vector2((health * 593) / 100, healthBar.rectTransform.sizeDelta.y);

  }


  void OnTriggerEnter(Collider other)
  {

    if (other.gameObject.tag == "Sword" && CharacterMovement.instance.canDamage == true && !isDead)
    {
      if (animator.GetBool("Cover") != true)
      {
        animator.SetTrigger("Damaged");
      }
      else
      {
        animator.SetTrigger("Block");

      }
      CharacterMovement.instance.canDamage = false;
    }
    if (other.gameObject.tag == "FightZone")
    {
      inFightArea = true;
    }



    if (other.gameObject.tag == "Foot" && CharacterMovement.instance.kick == true)
    {
      animator.SetBool("Kicked", true);
    }


  }



  void OnTriggerExit(Collider other)
  {
    if (other.gameObject.tag == "FightZone")
    {
      inFightArea = false;
    }

  }


}
