using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
  public bool isDead = false;
  public bool takeDamage;
  public float health = 100f;

  public static CharacterHealth instance;

  public Image healthBar;



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

  void Update(){
    UpdateBar();
  }
  void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "Weapon")
    {
      takeDamage = true;
    }


  }


  public void GetDamageFunc(float damage)
  {
    if (takeDamage)
    {
      if (!CharacterMovement.instance.covering)
      {
        health -= damage;

        CharacterMovement.instance.animator.SetBool("Attack", false);
        CharacterMovement.instance.animator.SetTrigger("Damaged");
        

        if (health <= 0)
        {
          isDead = true;
          CharacterMovement.instance.animator.SetBool("Death", isDead);
          CharacterMovement.instance.enabled = false;
        }
      }
      else
      {
        CharacterMovement.instance.animator.SetTrigger("Blocked");
      }

      takeDamage = false;
    }
  }

  void UpdateBar(){
      healthBar.rectTransform.sizeDelta = new Vector2((health*593)/100,healthBar.rectTransform.sizeDelta.y);
  }
}
