using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "AmmunitionBase", menuName = "Ammuntion/AmmuntionBase")]
public class AmmunitionData : ScriptableObject
{
    public float timeToSelfDestruct;
    public float speed;
    public string tagToAffect;
    public Vector3 direction;
}
