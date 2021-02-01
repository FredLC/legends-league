using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
  [SerializeField] private int startingHealth = 20;
  [SerializeField] private float timeSinceLastHit = 0.5f;
  [SerializeField] private float disappearSpeed = 2f;

  private AudioSource audio;
  private float timer = 0f;
  private Animator animator;
  private NavMeshAgent navMeshAgent;
  private bool isAlive;
  private Rigidbody rigidbody;
  private CapsuleCollider capsuleCollider;
  private bool disappearEnemy = false;
  private int currentHealth;
  private ParticleSystem blood;

  public bool IsAlive { get { return isAlive; } }

  void Start()
  {
    rigidbody = GetComponent<Rigidbody>();
    capsuleCollider = GetComponent<CapsuleCollider>();
    navMeshAgent = GetComponent<NavMeshAgent>();
    animator = GetComponent<Animator>();
    audio = GetComponent<AudioSource>();
    isAlive = true;
    currentHealth = startingHealth;
    blood = GetComponentInChildren<ParticleSystem>();
  }


  void Update()
  {
    timer += Time.deltaTime;

    if (disappearEnemy)
    {
      transform.Translate(-Vector3.up * disappearSpeed * Time.deltaTime);
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if (timer >= timeSinceLastHit && !GameManager.instance.GameOver)
    {
      if (other.tag == "PlayerWeapon")
      {
        takeHit();
        timeSinceLastHit = 0f;
        blood.Play();
      }
    }
  }

  private void takeHit()
  {
    if (currentHealth > 0)
    {
      audio.PlayOneShot(audio.clip);
      animator.Play("Hurt");
      currentHealth -= 10;
    }

    if (currentHealth <= 0)
    {
      isAlive = false;
      killEnemy();
    }
  }

  private void killEnemy()
  {
    capsuleCollider.enabled = false;
    navMeshAgent.enabled = false;
    animator.SetTrigger("EnemyDie");
    rigidbody.isKinematic = true;
    StartCoroutine(removeEnemy());
  }

  IEnumerator removeEnemy()
  {
    yield return new WaitForSeconds(4f);
    disappearEnemy = true;
    yield return new WaitForSeconds(2f);
    Destroy(gameObject);
  }
}
