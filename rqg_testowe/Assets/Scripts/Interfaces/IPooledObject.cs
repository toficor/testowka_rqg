using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IPooledObject
{
    Queue<GameObject> MyPool { get; set; }

    public void ReturnToPool();

}
