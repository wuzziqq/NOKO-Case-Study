using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Output : MonoBehaviour
{
    [SerializeField] private Transform shelf;
    [SerializeField] private Transform container;
    
    public List<GameObject> items;
    public int capacity;
    public float yOffset;
    [HideInInspector] public bool workAgain = true;
    [HideInInspector] public Vector3[] itemPlaceVector;
    private Vector3[] firstLayerItems = new Vector3[15];
    private int _placeIndex = 0;
    
    private void Awake()
    {
        SetItemPlaceGroupPosition();
    }

    private void SetItemPlaceGroupPosition()
    {
        itemPlaceVector = new Vector3[capacity];
        GetFirstLayerPosition();
        SetAllPlacerPosition();   
    }

    private void SetAllPlacerPosition()
    {
        for (int i = 0; i < capacity; i++)
        {
            Vector3 targetPos = new Vector3(firstLayerItems[_placeIndex].x, firstLayerItems[_placeIndex].y + yOffset,
                firstLayerItems[_placeIndex].z);
            itemPlaceVector[i] = targetPos;
            if (_placeIndex < 14)
            {
                _placeIndex++;
            }
            else
            {
                _placeIndex = 0;
                yOffset += 0.25f;
            }
        }
    }

    private void GetFirstLayerPosition()
    {
        for (int i = 0; i < firstLayerItems.Length; i++)
        {
            firstLayerItems[i] = shelf.GetChild(i).position;
        }
    }

    public void GetItem(GameObject gameObject)
    {
        items.Add(gameObject);
        gameObject.transform.SetParent(container);
    }

    private void RemoveItem(GameObject gameObject)
    {
        items.Remove(gameObject);
        gameObject.transform.SetParent(container);
    }

    private void MoveItemToPlayer(Collider other)
    {
        if (items.Count == 0) return;
        GameObject item = items[items.Count-1];
        RemoveItem(item);
        other.GetComponent<StackHandler>().AddList(item);
    }

    #region CollisionDetection

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            workAgain = false;
        }
        if (other.gameObject.CompareTag("AI"))
        {
            workAgain = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<StackHandler>().items.Count <= other.GetComponent<StackHandler>().capacity)
            {
                MoveItemToPlayer(other);
                workAgain = false;
            }
            else
            {
                workAgain = true;
            }
        }
        if (other.gameObject.CompareTag("AI"))
        {
            if (other.GetComponent<StackHandler>().items.Count <= other.GetComponent<StackHandler>().capacity)
            {
                MoveItemToPlayer(other);
                workAgain = false;
            }
            else
            {
                workAgain = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            workAgain = true;
        }
        if (other.gameObject.CompareTag("AI"))
        {
            workAgain = true;
        }
    }

    #endregion
    
    
}
