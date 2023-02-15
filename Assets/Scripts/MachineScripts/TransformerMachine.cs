using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CaseSDK;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class TransformerMachine : MonoBehaviour
{
    public Input input;
    public Output output;

    private AnimationHandler _animationHandler;
    public bool canCreateOutputItem = false;
    [SerializeField]private float spawnTime = 0.4f;

    private void Awake()
    {
        _animationHandler = GetComponent<AnimationHandler>();
    }

    private void Start()
    {
        InvokeRepeating("GetItemFromInput",3,spawnTime);
    }

    private void CreateItem()
    {
        GameObject item = PoolManager.Instance.Dequeue<TransformedAsset>();
        item.transform.position = transform.position;
        item.transform.DOJump(output.itemPlaceVector[output.items.Count], 3, 1, 0.6f);
        output.GetItem(item);
    }

    public void SendItemToOutput()
    {
        if (input.workAgain || output.workAgain)
        {
            CreateItem();
        }
    }

    public void GetItemFromInput()
    {
        if (input.items.Count > 0 && output.items.Count < output.capacity && input.workAgain && output.workAgain)
        {
            _animationHandler.SetBoolAnimation("isWork", true);
            input.items[input.items.Count - 1].transform.DOJump(transform.position, 3, 1, 0.6f);
            input.RemoveItem(input.items[input.items.Count - 1]);
            SendItemToOutput();
        }
        else
        {
            _animationHandler.SetBoolAnimation("isWork", false);   
        }
    }
    
}
