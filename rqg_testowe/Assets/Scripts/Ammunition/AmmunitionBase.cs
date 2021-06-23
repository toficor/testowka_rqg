using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionBase : MonoBehaviour, IDestructable
{
    [SerializeField] private AmmunitionData ammunitionData;

    protected AmmunitionMovement ammunitionMovement;

    protected virtual void Start()
    {
        ammunitionMovement = new AmmunitionMovement(ammunitionData, gameObject.transform);
    }

    protected virtual void Update()
    {
        ammunitionMovement.HandleMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ammunitionData.tagToAffect))
        {
            HandleDestroy();
        }
    }


    public virtual void HandleDestroy()
    {
        //later enquing
        Destroy(gameObject);
    }


}
