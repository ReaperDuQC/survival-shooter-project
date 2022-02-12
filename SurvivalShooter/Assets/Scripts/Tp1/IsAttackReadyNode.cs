using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAttackReadyNode : Node
{
    bool isReady;
    float lastAttackTime;
    float timeBetweenAttack;
    int attackCount;
    public IsAttackReadyNode(float timeBetweenAttack) 
    {
        this.timeBetweenAttack = timeBetweenAttack; 
        isReady = true;
        lastAttackTime = 0f;
    }
    public override NodeStates Evaluate()
    {
        isReady = Time.time - lastAttackTime >= timeBetweenAttack;
        m_nodeState = isReady ? NodeStates.SUCCESS : NodeStates.FAILURE;

        if(m_nodeState == NodeStates.SUCCESS)
        {
            Debug.Log("Number Of Attack " + ++attackCount);
            lastAttackTime = Time.time;
        }
        return m_nodeState;
    }
}
