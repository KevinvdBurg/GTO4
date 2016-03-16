using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TurnManager : MonoBehaviour {

    //Wie is er aan de buurt
    //Houd Actie Punten / Bouw Punten / Geld bij ( Zowel verhogen als verlagen)
    //Roteer camara na buurt

	public UIManager UIManager ;
	private List<Player> _players;

    // Use this for initialization
    void Start ()
	{
	    StartGame();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown("f"))
	    {
	        EndTurn(GetPlayerTurn(true));
	    }
	}

    public void StartGame()
    {
		_players = GameManager.instance.GetPlayerList ();
		StartTurn(_players[Random.Range (0, _players.Count)]);
    }

    void StartTurn(Player playerThatStartedTurn)
    {
        playerThatStartedTurn.HisTurn = true;
		string playerName = playerThatStartedTurn.Name;
		UIManager.TurnIndicator.text = playerName;
		UIManager.FlipCamera (playerName);
		UIManager.UpdateUI(playerThatStartedTurn);
    }
    
    void EndTurn(Player playerThatEndedTurn)
    {
		StartTurn(GetPlayerTurn(false));
        playerThatEndedTurn.Money += 100;
        playerThatEndedTurn.BuildingPoints += 5;
        playerThatEndedTurn.MovementPoints += 5;
        playerThatEndedTurn.HisTurn = false;
    }

    Player GetPlayerTurn(bool turn)
    {
        Player TurnPlayer = null;
        foreach (Player player in _players)
        {
            if (player.HisTurn == turn)
                TurnPlayer =  player;
        }

        return TurnPlayer;
    }

    

}
