using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public float Time = 1f;
    void Update()
    {

        if (Time > 0)
        {
            Time -= UnityEngine.Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
