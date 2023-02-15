using System;
using System.Collections;
using System.Collections.Generic;
using CaseSDK;
using UnityEngine;

public class PlayerMovement : Singleton<PlayerMovement>
{
    private AnimationHandler _animationHandler;
    private Camera _camera;
    [SerializeField] private VariableJoystick _joystick;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed;

    private void Awake()
    {
        _animationHandler = GetComponent<AnimationHandler>();
        _camera = Camera.main;
        
    }

    private void Update()
    {
        UpdateMovement();
    }


    #region Movement
    
        private void UpdateMovement()
        {
            Vector3 targetVector;
            targetVector = new Vector3(_joystick.Horizontal, 0,_joystick.Vertical);
            var movementVector = Movement(targetVector);
            Rotate(movementVector);
            if (targetVector.magnitude > 0)
            {
                _animationHandler.SetBoolAnimation("isRun",true);    
            }
            else _animationHandler.SetBoolAnimation("isRun",false);
            
        }
        
    
        private Vector3 Movement(Vector3 targetVector)
        {
            float moveSpeed = _movementSpeed * Time.deltaTime;
            
            targetVector = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) * targetVector;
            var targetPosition = transform.position + targetVector * moveSpeed;
            transform.position = targetPosition;
            return targetVector;
        }
    
        private void Rotate(Vector3 movementVector)
        {
            if (movementVector.magnitude == 0)
            {
                return;
            }
            var rotation = Quaternion.LookRotation(movementVector);
            transform.rotation = Quaternion.RotateTowards(transform.rotation,rotation,_rotationSpeed);
        }
    
        #endregion
        
}
