using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using CaseSDK;
using UnityEngine;

public class StackHandler : MonoBehaviour
{
    public List<GameObject> items;
    public Transform container;
    [SerializeField] private float sortSpeed = 10;
    public int capacity;
    
    private float offsetY = 0.25f;

    private void Update()
    {
        SortItems();
    }

    private void SortItems()
    {
        if (items.Count > 1)
        {
            for (int i = 1; i < items.Count; i++)
            {
                items[0].transform.position = container.position;
                var firstItem = items.ElementAt(i-1);
                var secondItem = items.ElementAt(i);
                var lerpedPosZ = Mathf.Lerp(secondItem.transform.position.z,firstItem.transform.position.z,sortSpeed * Time.deltaTime);
                var lerpedPosY = Mathf.Lerp(secondItem.transform.position.y, firstItem.transform.position.y + offsetY,sortSpeed * Time.deltaTime);
                var lerpedPosX = Mathf.Lerp(secondItem.transform.position.x,firstItem.transform.position.x,sortSpeed/2 * Time.deltaTime);
                secondItem.transform.position = new Vector3(lerpedPosX,lerpedPosY,lerpedPosZ);
            }
        }
    }
    
    public void RemoveList(GameObject item)
    {
        items.Remove(item);
    }
    public void AddList(GameObject item)
    {
        item.transform.SetParent(null);
        items.Add(item);
    }
}
