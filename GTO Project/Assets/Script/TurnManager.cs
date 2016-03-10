using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TurnManager : MonoBehaviour {

    //Wie is er aan de buurt
    //Houd Actie Punten / Bouw Punten / Geld bij ( Zowel verhogen als verlagen)
    //Roteer camara na buurt

	public UIManager UIManager ;
    private List<Player> _players = new List<Player>(2);

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
        StartTurn(GameManager.instance.PlayerList[Random.Range(0, GameManager.instance.PlayerList.Count)]);
    }

    void UpdateUI(Player updateUiPlayer)
    {
		UIManager.MP.text = updateUiPlayer.MovementPoints + "";
		UIManager.BP.text = updateUiPlayer.BuildingPoints + "";
		UIManager.GP.text = updateUiPlayer.Money + "";
    }

    void StartTurn(Player playerThatStartedTurn)
    {
        playerThatStartedTurn.HisTurn = true;
		UIManager.TurnIndicator.text = playerThatStartedTurn.Name;
        UpdateUI(playerThatStartedTurn);
    }
    
    void EndTurn(Player playerThatEndedTurn)
    {
        playerThatEndedTurn.Money += 100;
        playerThatEndedTurn.BuildingPoints += 5;
        playerThatEndedTurn.MovementPoints += 5;
        playerThatEndedTurn.HisTurn = false;

        StartTurn(GetPlayerTurn(false));

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
