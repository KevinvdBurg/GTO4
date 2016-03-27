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
	public int StoneState;

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

		StoneState = -1;
	}

	public void SwitchOwner(Player newPlayer, bool isShadow, int StoneState)
    {
        //switches the owner of the grid
        //if shadow is true the the bock is not from the player but you can't build on it
		SetPlayer(newPlayer);
		if (isShadow) {
			SetShadow (isShadow);
		}

		SetStoneState (StoneState);

    }



    public void SetPlayer(Player player)
    {

		if (player.Name == "Good" )
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
			else 
				this.GetComponent<SpriteRenderer>().color = GameColor.Good;
		}
		else if (Player.Name == "Evil")
		{
			if(isShadow)
				this.GetComponent<SpriteRenderer>().color = GameColor.EvilShadow;
			else
				this.GetComponent<SpriteRenderer>().color = GameColor.Evil;
		}
		else
		{
			this.GetComponent<SpriteRenderer>().color = GameColor.Error;
		}
	}

	public void SetStoneState(int StoneState){
		if (Player.Name == "Good")
		{
			if (StoneState == 5) {
				this.GetComponent<SpriteRenderer> ().color = GameColor.GoodStoneState5; 
				this.StoneState = 5;
			} else if (StoneState == 4) {
				this.GetComponent<SpriteRenderer> ().color = GameColor.GoodStoneState4;
				this.StoneState = 4;
			} else if (StoneState == 3) {
				this.GetComponent<SpriteRenderer> ().color = GameColor.GoodStoneState3;
				this.StoneState = 3;
			} else if (StoneState == 2) {
				this.GetComponent<SpriteRenderer> ().color = GameColor.GoodStoneState2;
				this.StoneState = 2;
			} else if (StoneState == 1) {
				this.GetComponent<SpriteRenderer> ().color = GameColor.GoodStoneState1;
				this.StoneState = 1;
			} else if (StoneState == 0) {
				this.GetComponent<SpriteRenderer> ().color = GameColor.GoodStoneState0;
				this.StoneState = 0;
			} else {
				this.StoneState = -1;
			}
				
		}
		else if (Player.Name == "Evil")
		{
			if (StoneState == 5) {
				this.GetComponent<SpriteRenderer> ().color = GameColor.EvilStoneState5; 
				this.StoneState = 5;
			} else if (StoneState == 4) {
				this.GetComponent<SpriteRenderer> ().color = GameColor.EvilStoneState4;
				this.StoneState = 4;
			} else if (StoneState == 3) {
				this.GetComponent<SpriteRenderer> ().color = GameColor.EvilStoneState3;
				this.StoneState = 3;
			} else if (StoneState == 2) {
				this.GetComponent<SpriteRenderer> ().color = GameColor.EvilStoneState2;
				this.StoneState = 2;
			} else if (StoneState == 1) {
				this.GetComponent<SpriteRenderer> ().color = GameColor.EvilStoneState1;
				this.StoneState = 1;
			} else if (StoneState == 0) {
				this.GetComponent<SpriteRenderer> ().color = GameColor.EvilStoneState0;
				this.StoneState = 0;
			} else {
				this.StoneState = -1;
			}
		}
		else
		{
			this.GetComponent<SpriteRenderer>().color = GameColor.Error;
		}
	}

	public bool GetShadow(){
		return IsShadow;
	}

	public int GetStoneState(){
		return StoneState;
	}



  public override string ToString(){
    return "Id: " + Id.ToString() + " - Owner: " + Player.Name + " - isShadow: " + IsShadow;
  }

    
}