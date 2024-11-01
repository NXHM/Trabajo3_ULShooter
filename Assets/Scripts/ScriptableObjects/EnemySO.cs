using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy Data")]
public class EnemySO : ScriptableObject
{
    public string EnemyName;
    public float Speed;
    public float InitialHealth;
    public int Damage;
}
