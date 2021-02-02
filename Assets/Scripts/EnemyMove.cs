using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
  private Transform player;
  [SerializeField] private float minRange;

  private NavMeshAgent navMeshAgent;
  private Animator animator;
  private EnemyHealth enemyHealth;

  void Start()
  {
    player = GameManager.instance.Player.transform;
    animator = GetComponent<Animator>();
    navMeshAgent = GetComponent<NavMeshAgent>();
    enemyHealth = GetComponent<EnemyHealth>();
  }

  void Update()
  {
    // if (Vector3.Distance(transform.position, player.transform.position) > minRange && enemyHealth.IsAlive)
    // {
    //   animator.SetBool("isWalking", true);
    //   navMeshAgent.SetDestination(player.position);
    // }
    // else
    // {
    //   animator.SetBool("isWalking", false);
    //   if (enemyHealth.IsAlive)
    //   {
    //     navMeshAgent.ResetPath();
    //   }
    // }

    if (!GameManager.instance.GameOver && enemyHealth.IsAlive)
    {
      navMeshAgent.SetDestination(player.position);
    }
    else if ((!GameManager.instance.GameOver || GameManager.instance.GameOver) && !enemyHealth.IsAlive)
    {
      navMeshAgent.enabled = false;
    }
    else
    {
      navMeshAgent.enabled = false;
      animator.Play("Idle");
    }
  }
}
