using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    [Header("game Config")]
    public GameObject[] playersPrefabs;
    public Vector2 BoardDimentions;
    public  int diceMax;

    [Header("game components")]
    public GameObject characterPrefap;
    public GameObject spawnPoint;

    [Header("HUD components")]
    public Text whoseTurnLabel;

    
    //----------------------------------
    private GameObject[] players;
    private bool isMovementFinished = true;
    private int WhoseTurn = 0;
    private int playersNum;
    //----------------------------------
	// Use this for initialization
	void Start () {
        //init 
        Movement.gameController = this;
        playersNum = playersPrefabs.Length;
        players = new GameObject[playersNum];
        WhoseTurn = 0;
        isMovementFinished = true;

        //spawnPlayers
        for (int i = 0; i < playersNum; i++)
        {
            players[i] = Instantiate(playersPrefabs[i]);
            players[i].transform.position = spawnPoint.transform.position;
            players[i].name = "player" + i;
            players[i].AddComponent<Movement>().boardDimentions = BoardDimentions;
        }

        //finalise
        spawnPoint.SetActive(false);
	}
	
//---------------------------------------------------------------------------
	public void ThroughtDice () {
        if (isMovementFinished)
        {
            int diceNum = Random.Range(1, diceMax + 1);
            Debug.Log("diceNum " + diceNum);
            moveBy(diceNum);
        }
	}
    public void MovementFinished()
    {
        isMovementFinished = true;
        calculateNextTurn();
    }

    //---------------------------------------------------
    void moveBy(int steps)
    {
        if (isMovementFinished)
        {
            isMovementFinished = false;
            players[WhoseTurn].GetComponent<Movement>().MoveForwardBy(steps);
        }    
    }

    private void calculateNextTurn()
    {
        WhoseTurn = (++WhoseTurn%(playersNum));
        Debug.Log("WhoseTurn" + WhoseTurn);
        whoseTurnLabel.text = (WhoseTurn+1).ToString();
    }



}
