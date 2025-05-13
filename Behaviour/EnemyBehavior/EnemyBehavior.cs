using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavior: EnemyEntity
{
    private Animator EnemyAnimator;

    public override void Update()
    {
        base.Update();
    }
    //public enum States
    //{
    //    Defolt,
    //    Dead,
    //    Move
    //}
    //private States State
    //{
    //    get { return (States)EnemyAnimator.GetInteger("vibr"); }
    //    set { EnemyAnimator.SetInteger("vibr", (int)value); }
    //}
}
