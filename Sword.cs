using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Sword : MonoBehaviour
{
    public GameObject vr1;
    public GameObject vr2;
    public GameObject boss;
    public GameObject un;
    private GameObject hranilUn;
    private GameObject hranilUn2;
    private GameObject hranilVr;
    private bool sav = false;
    private int tim=0;
    private int tim2 = 0;
    public int mani;
    private int colvoVr = 1;
    private int colvoUp = 0;
    private int i = 0;
    private int i2 = 0;
    private int ioth1 = 0;
    private Vector3[] map1 = new Vector3[21];
    private int ioth2 = 0;
    private Vector3[] map2 = new Vector3[21];
    void Start()
    {
        for (int m = 0; m < map1.Length; m++) { map1[m] = new Vector3(0, 0, -10); }

        for (int m = 0; m < map2.Length; m++) { map2[m] = new Vector3(0, 0, -10); }
        mani = 300;
        hranilUn = GameObject.Find("HranUn");
        hranilUn2 = GameObject.Find("HranUn2");
        hranilVr = GameObject.Find("HranVr");
        LoadGame();
        OnClic4();
    }
    int Knopka1(GameObject gm)
    {
        Transform[] children;
        children = gm.GetComponentsInChildren<Transform>();
        return ((children.Length)/6);//Учитовать количество свойств
    }
    void Update()
    {
        if (Knopka1(hranilUn)<2 && Knopka1(hranilUn2)==0 && mani<200)
        {
            mani = 300;
        }
        GameObject.Find("1").GetComponent<TextMeshProUGUI>().text = "Unit lv1  " + Knopka1(hranilUn).ToString();
        GameObject.Find("2").GetComponent<TextMeshProUGUI>().text = "Unit lv2  " + Knopka1(hranilUn2).ToString();
        GameObject.Find("3").GetComponent<TextMeshProUGUI>().text = mani.ToString()+" $     Buy";
        if (GameObject.Find("Button1").GetComponent<Graphic>().color == Color.black)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0) && Camera.main.ScreenToWorldPoint(Input.mousePosition).y>-32.5 && Camera.main.ScreenToWorldPoint(Input.mousePosition).y<=-3  && i< Knopka1(hranilUn))
            {
                hranilUn.transform.GetChild(i).gameObject.transform.position= Camera.main.ScreenToWorldPoint(Input.mousePosition)+ new Vector3(0,0,24);
                map1[ioth1]= Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 24);
                ioth1++;
                hranilUn.transform.GetChild(i).gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                if (i<= Knopka1(hranilUn)) i += 1;
            }
        }
        if (GameObject.Find("Button2").GetComponent<Graphic>().color == Color.black)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && Camera.main.ScreenToWorldPoint(Input.mousePosition).y > -32.5 && Camera.main.ScreenToWorldPoint(Input.mousePosition).y <= -3 && i2 < Knopka1(hranilUn2))
            {
                hranilUn2.transform.GetChild(i2).gameObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 24);
                map2[ioth2] = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 24);
                ioth2++;
                hranilUn2.transform.GetChild(i2).gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                if (i2 <= Knopka1(hranilUn2)) i2 += 1;
            }
        }
        if (GameObject.Find("Button6").GetComponent<Graphic>().color != Color.black)
        {
            Time.timeScale = 0;
            GameObject.Find("animation").GetComponent<anim>().atti = false;
            GameObject.Find("animation").GetComponent<anim>().a1 = true;
            int g1 = 0;
            int g2 = 0;
            int u1 = 0;
            int u2 = 0;
            foreach (Vector3 m in map1)
            {
                if (m.z!=-10 && 0 < Knopka1(hranilUn) && u1< Knopka1(hranilUn))
                {
                    u1++;
                    hranilUn.transform.GetChild(g1).gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    hranilUn.transform.GetChild(g1).gameObject.transform.position = m;
                    if (g1 <= Knopka1(hranilUn)) g1++;
                }
            }
            foreach (Vector3 m in map2)
            {
                if (m.z != -10 && 0 < Knopka1(hranilUn2) && g2 < Knopka1(hranilUn2))
                {
                    u2++;
                    hranilUn2.transform.GetChild(g2).gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    hranilUn2.transform.GetChild(g2).gameObject.transform.position = m;
                    if (g2 <= Knopka1(hranilUn2)) g2++;
                }
            }
            g1 = 0;
            g2 = 0;
            if (sav) { SaveGame(); sav = false; }
        }
        else
        {
            Time.timeScale = (float)(1 - ((Knopka1(hranilVr) + Knopka1(hranilUn) + Knopka1(hranilUn2) + 29) / 100));
            Finish();
        }
        if (GameObject.Find("Button8").GetComponent<Graphic>().color != Color.black)
        {
            boss.gameObject.transform.position = new Vector3(21, -100, 0);
        }
        if (GameObject.Find("Button8").GetComponent<Graphic>().color == Color.black && GameObject.Find("Button6").GetComponent<Graphic>().color != Color.black)
        {
            boss.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            boss.gameObject.transform.position = new Vector3(0, 15, 0);
        }
    }
    public void OnClic1()
    {
        if (GameObject.Find("Button1").GetComponent<Graphic>().color != Color.black &&  GameObject.Find("Button6").GetComponent<Graphic>().color != Color.black)
        {
            GameObject.Find("Button2").GetComponent<Graphic>().color = Color.white;
            GameObject.Find("2").GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            GameObject.Find("Button1").GetComponent<Graphic>().color = Color.black;
            GameObject.Find("1").GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }
        else
        {
            GameObject.Find("Button1").GetComponent<Graphic>().color = Color.white;
            GameObject.Find("1").GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        }
    }
    public void OnClic2()
    {
        if (GameObject.Find("Button2").GetComponent<Graphic>().color != Color.black  &&  GameObject.Find("Button6").GetComponent<Graphic>().color != Color.black)
        {
            GameObject.Find("Button1").GetComponent<Graphic>().color = Color.white;
            GameObject.Find("1").GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            GameObject.Find("Button2").GetComponent<Graphic>().color = Color.black;
            GameObject.Find("2").GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }
        else
        {
            GameObject.Find("Button2").GetComponent<Graphic>().color = Color.white;
            GameObject.Find("2").GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        }
    }
    public void OnClic3()
    {
        if (mani>=100 && GameObject.Find("Button6").GetComponent<Graphic>().color != Color.black && Knopka1(hranilUn2) + Knopka1(hranilUn) < 20)
        {
            Instantiate(un.transform.GetChild(0).gameObject, new Vector3(Random.Range(0, -30), Random.Range(-60, -100), 0), transform.rotation, hranilUn.transform);
            mani -= 100;
        }
        else if (GameObject.Find("Button6").GetComponent<Graphic>().color != Color.black && mani >= 100)
        {
            mani -= 100;
            if (Knopka1(hranilUn) >= tim+1)
            {
                hranilUn.transform.GetChild(tim).gameObject.GetComponent<AllyProgram>().exp = (int)hranilUn.transform.GetChild(tim).gameObject.GetComponent<AllyProgram>().damag * 5;
                tim += 1;
            }
            else if (Knopka1(hranilUn2) >= tim2+1)
            {
                hranilUn2.transform.GetChild(tim2).gameObject.GetComponent<AllyProgram>().exp = (int)hranilUn2.transform.GetChild(tim2).gameObject.GetComponent<AllyProgram>().damag * 5;
                tim2 += 1;
            }
            else
            {
                tim = 0;
                tim2 = 0;
                if (Knopka1(hranilUn) >= tim + 1)
                {
                    hranilUn.transform.GetChild(tim).gameObject.GetComponent<AllyProgram>().exp = (int)hranilUn.transform.GetChild(tim).gameObject.GetComponent<AllyProgram>().damag * 5;
                    tim += 1;
                }
                else if (Knopka1(hranilUn2) >= tim2 + 1)
                {
                    hranilUn2.transform.GetChild(tim2).gameObject.GetComponent<AllyProgram>().exp = (int)hranilUn2.transform.GetChild(tim2).gameObject.GetComponent<AllyProgram>().damag * 5;
                    tim2 += 1;
                }
            }
        }
        for (int k = 0; k < Knopka1(hranilUn); k++)
        {
            hranilUn.transform.GetChild(k).gameObject.GetComponent<AllyProgram>().heal = hranilUn.transform.GetChild(k).gameObject.GetComponent<AllyProgram>().maxheal;
        }
        for (int k = 0; k < Knopka1(hranilUn2); k++)
        {
            hranilUn2.transform.GetChild(k).gameObject.GetComponent<AllyProgram>().heal = hranilUn2.transform.GetChild(k).gameObject.GetComponent<AllyProgram>().maxheal;
        }
    }
    public void OnClic4()
    {
        if (true)
        {
            for (int k = 0; k < Knopka1(hranilUn); k++)
            {
                hranilUn.transform.GetChild(k).gameObject.transform.position = new Vector3(Random.Range(0, -30), Random.Range(-60, -100), 0);
                hranilUn.transform.GetChild(k).gameObject.GetComponent<AllyProgram>().heal = hranilUn.transform.GetChild(k).gameObject.GetComponent<AllyProgram>().maxheal;
                if (hranilUn.transform.GetChild(k).gameObject.GetComponent<AllyProgram>().heal >= 50)
                {
                    hranilUn.transform.GetChild(k).gameObject.transform.SetParent(hranilUn2.transform);
                }
            }
            for (int k = 0; k < Knopka1(hranilUn2); k++)
            {
                hranilUn2.transform.GetChild(k).gameObject.transform.position = new Vector3(Random.Range(0, -30), Random.Range(-60, -100), 0);
                hranilUn2.transform.GetChild(k).gameObject.GetComponent<AllyProgram>().heal = hranilUn2.transform.GetChild(k).gameObject.GetComponent<AllyProgram>().maxheal;
            }
            i = 0;
            i2 = 0;
            ioth1 = 0;
            ioth2 = 0;
            for (int m = 0; m < map1.Length; m++) { map1[m] = new Vector3(0, 0, -10); }
            for (int m = 0; m < map2.Length; m++) { map2[m] = new Vector3(0, 0, -10); }
        }
    }
    public void OnClic5()
    {
        if (GameObject.Find("Button5").GetComponent<Graphic>().color != Color.black && GameObject.Find("Button6").GetComponent<Graphic>().color != Color.black)
        {
            GameObject.Find("Button8").GetComponent<Graphic>().color = Color.white;
            GameObject.Find("8").GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            GameObject.Find("Button7").GetComponent<Graphic>().color = Color.white;
            GameObject.Find("7").GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            GameObject.Find("Button5").GetComponent<Graphic>().color = Color.black;
            GameObject.Find("5").GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }
    }
    public void OnClic7()
    {
        if (GameObject.Find("Button7").GetComponent<Graphic>().color != Color.black && GameObject.Find("Button6").GetComponent<Graphic>().color != Color.black)
        {
            GameObject.Find("Button5").GetComponent<Graphic>().color = Color.white;
            GameObject.Find("5").GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            GameObject.Find("Button8").GetComponent<Graphic>().color = Color.white;
            GameObject.Find("8").GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            GameObject.Find("Button7").GetComponent<Graphic>().color = Color.black;
            GameObject.Find("7").GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }
    }
    public void OnClic8()
    {
        if (GameObject.Find("Button6").GetComponent<Graphic>().color != Color.black && GameObject.Find("Button8").GetComponent<Graphic>().color != Color.black)
        {
            GameObject.Find("Button5").GetComponent<Graphic>().color = Color.white;
            GameObject.Find("5").GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            GameObject.Find("Button7").GetComponent<Graphic>().color = Color.white;
            GameObject.Find("7").GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            GameObject.Find("Button8").GetComponent<Graphic>().color = Color.black;
            GameObject.Find("8").GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            boss.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            boss.gameObject.transform.position = new Vector3(0,15,0);
        }
    }
    public void OnClic6()
    {
        if (GameObject.Find("Button6").GetComponent<Graphic>().color != Color.black && (GameObject.Find("Button5").GetComponent<Graphic>().color == Color.black || GameObject.Find("Button7").GetComponent<Graphic>().color == Color.black || GameObject.Find("Button8").GetComponent<Graphic>().color == Color.black))
        {
            for (int k = 0; k < Knopka1(hranilUn); k++)
            {
                hranilUn.transform.GetChild(k).gameObject.GetComponent<AllyProgram>().heal = hranilUn.transform.GetChild(k).gameObject.GetComponent<AllyProgram>().maxheal;
            }
            for (int k = 0; k < Knopka1(hranilUn2); k++)
            {
                hranilUn2.transform.GetChild(k).gameObject.GetComponent<AllyProgram>().heal = hranilUn2.transform.GetChild(k).gameObject.GetComponent<AllyProgram>().maxheal;
            }
            GameObject.Find("Button1").GetComponent<Graphic>().color = Color.white;
            GameObject.Find("1").GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            GameObject.Find("Button2").GetComponent<Graphic>().color = Color.white;
            GameObject.Find("2").GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            GameObject.Find("Button6").GetComponent<Graphic>().color = Color.black;
            GameObject.Find("6").GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            if (GameObject.Find("Button5").GetComponent<Graphic>().color == Color.black)
            {
                VragStart(vr1);
            }
            else if (GameObject.Find("Button7").GetComponent<Graphic>().color == Color.black)
            {
                VragStart(vr2);
            }
            else if (GameObject.Find("Button8").GetComponent<Graphic>().color == Color.black)
            {
                boss.gameObject.transform.position = new Vector3(21, -100, 0);
                Instantiate(boss, new Vector3(0, 15, 0), transform.rotation, hranilVr.transform);
                boss.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                
            }
        }
        sav = true;
    }
    public void onClic9()
    {
        if (colvoVr > 1 && GameObject.Find("Button6").GetComponent<Graphic>().color != Color.black)
        {
            colvoVr -= 1;
            boss.GetComponent<EnemyProgram>().maxheal = 200 * colvoVr;
            boss.GetComponent<EnemyProgram>().heal = 200 * colvoVr;
            boss.GetComponent<EnemyProgram>().damag = 20 * colvoVr;
            if (colvoVr < 10)
            {
                boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color.r + 0.1f, boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color.g, boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color.b + 0.1f);
            }
            else if (colvoVr < 20)
            {
                boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color.r - 0.1f, boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color.g + 0.1f, boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color.b);
            }
            else if (colvoVr < 30)
            {
                boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color.r, boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color.g, boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color.b - 0.1f);
            }
            else if (colvoVr < 40)
            {
                boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color.r + 0.1f, boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color.g, boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color.b);
            }
            else if (colvoVr < 50)
            {
                boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color.r, boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color.g, boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color.b + 0.1f);
            }
        }
             
        GameObject.Find("10").GetComponentInChildren<TextMeshProUGUI>().text = "            +          " + colvoVr.ToString()+"/50";
       
    }
    public void onClic10()
    {
        if (colvoVr < 50 && GameObject.Find("Button6").GetComponent<Graphic>().color != Color.black)
        {
            colvoVr += 1;
            boss.GetComponent<EnemyProgram>().maxheal = 200 * colvoVr;
            boss.GetComponent<EnemyProgram>().heal = 200 * colvoVr;
            boss.GetComponent<EnemyProgram>().damag = 20 * colvoVr;
            if (colvoVr<10)
            {
                boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color.r - 0.1f, boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color.g, boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color.b - 0.1f);
            }
            else if (colvoVr<20)
            {
                boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color.r + 0.1f, boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color.g-0.1f, boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color.b);
            }
            else if (colvoVr < 30)
            {
                boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color.r, boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color.g, boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color.b+0.1f);
            }
            else if (colvoVr < 40)
            {
                boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color.r-0.1f, boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color.g, boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color.b);
            }
            else if (colvoVr < 50)
            {
                boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color.r, boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color.g, boss.transform.GetChild(1).GetComponent<SpriteRenderer>().color.b-0.1f);
            }
        }   
            
        GameObject.Find("10").GetComponentInChildren<TextMeshProUGUI>().text = "            +          " + colvoVr.ToString() + "/50";
        
    }
    void VragStart(GameObject gm)
    {
        int zp = colvoVr;
        if (colvoVr>20)
        {
            colvoUp = colvoVr - 20;
            colvoVr = 20;
        }
        for (int i =0; colvoVr>i;i++)
        {
            Instantiate(gm, new Vector3(Random.Range(-20, 21), Random.Range(5,30), 0), transform.rotation, hranilVr.transform).gameObject.name=i.ToString()+"vr";
            GameObject gg = hranilVr.transform.GetChild(i).gameObject;
            if (colvoUp>0)
            {
                gg.GetComponent<EnemyProgram>().damag = gg.GetComponent<EnemyProgram>().damag+gg.GetComponent<EnemyProgram>().damag * ((float)colvoUp / 10);
                gg.GetComponent<EnemyProgram>().maxheal = gg.GetComponent<EnemyProgram>().maxheal+gg.GetComponent<EnemyProgram>().maxheal * ((float)colvoUp / 10);
                gg.GetComponent<EnemyProgram>().heal = gg.GetComponent<EnemyProgram>().maxheal;
                gg.GetComponent<SpriteRenderer>().color += new Color(0, (float)-colvoUp/100*3,0);
            }
        }
        colvoUp = 0;
        colvoVr = zp;
    }
    void Finish()
    {
        bool us = true;
        for (int k = 0; k < Knopka1(hranilUn); k++)
        {
            if (hranilUn.transform.GetChild(k).gameObject.transform.position.y > -32.5)
            {
                us = false;
            }
        }
        for (int k = 0; k < Knopka1(hranilUn2); k++)
        {
            if (hranilUn2.transform.GetChild(k).gameObject.transform.position.y > -32.5)
            {
                us = false;
            }
        }
        if (us || Knopka1(hranilVr) == 0)
        {
            GameObject.Find("6").GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            if (Knopka1(hranilVr) > 0)
            {
                for (int k = 0; k < Knopka1(hranilVr); k++)
                {
                    Destroy(hranilVr.transform.GetChild(k).gameObject);
                }
            }
            if (Knopka1(hranilVr) == 0)
            {             
                for (int k = 0; k < Knopka1(hranilUn); k++)
                {
                    hranilUn.transform.GetChild(k).gameObject.transform.position = new Vector3(Random.Range(0,-30), Random.Range(-60, -100), 0);
                    hranilUn.transform.GetChild(k).gameObject.GetComponent<AllyProgram>().heal = hranilUn.transform.GetChild(k).gameObject.GetComponent<AllyProgram>().maxheal;
                    if (hranilUn.transform.GetChild(k).gameObject.GetComponent<AllyProgram>().heal>=50)
                    {
                        hranilUn.transform.GetChild(k).gameObject.transform.SetParent(hranilUn2.transform);
                    }
                }
                for (int k = 0; k < Knopka1(hranilUn2); k++)
                {
                    hranilUn2.transform.GetChild(k).gameObject.transform.position = new Vector3(Random.Range(0, -30), Random.Range(-60, -100), 0);
                    hranilUn2.transform.GetChild(k).gameObject.GetComponent<AllyProgram>().heal = hranilUn2.transform.GetChild(k).gameObject.GetComponent<AllyProgram>().maxheal;
                }

            }
            GameObject.Find("Button6").GetComponent<Graphic>().color = Color.white;
        }
    }
    void SaveGame()
    {
        PlayerPrefs.SetInt("hranilUnColvo", Knopka1(hranilUn));
        PlayerPrefs.SetInt("hranilUn2Colvo", Knopka1(hranilUn2));
        PlayerPrefs.SetInt("tim", tim);
        PlayerPrefs.SetInt("tim2", tim2);
        PlayerPrefs.SetInt("moni", mani);
        for (int i=0;i<Knopka1(hranilUn);i++)
        {
            PlayerPrefs.SetFloat("hranilUnMaxheal"+i.ToString(), hranilUn.transform.GetChild(i).gameObject.GetComponent<AllyProgram>().maxheal);
            PlayerPrefs.SetFloat("hranilUnDamag" + i.ToString(), hranilUn.transform.GetChild(i).gameObject.GetComponent<AllyProgram>().damag);

            PlayerPrefs.SetInt("hranilUnRGB" + i.ToString(), hranilUn.transform.GetChild(i).gameObject.GetComponent<AllyProgram>().rgb);
            PlayerPrefs.SetFloat("hranilUnColorR" + i.ToString(), hranilUn.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color.r);
            PlayerPrefs.SetFloat("hranilUnColorG" + i.ToString(), hranilUn.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color.g);
            PlayerPrefs.SetFloat("hranilUnColorB" + i.ToString(), hranilUn.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color.b);
        }
        for (int i = 0; i < Knopka1(hranilUn2); i++)
        {
            PlayerPrefs.SetFloat("hranilUn2Maxheal" + i.ToString(), hranilUn2.transform.GetChild(i).gameObject.GetComponent<AllyProgram>().maxheal);
            PlayerPrefs.SetFloat("hranilUn2Damag" + i.ToString(), hranilUn2.transform.GetChild(i).gameObject.GetComponent<AllyProgram>().damag);

            PlayerPrefs.SetInt("hranilUn2RGB" + i.ToString(), hranilUn2.transform.GetChild(i).gameObject.GetComponent<AllyProgram>().rgb);
            PlayerPrefs.SetFloat("hranilUn2ColorR" + i.ToString(), hranilUn2.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color.r);
            PlayerPrefs.SetFloat("hranilUn2ColorG" + i.ToString(), hranilUn2.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color.g);
            PlayerPrefs.SetFloat("hranilUn2ColorB" + i.ToString(), hranilUn2.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color.b);
        }

        PlayerPrefs.Save();
        Debug.Log("Game data saved!");
    }
    void LoadGame()
    {
        if(PlayerPrefs.HasKey("hranilUnColvo"))
        {
            tim = PlayerPrefs.GetInt("tim");
            tim2 = PlayerPrefs.GetInt("tim2");
            mani = PlayerPrefs.GetInt("moni");
            for (int i = 0; i < PlayerPrefs.GetInt("hranilUnColvo"); i++)
            {
                Instantiate(un.transform.GetChild(0), new Vector3(Random.Range(0, -30), Random.Range(-60, -100), 0), un.gameObject.transform.rotation, hranilUn.transform).gameObject.name="un"+i.ToString();
                GameObject.Find("un" + i.ToString()).GetComponent<AllyProgram>().maxheal = PlayerPrefs.GetFloat("hranilUnMaxheal" + i.ToString());
                GameObject.Find("un" + i.ToString()).GetComponent<AllyProgram>().damag = PlayerPrefs.GetFloat("hranilUnDamag" + i.ToString());
                GameObject.Find("un" + i.ToString()).GetComponent<AllyProgram>().rgb = PlayerPrefs.GetInt("hranilUnRGB" + i.ToString());
                GameObject.Find("un" + i.ToString()).GetComponent<SpriteRenderer>().color = new Color(PlayerPrefs.GetFloat("hranilUnColorR" + i.ToString()), PlayerPrefs.GetFloat("hranilUnColorG" + i.ToString()), PlayerPrefs.GetFloat("hranilUnColorB" + i.ToString()));
            }
            for (int i = 0; i < PlayerPrefs.GetInt("hranilUn2Colvo"); i++)
            {
                Instantiate(un.transform.GetChild(0), new Vector3(Random.Range(0, -30), Random.Range(-60, -100), 0), un.gameObject.transform.rotation, hranilUn2.transform).gameObject.name = "2un" + i.ToString();
                GameObject.Find("2un" + i.ToString()).GetComponent<AllyProgram>().maxheal = PlayerPrefs.GetFloat("hranilUn2Maxheal" + i.ToString());
                GameObject.Find("2un" + i.ToString()).GetComponent<AllyProgram>().damag = PlayerPrefs.GetFloat("hranilUn2Damag" + i.ToString());
                GameObject.Find("2un" + i.ToString()).GetComponent<AllyProgram>().rgb = PlayerPrefs.GetInt("hranilUn2RGB" + i.ToString());
                GameObject.Find("2un" + i.ToString()).GetComponent<SpriteRenderer>().color = new Color(PlayerPrefs.GetFloat("hranilUn2ColorR" + i.ToString()), PlayerPrefs.GetFloat("hranilUn2ColorG" + i.ToString()), PlayerPrefs.GetFloat("hranilUn2ColorB" + i.ToString()));
            }
        }
    }
}
