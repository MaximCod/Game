using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AnimationBoss : MonoBehaviour
{ 
    private Animator anime;
    public bool Attack = false;
    public bool pr = true;
    public bool Dead = false;
    void Start()
    {
        anime = GetComponent<Animator>();
        State = States.shag;
        pr = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Attack && pr)
        {
            State = States.att;
            pr = false;
        }
        if (Dead)
        {
            State = States.deed;
        }
    }
    
    public enum States
    {
        idel,
        shag,
        att,
        deed
    }
    private States State
    {
        get { return (States)anime.GetInteger("attak"); }
        set { anime.SetInteger("attak", (int)value); }
    }
}
