using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
  [SerializeField] private float range = 3f;
  [SerializeField] private float timeBetweenAttacks = 1f;

  private Animator animator;
  private GameObject player;
  private bool playerInRange;
  private BoxCollider[] weaponColliders;
  private EnemyHealth enemyHealth;

  void Start()
  {
    weaponColliders = GetComponentsInChildren<BoxCollider>();
    player = GameManager.instance.Player;
    animator = GetComponent<Animator>();
    enemyHealth = GetComponent<EnemyHealth>();
    StartCoroutine(Attack());
  }


  void Update()
  {
    transform.LookAt(player.transform);
    if (Vector3.Distance(transform.position, player.transform.position) < range && enemyHealth.IsAlive)
    {
      playerInRange = true;
    }
    else
    {
      playerInRange = false;
    }
  }
  IEnumerator Attack()
  {
    if (playerInRange && !GameManager.instance.GameOver)
    {
      animator.Play("Attack");
      yield return new WaitForSeconds(timeBetweenAttacks);
    }

    yield return null;
    StartCoroutine(Attack());
  }

  public void EnemyBeginAttack()
  {
    foreach (var weapon in weaponColliders)
    {
      weapon.enabled = true;
    }
  }

  public void EnemyEndAttack()
  {
    foreach (var weapon in weaponColliders)
    {
      weapon.enabled = false;
    }
  }
}
