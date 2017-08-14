using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UsernameDone : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject username;
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
        GameControl.control.Username = username.GetComponent<Text>().text;
        GameControl.control.WriteStats();
        //ButtonSound.Play();
        StartCoroutine("LoadNewLevel");
    }
    public void OnPointerUp(PointerEventData eventData)
    {
    }
    IEnumerator LoadNewLevel()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("MainMenu");
    }
}
