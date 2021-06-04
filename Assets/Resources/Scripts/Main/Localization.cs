using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class Localization : MonoBehaviour {

    public string[] tags;
    public TextAsset languageFile;
    public string lang;
    public GameMain main;
    public List<Text> texts = new List<Text>();
    public CanvasScaler scaler;
    public Sprite[] gameIcons_EN;
    public Sprite[] gameIcons_RU;
    public StartIcon[] icons;
    public string Lang
    {
        get
        {
            return lang;
        }

        set
        {
            PlayerPrefs.SetString("SLanguageL", value);
            lang = value;
        }
    }

    public string GetLang()
    {
        return lang;
    }

    public void SetLang(string lan)
    {
       PlayerPrefs.SetString("SLanguageL", lan);
    }

    private Dictionary<string, Dictionary<string, string>> languages;

    private XmlDocument xmlDoc = new XmlDocument();
    private XmlReader reader;
    // Use this for initialization
    public void Load()
    {
        
        Resolution();
        
        CheckLeng();
        languages = new Dictionary<string, Dictionary<string, string>>();
        reader = XmlReader.Create(new StringReader(languageFile.text));
        xmlDoc.Load(reader);

        for (int i = 0; i < tags.Length; i++)
        {
            languages.Add(tags[i], new Dictionary<string, string>());
            XmlNodeList langs = xmlDoc["Data"].GetElementsByTagName(tags[i]);
            for (int j = 0; j < langs.Count; j++)
            {
                languages[tags[i]].Add(langs[j].Attributes["Key"].Value, langs[j].Attributes["Word"].Value);
            }
        }
        ChangeImages();
        ChangeText();


    }
    public void ChangeImages()
    {
       if(Lang == "EN")main.gameIcons = gameIcons_EN;else main.gameIcons = gameIcons_RU;
        for (int i=0;i<icons.Length;i++) {
            icons[i].gameObject.GetComponent<Image>().sprite = main.gameIcons[icons[i].n];
        }

    }
    public void Resolution()
    {
        if (Mathf.Abs((Screen.height / Screen.width) - 2) < 0.1f) scaler.matchWidthOrHeight = 0.9f;
    }
    public void CheckLeng()
    {
        if (!PlayerPrefs.HasKey("SLanguageL"))
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Russian: Lang = "RU"; break;
                case SystemLanguage.English: Lang = "EN"; break;
                default: Lang = "EN"; break;
            }

        }
        else {
            Lang = PlayerPrefs.GetString("SLanguageL");
        }

    }
    public void ChangeLang() { 
        if(Lang == "RU") Lang = "EN";else Lang = "RU";
        ChangeImages();
        ChangeText();
    }
    public string GetWord(string lan, string key)
    {
        return languages[lan][key];
    }

    public string GetWord(string key)
    {
        string s = languages[lang][key];
        if (s == null) return ""; else return s;
    }
    void ChangeText()
    {
        foreach (Text text in texts)
        {
            ChangeList(text);
        };


    }
    void ChangeList(Text text)
    {
        string word = GetWord(text.tag);
        text.text = word;
    }

}
