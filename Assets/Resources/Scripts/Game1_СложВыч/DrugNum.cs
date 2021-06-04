using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DrugNum : MonoBehaviour,IBeginDragHandler,IEndDragHandler, IDragHandler,IDropHandler {

    public int id;
    public int num;
    public Text text;
    RectTransform rect;
    Vector3 begin;
    public bool b = false;
   
    public Sprite[] icons;
    public GameMain main;
    public void Start() {
        rect = GetComponent<RectTransform>();
        
    }

    public void SetNum(int n, GameMain main, int id,int color) {
        this.id = id;
        num = n;
        text.text = num.ToString();
        this.main = main;
        GetComponent<Image>().sprite = icons[color];
        
    }
    public void SetNum(int n,string st, GameMain main, int id, int color)
    {
        this.id = id;
        num = n;
        text.text = st;
        this.main = main;
        GetComponent<Image>().sprite = icons[color];
      
    }
    public void OnDrag(PointerEventData eventData)
    {
      
     //   if (game.idDrag != id) return;
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rect.position= new Vector3(pos.x+(transform.localScale.x/2),pos.y+(transform.localScale.y/2),rect.position.z);
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (main.gameId == 0)
        {
            if (!b) {
                rect.parent.GetComponent<GridLayoutGroup>().enabled = false;
                rect.position = begin;
                rect.SetSiblingIndex(id);
                rect.parent.GetComponent<GridLayoutGroup>().enabled = true;
            }
            else
            {
                b = false;
                bool ass = true;
                ass = main.games[0].GetComponent<GameAddSub>().Answer(num);
                if (!ass)
                {
                    rect.parent.GetComponent<GridLayoutGroup>().enabled = false;
                    rect.position = begin;
                    rect.SetSiblingIndex(id);
                    rect.parent.GetComponent<GridLayoutGroup>().enabled = true;

                }
                else
                {
                    rect.position = main.games[0].GetComponent<GameAddSub>().q.transform.position;
                }
            }
        }
        else if (main.gameId == 1)
        {
            if (!b) {
                rect.parent.GetComponent<GridLayoutGroup>().enabled = false;
                rect.position = begin;
                rect.SetSiblingIndex(id);
                rect.parent.GetComponent<GridLayoutGroup>().enabled = true;
            }
            else
            {
                b = false;
                bool ass = true;
                ass = main.games[1].GetComponent<GameMultiplay>().Answer(num);
                if (!ass)
                {
                    rect.parent.GetComponent<GridLayoutGroup>().enabled = false;
                    rect.position = begin;
                    rect.SetSiblingIndex(id);
                    rect.parent.GetComponent<GridLayoutGroup>().enabled = true;
                }
                else
                {
                    rect.position = main.games[1].GetComponent<GameMultiplay>().q.transform.position;
                }

            }

        }
        else if (main.gameId == 2) {
            if (!b) {
                rect.parent.GetComponent<GridLayoutGroup>().enabled = false;
                rect.position = begin;
                rect.SetSiblingIndex(id);
                rect.parent.GetComponent<GridLayoutGroup>().enabled = true;
            }
            else
            {
                b = false;
                bool ass = true;
                ass = main.games[2].GetComponent<GameFindNumber>().Answer(num);
                if (!ass)
                {
                    rect.parent.GetComponent<GridLayoutGroup>().enabled = false;
                    rect.position = begin;
                    rect.SetSiblingIndex(id);
                    rect.parent.GetComponent<GridLayoutGroup>().enabled = true;
                }
                else
                {
                    rect.position = main.games[2].GetComponent<GameFindNumber>().q.transform.position;
                }

            }

        }
        else if (main.gameId == 3)
        {
            if (!b) {
                rect.parent.GetComponent<GridLayoutGroup>().enabled = false;
                rect.position = begin;
                rect.SetSiblingIndex(id);
                rect.parent.GetComponent<GridLayoutGroup>().enabled = true;
            }
            else
            {
                b = false;
                bool ass = true;
                ass = main.games[3].GetComponent<GameFindSign>().Answer(num);
                if (!ass)
                {
                    rect.parent.GetComponent<GridLayoutGroup>().enabled = false;
                    rect.position = begin;
                    rect.SetSiblingIndex(id);
                    rect.parent.GetComponent<GridLayoutGroup>().enabled = true;
                }
                else {
                    rect.position = main.games[3].GetComponent<GameFindSign>().q.transform.position;
                }

            }

        }
        else if (main.gameId == 7)
        {
            if (!b) {
                rect.parent.GetComponent<GridLayoutGroup>().enabled = false;
                rect.position = begin;
                rect.SetSiblingIndex(id);
                rect.parent.GetComponent<GridLayoutGroup>().enabled = true;
            }
            else
            {
                b = false;
                bool ass = true;
                ass = main.games[7].GetComponent<GameAddSubImages>().Answer(num);
                if (!ass)
                {
                    rect.parent.GetComponent<GridLayoutGroup>().enabled = false;
                    rect.position = begin;
                    rect.SetSiblingIndex(id);
                    rect.parent.GetComponent<GridLayoutGroup>().enabled = true;
                }
                else
                {
                    rect.position = main.games[7].GetComponent<GameAddSubImages>().Q.transform.position;
                }


            }

        }
        
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        //transform.parent.GetComponent<GridLayoutGroup>().enabled = false;
        
       
        
    }

     public void OnEndDrag(PointerEventData eventData)
    {
        //transform.parent.GetComponent<GridLayoutGroup>().enabled = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.name == "Board") b = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        b = false;

    }

    
}
