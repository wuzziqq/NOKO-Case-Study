using System;
using System.Collections;
using System.Collections.Generic;
using CaseSDK;
using UnityEngine;
using Random = UnityEngine.Random;

public class PatrollingArea : Singleton<PatrollingArea>
{
    [SerializeField]private float zMax;
    [SerializeField]private float xMax;

    public Vector3[] patrollingPosition = new Vector3[5];

    private void Awake()
    {
        for (int i = 0; i < patrollingPosition.Length; i++)
        {
            Vector3 position = CreatePatrollingPosition();
            patrollingPosition[i] = position;
        }
    }

    private Vector3 CreatePatrollingPosition()
    {
        float x = Random.Range(-xMax, xMax);
        float z = Random.Range(-zMax, zMax);
        return new Vector3(x, 0, z);
    }
}
