using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateStats : MonoBehaviour {

    public GameObject Username;
    public GameObject OnlineMatches;
    public GameObject OfflineMatches;
    public GameObject Wins;
    public GameObject Losses;
    public GameObject HighestOnlineScore;
    public GameObject HighestOffilneScore;
    public GameObject QuickestOnlineGame;
    public GameObject QuickestOfflineGame;
    
    // Use this for initialization
    void Start () {
        Username.GetComponent<Text>().text = "" + GameControl.control.Username;
        OnlineMatches.GetComponent<Text>().text = "" + GameControl.control.OnlineMatches;
        OfflineMatches.GetComponent<Text>().text = "" + GameControl.control.OfflineMatches;
        Wins.GetComponent<Text>().text = "" + GameControl.control.Wins;
        Losses.GetComponent<Text>().text = "" + GameControl.control.Losses;
        HighestOnlineScore.GetComponent<Text>().text = "" + GameControl.control.HighestOnlineScore;
        HighestOffilneScore.GetComponent<Text>().text = "" + GameControl.control.HighestOffilneScore;
        QuickestOnlineGame.GetComponent<Text>().text = "" + GameControl.control.QuickestOnlineGame;
        QuickestOfflineGame.GetComponent<Text>().text = "" + GameControl.control.QuickestOfflineGame;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
