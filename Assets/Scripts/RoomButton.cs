using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RoomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    //private AudioSource ButtonSound;
    // Use this for initialization
    void Start()
    {
        //ButtonSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //ButtonSound.Play();
        StartCoroutine("LoadNewLevel");
    }
    public void OnPointerUp(PointerEventData eventData)
    {
    }
    IEnumerator LoadNewLevel()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("MultiWordSelection");
    }
}
