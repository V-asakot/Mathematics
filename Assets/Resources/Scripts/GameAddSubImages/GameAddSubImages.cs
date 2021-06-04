using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAddSubImages: MonoBehaviour
{

    public GameMain main;
    public Text symbol;
    public Image leftImage;
    public Image rightImage;
    public Text errors;
    public Text time;
    public GameObject container;
    public GameObject sampleNumber;
    public GameObject Q;
    public int answer;
    void Update()
    {
        if (main.gameId == 7 && main.hasStarted)
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
    public void GenerateNums(int[] nums)
    {
        int[] colors = { 1, 2, 0, 0, 1, 2 };
        for (int i = container.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(container.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < 3; i++)
        {
            GameObject g = Instantiate(sampleNumber, container.transform);
            g.GetComponent<DrugNum>().SetNum(nums[i], main, i, colors[i]);
        }

    }
    public void Next() {
        answer = 0;
        int leftNumber = 0;
        int rightNumber = 0;
        
        int c = Random.Range(1,3);
        if (c == 1) {
            symbol.text = "+";
            answer = Random.Range(2, 10);
            leftNumber = Random.Range(1, answer);
            rightNumber = answer - leftNumber;
            
        }
        else
        {
           symbol.text = "-";
            answer = Random.Range(1, 9);
            do {
                rightNumber = Random.Range(answer, 10);
            } while (rightNumber==answer);
            leftNumber = rightNumber - answer;
            
        }
        int type = main.games[3].GetComponent<GameFindSign>().setSprite(rightNumber,leftImage);
        main.games[3].GetComponent<GameFindSign>().setSprite(leftNumber, rightImage, type);
        int[] variants = new int[3];
        bool[] values = new bool[10];
        values[answer] = true;
        int place = Random.Range(0, 3);
        for (int i = 0; i < 3;i++)
        {
            variants[i] = answer+(i-place);
            if (variants[i] < 0) variants[i] = answer + (i + place);
        }
        GenerateNums(variants);

    }
    public bool Answer(int n)
    {
        if (n == answer) { main.Answer(true); return true; } else { main.Answer(false); return false; }
    }


}
    
