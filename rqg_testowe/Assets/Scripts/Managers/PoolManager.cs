using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private PoolManagerData poolManagerData;

    private Dictionary<string, Queue<GameObject>> allPools = new Dictionary<string, Queue<GameObject>>();

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

        poolManagerData.allPools.Clear();
        poolManagerData.allPools = allPools;
        poolManagerData.OnPoolsDone?.Invoke(1);
    }

    private IEnumerator MakePool(AssetReference assetReference, Vector3 vector3, int quantity)
    {
        int numberOfObjects = quantity;
        GameObject parent = new GameObject("Parent_" + assetReference.SubObjectName);
        parent.transform.SetParent(this.transform);

        while(numberOfObjects > 0)
        {
            numberOfObjects--;
            yield return StartCoroutine(GetObject(assetReference, vector3, parent.transform));
        }

    }

    private IEnumerator GetObject(AssetReference assetReference, Vector3 position, Transform parent)
    {
        assetReference.InstantiateAsync(position, Quaternion.identity, parent).Completed += (asyncOperationHandle) =>
        {
            string groupName = asyncOperationHandle.Result.name.Replace("(Clone)", "");

            if (allPools.ContainsKey(groupName) == false)
            {
                allPools[groupName] = new Queue<GameObject>();
            }

            asyncOperationHandle.Result.SetActive(false);
            allPools[groupName].Enqueue(asyncOperationHandle.Result);
            Debug.Log(asyncOperationHandle.Result.name + " added to pool");
        };

        while (!assetReference.IsDone)
        {
            yield return null;
        }

    }
}
