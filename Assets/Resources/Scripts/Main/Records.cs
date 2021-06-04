using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Records : MonoBehaviour {

    public int id;
    public Settings settings;
    public Text time;
    public Text errors;
    public Image GameIcon;
    public void Open(int id) {
        gameObject.SetActive(true);
        int t = settings.GetSettings().time[id];
        int m = t / 60;
        int e = settings.GetSettings().errors[id];
        int s = t - (m * 60);
        GameIcon.sprite = settings.main.gameIcons[id];
        if (t < 9999 || e < 9999)
        {
            time.text = settings.local.GetWord("Time")+" ";
            if (m >= 10) time.text += m; else time.text += "0" + m;
            time.text += " : ";
            if (s >= 10) time.text += s; else time.text += "0" + s;
            errors.text = settings.local.GetWord("Errors") + " " + e;
        }
        else {
            time.text = settings.local.GetWord("YouHave");
            errors.text = settings.local.GetWord("YouHave");
        }
    }
}
