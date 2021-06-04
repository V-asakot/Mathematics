using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameShow: MonoBehaviour
{

    public Text count;
    public int num = 1;
    public Sprite[] imgs;
    public GameMain main;
    public Image icon;
    public void Page(bool right) {
        if (right)
        {
            if (num == 9) num = 1; else num++;

        }else
        {
            if (num == 1) num = 9; else num--;

        }
        count.text = num.ToString();
        icon.sprite = imgs[num-1];
        main.PlaySound(num+2);

    }
}
    
