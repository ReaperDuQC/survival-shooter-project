using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTargetInRange : Node
{
    Transform origin;
    Transform target;
    float distance;
    public IsTargetInRange(Transform o,Transform t, float d)
    {
        origin = o;
        target = t;
        distance = d;
    }
    public override NodeStates Evaluate()
    {
        float currentDistance = Vector3.Distance(origin.position, target.position);
        m_nodeState = currentDistance < distance ? NodeStates.SUCCESS : NodeStates.FAILURE;
        return m_nodeState;
    }
}
