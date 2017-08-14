using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Text AvailableSelections;
    public List<string> Words;
    public bool game;
    public bool oppGameOver;
    public int tries;
    public int FinalP;
    public int OppFinalP;
    public float duration;
    public float oppDuration;
    int score;
    int multiplier;
    int tMul;
    int opptMul;
    int t;
    bool onceflag;
    bool nameUpdated;
    string[] freq;
    Text Result;
    Text Points;
    Text TR;
    Text FinalPoints;
    Text FinalPointsOpp;
    Text TimeMultiplier;
    CasePlacer[] Cases;
    Sprite[] sprites;
    Animator anim;
    GameObject alphas;
    GameObject Exc;
    AudioSource wrongAns;
    AudioSource backgroudMusic;

    // Use this for initialization
    void Start () {
        nameUpdated = false;
        onceflag = true;
        FinalP = 0;
        oppGameOver = false;
        t = 0;
        alphas = GameObject.Find("AlphaList");
        anim = GameObject.Find("Canvas").GetComponent<Animator>();
        score = 0;
        duration = 0;
        tries = 0;
        game = true;
        multiplier = 0;
        freq =new string[]{ "VKXQJZ", "BPGWYFMLUC", "DHRSNIOATE"};
        Cases = GameObject.FindObjectsOfType<CasePlacer>();
        sprites = Resources.LoadAll<Sprite>("g5101");
        TR = GameObject.Find("TimeRemaining").GetComponent<Text>();
        Points = GameObject.Find("Points").GetComponent<Text>();
        FinalPoints = GameObject.Find("FinalPoints").GetComponent<Text>();
        TimeMultiplier = GameObject.Find("TimeMultiplier").GetComponent<Text>();
        AvailableSelections = GameObject.Find("AvailableSelections").GetComponent<Text>();
        Exc = GameObject.Find("Exclamation");
        Words = GameObject.Find("WordPlacer").GetComponent<WordPlacer>().selectedWords;
        wrongAns = GameObject.Find("GameManager").GetComponent<AudioSource>();
        backgroudMusic = GameObject.Find("MenuScript").GetComponent<AudioSource>();
        filterAlphabets();
        Exc.SetActive(false);
        AvailableSelections.text = "Available Selections (4)";

        if(SceneManager.GetActiveScene().name == "WordChallenge")
        {
            GameControl.control.OfflineMatches++;
            GameControl.control.WriteStats();
        }
        else if (SceneManager.GetActiveScene().name == "MultiWordChallenge")
        {
            GameControl.control.OnlineMatches++;
            GameControl.control.WriteStats();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (game) { 
            duration += Time.deltaTime;
            TR.text = "" + (100-Math.Ceiling(duration));
        }
        if( t==0 && duration > 75)
        {
            t = 1;
            anim.SetTrigger("TimeUp");
        }        
		if(Cases.Length == 0)
            Cases = GameObject.FindObjectsOfType<CasePlacer>();
        if (Words.Count == 0)
        {
            Words = GameObject.Find("WordPlacer").GetComponent<WordPlacer>().selectedWords;
        }
        if (duration > 100 && game) {
            GameIsOver();
        }
        if (SceneManager.GetActiveScene().name == "MultiWordChallenge")
        {
            if(PhotonNetwork.otherPlayers.Length != 0 && !nameUpdated)
            {
                GameObject.Find("OppScore").GetComponent<Text>().text = PhotonNetwork.otherPlayers[0].NickName;
                nameUpdated = true;
            }
            if(FinalPointsOpp == null)
            {
                FinalPointsOpp = GameObject.Find("FinalPointsOpp").GetComponent<Text>();
            }
            if(Result == null)
            {
                Result = GameObject.Find("Result").GetComponent<Text>();
            }
            if (oppGameOver && onceflag)
            {
                onceflag = false;
                if (oppDuration > 99)
                {
                    opptMul = 1;
                }
                else
                {
                    opptMul = 100 - (int)Math.Ceiling(oppDuration);
                }
                OppFinalP = opptMul * int.Parse(GameObject.Find("MultiMsgBoard").GetComponent<Text>().text);
                FinalPointsOpp.text = "" + OppFinalP;
                ShowResult();
            }
        }
    }
    public void LetterSelect(char cur_char)
    {
        bool tempFlag = false;
        int occurence = 0;
        tries++;
        if (tries > 4)
        {
            Exc.SetActive(true);
            StartCoroutine("TriesOver");
        }
        //At this point, I know which character the player has selected
        //Process this
        else
        {
            AvailableSelections.text = "Available Selections (" + (4-tries) + ")";
            for (int x = 0; x < Cases.Length; x++)
            {
                string curSprite = Cases[x].GetComponentInChildren<SpriteRenderer>().sprite.name;
                if (curSprite[curSprite.Length - 1] == cur_char && curSprite[0] != 'G')
                {
                    tempFlag = true;
                    occurence++;
                    Cases[x].GetComponentInChildren<SpriteRenderer>().sprite = sprites[(int)cur_char - 65 + 38];
                    GameObject tracker = GameObject.Find("Green" + cur_char);
                    tracker.GetComponent<SpriteRenderer>().sprite = sprites[(int)cur_char - 65 + 12];
                }
            }
            if (freq[0].Contains(cur_char))
                multiplier = 5;
            else if (freq[1].Contains(cur_char))
                multiplier = 3;
            else if (freq[2].Contains(cur_char))
                multiplier = 1;
            score += multiplier * occurence;
            Points.text = "" + score;
            checkGameOver();
        }
        if (!tempFlag)
        {
            StartCoroutine("WrongAnswer");
        }
    }
    public void filterAlphabets()
    {
        for(int x = 65; x < 91; x++)
        {
            char t1 = (char)x;
            char t2 = (char)(x + 32); 
            int flag = -1;
            for(int y = 0; y < Words.Count; y++)
            {
                if (Words[y].Contains(t1) || Words[y].Contains(t2))
                {
                    flag = 1;
                }
            }
            if (flag == -1)
            {
                Destroy(GameObject.Find("Green" + t1));
            }
        }
    }
    public void checkGameOver()
    {
        GameObject alphaList = GameObject.Find("AlphaList");
        SpriteRenderer[] spr = alphaList.GetComponentsInChildren<SpriteRenderer>();
        int flag = -1;
        for(int x = 0; x < spr.Length; x++)
        {
            if(spr[x].sprite.name[0] == 'G')
            {
                flag = 1;
            }
        }
        if (flag == -1)
        {
            GameIsOver();
        }
    }
    public void GameIsOver()
    {
        game = false;
        if (duration > 99)
        {
            tMul = 3;
        }
        else
        {
            tMul = 100 - (int)Math.Ceiling(duration);
        }
        TimeMultiplier.text = "" + tMul;
        FinalP = ((int) Math.Floor(tMul/3f)) * score;
        FinalPoints.text = "" + FinalP;
        if(SceneManager.GetActiveScene().name == "WordChallenge")
        {
            if(FinalP > GameControl.control.HighestOffilneScore)
            {
                GameControl.control.HighestOffilneScore = FinalP;
                GameControl.control.WriteStats();
            }
            if(duration < GameControl.control.QuickestOfflineGame)
            {
                GameControl.control.QuickestOfflineGame = (int)Math.Ceiling(duration);
                GameControl.control.WriteStats();
            }
        }
        if (SceneManager.GetActiveScene().name == "MultiWordChallenge")
        {
            if (FinalP > GameControl.control.HighestOnlineScore)
            {
                GameControl.control.HighestOnlineScore = FinalP;
                GameControl.control.WriteStats();
            }
            if (duration < GameControl.control.QuickestOnlineGame)
            {
                GameControl.control.QuickestOnlineGame = (int)Math.Ceiling(duration);
                GameControl.control.WriteStats();
            }
        }
        anim.StopPlayback();
        anim.SetTrigger("GameO");
        alphas.SetActive(false);
        Destroy(GameObject.Find("WordHolder"));
        CasePlacer[] cases = GameObject.FindObjectsOfType<CasePlacer>();
        for (int x = 0; x < cases.Length; x++)
        {
            Destroy(cases[x].gameObject);
        }
        GameObject tbuttons = GameObject.Find("Buttons");
        tbuttons.SetActive(false);
    }
    public void LoadMenu()
    {
        //ButtonSound.Play();
        StartCoroutine("MainMenu");
    }
    public void LoadLevel()
    {
        //ButtonSound.Play();
        StartCoroutine("RestartLevel");
    }
    public void ShowResult()
    {
        if(OppFinalP > FinalP)
        {
            Result.text = "You Lose!!";
            Result.color = Color.red;
            GameControl.control.Losses++;
            GameControl.control.WriteStats();
        }
        else
        {
            Result.text = "You Win!!";
            Result.color = Color.green;
            GameControl.control.Wins++;
            GameControl.control.WriteStats();
        }
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(GameObject.Find("WordHolder"));
        SceneManager.LoadScene("WordSelection");
    }
    IEnumerator MainMenu()
    {
        yield return new WaitForSeconds(0.1f);
        if (SceneManager.GetActiveScene().name == "MultiWordChallenge")
            PhotonNetwork.Disconnect();
        SceneManager.LoadScene("MainMenu");
    }
    IEnumerator TriesOver()
    {
        yield return new WaitForSeconds(0.4f);
        Exc.SetActive(false);
    }
    IEnumerator WrongAnswer()
    {
        backgroudMusic.volume = 0.3f;
        wrongAns.Play();
        yield return new WaitForSeconds(0.4f);
        backgroudMusic.volume = 1;
    }

}
