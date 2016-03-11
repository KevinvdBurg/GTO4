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
		if (isShadow) {
			SetShadow (isShadow);
		}

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
		

	public void OnMouseUp(){
		OnPClick ();
	}

	public void OnPClick(){
		this.GetComponent<SpriteRenderer>().color = GameColor.Error;
	}

	public void SetShadow(bool isShadow){
		IsShadow = isShadow;

		if (Player.Name == "Good")
		{
			if(isShadow)
				this.GetComponent<SpriteRenderer>().color = GameColor.GoodShadow;
		}
		else if (Player.Name == "Evil")
		{
			if(isShadow)
				this.GetComponent<SpriteRenderer>().color = GameColor.EvilShadow;
		}
		else
		{
			this.GetComponent<SpriteRenderer>().color = GameColor.Error;
		}
	}

	public bool GetShadow(){
		return IsShadow;
	}


    
}