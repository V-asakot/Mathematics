using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Resources.Scripts.Interfaces;
public class GameMultiplay : MonoBehaviour,IGame{
    public Text count;
    public GameObject container;
    public GameObject sampleNumber;
    public GameMain main;
    public int ans;
    public Text time;
    public Text errors;
    public bool[] nums = new bool[9];
    public GameObject cifri;
    public Queue<int> rounds;

    public int currentNum = 0;
    public int currentLenght = 0;
    public int NumsCount=0;
    public bool[] currentVariants;
    public int currentTab=2;
    public Text Tab;
    public GameObject q;

    public void ChangeTab(bool right) {
        if (right) {
            if (currentTab >= 9) currentTab = 1; else currentTab++;
        } else {
            if (currentTab <= 1) currentTab = 9; else currentTab--;
        }
        Tab.text = "";
        for (int i=1;i<10;i++)
        {
            Tab.text += currentTab + "  x  " + i + "  =  "+(currentTab*i)+"\n";
            Tab.text += "\n";
        }

    }

    void Update() {
        if (main.gameId == 1&&main.hasStarted)
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
    public void setNums() {
        if (cifri.transform.GetChild(0).GetComponent<Toggle>().isOn)
        {
            for (int i = 0; i < 9; i++)
            {
                nums[i] = true;

            }
        }
        else {
            bool has = false;
            for (int i = 1; i < 10; i++)
            {
                if (cifri.transform.GetChild(i).GetComponent<Toggle>().isOn) { nums[i - 1] = true; has = true; } else nums[i - 1] = false;

            }
            if (!has) {
                nums[Random.Range(0, 9)]=true;
            }
        }


    }
    public void SetTongle() {
        cifri.transform.GetChild(0).GetComponent<Toggle>().isOn = false;    

    }
    public void Reload(){
        rounds = new Queue<int>();
        NumsCount = 0;
        currentNum = 0;

        Iteration();
    }
    public void Iteration() {

        if (main.difficult == 0) currentNum = Random.Range(1,6);else
        if (main.difficult == 1) currentNum = Random.Range(3, 6);else
        if (main.difficult == 2) currentNum = Random.Range(6, 10);
        GenerateCount(currentNum);

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
    public void CheckCurrentVariants() {
        bool m = false;
        for (int i = 0; i < 9; i++) {
            if (currentVariants[i] == false) m = true;
        }
        if (!m) {
            for (int i = 0; i < 9; i++)
            {
                currentVariants[i] = false;
            }
        }

    }
    public void GenerateCount(int a)
    {
        int left = 1;
        int right = 10;
        /*CheckCurrentVariants();
        int b = Random.Range(left, right);
        while (currentVariants[b - 1]) {
            b = Random.Range(left, right);
        }
        */
        int b = 0;
        if (main.difficult == 0) b = Random.Range(1, 6);
        else
        if (main.difficult == 1) b = Random.Range(6, 10);
        else
        if (main.difficult == 2) b = Random.Range(6, 10);

        //currentVariants[b - 1] = true;
        ans = a * b;
        count.text = a+" x "+b+" =";
        
        int[] variants = new int[6];
        bool[] values = new bool[90];
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
