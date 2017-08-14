using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class MenuButtons : MonoBehaviour, IPointerDownHandler,IPointerUpHandler {

    MenuScript MS;
    // Use this for initialization
    void Start () {
        MS = GameObject.Find("MenuScript").GetComponent<MenuScript>();
	}
	// Update is called once per frame
	void Update () {
        if (MS == null)
        {
            MS = GameObject.Find("MenuScript").GetComponent<MenuScript>();
            Debug.Log("Found");
        }
	}
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pressed " + gameObject.name);
        if (gameObject.name == "Start Game")
            MS.LoadGame();
        else if (gameObject.name == "Offline Challenges")
            MS.LoadOffline();
        else if (gameObject.name == "Leaderboards")
            MS.LoadLeaderboards();
        else if (gameObject.name == "Settings")
            MS.LoadSettings();
        else if (gameObject.name == "Exit")
            MS.ExitGame();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }


}
