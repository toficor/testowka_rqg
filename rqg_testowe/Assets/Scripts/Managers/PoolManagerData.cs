using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System;

[CreateAssetMenu(fileName = "PoolManagerData", menuName = "Managers/PoolManagerData")]
public class PoolManagerData : ScriptableObject
{
    //for debug
   // [HideInInspector]
    public List<PoolData> poolData = new List<PoolData>();
    public Dictionary<string, Queue<GameObject>> allPools = new Dictionary<string, Queue<GameObject>>();

    [Header("Assets")]
    public AssetReference playerAasset;

    public AssetReference projectile;

    public AssetReference npc1Asset;
    public AssetReference npc2Asset;
    public AssetReference npc3Asset;


    [Header("Quantity of pools")]
    public int projectilePoolSize;
    public int npc1PoolSize;
    public int npc2PoolSize;
    public int npc3PoolSize;


    public Action OnLoadingDone;
    public Action<int> OnPoolsDone;     


    [Serializable]
    public class PoolData
    {
        public AssetReference assetReference;
        public int quantity;
    }

}
