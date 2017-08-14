using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LobbySetup : MonoBehaviour
{
    private int RoomCount;
    private Text RoomMsg;
    public GameObject JB;

    void Start()
    {
        JB = GameObject.Find("Join");
        RoomCount = PhotonNetwork.countOfRooms;
        RoomMsg = GameObject.Find("RoomInfo").GetComponent<Text>();

        Connect();
    }

    void Update()
    {
        if (JB == null)
        {
            JB = GameObject.Find("Join");
        }
        if (RoomMsg == null)
        {
            RoomMsg = GameObject.FindWithTag("RoomInfo").GetComponent<Text>();
        }

        RoomCount = PhotonNetwork.countOfRooms;
        RoomMsg.text = "" + RoomCount + " Available";

        if (RoomCount == 0 && JB != null)
        {
            JB.SetActive(false);
            RoomMsg.enabled = false;
            JB.GetComponentInChildren<Text>().color = Color.white;
        }
        if (RoomCount != 0 && JB != null)
        {
            JB.SetActive(true);
            RoomMsg.enabled = true;
            JB.GetComponentInChildren<Text>().color = Color.blue;
        }
    }

    void Connect()
    {
        PhotonNetwork.ConnectUsingSettings("TheWordsGame v1");
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    void OnJoinedLobby()
    {
        PhotonNetwork.playerName = GameControl.control.Username;
    }
}
