using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AllyBehavior : AllyEntity
{
    private Animator AllyAnimator;

    void Start()
    {
        AllyAnimator = GetComponent<Animator>();
        //State = States.move;
    }

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

    //    get { return (States)AllyAnimator.GetInteger("vibr"); }
    //    set { AllyAnimator.SetInteger("vibr", (int)value); }
    //}
}
