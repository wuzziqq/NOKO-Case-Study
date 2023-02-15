using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private Animator _animator;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetBoolAnimation(string boolName,bool status)
    {
        _animator.SetBool(boolName,status);
    }
}
