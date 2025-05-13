using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Menu : MonoBehaviour
{
    public GameObject[] Button;
    public GameObject[] TextButton;
    public TextMeshProUGUI[] Text;
    public Image[] ImageButton;

    public void ChangColor(int i) => ImageButton[i].color = ImageButton[i].color == Color.black ? Color.white : Color.black;

    public void Initializing()
    {
        ImageButton = new Image[Button.Length];
        TextButton = new GameObject[Button.Length];
        Text = new TextMeshProUGUI[Button.Length];
        for (int i = 0; i < Button.Length; i++) 
        {
            ImageButton[i] = Button[i].GetComponent<Image>();
            TextButton[i] = Button[i].transform.GetChild(0).gameObject;
            Text[i] = TextButton[i].GetComponent<TextMeshProUGUI>();
        }
    }
    protected virtual void Awake()
    {
        if (Button.Length > 0)
        {
            Initializing();
        }
    }

    public int CountButton(GameObject gm)
    {
        Transform[] children;
        children = gm.GetComponentsInChildren<Transform>();
        return ((children.Length-1) / 2);//Учитывать количество всех дочерних объектов
    }
}