using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    #region Properties
    [SerializeField]
    private Transform m_Player;
    [SerializeField]
    private float m_DistanceToChase = 6f;
    [SerializeField]
    private NavMeshAgent m_Agent;
    public EnemySO enemyType;
    #endregion

    #region States
    public EnemyIdle IdleState;
    public EnemyChase ChaseState{private set; get;}

    private EnemyState m_CurrentState; // General
    #endregion

    private void Awake() 
    {
        m_Agent = GetComponent<NavMeshAgent>();

        IdleState = new EnemyIdle(this);
        ChaseState = new EnemyChase(this);
        //...
        StartStateMachine();
    }

    private void Update() 
    {
        foreach (var transition in m_CurrentState.Transitions)
        {
            if (transition.IsValid())
            {
                m_CurrentState.OnFinish();
                m_CurrentState = transition.GetNextState();
                m_CurrentState.OnStart();
                break;
            }
        }
        m_CurrentState.OnUpdate();    
    }

    private void StartStateMachine()
    {
        IdleState.OnStart();
        m_CurrentState = IdleState;
    }

    public Transform GetPlayer()
    {
        return m_Player;
    }

    public float GetDistanceToChase()
    {
        return m_DistanceToChase;
    }

    public NavMeshAgent GetAgent()
    {
        return m_Agent;
    }
    
    public int GetDamage()
    {
        return enemyType.Damage;
    }

   
}
