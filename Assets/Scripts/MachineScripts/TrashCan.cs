using System.Collections;
using System.Collections.Generic;
using CaseSDK;
using DG.Tweening;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public Input input;
    [SerializeField]private float spawnTime = 0.4f;
    void Start()
    {
        InvokeRepeating("RemoveItemFromInput",0,spawnTime);
    }

    public void RemoveItemFromInput()
    {
        if (input.items.Count > 0  && input.workAgain)
        {
            GameObject removingItem = input.items[input.items.Count - 1]; 
            removingItem.transform.DOScale(0, 0.3f).OnComplete((() => RemoveItem(removingItem)));
        }
    }

    public void RemoveItem(GameObject item)
    {
        input.RemoveItem(item);
        PoolManager.Instance.Enqueue<GameObject>(item);
    }
}
