using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MainMenu: Placement
{

    public GameObject Enemy1;
    public GameObject Enemy2;

    public int Money;

    internal int Queue1 = 0;
    internal int Queue2 = 0;

    protected override void Awake()
    {
        Money = 300;
        Clear();
        base.Awake();
    }

    void TextButtonUpdate()
    {
        Text[0].text = "Unit lv1  " + CountUnit(StorageAlly1).ToString();
        Text[1].text = "Unit lv2  " + CountUnit(StorageAlly2).ToString();
        Text[2].text = Money.ToString() + " $  "+ BehaviourAlly.Cost.ToString()+" Buy"; 
    }

    protected override void Update()
    {
        base.Update();
        if (CountUnit(StorageAlly1)<2 && CountUnit(StorageAlly2)==0 && Money<200)
        {
            Money = 300;
        }
        TextButtonUpdate();
    }
    public void SelectAlly1()
    {
        if (ImageButton[0].color != Color.black && ImageButton[4].color != Color.black)
        {
            ImageButton[1].color = Color.white;
            Text[1].color = Color.black; //GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            ImageButton[0].color = Color.black;
            Text[0].color = Color.white;
        }
        else
        {
            ImageButton[0].color = Color.white;
            Text[0].color = Color.black;
        }
    }
    public void SelectAlly2()
    {
        if (ImageButton[1].color != Color.black  && ImageButton[4].color != Color.black)
        {
            ImageButton[0].color = Color.white;
            Text[0].color = Color.black;
            ImageButton[1].color = Color.black;
            Text[1].color = Color.white;
        }
        else
        {
            ImageButton[1].color = Color.white;
            Text[1].color = Color.black;
        }
    }
    public void BuyAlly()
    {
        if (Money>= BehaviourAlly.Cost && ImageButton[4].color != Color.black && CountUnit(StorageAlly2) + CountUnit(StorageAlly1) < 20)
        {
            Instantiate(Ally, new Vector3(Random.Range(0, -30), Random.Range(-60, -100), 0), transform.rotation, StorageAlly1.transform);
            Money -= BehaviourAlly.Cost;
        }
        else if (ImageButton[4].color != Color.black && Money >= 100)
        {
            Money -= BehaviourAlly.Cost;
            if (CountUnit(StorageAlly1) >= Queue1+1)
            {
                AllyBehavior AllyPr1 = StorageAlly1.transform.GetChild(Queue1).gameObject.GetComponent<AllyBehavior>();
                AllyPr1.Experience = (int)AllyPr1.DamageSettings.Damage * 5;
                Queue1 += 1;
            }
            else if (CountUnit(StorageAlly2) >= Queue2+1)
            {
                AllyBehavior AllyPr2 = StorageAlly2.transform.GetChild(Queue2).gameObject.GetComponent<AllyBehavior>();
                AllyPr2.Experience = (int)AllyPr2.DamageSettings.Damage * 5;
                Queue2 += 1;
            }
            else
            {
                Queue1 = 0;
                Queue2 = 0;
                if (CountUnit(StorageAlly1) >= Queue1 + 1)
                {
                    AllyBehavior AllyPr1 = StorageAlly1.transform.GetChild(Queue1).gameObject.GetComponent<AllyBehavior>();
                    AllyPr1.Experience = (int)AllyPr1.DamageSettings.Damage * 5;
                    Queue1 += 1;
                }
                else if (CountUnit(StorageAlly2) >= Queue2 + 1)
                {
                    AllyBehavior AllyPr2 = StorageAlly2.transform.GetChild(Queue2).gameObject.GetComponent<AllyBehavior>();
                    AllyPr2.Experience = (int)AllyPr2.DamageSettings.Damage * 5;
                    Queue2 += 1;
                }
            }
        }
        for (int k = 0; k < CountUnit(StorageAlly1); k++)
        {
            AllyBehavior AllyPr1 = StorageAlly1.transform.GetChild(k).gameObject.GetComponent<AllyBehavior>();
            AllyPr1.HealthSettings.CurrentHealth = AllyPr1.HealthSettings.MaxHealth;
        }
        for (int k = 0; k < CountUnit(StorageAlly2); k++)
        {
            AllyBehavior AllyPr2 = StorageAlly2.transform.GetChild(Queue2).gameObject.GetComponent<AllyBehavior>();
            AllyPr2.HealthSettings.CurrentHealth = AllyPr2.HealthSettings.MaxHealth;
        }
    }
    public void Clear()
    {
        if (true)
        {
            for (int k = 0; k < CountUnit(StorageAlly1); k++)
            {
                GameObject Ally1 = StorageAlly1.transform.GetChild(k).gameObject;
                AllyBehavior AllyPr1 = Ally1.GetComponent<AllyBehavior>();
                Ally1.transform.position = new Vector3(Random.Range(0, -30), Random.Range(-60, -100), 0);
                AllyPr1.HealthSettings.CurrentHealth = AllyPr1.HealthSettings.MaxHealth;
                if (AllyPr1.HealthSettings.CurrentHealth >= 50)
                {
                    Ally1.transform.SetParent(StorageAlly2.transform);
                }
            }
            for (int k = 0; k < CountUnit(StorageAlly2); k++)
            {
                GameObject Ally2 = StorageAlly2.transform.GetChild(k).gameObject;
                AllyBehavior AllyPr2 = Ally2.GetComponent<AllyBehavior>();
                Ally2.transform.position = new Vector3(Random.Range(0, -30), Random.Range(-60, -100), 0);
                AllyPr2.HealthSettings.CurrentHealth = AllyPr2.HealthSettings.MaxHealth;
            }
            CountAlly1 = 0;
            CountAlly2 = 0;
            DeliveredAlly1 = 0;
            DeliveredAlly2 = 0;
            for (int m = 0; m < CoordinateAlly1.Length; m++) { CoordinateAlly1[m] = new Vector3(0, 0, -10); }
            for (int m = 0; m < CoordinateAlly2.Length; m++) { CoordinateAlly2[m] = new Vector3(0, 0, -10); }
        }
    }
    public void SelectEnemy1()
    {
        if (ImageButton[5].color != Color.black && ImageButton[4].color != Color.black)
        {
            ImageButton[7].color = Color.white;
            Text[7].color = Color.black;
            ImageButton[6].color = Color.white;
            Text[6].color = Color.black;
            ImageButton[5].color = Color.black;
            Text[5].color = Color.white;
        }
    }
    public void SelectEnemy2()
    {
        if (ImageButton[6].color != Color.black && ImageButton[4].color != Color.black)
        {
            ImageButton[5].color = Color.white;
            Text[5].color = Color.black;
            ImageButton[7].color = Color.white;
            Text[7].color = Color.black;
            ImageButton[6].color = Color.black;
            Text[6].color = Color.white;
        }
    }
    public void SelectBoss()
    {
        if (ImageButton[4].color != Color.black && ImageButton[7].color != Color.black)
        {
            ImageButton[5].color = Color.white;
            Text[5].color = Color.black;
            ImageButton[6].color = Color.white;
            Text[6].color = Color.black;
            ImageButton[7].color = Color.black;
            Text[7].color = Color.white;
            Boss.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            Boss.gameObject.transform.position = new Vector3(0,15,0);
        }
    }
    public void StartGame()
    {
        if (ImageButton[4].color != Color.black && (ImageButton[5].color == Color.black || ImageButton[6].color == Color.black || ImageButton[7].color == Color.black))
        {
            for (int k = 0; k < CountUnit(StorageAlly1); k++)
            {
                StorageAlly1.transform.GetChild(k).gameObject.GetComponent<AllyBehavior>().HealthSettings.CurrentHealth = StorageAlly1.transform.GetChild(k).gameObject.GetComponent<AllyBehavior>().HealthSettings.MaxHealth;
            }
            for (int k = 0; k < CountUnit(StorageAlly2); k++)
            {   
                StorageAlly2.transform.GetChild(k).gameObject.GetComponent<AllyBehavior>().HealthSettings.CurrentHealth = StorageAlly2.transform.GetChild(k).gameObject.GetComponent<AllyBehavior>().HealthSettings.MaxHealth;
            }
            ImageButton[0].color = Color.white;
            Text[0].color = Color.black;
            ImageButton[1].color = Color.white;
            Text[1].color = Color.black;
            ImageButton[4].color = Color.black;
            Text[4].color = Color.white;
            if (ImageButton[5].color == Color.black)
            {
                EnemyStart(Enemy1);
            }
            else if (ImageButton[6].color == Color.black)
            {
                EnemyStart(Enemy2);
            }
            else if (ImageButton[7].color == Color.black)
            {
                Boss.gameObject.transform.position = new Vector3(21, -100, 0);
                Instantiate(Boss, new Vector3(0, 15, 0), transform.rotation, StorageEnemy.transform);
                Boss.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                
            }
        }
    }
    public void ReduceComplexity()
    {
        if (CountEnemy > 1 && ImageButton[4].color != Color.black)
        {
            CountEnemy -= 1;
            Boss.GetComponent<EnemyBehavior>().HealthSettings.MaxHealth = 200 * CountEnemy;
            Boss.GetComponent<EnemyBehavior>().HealthSettings.CurrentHealth = 200 * CountEnemy;
            Boss.GetComponent<EnemyBehavior>().DamageSettings.Damage = 20 * CountEnemy;
        }

        Text[9].text = "            +          " + CountEnemy.ToString()+"/50";
       
    }
    public void AddComplexity()
    {
        if (CountEnemy < 50 && ImageButton[4].color != Color.black)
        {
            CountEnemy += 1;
            Boss.GetComponent<EnemyBehavior>().HealthSettings.MaxHealth = 200 * CountEnemy;
            Boss.GetComponent<EnemyBehavior>().HealthSettings.CurrentHealth = 200 * CountEnemy;
            Boss.GetComponent<EnemyBehavior>().DamageSettings.Damage = 20 * CountEnemy;
        }

        Text[9].text = "            +          " + CountEnemy.ToString() + "/50";
        
    }
    void EnemyStart(GameObject gameobject)
    {
        int count = CountEnemy;
        if (CountEnemy>20)
        {
            CountHard = CountEnemy - 20;
            CountEnemy = 20;
        }
        for (int i =0; CountEnemy>i;i++)
        {
            Instantiate(gameobject, new Vector3(Random.Range(-20, 21), Random.Range(5,30), 0), transform.rotation, StorageEnemy.transform).gameObject.name=i.ToString()+"vr";
            GameObject enemy = StorageEnemy.transform.GetChild(i).gameObject;
            EnemyBehavior PrEnemy = enemy.GetComponent<EnemyBehavior>();
            if (CountHard>0)
            {
                PrEnemy.DamageSettings.Damage = PrEnemy.DamageSettings.Damage + PrEnemy.DamageSettings.Damage * ((float)CountHard / 10);
                PrEnemy.HealthSettings.MaxHealth = PrEnemy.HealthSettings.MaxHealth +PrEnemy.HealthSettings.MaxHealth * ((float)CountHard / 10);
                PrEnemy.HealthSettings.CurrentHealth = PrEnemy.HealthSettings.MaxHealth;
                enemy.GetComponent<SpriteRenderer>().color += new Color(0, (float)-CountHard/100*3,0);
            }
        }
        CountHard = 0;
        CountEnemy = count;
    }
}
