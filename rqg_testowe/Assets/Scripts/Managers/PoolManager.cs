using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private PoolManagerData poolManagerData;

    public Dictionary<AssetReference, Queue<GameObject>> allPools = new Dictionary<AssetReference, Queue<GameObject>>();

    private void Awake()
    {
        poolManagerData.OnLoadingDone += StartMakingPools;
    }

    private void StartMakingPools()
    {
        StartCoroutine(MakePools());
    }

    private IEnumerator MakePools()
    {
        foreach(var data in poolManagerData.poolData)
        {
            yield return StartCoroutine(MakePool(data.assetReference, Vector3.zero, data.quantity));
        }

        poolManagerData.OnPoolsDone?.Invoke(1);
    }

    private IEnumerator MakePool(AssetReference assetReference, Vector3 vector3, int quantity)
    {
        int numberOfObjects = quantity;

        while(numberOfObjects > 0)
        {
            numberOfObjects--;
            yield return StartCoroutine(GetObject(assetReference, vector3));
        }

    }

    private IEnumerator GetObject(AssetReference assetReference, Vector3 position)
    {
        assetReference.InstantiateAsync(position, Quaternion.identity).Completed += (asyncOperationHandle) =>
        {

            if (allPools.ContainsKey(assetReference) == false)
            {
                allPools[assetReference] = new Queue<GameObject>();
            }

            asyncOperationHandle.Result.SetActive(false);
            allPools[assetReference].Enqueue(asyncOperationHandle.Result);
            Debug.Log(asyncOperationHandle.Result.name + " added to pool");
        };

        while (!assetReference.IsDone)
        {
            yield return null;
        }

    }
}
