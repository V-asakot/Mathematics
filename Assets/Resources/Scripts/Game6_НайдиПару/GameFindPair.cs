using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Resources.Scripts.Interfaces;

public class GameFindPair : MonoBehaviour,IBaseGame
{

    public GameMain main;
    public int ans;
    public Text time;
    public Text errors;
    public float waitTime;
    public GameObject container;
    public GameObject sample;
    public Sprite[] cardColors;
    public GameObject[] cards;
    CardRemember curr;
    CardRemember prev;
    public bool process;
    void Update()
    {
        if (main.gameId == 5 && main.hasStarted)
        {
            System.TimeSpan now = main.GetTime();
            int s = now.Seconds;
            int m = now.Minutes;
            errors.text = main.settings.local.GetWord("Errors") + " " + main.errors;
            time.text = main.settings.local.GetWord("Time") + " ";
            if (m >= 10) time.text += m; else time.text += "0" + m;
            time.text += " : ";
            if (s >= 10) time.text += s; else time.text += "0" + s;
        }
    }
    public void Reload()
    {
        int lenght=0;
        float w = 0, h = 0;
        float sx = 0, sy = 0;
        if (main.difficult == 0) { lenght = 12;w = 64.3f;h = 83.7f;sx = 25;sy = 16; } else
        if (main.difficult == 1) { lenght = 20; w = 53.9f; h = 70.4f; ; sx = 16; sy = 16; }else
        if (main.difficult == 2) { lenght = 30; w = 44.5f; h = 58.1f; ; sx = 10; sy = 16; }

        int[] mass = new int[lenght];
        for (int i = 0; i < lenght/2; i++)
        {
            mass[i] = i + 1;
        }

        for (int i = lenght / 2, j = 0; i < lenght; i++,j++)
        {
            mass[i] = j + 1;
            
        }
        System.Random rand = new System.Random();
        for (int i = mass.Length - 1; i >= 0; i--)
        {
            int j = rand.Next(i);
            int tmp = mass[i];
            mass[i] = mass[j];
            mass[j] = tmp;
        }
        for (int i=0;i<30;i++)
        {
            cards[i].SetActive(false);

        }
        container.GetComponent<GridLayoutGroup>().cellSize = new Vector2(w,h);
        container.GetComponent<GridLayoutGroup>().spacing = new Vector2(sx,sy);
        for (int i = 0; i < lenght; i++)
        {
            cards[i].GetComponent<CardRemember>().Set(main,mass[i],cardColors[main.difficult]);
           cards[i].SetActive(true);

        }


    }
    public void Answer(int n,CardRemember card)
    {
        if (process) return;
        if (ans == 0) { ans = n; prev = card;prev.Open(true);}
        else if (ans == n) { ans = 0;curr = card;curr.Open(true); main.Answer(true); }
        else {
            curr = card;
            ans = 0;
            StartCoroutine(Wait());
            main.Answer(false);
        }
    }
    public IEnumerator Wait()
    {
        process = true;
        curr.Open(true);
        float w = waitTime;
       
        yield return new WaitForSeconds(w);
        prev.Open(false);
        curr.Open(false);
        process = false;
    }

}
    
