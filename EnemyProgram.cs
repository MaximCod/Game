using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyProgram: MonoBehaviour
{
    private Animator animes;
    private float speed= 10f;
    public float heal;
    public float maxheal = 10;
    public float damag = 2;
    public float sleep = 1f;
    private GameObject child,chil;
    private bool dvig = true;
    private bool did = true;
    private Slider ch;
   
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Vector3 mousePosition = collision.transform.position;
            Vector2 direction = mousePosition - transform.position;
            float angle = Vector2.SignedAngle(Vector2.down, direction);
            Vector3 targetRotation = new Vector3(0, 0, angle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), 250 * Time.deltaTime);
        }
        else if (collision.tag=="Respawn")
        {
            Vector2 direction = -transform.position;
            float angle = Vector2.SignedAngle(Vector2.down, direction);
            Vector3 targetRotation = new Vector3(0, 0, angle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), 250 * Time.deltaTime);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("un") && sleep<0)
        {
            int dm = (int)damag + (int)Random.Range(0, (int)(damag / 2));
            if (gameObject.name.Contains("boss")) chil.GetComponent<anim>().atti = true;
            collision.gameObject.GetComponent<AllyProgram>().heal -= dm;
            if (gameObject.name.Contains("boss")) sleep = 0.3f; else sleep = 1f;
        }
        else
        {
            sleep -= Time.deltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("un")) speed = 0.2f;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        speed = 10f;
    }

    // Start is called before the first frame update
    void Start()
    {

        animes = GetComponent<Animator>();
        if (gameObject.name.Contains("boss")) sleep = 0.6f;
        heal = maxheal;
        child = transform.GetChild(0).gameObject;
        if (gameObject.name.Contains("boss")) chil = transform.GetChild(1).gameObject;
        ch = child.transform.GetChild(0).GetComponent<Slider>();
        ch.maxValue = maxheal;
        ch.value = maxheal;
        if(!gameObject.name.Contains("boss"))State = States.shag;
    }

    // Update is called once per frame
    void Update()
    {
        child.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -gameObject.transform.rotation.z));
        ch.value = heal;
        if(dvig)transform.Translate(0, speed*-Time.deltaTime, 0);
        if(heal<=0 && did)
        {
            did = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            dvig = false;
            if (!gameObject.name.Contains("boss")) State = States.deed;
            if (gameObject.name.Contains("boss")) gameObject.transform.GetChild(1).GetComponent<anim>().a2 = true;
            if (gameObject.name.Contains("boss")) Destroy(gameObject, 1.1f); else Destroy(gameObject,1.55f);
            if (heal!=50)
            {
                GameObject.Find("Main Camera").GetComponent<Sword>().mani += 20;
            }
            else
            {
                GameObject.Find("Main Camera").GetComponent<Sword>().mani += 50;
            }
            
        }
        
    }
    public enum States
    {
        idel,
        deed,
        shag
    }
    private States State
    {
        get { return (States)animes.GetInteger("vibr"); }
        set { animes.SetInteger("vibr", (int)value); }
    }
}
