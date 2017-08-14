using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    WordHolder WH;
    Text thisText;
    // Use this for initialization
    void Start () {
        thisText = this.gameObject.GetComponentInChildren<Text>();
        WH = GameObject.Find("WordHolder").GetComponent<WordHolder>();
    }
	
	// Update is called once per frame
	void Update () {
		if(WH.selections != 4)
        {
            thisText.color = Color.white;
        }
        else
        {
            thisText.color = Color.blue;
        }
	}
    public void OnPointerDown(PointerEventData eventData)
    {
        if(thisText.color == Color.blue)
        {
            if (SceneManager.GetActiveScene().name == "WordSelection")
                SceneManager.LoadScene("WordChallenge");
            else if (SceneManager.GetActiveScene().name == "MultiWordSelection")
            {
                WH.joincreate = gameObject.name;
                SceneManager.LoadScene("MultiWordChallenge");
            }
                
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

}
