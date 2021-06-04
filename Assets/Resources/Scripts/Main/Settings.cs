using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class SettingsFile {
   
    public bool vip;
    public bool sound = true;
    public bool music = true;
    public bool english = true;
    public int[] time = new int[8]{ 9999,9999,9999,9999, 9999 , 9999, 9999, 9999 };
    public int[] errors = new int[8] { 9999, 9999, 9999, 9999, 9999, 9999, 9999, 9999 };
}
public class Settings : MonoBehaviour {
    public string filep;
  
    private SettingsFile _settings;
    public GameMain main;
    public Localization local;
    private int backBuy;
    public PurchaseManager purch;
    public AudioSource source;
    public bool change=false;
    public AudioClip[] clips;
    public int currentClip;
    public void Start() {
        Debug.Log("Start");
        filep += Application.persistentDataPath;
        LoadSettings();
        if (_settings.music) source.Play(); else source.Stop();
        
         if (!_settings.vip) {
             try
             {
                 if (purch.CheckBuyState("vip"))
                 {

                    _settings.vip = true;
                     SaveSettings();
                 }

             }
             catch {
                Debug.Log("Check error");
             }  

         }

        main.ads.noAds = _settings.vip;
        main.ads.Load();
        if (_settings.vip) main.lenght = 13;
        local.Load();
       
    }

    public SettingsFile GetSettings()
    {
        return _settings;
    }

    public void Update() {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Back();
        }
        if (_settings.music && !source.isPlaying)
        {
            currentClip++;
            if (currentClip >= clips.Length) currentClip = 0;
            source.clip = clips[currentClip];
            source.Play();
        }
    }
    public void Back() {
        if (main.endAnimation) return;
        if (main.hasStarted) main.EndGame(false);
        else if (main.difficultMenu.active) { main.difficultMenu.SetActive(false); main.mainMenu.SetActive(true); }
        else if (main.resultsMenu.active) { main.resultsMenu.SetActive(false); main.mainMenu.SetActive(true); }
        else if (main.buy.active){
            
           if (main.aftergame) {
                if (backBuy == 3) { main.difficultMenu.SetActive(true); main.buy.SetActive(false); }
                if (backBuy == 0) main.resultsMenu.SetActive(true);
                else if (backBuy == 1) main.best.SetActive(true);
                else if (backBuy == 2) main.sett.SetActive(true);
                main.buy.SetActive(false);
                backBuy = -1;
            }
        }
        else if (main.records.active){main.records.SetActive(false); main.mainMenu.SetActive(true); }
        else if (main.best.active) { main.best.SetActive(false); main.records.SetActive(true); }
        else if (main.game1.active) { main.game1.SetActive(false); main.mainMenu.SetActive(true); }
        else if (main.game2.active) { main.game2.SetActive(false); main.mainMenu.SetActive(true); }
        else if (main.games[1].transform.GetChild(1).gameObject.active) { main.games[1].transform.GetChild(1).gameObject.SetActive(false); main.mainMenu.SetActive(true); }
        else if (main.sett.active) { main.sett.SetActive(false); main.mainMenu.SetActive(true); }
        else Exit();
    }
    public void SetBuyBack(int a)
    {
        backBuy = a;
    }
    public void Open() {
        change = false;
        main.sett.SetActive(true);
        main.toggle.isOn = _settings.sound;
        main.toggle_.isOn = _settings.music;
        change = true;
    }
    public void SetSound(bool b) {
        if (!change) return;
        _settings.sound = !_settings.sound;
        SaveSettings();
    }
    public void SetMusic(bool b)
    {
        if (!change) return;
        _settings.music = !_settings.music;
        if (_settings.music) source.Play();else source.Stop();
        SaveSettings();
    }
    public void SetVip(bool b)
    {
        _settings.vip = b;
        SaveSettings();
        main.ads.noAds = b;
        main.ads.HideBanner(b);
       
    }
    
    public void Exit()
    {
        Debug.Log("exit");
        SaveSettings();
        Application.Quit();

    }

    public void LoadSettings()
    {
        try
        {
            if (!Directory.Exists(filep))
            {
                Directory.CreateDirectory(filep);

            }
            string f = filep + "/Settings.set";
            if (!File.Exists(f))
            {
                SaveSettings();

            }
           
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(f, FileMode.Open);
            _settings = (SettingsFile)bf.Deserialize(file);
            
            file.Close();
        }
        catch (IOException ex)
        {

            File.Delete(filep + "/Settings.set");
            SaveSettings();
        }
    }
    public void SaveSettings()
    {
        try
        {

           
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filep + "/Settings.set", FileMode.OpenOrCreate);
            bf.Serialize(file, _settings);
            file.Close();
            
        }
        catch (IOException ex)
        {


            File.Delete(filep + "/Settings.set");
           

        }


    }
    public void Record(int t, int e,int id) {
        if (e < _settings.errors[id]) _settings.errors[id] = e;
        if (t < _settings.time[id]) _settings.time[id] = t;
        SaveSettings();
    }
}
