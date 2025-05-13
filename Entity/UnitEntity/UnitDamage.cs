using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType { Melee, Range }

[System.Serializable]
public class UnitDamage 
{
    public float Damage = 5;
    public float AttackCooldown = 1f;
    public AttackType Type = AttackType.Melee;
    public bool TakeDamageBool = false;
    public int TakeDamageInt = 0;
}
