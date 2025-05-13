using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Caching : MonoBehaviour
{
    MainMenu MainMenu;
    void Start()
    {
        MainMenu = GetComponent<MainMenu>();
        LoadGame();
    }
    void Update()
    {
        if (MainMenu.Save)
        { 
            SaveGame();
            MainMenu.Save = false;
        }
    }
    int CountUnit(GameObject gm)
    {
        Transform[] children;
        children = gm.GetComponentsInChildren<Transform>();
        return ((children.Length - 1) / 6);//Учитывать количество всех дочерних объектов
    }
    void SaveGame()
    {
        PlayerPrefs.SetInt("Ally1StorageCount", CountUnit(MainMenu.StorageAlly1));
        PlayerPrefs.SetInt("Ally2StorageCount", CountUnit(MainMenu.StorageAlly2));
        PlayerPrefs.SetInt("Queue1", MainMenu.Queue1);
        PlayerPrefs.SetInt("Queue2", MainMenu.Queue2);
        PlayerPrefs.SetInt("Money", MainMenu.Money);
        for (int i = 0; i < CountUnit(MainMenu.StorageAlly1); i++)
        {
            GameObject Ally1 = MainMenu.StorageAlly1.transform.GetChild(i).gameObject;
            AllyBehavior AllyPr = Ally1.GetComponent<AllyBehavior>();
            SpriteRenderer SpRender = Ally1.GetComponent<SpriteRenderer>();
            PlayerPrefs.SetFloat("Ally1StorageMaxHealth" + i.ToString(), AllyPr.HealthSettings.MaxHealth);
            PlayerPrefs.SetFloat("Ally1StorageDamage" + i.ToString(), AllyPr.DamageSettings.Damage);
            PlayerPrefs.SetFloat("Ally1StorageColorR" + i.ToString(), SpRender.color.r);
            PlayerPrefs.SetFloat("Ally1StorageColorG" + i.ToString(), SpRender.color.g);
            PlayerPrefs.SetFloat("Ally1StorageColorB" + i.ToString(), SpRender.color.b);
        }
        for (int i = 0; i < CountUnit(MainMenu.StorageAlly2); i++)
        {
            GameObject Ally2 = MainMenu.StorageAlly2.transform.GetChild(i).gameObject;
            AllyBehavior AllyPr = Ally2.GetComponent<AllyBehavior>();
            SpriteRenderer SpRender = Ally2.GetComponent<SpriteRenderer>();
            PlayerPrefs.SetFloat("Ally2StorageMaxHealth" + i.ToString(), AllyPr.HealthSettings.MaxHealth);
            PlayerPrefs.SetFloat("Ally2StorageDamage" + i.ToString(), AllyPr.DamageSettings.Damage);
            PlayerPrefs.SetFloat("Ally2StorageColorR" + i.ToString(), SpRender.color.r);
            PlayerPrefs.SetFloat("Ally2StorageColorG" + i.ToString(), SpRender.color.g);
            PlayerPrefs.SetFloat("Ally2StorageColorB" + i.ToString(), SpRender.color.b);
        }
        PlayerPrefs.Save();
        Debug.Log("Game data saved!");
    }

    void LoadGame()
    {
        if (PlayerPrefs.HasKey("Ally1StorageCount"))
        {
            MainMenu.Queue1 = PlayerPrefs.GetInt("Queue1");
            MainMenu.Queue2 = PlayerPrefs.GetInt("Queue2");
            MainMenu.Money = PlayerPrefs.GetInt("Money");
            Transform sourceAlly = MainMenu.SourceAlly.transform.GetChild(0);
            for (int i = 0; i < PlayerPrefs.GetInt("Ally1StorageCount"); i++)
            {
                float ColorR = PlayerPrefs.GetFloat("Ally1StorageColorR" + i.ToString());
                float ColorG = PlayerPrefs.GetFloat("Ally1StorageColorG" + i.ToString());
                float ColorB = PlayerPrefs.GetFloat("Ally1StorageColorB" + i.ToString());
                string name = "Ally1" + i.ToString();
                Instantiate(sourceAlly, new Vector3(Random.Range(0, -30), Random.Range(-60, -100), 0), transform.rotation, MainMenu.StorageAlly1.transform).gameObject.name = name;
                GameObject newAlly = GameObject.Find(name);
                newAlly.GetComponent<AllyBehavior>().HealthSettings.MaxHealth = PlayerPrefs.GetFloat("Ally1StorageMaxHealth" + i.ToString());
                newAlly.GetComponent<AllyBehavior>().DamageSettings.Damage = PlayerPrefs.GetFloat("Ally1StorageDamage" + i.ToString());
                newAlly.GetComponent<SpriteRenderer>().color = new Color(ColorR, ColorG, ColorB);
            }
            for (int i = 0; i < PlayerPrefs.GetInt("Ally2StorageCount"); i++)
            {
                float ColorR = PlayerPrefs.GetFloat("Ally2StorageColorR" + i.ToString());
                float ColorG = PlayerPrefs.GetFloat("Ally2StorageColorG" + i.ToString());
                float ColorB = PlayerPrefs.GetFloat("Ally2StorageColorB" + i.ToString());
                string name = "Ally2" + i.ToString();
                Instantiate(sourceAlly, new Vector3(Random.Range(0, -30), Random.Range(-60, -100), 0), transform.rotation, MainMenu.StorageAlly2.transform).gameObject.name = name;
                GameObject newAlly = GameObject.Find(name);
                newAlly.GetComponent<AllyBehavior>().HealthSettings.MaxHealth = PlayerPrefs.GetFloat("Ally2StorageMaxHealth" + i.ToString());
                newAlly.GetComponent<AllyBehavior>().DamageSettings.Damage = PlayerPrefs.GetFloat("Ally2StorageDamage" + i.ToString());
                newAlly.GetComponent<SpriteRenderer>().color = new Color(ColorR, ColorG, ColorB);
            }
        }
    }
}
