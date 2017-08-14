using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutScript : MonoBehaviour {

    public TutMarker[] TutorialScreen;
    int currentScreen;
    // Use this for initialization
	void Start () {
        for(int x = 1; x < TutorialScreen.Length; x++)
        {
            TutorialScreen[x].gameObject.SetActive(false);
        }
        currentScreen = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void NextTut()
    {
        TutorialScreen[currentScreen].gameObject.SetActive(false);
        currentScreen = (currentScreen + 1) % 12;
        TutorialScreen[currentScreen].gameObject.SetActive(true);
    }
}
