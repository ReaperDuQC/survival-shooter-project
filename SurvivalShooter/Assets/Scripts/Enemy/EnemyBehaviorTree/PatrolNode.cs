using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PatrolNode : Node
{
    Transform[] patrolPoints;
    NavMeshAgent agent;
    Animator animator;
    public PatrolNode(NavMeshAgent agent, Animator animator, Transform[] patrolPoints) 
    {
        this.patrolPoints = patrolPoints;
        this.agent = agent;
        this.animator = animator;
    }
    public override NodeStates Evaluate()
    {
        agent.isStopped = false;
        animator.SetBool("IsNight", false);
        if (!agent.pathPending && agent.remainingDistance < 1f)
        {
            agent.SetDestination(patrolPoints[Random.Range(0, patrolPoints.Length)].position);
        }
        return NodeStates.SUCCESS;
    }
}
