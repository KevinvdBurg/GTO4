using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TurnManager : MonoBehaviour {

    //Wie is er aan de buurt
    //Houd Actie Punten / Bouw Punten / Geld bij ( Zowel verhogen als verlagen)
    //Roteer camara na buurt

    public UIManager ui;
    private List<Player> Players = new List<Player>(2);

    // Use this for initialization
    void Start ()
	{
	    StartGame();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown("f"))
	    {
	        EndTurn(getPlayerTurn(true));
	    }
	}

    public void StartGame()
    {
        StartTurn(GameManager.instance.PlayerList[Random.Range(0, GameManager.instance.PlayerList.Count)]);
    }

    void UpdateUI(Player updateUiPlayer)
    {
        ui.MP.text = updateUiPlayer.MovementPoints + "";
        ui.BP.text = updateUiPlayer.BuildingPoints + "";
        ui.GP.text = updateUiPlayer.Money + "";
    }

    void StartTurn(Player playerThatStartedTurn)
    {
        playerThatStartedTurn.HisTurn = true;
        ui.TurnIndicator.text = playerThatStartedTurn.Name;
        UpdateUI(playerThatStartedTurn);
    }
    
    void EndTurn(Player playerThatEndedTurn)
    {
        playerThatEndedTurn.Money += 100;
        playerThatEndedTurn.BuildingPoints += 5;
        playerThatEndedTurn.MovementPoints += 5;
        playerThatEndedTurn.HisTurn = false;

        StartTurn(getPlayerTurn(false));

    }

    Player getPlayerTurn(bool turn)
    {
        Player TurnPlayer = null;
        foreach (Player player in Players)
        {
            if (player.HisTurn == turn)
                TurnPlayer =  player;
        }

        return TurnPlayer;
    }

    

}
