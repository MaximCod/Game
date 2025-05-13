using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitCollisionHandler : MonoBehaviour
{
    private readonly UnitEntity Unit;
    private float Sleep;

    public UnitCollisionHandler(UnitEntity unit)
    {
        Unit = unit;
        Sleep = unit.DamageSettings.AttackCooldown;
    }

    public void Attack(Collision2D collision)
    {
        string tag = Unit.GetGoalTag();
        if (collision.gameObject.tag == tag && Sleep < 0)
        {
            int damage = (int)Unit.DamageSettings.Damage + (int)Random.Range(0, (int)(Unit.DamageSettings.Damage / 2));
            collision.gameObject.GetComponent<UnitEntity>().TakeDamage(damage);
            Sleep = Unit.DamageSettings.AttackCooldown;
        }
        else
        {
            Sleep -= Time.deltaTime;
        }
    }

    public void Deceleration(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<UnitEntity>())
        {
            if (collision.gameObject.GetComponent<UnitEntity>().Type != Unit.Type)
            {
                Unit.MovementSettings.Speed = Unit.MovementSettings.RunSpeedAfter—ollision;
            }
        }
    }

    public void SpeedRecovery(Collision2D collision)
    {
        Unit.MovementSettings.Speed = Unit.MovementSettings.MaxSpeed;
    }
}
