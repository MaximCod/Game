using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitHealth
{
    public float MaxHealth = 10f;
    public float CurrentHealth = 10f;

    public void TakeDamage(float damage)
    {
        CurrentHealth = Mathf.Max(0, CurrentHealth - damage);
    }
    public void Reset()
    {
        CurrentHealth = MaxHealth;
    }
}
