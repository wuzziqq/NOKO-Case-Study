using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private Vector3 offset,_lookAtOffset;
    [SerializeField] private bool _lookAt;


    private void Awake()
    {
        target = PlayerMovement.Instance.gameObject;
    }


    private void LateUpdate()
    {
        CameraFollow();
    }

    public void CameraFollow()
    {
        if (target == null) return;
    
        if (target != null)
        {
            Vector3 desiredPosition = new Vector3(target.transform.position.x,target.transform.position.y,target.transform.position.z) + offset;
            Vector3 smoothed = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothed;
            if (_lookAt)
            {
                Vector3 lookAtTarget = new Vector3(target.transform.position.x,target.transform.position.y,target.transform.position.z) + _lookAtOffset;
                transform.LookAt(new Vector3(lookAtTarget.x,lookAtTarget.y,lookAtTarget.z));    
            }
        }
    
    }
}
