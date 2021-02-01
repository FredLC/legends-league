using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
  [SerializeField] int startingHealth = 100;
  [SerializeField] float timeSinceLastHit = 2f;

  private float timer = 0f;
  private CharacterController characterController;
  private Animator animator;
  private int currentHealth;
  private AudioSource audio;

  void Start()
  {
    animator = GetComponent<Animator>();
    characterController = GetComponent<CharacterController>();
    currentHealth = startingHealth;
    audio = GetComponent<AudioSource>();
  }


  void Update()
  {
    timer += Time.deltaTime;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (timer >= timeSinceLastHit && !GameManager.instance.GameOver)
    {
      if (other.tag == "Weapon")
      {
        takeHit();
        timer = 0;
      }
    }
  }

  private void takeHit()
  {
    if (currentHealth > 0)
    {
      GameManager.instance.PlayerHit(currentHealth);
      animator.Play("Hurt");
      currentHealth -= 10;
      audio.PlayOneShot(audio.clip);
    }

    if (currentHealth <= 0)
    {
      killPlayer();
    }
  }

  private void killPlayer()
  {
    GameManager.instance.PlayerHit(currentHealth);
    animator.SetTrigger("HeroDie");
    characterController.enabled = false;
    audio.PlayOneShot(audio.clip);
  }
}
