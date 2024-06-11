using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Player enemy;
    public Animator animator;
    public NavMeshAgent agent;
    public float range = 40f;
    private bool isMoving;

    void Start()
    {
        enemy = GetComponent<Player>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Vector3.Distance(agent.destination, agent.transform.position) <= agent.stoppingDistance)
        {
            animator.SetBool("IsIdle", true);
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsAttack", false);
            Vector3 point;
            if (RandomDestination(transform.position, range, out point))
            {
                isMoving = true;
                agent.destination = point;
            }
        }
        if (enemy.inRange == true)
        {
            agent.isStopped = true;
            isMoving = false;
            animator.SetBool("IsAttack", true);
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsIdle", false);
        }
        else
        {
            isMoving = true;
            agent.isStopped = false;
        }

        if (isMoving == true)
        {
            isMoving = false;
            animator.SetBool("IsRunning", true);
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsAttack", false);
        }
        if (enemy.isDead == true)
        {
            agent.isStopped = true;
            animator.SetBool("IsDead", true);
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsAttack", false);
        }

        if (PlayerAlive.playerList.Count() == 1)
        {
            agent.isStopped = true;
            animator.SetBool("IsWin", true);
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsAttack", false);
        }

    }

    public bool RandomDestination(Vector3 center, float range, out Vector3 result)
    {
        Vector3 newDest = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(newDest, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
