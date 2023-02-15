using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;

public class AIHandler : CaseSDK.Singleton<AIHandler>
{
    private AnimationHandler _animationHandler;
    private StackHandler _stackHandler;
    private NavMeshAgent _navMeshAgent;
    private AIStateManager _aiStateManager;
    [SerializeField] private float nextStateWaitTime = 5;
    [SerializeField] private float searchArea;
    [SerializeField] private string[] _machineTag;
    public Transform _target;
    private bool moveTarget;
    public int stringIndex = 0;
    private void Awake()
    {
        _aiStateManager = GetComponent<AIStateManager>();
        _animationHandler = GetComponent<AnimationHandler>();
        _stackHandler = GetComponent<StackHandler>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    

    private void UpdateTarget()
    {
        GameObject[] machines = GameObject.FindGameObjectsWithTag(_machineTag[stringIndex]);
        float shortestMachine = Mathf.Infinity;
        GameObject nearestMachine = null;
        foreach (var machine in machines)
        {
            float distanceToMachine = Vector3.Distance(transform.position, machine.transform.position);
            if (distanceToMachine < shortestMachine)
            {
                shortestMachine = distanceToMachine;
                nearestMachine = machine;
            }
        }

        if (nearestMachine != null && shortestMachine <= searchArea)
        {
            _target = nearestMachine.transform;
        }
    }

    private void StateStatus()
    {
        switch (stringIndex)
        {
            case 0: // IDLE
                SetAITarget(1);
                break;
            case 1: // GoingSpawnerOutput
                SetAITarget(2);
                break;
            case 2: // Going TransformerInput
                SetAITarget(3);
                break;
            case 3: // Going Idle
                SetAITarget(4);
                break;
            case 4: // TransformerOutput
                SetAITarget(5);
                break;
            case 5: // Going TrashCan
                SetAITarget(0);
                break;
        }
    }

    private void SetAITarget(int index)
    {
        nextStateWaitTime -= Time.deltaTime;
        if (nextStateWaitTime <= 0)
        {
            stringIndex = index;
            UpdateTarget();
            nextStateWaitTime = 5;
        }
    }
    
    private void Update()
    {
        StateStatus();
        if (_target == null) return;
        _navMeshAgent.SetDestination(_target.position);
        _animationHandler.SetBoolAnimation("isRun",true);
        if (Vector3.Distance(transform.position,_target.transform.position) < 0.5f)
        {
            _animationHandler.SetBoolAnimation("isRun",false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,searchArea);
    }
}
