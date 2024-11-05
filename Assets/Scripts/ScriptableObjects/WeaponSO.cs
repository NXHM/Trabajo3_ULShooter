using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Weapon Data")]
public class WeaponSO : ScriptableObject
{
    public string WeaponName;
    public float FireRange = 10f;
    public float FireRate = 1f;
    public GameObject FireSphere;
    public int BulletDamage = 1;
}
