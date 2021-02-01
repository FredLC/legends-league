using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class PlayerHealth : MonoBehaviour
{
  [SerializeField] int startingHealth = 100;
  [SerializeField] float timeSinceLastHit = 2f;
  [SerializeField] Slider healthSlider;

  private float timer = 0f;
  private CharacterController characterController;
  private Animator animator;
  private int currentHealth;
  private AudioSource audio;
  private ParticleSystem blood;

  private void Awake()
  {
    Assert.IsNotNull(healthSlider);
  }

  void Start()
  {
    animator = GetComponent<Animator>();
    characterController = GetComponent<CharacterController>();
    currentHealth = startingHealth;
    audio = GetComponent<AudioSource>();
    blood = GetComponentInChildren<ParticleSystem>();
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
      healthSlider.value = currentHealth;
      audio.PlayOneShot(audio.clip);
      blood.Play();
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
    blood.Play();
  }
}
