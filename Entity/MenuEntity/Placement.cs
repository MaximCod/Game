using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Placement : Menu
{

    protected int DeliveredAlly1 = 0;
    protected Vector3[] CoordinateAlly1 = new Vector3[21];
    protected int DeliveredAlly2 = 0;
    protected Vector3[] CoordinateAlly2 = new Vector3[21];
    protected int CountAlly1 = 0;
    protected int CountAlly2 = 0;
    protected int CountEnemy = 1;
    protected int CountHard = 0;
    internal bool Save = false;

    protected GameObject Ally;
    protected AllyBehavior BehaviourAlly;
    public GameObject Boss;
    public GameObject SourceAlly;
    public GameObject StorageAlly1;
    public GameObject StorageAlly2;
    public GameObject StorageButton;
    

    public GameObject StorageEnemy;

    protected override void Awake()
    {
        Ally = SourceAlly.transform.GetChild(0).gameObject;
        BehaviourAlly = Ally.GetComponent<AllyBehavior>();
        Button = new GameObject[CountButton(StorageButton)];
        for (int i = 0; i < Button.Length; i++)
        {
            Button[i] = StorageButton.transform.GetChild(i).gameObject;
        }
        for (int m = 0; m < CoordinateAlly1.Length; m++) { CoordinateAlly1[m] = new Vector3(0, 0, -10); }
        for (int m = 0; m < CoordinateAlly2.Length; m++) { CoordinateAlly2[m] = new Vector3(0, 0, -10); }
        base.Awake();
    }

    public int CountUnit(GameObject gm)
    {
        Transform[] children;
        children = gm.GetComponentsInChildren<Transform>();
        return ((children.Length - 1) / 6);//Учитывать количество всех дочерних объектов
    }

    public void PlacementAlly1()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) &&
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y > -32.5 &&
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y <= -3 && CountAlly1 < CountUnit(StorageAlly1))
        {
            Transform ally = StorageAlly1.transform.GetChild(CountAlly1).gameObject.transform;
            ally.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 24);
            ally.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            CoordinateAlly1[DeliveredAlly1] = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 24);
            DeliveredAlly1++;
            if (CountAlly1 <= CountUnit(StorageAlly1)) CountAlly1 += 1;
        }
    }
    public void PlacementAlly2()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) &&
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y > -32.5 &&
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y <= -3 && CountAlly2 < CountUnit(StorageAlly2))
        {
            Transform ally = StorageAlly2.transform.GetChild(CountAlly2).gameObject.transform;
            ally.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 24);
            ally.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            CoordinateAlly2[DeliveredAlly2] = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 24);
            DeliveredAlly2++;
            if (CountAlly2 <= CountUnit(StorageAlly2)) CountAlly2 += 1;
        }
    }

    void PlacementUnitInPause()
    {
        if (ImageButton[4].color != Color.black)
        {
            Time.timeScale = 0;
            //GameObject.Find("animation").GetComponent<AnimationBoss>().Attack = false;
            //GameObject.Find("animation").GetComponent<AnimationBoss>().pr = true;
            int numberAlly1 = 0;
            int numberAlly2 = 0;
            int countAlly1 = 0;
            int countAlly2 = 0;
            foreach (Vector3 m in CoordinateAlly1)
            {
                if (m.z != -10 && 0 < CountUnit(StorageAlly1) && countAlly1 < CountUnit(StorageAlly1))
                {
                    countAlly1++;
                    StorageAlly1.transform.GetChild(numberAlly1).gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    StorageAlly1.transform.GetChild(numberAlly1).gameObject.transform.position = m;
                    if (numberAlly1 <= CountUnit(StorageAlly1)) numberAlly1++;
                }
            }
            foreach (Vector3 m in CoordinateAlly2)
            {
                if (m.z != -10 && 0 < CountUnit(StorageAlly2) && countAlly2 < CountUnit(StorageAlly2))
                {
                    countAlly2++;
                    StorageAlly2.transform.GetChild(numberAlly2).gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    StorageAlly2.transform.GetChild(numberAlly2).gameObject.transform.position = m;
                    if (numberAlly2 <= CountUnit(StorageAlly2)) numberAlly2++;
                }
            }
        }
        else
        {
            Time.timeScale = (float)(1 - ((CountUnit(StorageEnemy) + CountUnit(StorageAlly1) + CountUnit(StorageAlly2) + 29) / 100));
            Final();
        }
    }

    void PlacementBossInPause()
    {
        if (ImageButton[7].color != Color.black)
        {
            Boss.gameObject.transform.position = new Vector3(21, -100, 0);
        }
        if (ImageButton[7].color == Color.black && ImageButton[4].color != Color.black)
        {
            Boss.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            Boss.gameObject.transform.position = new Vector3(0, 15, 0);
        }
    }

    protected virtual void Update()
    {
        if (ImageButton[0].color == Color.black)
            PlacementAlly1();
        if (ImageButton[1].color == Color.black)
            PlacementAlly2();
        PlacementUnitInPause();
        PlacementBossInPause();
    }

    void ClearingPlayingField()
    {
        if (CountUnit(StorageEnemy) > 0)
        {
            for (int k = 0; k < CountUnit(StorageEnemy); k++)
                Destroy(StorageEnemy.transform.GetChild(k).gameObject);
        }
        if (CountUnit(StorageEnemy) == 0)
        {
            for (int k = 0; k < CountUnit(StorageAlly1); k++)
            {
                GameObject ally1 = StorageAlly1.transform.GetChild(k).gameObject;
                ally1.transform.position = new Vector3(Random.Range(0, -30), Random.Range(-60, -100), 0);
                ally1.GetComponent<AllyBehavior>().HealthSettings.CurrentHealth = ally1.GetComponent<AllyBehavior>().HealthSettings.MaxHealth;
                if (ally1.GetComponent<AllyBehavior>().HealthSettings.CurrentHealth >= 50)
                    ally1.transform.SetParent(StorageAlly2.transform);
            }
            for (int k = 0; k < CountUnit(StorageAlly2); k++)
            {
                GameObject ally2 = StorageAlly2.transform.GetChild(k).gameObject;
                ally2.transform.position = new Vector3(Random.Range(0, -30), Random.Range(-60, -100), 0);
                ally2.GetComponent<AllyBehavior>().HealthSettings.CurrentHealth = ally2.GetComponent<AllyBehavior>().HealthSettings.MaxHealth;
            }

        }
        ImageButton[4].color = Color.white;
        Save = true;
    }
    public void Final()
    {
        bool us = true;
        for (int k = 0; k < CountUnit(StorageAlly1); k++)
        {
            if (StorageAlly1.transform.GetChild(k).gameObject.transform.position.y > -32.5)
                us = false;
        }
        for (int k = 0; k < CountUnit(StorageAlly2); k++)
        {
            if (StorageAlly2.transform.GetChild(k).gameObject.transform.position.y > -32.5)
                us = false;
        }
        if (us || CountUnit(StorageEnemy) == 0)
        {
            ClearingPlayingField();
        }
    }

    public void PlacementEnemyStart(GameObject Enemy)
    {
        int count = CountEnemy;
        if (CountEnemy > 20)
        {
            CountHard = CountEnemy - 20;
            CountEnemy = 20;
        }
        for (int i = 0; CountEnemy > i; i++)
        {
            Instantiate(Enemy, new Vector3(Random.Range(-20, 21), Random.Range(5, 30), 0), transform.rotation, StorageEnemy.transform).gameObject.name = i.ToString() + "vr";
            GameObject enemy = StorageEnemy.transform.GetChild(i).gameObject;
            EnemyBehavior PrEnemy = enemy.GetComponent<EnemyBehavior>();
            if (CountHard > 0)
            {
                PrEnemy.DamageSettings.Damage = PrEnemy.DamageSettings.Damage + PrEnemy.DamageSettings.Damage * ((float)CountHard / 10);
                PrEnemy.HealthSettings.MaxHealth = PrEnemy.HealthSettings.MaxHealth + PrEnemy.HealthSettings.MaxHealth * ((float)CountHard / 10);
                PrEnemy.HealthSettings.CurrentHealth = PrEnemy.HealthSettings.MaxHealth;
                enemy.GetComponent<SpriteRenderer>().color += new Color(0, (float)-CountHard / 100 * 3, 0);
            }
        }
        CountHard = 0;
        CountEnemy = count;
    }
}
