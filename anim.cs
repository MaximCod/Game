using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class anim : MonoBehaviour
{ 
    private Animator anime;
    public bool atti=false;
    public bool a1=true;
    public bool a2 = false;
    void Start()
    {
        anime = GetComponent<Animator>();
        State = States.shag;
        a1 = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(atti && a1)
        {
            State = States.att;
            a1 = false;
        }
        if (a2)
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
