using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardRemember : MonoBehaviour,IPointerDownHandler{
    public Sprite standart;
    public Sprite opened;
    public int num;
    public GameMain main;
    public bool open;
    public Text Count;
    
    public void Set(GameMain main,int num,Sprite opened)
    {
        this.opened=opened;
        this.num = num;
        this.main = main;
        Count.text = num.ToString();
        Open(false);
    }
    public void Open(bool b) {
        open = b;
        if (b) {
            GetComponent<Image>().sprite = opened;
            Count.gameObject.SetActive(true);
        } else {
            GetComponent<Image>().sprite = standart;
            Count.gameObject.SetActive(false);
        }


    }

    public void OnPointerDown(PointerEventData eventData)
    {

        if (!open) {
            if (main.gameId == 4)
            {
                bool b = main.games[4].GetComponent<GameRemember>().Answer(num);
                if (b) { Open(true); }
            }
            else if (main.gameId == 5)
            {
                main.games[5].GetComponent<GameFindPair>().Answer(num,this);
                
            }
        }

    }

}
