using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaPlacer : MonoBehaviour {

    public Button[] Buttons;
    float refreshCycle;
    float cycleDuration;
    int spritePos;
    GameManager GM;
    Sprite[] sprites;

    // Use this for initialization
    void Start () {
        Buttons = GameObject.FindObjectsOfType<Button>();
        sprites = Resources.LoadAll<Sprite>("g5101");
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        refreshWords();
        refreshCycle = 4;
        cycleDuration = 0;
    }
	
	// Update is called once per frame
	void Update () {
        cycleDuration += Time.deltaTime;
        
        if (cycleDuration > refreshCycle)
        {
            cycleDuration = 0;
            refreshWords();
            GM.AvailableSelections.text = "Available Selections (4)";
        }
    }
    public void refreshWords()
    {
        int[] Letters = new int[8];
        int letCount = 0;
        GM.tries = 0;
        for (int x = 0; x < Buttons.Length; x++)
        {
            int flag = 0;
            spritePos = UnityEngine.Random.Range(12,38);
            for(int y = 0; y < letCount; y++)
            {
                if (Letters[y] == spritePos) { 
                    flag++;
                    x--;
                }
            }
            if (flag == 0) {
                Letters[letCount] = spritePos;
                letCount++;
                Buttons[x].GetComponent<Image>().sprite = sprites[spritePos];
            }
        }
    }
}
