using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordHolder : MonoBehaviour {

    public string[] selectedWords;
    public int selections;
    public string joincreate;
    // Use this for initialization
	void Start () {
        selectedWords = new string[4];
        selections = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void WordAdded(string word)
    {
        selectedWords[selections] = word;
        selections++;
    }
    public void WordRemoved(string word)
    {
        selections--;
    }
}
