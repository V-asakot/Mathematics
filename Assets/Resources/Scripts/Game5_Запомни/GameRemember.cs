using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Resources.Scripts.Interfaces;
public class GameRemember : MonoBehaviour,IGame
{
    
    public GameMain main;
    public int ans;
    public Text time;
    public Text errors;
    public int waitTime;
    public CardRemember[] cards;
    public int lineLenght=3;
    public int lineCurrent = 0;
    public Sprite[] cardColors;
    public GameObject clock;
    void Update()
    {
        if (main.gameId == 4 && main.hasStarted)
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
        Iteration();
    }

    public void Iteration() {
        ans = 1;
        lineCurrent = 0;
        lineLenght = 3;
        for (int i = 0; i < 12; i++)
        {
            cards[i].gameObject.SetActive(false);
        }
        StartCoroutine(RoundIEnumerator());
    }

    public IEnumerator RoundIEnumerator() {
        int[] mass = new int[lineLenght];
        for (int i=0; i< lineLenght; i++) {
            mass[i] = i + 1;
        }
        System.Random rand = new System.Random();
        for (int i = mass.Length - 1; i >= 0; i--)
        {
            int j = rand.Next(i);
            int tmp = mass[i];
            mass[i] = mass[j];
            mass[j] = tmp;
        }
        for (int i = 0; i < lineLenght; i++)
        {
           cards[i].Set(main,mass[i],cardColors[Random.Range(0,3)]);
           cards[i].gameObject.SetActive(true);
           cards[i].Open(true);
        }
        clock.SetActive(true);
        float w = waitTime;
        if (lineCurrent >= 7) w += 1;
        yield return new WaitForSeconds(w);
        for (int i = 0; i < lineLenght; i++)
        {
            cards[i].Open(false);
        }
        clock.SetActive(false);
    }
    public bool Answer(int n) {
        if (n == ans) {
            lineCurrent++;

            if (lineCurrent == lineLenght)
            {
                lineCurrent = 0;
                lineLenght++;
                ans = 1;
                main.Answer(true);
                if (main.hasStarted) StartCoroutine(RoundIEnumerator());
            }
            else { main.Answer(true); ans++; } 
            return true;
        } else {
            main.Answer(false);
            return false;
        }

    }

}
