using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement
{
    private Transform enemyTransform;
    private Vector3 moveOffset;

    public EnemyMovement(Transform enemyTransform, Vector3 moveOffset)
    {
        this.enemyTransform = enemyTransform;
        this.moveOffset = moveOffset;
    }

    public void Move()
    {

    }


}
