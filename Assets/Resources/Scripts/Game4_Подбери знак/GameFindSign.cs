using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Resources.Scripts.Interfaces;

public class GameFindSign : MonoBehaviour,IGame {
   
    public GameObject container;
    public GameObject sampleNumber;
    public GameMain main;
    public int answer;
    public Text time;
    public Text errors;
    public Image leftImage;
    public Image rightImage;
    public Sprite[] spritePair1;
    public Sprite[] spritePair2;
    public Sprite[] spritePair3;
    public Sprite[] spritePair4;
    public Sprite[] spritePair5;
    public Sprite[] spritePair6;
    public Sprite[] spritePair7;
    public Sprite[] spritePair8;
    public Sprite[] spritePair9;
    public Sprite[] spritePair10;
    public GameObject q;
    void Update() {
        if (main.gameId == 3&&main.hasStarted)
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
    public void Reload(){
        Iteration();
    }
    public void Iteration() {
        GenerateCount();
    }
    public void GenerateNums(int[] nums)
    {
        int[] colors = {1,2,0,0,1,2};
        for (int i= container.transform.childCount-1; i>=0;i--) {
            Destroy(container.transform.GetChild(i).gameObject);
        }
        for (int i = 0;i<3;i++)
        {
            GameObject card = Instantiate(sampleNumber, container.transform);
            string st = "";
            if (nums[i] == -1) st = "<";
            else if (nums[i] == 1) st = ">";
            else st = "=";

            card.GetComponent<DrugNum>().SetNum(nums[i],st,main,i,colors[i]);
        }

    }
    public void GenerateCount()
    {
        
        int leftNumber = 0;
        int rightNumber = 0;
        if (main.difficult == 0)
        {   
            leftNumber = Random.Range(1, 3);
            Debug.Log("   ");
            rightNumber = Random.Range(1, 3);
        }
        else if (main.difficult == 1)
        {  
            leftNumber = Random.Range(1, 6);
            Debug.Log("   ");
            rightNumber = Random.Range(1, 6);
        }
        else if (main.difficult == 2) {
            
            leftNumber = Random.Range(5, 11);
            Debug.Log("   ");
            rightNumber = Random.Range(5, 11);
        }
        if (leftNumber > rightNumber) answer = 1; else if (leftNumber < rightNumber) answer = -1; else answer = 0;
        setSprite(leftNumber, leftImage);
        setSprite(rightNumber, rightImage);
        int[] variants = { -1,0,1};

        GenerateNums(variants);
    }
    public int setSprite(int number ,Image image) {
        int a =0;
        if (number == 1) { a = Random.Range(0, spritePair1.Length); image.sprite = spritePair1[a];}else
        if (number == 2) { a = Random.Range(0, spritePair2.Length); image.sprite = spritePair2[a];}else
        if (number == 3) { a = Random.Range(0, spritePair3.Length); image.sprite = spritePair3[a]; }else
        if (number == 4) { a = Random.Range(0, spritePair4.Length); image.sprite = spritePair4[a]; }else
        if (number == 5) { a = Random.Range(0, spritePair5.Length); image.sprite = spritePair5[a]; }else
        if (number == 6) { a = Random.Range(0, spritePair6.Length); image.sprite = spritePair6[a]; }else
        if (number == 7) { a = Random.Range(0, spritePair7.Length); image.sprite = spritePair7[a]; }else
        if (number == 8) { a = Random.Range(0, spritePair8.Length); image.sprite = spritePair8[a]; }else
        if (number == 9) { a = Random.Range(0, spritePair9.Length); image.sprite = spritePair9[a]; }else
        if (number == 10) { a = Random.Range(0, spritePair10.Length); image.sprite = spritePair10[a]; }
        return a;


    }

    public void setSprite(int number, Image image,int type)
    {
      
        if (number == 1) { image.sprite = spritePair1[type]; }
        else
        if (number == 2) { image.sprite = spritePair2[type]; }
        else
        if (number == 3) { image.sprite = spritePair3[type]; }
        else
        if (number == 4) { image.sprite = spritePair4[type]; }
        else
        if (number == 5) { image.sprite = spritePair5[type]; }
        else
        if (number == 6) { image.sprite = spritePair6[type]; }
        else
        if (number == 7) { image.sprite = spritePair7[type]; }
        else
        if (number == 8) { image.sprite = spritePair8[type]; }
        else
        if (number == 9) { image.sprite = spritePair9[type]; }
        else
        if (number == 10) { image.sprite = spritePair10[type]; }
 


    }
    public bool Answer(int n) {
        if (n == answer) { main.Answer(true); return true; } else { main.Answer(false);return false;  }
    }

    
}
