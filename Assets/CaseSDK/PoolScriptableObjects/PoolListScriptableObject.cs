using System.Collections.Generic;
using UnityEngine;


namespace CaseSDK.PoolScriptableObjects
{
    [CreateAssetMenu(fileName = "PoolList")]
    public class PoolListScriptableObject : ScriptableObject
    {
        public List<PoolScriptableObject> list;
    }
}
