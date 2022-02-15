using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ChaseNode : Node
{
    NavMeshAgent agent;
    Animator animator;
    Transform target;
    public ChaseNode(NavMeshAgent agent, Animator animator, Transform target)
    {
        this.agent = agent;
        this.target = target;
        this.animator = animator;
    }
    public override NodeStates Evaluate()
    {
        agent.isStopped = false;
        animator.SetBool("IsNight", false);
        agent.SetDestination(target.position);
        return NodeStates.SUCCESS;
    }
}
