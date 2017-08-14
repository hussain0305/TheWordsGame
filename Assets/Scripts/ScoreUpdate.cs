using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreUpdate : Photon.MonoBehaviour
{

    Text MsgBoard;
    Text ourPoints;
    GameManager GM;
    string temp;
    // Use this for initialization
    void Start()
    {
        MsgBoard = GameObject.Find("MultiMsgBoard").GetComponent<Text>();
        ourPoints = GameObject.Find("Points").GetComponent<Text>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            // Do nothing -- the character motor/input/etc... is moving us
        }
        else
        {
            MsgBoard.text = temp;
        }
        if (MsgBoard == null || ourPoints == null || GM == null)
        {
            MsgBoard = GameObject.Find("MultiMsgBoard").GetComponent<Text>();
            ourPoints = GameObject.Find("Points").GetComponent<Text>();
            GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // This is OUR player. We need to send our actual position to the network.
            stream.SendNext(ourPoints.text);
            stream.SendNext(GM.duration);
            stream.SendNext(GM.game);
        }
        else
        {
            // This is someone else's player. We need to receive their position (as of a few
            // millisecond ago, and update our version of that player.
            temp = (string)stream.ReceiveNext();
            GM.oppDuration = (float)stream.ReceiveNext();
            GM.oppGameOver = !(bool)stream.ReceiveNext();
        }

    }
}
