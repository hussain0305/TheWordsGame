using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ChooseWordsButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Color normal;
    Color Highlighted;
    WordHolder WH;
    Text ButtonText;
	// Use this for initialization
	void Start () {
        ButtonText = this.gameObject.GetComponentInChildren<Text>();
        WH = GameObject.Find("WordHolder").GetComponent<WordHolder>();
        normal = Color.black;
        Highlighted = Color.green;
        this.gameObject.GetComponentInChildren<Text>().color = normal;
    }
	
	// Update is called once per frame
	void Update () {
        if (WH == null)
        {
            WH = GameObject.Find("WordHolder").GetComponent<WordHolder>();
        }
	}
    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (ButtonText.color == Highlighted)
        {
            ButtonText.color = normal;
            WH.WordRemoved(ButtonText.text);
        }
        else if (ButtonText.color == normal && WH.selections < 4)
        {
            ButtonText.color = Highlighted;
            WH.WordAdded(ButtonText.text);
        }
        
    }
}
