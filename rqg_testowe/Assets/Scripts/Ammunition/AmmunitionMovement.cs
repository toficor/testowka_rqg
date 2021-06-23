using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionMovement
{
    private AmmunitionData ammunitionData;
    private Transform ammoTransform;

    public AmmunitionMovement(AmmunitionData ammunitionData, Transform ammoTransform)
    {
        this.ammunitionData = ammunitionData;
        this.ammoTransform = ammoTransform;
    }

    public virtual void HandleMovement()
    {
        ammoTransform.position += new Vector3(0f, 0f, ammunitionData.speed * Time.deltaTime * ammunitionData.direction.z);
    }
    
}
