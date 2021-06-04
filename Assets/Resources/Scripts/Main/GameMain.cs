using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMain : MonoBehaviour {
    public int gameId=0;
    public int difficult=0;
    public int lenght = 6;
    public int currentTask=0;
    public int errors = 0;
    public bool hasDiff;
    public DateTime startTime;
    public TimeSpan time;
    public GameObject[] games;
    public AudioClip[] clips;
    public Sprite[] gameIcons;
    public GameObject mainMenu;
    public GameObject difficultMenu;
    public GameObject resultsMenu;
    public Image resultsGame;
    public GameObject buy;
    public GameObject records;
    public GameObject best;
    public GameObject game1;
    public GameObject game2;
    public GameObject sett;
    public Toggle toggle;
    public Toggle toggle_;
    public bool hasStarted;
    public AudioSource source;
    public Settings settings;
    public bool aftergame;
    public AdsManager ads;
    public GameObject effect;
    public bool endAnimation = false;


    public void OpenBuy(GameObject obj) {
        if (!settings.GetSettings().vip)
        {
            obj.SetActive(false);
            buy.SetActive(true);
        }

    }
    public void SetDifficult(int d) {
        if (settings.GetSettings().vip || d != 2 || gameId == 0)
        {
            aftergame = false;
            difficult = d;
            difficultMenu.SetActive(false);
            if(gameId!=1) StartGame();else games[1].transform.GetChild(1).gameObject.SetActive(true);
        }
        else {
            difficultMenu.SetActive(false);
            settings.SetBuyBack(3);
            buy.SetActive(true);
        }
    }
    public void HasDiff(bool b) {
        hasDiff = b;
    }
    public void SetGame(int GameId) {
        this.gameId = GameId;
        mainMenu.SetActive(false);
        if (hasDiff)
        {
            difficultMenu.transform.GetChild(0).GetComponent<Image>().sprite = gameIcons[GameId];
            difficultMenu.SetActive(true);
        } else if (GameId==1) {
            games[1].SetActive(true);
            difficultMenu.transform.GetChild(0).GetComponent<Image>().sprite = gameIcons[GameId];
            difficultMenu.SetActive(true);
            // games[1].transform.GetChild(0).gameObject.SetActive(true);
            games[1].transform.GetChild(1).gameObject.SetActive(false);
            games[1].transform.GetChild(2).gameObject.SetActive(false);
        }else if (GameId == 4||GameId == 7) {
            StartGame();
        }
    }
    public void StartGame(int GameId) {
        this.gameId = GameId;
        StartGame();
    }
    public void StartGame()
    {
        currentTask = 0;
        errors = 0;
        startTime = DateTime.Now;
        for (int i=0;i<games.Length;i++)
        {
            if(i!=gameId)games[i].SetActive(false);
        }
        if (gameId == 0)
        {

            games[0].SetActive(true);
            games[0].GetComponent<GameAddSub>().Reload();

        }
        else if (gameId == 1)
        {
            games[1].SetActive(true);
            games[1].transform.GetChild(1).gameObject.SetActive(false);
            games[1].transform.GetChild(2).gameObject.SetActive(true);
            games[1].GetComponent<GameMultiplay>().Reload();
        } else if (gameId == 2)
        {

            games[2].SetActive(true);
            games[2].GetComponent<GameFindNumber>().Reload();

        }
        else if (gameId == 3)
        {

            games[3].SetActive(true);
            games[3].GetComponent<GameFindSign>().Reload();

        } else if (gameId == 4) {
            games[4].SetActive(true);
            games[4].GetComponent<GameRemember>().Reload();
        } else if (gameId == 5){
            games[5].SetActive(true);
            games[5].GetComponent<GameFindPair>().Reload();
        }
        else if (gameId == 7)
        {
            games[7].SetActive(true);
            games[7].GetComponent<GameAddSubImages>().Next();
        }
        hasStarted = true;
    }

    public void EndGame()
    {
        StartCoroutine(End());
    }
    public void EndGame(bool finished)
    {
        time = DateTime.Now - startTime;
        games[gameId].SetActive(false);
        difficultMenu.SetActive(false);
        
        if (!settings.GetSettings().vip) ads.LoadInter();
        mainMenu.SetActive(true);
        hasStarted = false;
        aftergame = true;
        if (finished)
        {
            StartCoroutine(End());
        }

    }
    IEnumerator End()
    {
        this.time = DateTime.Now - startTime;
        endAnimation = true;
        effect.SetActive(true);
        if (!settings.GetSettings().vip) ads.LoadInter();
        PlaySound(2);
        yield return new WaitForSeconds(2f);
        games[gameId].SetActive(false);
        difficultMenu.SetActive(false);

        
        int m = this.time.Minutes;
        int s = this.time.Seconds;

        Text texttTime = resultsMenu.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        texttTime.text = settings.local.GetWord("Time") + " " + this.time.ToString("mm\\:ss");
        /*if (m >= 10) texttTime.text += m; else texttTime.text += "0" + m;
        texttTime.text += " : ";
        if (s >= 10) texttTime.text += s; else texttTime.text += "0" + s;*/

        resultsMenu.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = settings.local.GetWord("Errors") + " " + errors;
        resultsMenu.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = settings.local.GetWord("Done") + " " + currentTask;
        resultsMenu.SetActive(true);
        hasStarted = false;
        aftergame = true;
        int time = s + (m * 60);
        settings.Record(time, errors, gameId);
        resultsGame.sprite = gameIcons[7];
        effect.SetActive(false);
        endAnimation = false;
    }
    public void Answer(bool b) {
        if (!b)
        {
            errors++;
            PlaySound(1);
        }
        else
        { PlaySound(0);
            StartCoroutine(Round());
          
        }
    }
    public IEnumerator Round() {
        currentTask++;
        yield return new WaitForSeconds(1);
        if (gameId == 0)
        {
            if (currentTask == lenght) { EndGame(); yield break; }
            games[0].GetComponent<GameAddSub>().Iteration();


        }
        else if (gameId == 1)
        {

            if (currentTask == lenght) { EndGame(); yield break; }
            games[1].GetComponent<GameMultiplay>().Iteration();


        }
        else if (gameId == 2)
        {
            if (currentTask == lenght) { EndGame(); yield break; }
            games[2].GetComponent<GameFindNumber>().Iteration();


        }
        else if (gameId == 3)
        {
            if (currentTask == lenght) { EndGame(); yield break; }
            games[3].GetComponent<GameFindSign>().Iteration();

        }
        else if (gameId == 4)
        {
            if (lenght == 6)
            {
                if (games[4].GetComponent<GameRemember>().lineLenght > 9)
                {
                    EndGame(); yield break;
                }

            }
            else if (lenght == 13)
            {
                if (games[4].GetComponent<GameRemember>().lineLenght > 12)
                {
                    EndGame(); yield break;
                }

            }


        }
        else if (gameId == 5) {
            if (difficult == 0) { if (currentTask == 6) EndGame(); }else
            if (difficult == 1) { if (currentTask == 10) EndGame(); }else
            if (difficult == 2) { if (currentTask == 15) EndGame(); }
        }
        else if (gameId == 7)
        {
            if (currentTask == lenght) { EndGame(); yield break; }
            games[7].GetComponent<GameAddSubImages>().Next();
        }


    }
    public TimeSpan GetTime() {
        time = DateTime.Now - startTime;
        return time; 
    }
    
    public void PlaySound(int a) {
        if (!settings.GetSettings().sound) return;
        source.clip = clips[a];
        source.Play();
    }

}
