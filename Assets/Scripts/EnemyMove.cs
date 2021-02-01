using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

public class EnemyMove : MonoBehaviour
{
  [SerializeField] Transform player;
  private NavMeshAgent navMeshAgent;
  private Animator animator;
  private EnemyHealth enemyHealth;

  private void Awake()
  {
    Assert.IsNotNull(player);
  }

  void Start()
  {
    animator = GetComponent<Animator>();
    navMeshAgent = GetComponent<NavMeshAgent>();
    enemyHealth = GetComponent<EnemyHealth>();
  }

  void Update()
  {
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
