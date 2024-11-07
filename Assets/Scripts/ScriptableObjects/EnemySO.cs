using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy Data")]
public class EnemySO : ScriptableObject
{
    public string EnemyName;
    public float DistanceToChase = 6f;
    public float Speed;
    public int InitialHealth = 1;
    public int Damage = 1;
}
