using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenuScript : MonoBehaviour {

    static MenuScript instance;
    AudioSource[] sounds;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

    }

    void Start () {
        sounds = GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if (SceneManager.GetActiveScene().name == "MainMenu")
                    Application.Quit();
                if (GameObject.Find("WordHolder") != null)
                    Destroy(GameObject.Find("WordHolder"));
                if (PhotonNetwork.connected)
                    PhotonNetwork.Disconnect();
                if (GameObject.Find("MenuScript") != null)
                    Destroy(GameObject.Find("MenuScript"));
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
    public void LoadGame()
    {
        StartCoroutine("LoadRequestedScene", "MultiWordSelection");
    }
    public void LoadOffline()
    {
        StartCoroutine("LoadRequestedScene", "WordSelection");
    }
    public void LoadSettings()
    {
        StartCoroutine("LoadRequestedScene", "Tutorial");
    }
    public void LoadLeaderboards()
    {
        StartCoroutine("LoadRequestedScene", "Stats");
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadRequestedScene(string scn)
    {
        sounds[2].Play();
        yield return new WaitForSecondsRealtime(0.4f);
        Debug.Log("before");
        SceneManager.LoadScene(scn);
        Debug.Log("after");
    }
    
}
