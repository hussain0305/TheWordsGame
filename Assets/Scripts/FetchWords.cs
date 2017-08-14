using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FetchWords : MonoBehaviour {

    TextAsset AllWords;
    string[] words;
    GameObject[] buttons;

    // Use this for initialization
    void Start () {
        //AllWords = System.IO.File.ReadAllText("Assets/Resources/WordFile.txt");
        AllWords = Resources.Load("WordFile") as TextAsset;

        //words = AllWords.Split('\n');
        words = AllWords.text.Split('\n');
        buttons = GameObject.FindGameObjectsWithTag("Button");
        PlaceWordsOnButtons();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void PlaceWordsOnButtons()
    {
        for (int x = 0; x < buttons.Length; x++)
        {
            buttons[x].GetComponentInChildren<Text>().text = words[UnityEngine.Random.Range(0, words.Length)];
        }
    }
}
