using System;
using UnityEngine;
using System.Collections;
using Assets;
using System.Collections.Generic;


public class Tile : MonoBehaviour
{

	public Vector2 Id; //0 = coll, 1 row
    public Player Player; //owner
    public bool IsShadow;

    // Use this for initialization
    void Start()
    {
        //GetNeighbor();

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SwitchOwner(Player newPlayer, bool isShadow)
    {
        //switches the owner of the grid
        //if shadow is true the the bock is not from the player but you can't build on it
		SetPlayer(newPlayer);
		IsShadow = isShadow;

    }

    public void SetPlayer(Player player)
    {

		if (player.Name == "Good")
		{
			this.GetComponent<SpriteRenderer>().color = GameColor.Good;
		}
		else if (player.Name == "Evil")
		{
			this.GetComponent<SpriteRenderer>().color = GameColor.Evil;
		}
		else
		{
			this.GetComponent<SpriteRenderer>().color = GameColor.Error;
		}

		this.Player = player;
    }

	public Player getPlayer()
	{
		return Player;
	}

	public void OnMouseUp(){
		OnPClick ();
	}

	public void OnPClick(){
		this.GetComponent<SpriteRenderer>().color = GameColor.Error;
	}


    
}