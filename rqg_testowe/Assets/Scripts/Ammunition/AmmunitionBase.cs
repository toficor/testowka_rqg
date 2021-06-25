using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionBase : MonoBehaviour, IDestructable, IPooledObject
{
    [SerializeField] private GameManagerData gameManagerData;
    [SerializeField] private AmmunitionData ammunitionData;

    protected AmmunitionMovement ammunitionMovement;
    private float selfDestructTimer;
    public Queue<GameObject> MyPool { get; set; }

    protected virtual void Start()
    {
        ammunitionMovement = new AmmunitionMovement(ammunitionData, gameObject.transform);        
    }

    private void OnEnable()
    {
        gameManagerData.OnSpawningWave += HandleDestroy;
    }

    private void OnDisable()
    {
        gameManagerData.OnSpawningWave -= HandleDestroy;
    }

    protected virtual void Update()
    {
        HandleSelfDestruct();
        ammunitionMovement.HandleMovement();
    }

    protected virtual void HandleSelfDestruct()
    {
        selfDestructTimer += Time.deltaTime;
        if (selfDestructTimer >= ammunitionData.timeToSelfDestruct)
        {
            HandleDestroy();
            selfDestructTimer = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ammunitionData.tagToAffect))
        {
            HandleDestroy();
        }
    }

    public string GetAffectedTag()
    {
        return ammunitionData.tagToAffect;
    }

    public virtual void HandleDestroy()
    {
        gameObject.SetActive(false);
        ReturnToPool();
    }

    public void ReturnToPool()
    {
        MyPool.Enqueue(this.gameObject);
    }
}
