using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour
{

    // Use this for initialization
    public static GameControl control;

    public string Username = "executioner.";
    public int OnlineMatches = 0;
    public int OfflineMatches = 0;
    public int Wins = 0;
    public int Losses = 0;
    public int HighestOnlineScore = 0;
    public int QuickestOnlineGame = 101;
    public int QuickestOfflineGame = 101;
    public int HighestOffilneScore = 0;

    void Awake()
    {

        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ReadStats();
        if (Username == "executioner.")
        {
            askUsername();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void WriteStats()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Stats.dat");

        Stats stat = new Stats();
        stat.Username = Username;
        stat.OnlineMatches = OnlineMatches;
        stat.OfflineMatches = OfflineMatches;
        stat.Wins = Wins;
        stat.Losses = Losses;
        stat.HighestOffilneScore = HighestOffilneScore;
        stat.HighestOnlineScore = HighestOnlineScore;
        stat.QuickestOfflineGame = QuickestOfflineGame;
        stat.QuickestOnlineGame = QuickestOnlineGame;
        bf.Serialize(file, stat);
        file.Close();
    }
    public void ReadStats()
    {
        if (File.Exists(Application.persistentDataPath + "/Stats.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Stats.dat", FileMode.Open);
            Stats stat = (Stats)bf.Deserialize(file);
            file.Close();

            Username = stat.Username;
            OnlineMatches = stat.OnlineMatches;
            OfflineMatches = stat.OfflineMatches;
            Wins = stat.Wins;
            Losses = stat.Losses;
            HighestOffilneScore = stat.HighestOffilneScore;
            HighestOnlineScore = stat.HighestOnlineScore;
            QuickestOfflineGame = stat.QuickestOfflineGame;
            QuickestOnlineGame = stat.QuickestOnlineGame;
        }
    }
    public void askUsername()
    {
        SceneManager.LoadScene("Username");
    }
}

[Serializable]
class Stats
{
    public string Username;
    public int OnlineMatches;
    public int OfflineMatches;
    public int Wins;
    public int Losses;
    public int HighestOnlineScore;
    public int QuickestOnlineGame;
    public int QuickestOfflineGame;
    public int HighestOffilneScore;
}
