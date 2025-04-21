using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AllyProgram: MonoBehaviour
{
    private Animator animes;
    private float speed = 10f;
    public float heal;
    public float maxheal = 10;
    public float damag = 2;
    public int exp = 0;
    public float sleep = 1f;
    private GameObject child;
    private Slider ch;
    private float tim=0.4f;
    private bool tr = true;
    private bool dvig = true;
    private bool did = true;
    public int rgb = 0;
    private GameObject damtex;
    private GameObject hran;
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag=="Finish")
        {
            Vector3 mousePosition = collision.transform.position;
            Vector2 direction = mousePosition - transform.position;
            float angle = Vector2.SignedAngle(Vector2.up, direction);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle)), 250 * Time.deltaTime);
        }
        else if (collision.tag == "Respawn")
        {
            Vector2 direction = -transform.position;
            float angle = Vector2.SignedAngle(Vector2.up, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle)), 250 * Time.deltaTime);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("vr") && sleep < 0)
        {
            int dm = (int)damag + (int)Random.Range(0, (int)(damag / 2));
            collision.gameObject.GetComponent<EnemyProgram>().heal -= dm;
            Instantiate(damtex, gameObject.transform.position + new Vector3(0, 0,-20), Quaternion.Euler(new Vector3(0, 0, 0)),hran.transform).GetComponent<TextMeshProUGUI>().text=dm.ToString();
            exp += (int)dm;
            sleep = 1f;
        }
        else
        {
            sleep -= Time.deltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("vr")) speed = 0.2f;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        speed = 10f;
    }
    // Start is called before the first frame update
    void Start()
    {
        hran = GameObject.Find("hrantex");
        damtex = GameObject.Find("damtx");
        animes = GetComponent<Animator>();
        gameObject.GetComponent<CircleCollider2D>().radius = 20;
        float heal = maxheal;
        child = transform.GetChild(0).gameObject;
        ch = child.transform.GetChild(0).GetComponent<Slider>();
        ch.maxValue = maxheal;
        ch.value = maxheal;
        State = States.shag;
    }

    // Update is called once per frame
    void Update()
    {
        child.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -gameObject.transform.rotation.z));
        if (tim<0 && tr)
        {
            gameObject.GetComponent<CircleCollider2D>().radius = 0.45f;
            tr = false;
        }
        else
        {
            tim -= Time.deltaTime;
        }
        ch.maxValue = maxheal;
        if (exp >= damag*5)
        {
            maxheal += 10;
            damag += 2;
            exp = 0;
            if (rgb == 0)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g - 0.1f, gameObject.GetComponent<SpriteRenderer>().color.b - 0.1f);
                if (gameObject.GetComponent<SpriteRenderer>().color.g < 0) 
                {
                    rgb = 1;
                }
                
            }
            else if (rgb==1)
            {
               
                gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r-0.1f, gameObject.GetComponent<SpriteRenderer>().color.g + 0.1f, gameObject.GetComponent<SpriteRenderer>().color.b + 0.1f);
                if (gameObject.GetComponent<SpriteRenderer>().color.r < 0) 
                {
                   rgb = 2;
                }
            }
            else if (rgb==2)
            {

                gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r + 0.1f, gameObject.GetComponent<SpriteRenderer>().color.g - 0.1f, gameObject.GetComponent<SpriteRenderer>().color.b);
                if (gameObject.GetComponent<SpriteRenderer>().color.g < 0) 
                {
                    rgb = 3;
                }
            }
            else if (rgb == 3)
            {
                
                gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g + 0.1f, gameObject.GetComponent<SpriteRenderer>().color.b- 0.1f);
                if (gameObject.GetComponent<SpriteRenderer>().color.b < 0) 
                {
                    rgb = 4;
                }
            }
            else if (rgb == 4)
            {

                gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r-0.1f, gameObject.GetComponent<SpriteRenderer>().color.g , gameObject.GetComponent<SpriteRenderer>().color.b);
            }
        }
        ch.value = heal;
        if (dvig)transform.Translate(0,speed*Time.deltaTime,0);
        if (heal <= 0 && did)
        {
            did = false;
            gameObject.transform.position -= new Vector3(0,0,5f);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            dvig = false;
            State = States.deed;
            Destroy(gameObject,1.54f);
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
