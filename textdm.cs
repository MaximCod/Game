using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textdm : MonoBehaviour
{
    private float tim = 1f;
    private bool pp = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameObject.name.Contains("Clone")) pp = true;
        if (pp)
        {
            if (tim > 0)
            {
                tim -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
    }
}
