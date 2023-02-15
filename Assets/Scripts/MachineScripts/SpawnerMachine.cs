using System;
using System.Collections;
using System.Collections.Generic;
using CaseSDK;
using DG.Tweening;
using UnityEngine;

public class SpawnerMachine : MonoBehaviour
{
    public Output output;

    private AnimationHandler _animationHandler;
    [SerializeField]private float spawnTime = 0.4f;

    private void Awake()
    {
        _animationHandler = GetComponent<AnimationHandler>();
    }

    private void Start()
    {
        InvokeRepeating("SendItemToOutput",0,spawnTime);
    }

    private void CreateItem()
    {
        GameObject item = PoolManager.Instance.Dequeue<SpawnedAsset>();
        item.transform.position = transform.position;
        item.transform.DOMove(output.itemPlaceVector[output.items.Count], 0.6f);
        output.GetItem(item);
    }
    
    
    public void SendItemToOutput()
    {
        if (output.items.Count < output.capacity && output.workAgain)
        {
            _animationHandler.SetBoolAnimation("isWork", true);
            CreateItem();
        }
        else
        {
            _animationHandler.SetBoolAnimation("isWork", false);   
        }
    }
}
