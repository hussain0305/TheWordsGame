using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class WordPlacer : MonoBehaviour {
    WordHolder WH;
    Sprite[] sprites;
    string charname;
    public List<string> selectedWords;
    public GameObject Canvas;
    private GameObject cur_GO;
    GameObject P2w;
    int spritePos;
    int noOfWords;
    int mulCount;
    public int wordYPosition;
    GameManager GameState;
    // Use this for initialization
    void Start () {
        if(SceneManager.GetActiveScene().name == "MultiWordChallenge")
        {
            P2w = GameObject.Find("Player2wait");
            GameState = GameObject.Find("GameManager").GetComponent<GameManager>();
            mulCount = 0;
        }
        WH = GameObject.Find("WordHolder").GetComponent<WordHolder>();
        wordYPosition = 150;
        noOfWords = 4;
        selectedWords = new List<string>();
        for(int x = 0; x < noOfWords; x++)
        {
            selectedWords.Add(WH.selectedWords[x].Trim());
        }
        sprites = Resources.LoadAll<Sprite>("g5101");
        PlaceWords();
    }

    // Update is called once per frame
    void Update () {
        if (Time.timeScale == 1 && SceneManager.GetActiveScene().name == "MultiWordChallenge" && PhotonNetwork.playerList.Length != 2)
        {
            if(GameState.game)
                Time.timeScale = 0;
            mulCount++;
            
            if(mulCount > 1 && GameState.game)
            {
                P2w.SetActive(true);
                P2w.GetComponentInChildren<Text>().text = "Opponent Disconnected";
            }
        }
        if (Time.timeScale == 0 && SceneManager.GetActiveScene().name == "MultiWordChallenge" && PhotonNetwork.playerList.Length == 2)
        {
            Time.timeScale = 1;
            P2w.SetActive(false);
        }
    }

    void PlaceWords()
    {
        for (int z = 0; z < selectedWords.Count; z++)
        {
            int length = selectedWords[z].Length;
            int cur_x;
            char cur_char;
            if (length % 2 == 0)
            {
                cur_x = -1 * (length - 1) * 25;
                for (int x = 0; x < length; x++)
                {
                    cur_char = Char.ToUpper(selectedWords[z][x]);
                    charname = "Red" + cur_char;
                    spritePos = (int)cur_char - 65 + 64;
                    cur_GO = Instantiate(Resources.Load("Case")) as GameObject;
                    cur_GO.transform.SetParent(Canvas.transform, false);
                    cur_GO.GetComponent<RectTransform>().localPosition = new Vector3(cur_x, wordYPosition);
                    cur_GO.GetComponentInChildren<SpriteRenderer>().sprite = sprites[spritePos];
                    cur_GO.transform.localScale = new Vector3(14, 14, 0);
                    cur_x += 50;
                }
            }
            else
            {
                cur_x = -1 * ((length - 1) / 2) * 50;
                for (int x = 0; x < length; x++)
                {
                    cur_char = Char.ToUpper(selectedWords[z][x]);
                    charname = "Red" + cur_char;
                    spritePos = (int)cur_char - 65 + 64;
                    cur_GO = Instantiate(Resources.Load("Case")) as GameObject;
                    cur_GO.transform.SetParent(Canvas.transform, false);
                    cur_GO.GetComponent<RectTransform>().localPosition = new Vector3(cur_x, wordYPosition);
                    cur_GO.GetComponentInChildren<SpriteRenderer>().sprite = sprites[spritePos];
                    cur_GO.transform.localScale = new Vector3(14, 14, 0);
                    cur_x += 50;
                }
            }
            wordYPosition += 100;
        }
    }
}
