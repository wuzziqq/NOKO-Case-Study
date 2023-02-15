using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Sheep : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private AnimationHandler _animationHandler;
    private Vector3 targetPos;
    private float waitTime = 3;
    private void Awake()
    {
        _animationHandler = GetComponent<AnimationHandler>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void NextTarget()
    {
        int randomPos = Random.Range(0, 5);
        targetPos = PatrollingArea.Instance.patrollingPosition[randomPos];
    }

    private void Update()
    {
        if (targetPos == null) return;
        _navMeshAgent.SetDestination(targetPos);
        _animationHandler.SetBoolAnimation("isRun",true);
        if (Vector3.Distance(transform.position,targetPos) < 0.5f)
        {
            _animationHandler.SetBoolAnimation("isRun",false);
        }
        waitTime -= Time.deltaTime;
        if (waitTime<= 0)
        {
            waitTime = Random.Range(0, 4);
            NextTarget();
        }
    }
}
