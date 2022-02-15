using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SleepNode : Node
{
    NavMeshAgent agent;
    Animator animator;
    public SleepNode(NavMeshAgent agent, Animator animator)
    {
        this.agent = agent;
        this.animator = animator;
    }
    public override NodeStates Evaluate()
    {
        agent.isStopped = true;
        animator.SetBool("IsNight", true);
        return NodeStates.SUCCESS;
    }
}
