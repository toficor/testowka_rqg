using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private PoolManagerData poolManagerData;

    private readonly Dictionary<AssetReference, AsyncOperationHandle<GameObject>> asyncOperationHandles
        = new Dictionary<AssetReference, AsyncOperationHandle<GameObject>>();

    public bool IsPartialLoadingComplete { get; private set; }
    public bool IsFullLoadingComplete { get; private set; }


    private List<PoolManagerData.PoolData> allAssetsToLoad;


    private List<PoolManagerData.PoolData> CreatAllAssetsList()
    {
        var assets = new List<PoolManagerData.PoolData>();
        assets.Add(new PoolManagerData.PoolData
        {
            assetReference = poolManagerData.npc1Asset,
            quantity = poolManagerData.npc1PoolSize
        }); 
        assets.Add(new PoolManagerData.PoolData
        {
            assetReference = poolManagerData.npc2Asset,
            quantity = poolManagerData.npc2PoolSize
        });
        assets.Add(new PoolManagerData.PoolData
        {
            assetReference = poolManagerData.npc3Asset,
            quantity = poolManagerData.npc3PoolSize
        });
        assets.Add(new PoolManagerData.PoolData
        {
            assetReference = poolManagerData.projectile,
            quantity = poolManagerData.projectilePoolSize
        }); 
        assets.Add(new PoolManagerData.PoolData
        {
            assetReference = poolManagerData.enemyProjectile,
            quantity = poolManagerData.enemyProjectilePoolSize
        }); 


        return assets;
    }

    private void Awake()
    {
        StartCoroutine(LoadAll());
    }

    private IEnumerator LoadAll()
    {
        IsFullLoadingComplete = false;
        allAssetsToLoad = CreatAllAssetsList();

        foreach (var asset in allAssetsToLoad)
        {
            yield return StartCoroutine(LoadData(asset.assetReference));
        }

        if(poolManagerData.poolData != null)
        {
            poolManagerData.poolData.Clear();
        }

        poolManagerData.poolData = allAssetsToLoad;
        poolManagerData.OnLoadingDone?.Invoke();        
        IsFullLoadingComplete = true;
    }

    private IEnumerator<AsyncOperationHandle<GameObject>> LoadData(AssetReference assetReference)
    {
        IsPartialLoadingComplete = false;
        AsyncOperationHandle<GameObject> op = Addressables.LoadAssetAsync<GameObject>(assetReference);
        asyncOperationHandles[assetReference] = op;
        op.Completed += (operation) =>
        {
            IsPartialLoadingComplete = true;
            Debug.Log("Loaded" + op.Result.name);
        };


        while (!op.IsDone)
        {
            yield return op;
        }

    }
}
