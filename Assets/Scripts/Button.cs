using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject GameMan;
	// Use this for initialization
	void Start () {
        GameMan = GameObject.Find("GameManager");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnPointerDown(PointerEventData eventData)
    {
        string temp = gameObject.GetComponent<Image>().sprite.name;
        GameMan.GetComponent<GameManager>().LetterSelect(temp[temp.Length-1]);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
    }
}
