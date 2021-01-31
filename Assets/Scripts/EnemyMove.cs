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

  private void Awake()
  {
    Assert.IsNotNull(player);
  }

  void Start()
  {
    animator = GetComponent<Animator>();
    navMeshAgent = GetComponent<NavMeshAgent>();
  }

  void Update()
  {
    navMeshAgent.SetDestination(player.position);
  }
}
