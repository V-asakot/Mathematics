using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Resources.Scripts.Interfaces;
public class GameFindNumber : MonoBehaviour,IGame {
    public Text count;
    public GameObject container;
    public GameObject sampleNumber;
    public GameMain main;
    public int ans;
    public Text time;
    public Text errors;
    public GameObject q;
    void Update() {
        if (main.gameId == 2 && main.hasStarted)
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
        for (int i = 0;i<6;i++)
        {
            GameObject g = Instantiate(sampleNumber, container.transform);
            g.GetComponent<DrugNum>().SetNum(nums[i],main,i,colors[i]);
        }

    }
    public void GenerateCount()
    {
        int left = 1;
        int right = 10;
       int a = 0;
        if (main.difficult == 0)
        {   right = 10;
            a = Random.Range(left, right);
            
        }
        else if (main.difficult == 1)
        {   right = 20;
            a = Random.Range(9, right);
            
        }
        else if (main.difficult == 2) {
            right = 100;
            a = Random.Range(20, right);
            
        }
        int op = Random.Range(1, 3);
        
        if (op == 1) {
            
            int f1 = Random.Range(left, a);
            ans = a - f1;
            count.text = f1+ " +        = " + a;
        }
        else
        {
            
            int f1 = Random.Range(a, right);
            ans = f1 - a;
            count.text = f1 + " -        = " + a;
        }
        int[] variants = new int[6];
        bool[] values = new bool[right + 1];
        values[ans] = true;  
        int place = Random.Range(0, 6);
        for (int i = 0; i < 6;i++)
        {
            variants[i] = ans + (i - place);
            if (variants[i] < 0) variants[i] = ans + (i + place);
        }
        GenerateNums(variants);
    }
    public bool Answer(int answer) {
        if (answer == ans) { main.Answer(true); return true; } else { main.Answer(false);return false;  }
    }

    
}
