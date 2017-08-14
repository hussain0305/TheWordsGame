using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{
    GameObject myPlayerGo;
    WordHolder WH;
    void Start()
    {
        WH = GameObject.Find("WordHolder").GetComponent<WordHolder>();
        if (WH.joincreate == "Join") { 
            PhotonNetwork.JoinRandomRoom();
        }
        else if(WH.joincreate == "Create")
        {
            RoomOptions RO = new RoomOptions();
            RO.MaxPlayers = 2;
            PhotonNetwork.CreateRoom("Room" + UnityEngine.Random.Range(0,100), RO, null);
        }
    }
    void OnPhotonRandomJoinFailed()
    {
        RoomOptions RO = new RoomOptions();
        RO.MaxPlayers = 2;
        PhotonNetwork.CreateRoom("Room" + UnityEngine.Random.Range(0, 100), RO, null);
    }
    void Update()
    {
    }

    void OnJoinedRoom()
    {
        SpawnMyPlayer();
    }
    void SpawnMyPlayer()
    {
        myPlayerGo = (GameObject)PhotonNetwork.Instantiate("PlayerWG", Vector3.zero, Quaternion.identity, 0);
        DontDestroyOnLoad(myPlayerGo);
        enableSpawnedPlayerComponents();
    }
    void enableSpawnedPlayerComponents()
    {
        ((MonoBehaviour)myPlayerGo.GetComponent("ScoreUpdate")).enabled = true;
    }
}
