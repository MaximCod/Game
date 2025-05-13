using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTriggerHandler : MonoBehaviour
{
    private readonly UnitEntity Unit;

    public UnitTriggerHandler(UnitEntity unit)
    {
        Unit = unit;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    public void MovingTowardsGoal(Collider2D collision)
    {
        string tag = Unit.GetGoalTag();
        if (collision.tag == tag)
        {
            Vector3 direction = collision.transform.position - Unit.transform.position;
            Unit.MoveOn(direction);
        }
        else if (collision.tag == "Respawn")
        {
            Vector3 direction = -Unit.transform.position;
            Unit.MoveOn(direction);
        }
    }
}
