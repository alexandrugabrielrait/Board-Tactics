using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSurrender : MonoBehaviour {

    public int type;
    public TurnManager manager;
    public GameObject text;

    void OnMouseDown()
    {
        if(manager.player==type)manager.Win(1 - type);
    }
    void Update()
    {
        if (manager.player == type) GetComponent<SpriteRenderer>().color = Color.white;
        else GetComponent<SpriteRenderer>().color = Color.grey;
        text.GetComponent<Text>().color = GetComponent<SpriteRenderer>().color;
    }
    }
