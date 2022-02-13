// The enemy behavior tree manages the enemy's behavior through nodes. The behaviors to be selected are Dead, Sleep, Attack, Chase and Patrol.
//
// Dead:    Checks if the enemy is dead, then disables its ability to act if true
// Sleep:   Checks if it's night time, if true put the enemy to sleep
// Attack:  Checks if the player is in attack range, then looks if the enemy attack is ready, then attacks the player
// Chase:   Checks if either the player is in chase range or nearby enemies in crowd range are in chase range of the player,
//          then the enemy uses NavMesh features for pathfinding to go to the player's position
// Patrol:  Selects a random position from an array of transforms, then uses NavhMesh feature to go to the location

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.AI;

public class EnemyBehaviorTree : MonoBehaviour
{
    [SerializeField] DayNightSystem daynight;
    [SerializeField] Transform player;
    Transform[] patrolPoints;

    [SerializeField] int damage = 0;
    [SerializeField] float m_distanceToChase;
    [SerializeField] float m_distanceToAttack;
    [SerializeField] float m_timeBetweenAttack;
    [SerializeField] float m_distanceCrowding;
    [SerializeField] LayerMask layerMask;

    public Selector m_rootNode;
    public Sequence m_deadNode;
    public Sequence m_sleepNode;
    public Sequence m_attackNode;
    public Sequence m_chaseNode;
    public Sequence m_patrolNode;

    private void Update()
    {
        m_rootNode.Evaluate();
    }

    public void InitializeEnemy(DayNightSystem daynight, Transform player, Transform[] patrolPoints)
    {
        this.daynight = daynight;
        this.player = player;
        this.patrolPoints = patrolPoints;

        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        CompleteProject.PlayerHealth health = player.GetComponent<CompleteProject.PlayerHealth>();

        // Dead Sequence
        IsDeadNode isDeadNode = new IsDeadNode(GetComponent<CompleteProject.EnemyHealth>());
        DeadNode deadNode = new DeadNode(agent);
        List<Node> deadChildren = new List<Node>();
        deadChildren.Add(isDeadNode);
        deadChildren.Add(deadNode);
        m_deadNode = new Sequence(deadChildren);

        // Sleep Sequence
        IsNightNode isNightNode = new IsNightNode(daynight);
        SleepNode sleepNode = new SleepNode(agent);
        List<Node> sleepChildren = new List<Node>();
        sleepChildren.Add(isNightNode);
        sleepChildren.Add(sleepNode);
        m_sleepNode = new Sequence(sleepChildren);

        // Attack Sequence
        IsTargetInRange isPlayerInAttackRange = new IsTargetInRange(transform, player, m_distanceToAttack);
        IsAttackReadyNode isAttackReadyNode = new IsAttackReadyNode(m_timeBetweenAttack);
        AttackNode attackNode = new AttackNode(health, damage);
        List<Node> attackChildren = new List<Node>();
        attackChildren.Add(isPlayerInAttackRange);
        attackChildren.Add(isAttackReadyNode);
        attackChildren.Add(attackNode);
        m_attackNode = new Sequence(attackChildren);

        // Chase Sequence
        IsTargetInRange isPlayerInChaseRange = new IsTargetInRange(transform, player, m_distanceToChase);
        IsAnyEnemyInRangeChasingNode isCrowding = new IsAnyEnemyInRangeChasingNode(transform, player, m_distanceCrowding, m_distanceCrowding, layerMask.value);
        List<Node> chaseChildren = new List<Node>();
        List<Node> chaseSeletorChildren = new List<Node>();
        chaseSeletorChildren.Add(isPlayerInChaseRange);
        chaseSeletorChildren.Add(isCrowding);
        Selector chaseSelector = new Selector(chaseSeletorChildren);
        ChaseNode chaseNode = new ChaseNode(agent, player);
        chaseChildren.Add(chaseSelector);
        chaseChildren.Add(chaseNode);
        m_chaseNode = new Sequence(chaseChildren);

        // Patrol Sequence
        PatrolNode patrolNode = new PatrolNode(agent, patrolPoints);
        List<Node> patrolChildren = new List<Node>();
        patrolChildren.Add(patrolNode);
        m_patrolNode = new Sequence(patrolChildren);
        
        // Root Selector
        List<Node> rootChildren = new List<Node>();
        rootChildren.Add(m_deadNode);
        rootChildren.Add(m_sleepNode);
        rootChildren.Add(m_attackNode);
        rootChildren.Add(m_chaseNode);
        rootChildren.Add(m_patrolNode);
        m_rootNode = new Selector(rootChildren);
    }
}
