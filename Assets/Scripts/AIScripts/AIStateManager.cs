using System;
using UnityEngine;

public class AIStateManager : MonoBehaviour
{
    public AIStates aiStates;

}

public enum AIStates {
    Idle,StackSpawned,ConsumeTransform,StackTransform,TrashCan
}
