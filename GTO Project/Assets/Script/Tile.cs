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

	public Vector2 Up;
	public Vector2 Down;
	public Vector2 Left;
	public Vector2 Right;

	public string UpOwner;
	public string DownOwner;
	public string LeftOwner;
	public string RightOwner;


    // Use this for initialization
    void Start()
    {
        GetNeighbor();

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void SwitchOwner(Player newPlayer, bool isShadow)
    {
        //switches the owner of the grid
        //if shadow is true the the bock is not from the player but you can't build on it
    }

    void SetPlayer(Player player)
    {
		this.Player = player;
    }

	public Player getPlayer()
	{
		return Player;
	}

	void GetNeighbor()
	{
		Up = new Vector2 ( Id.x , (Id.y + 1));
		Down = new Vector2 ( Id.x, (Id.y - 1) );
		Left = new Vector2 ( (Id.x - 1), Id.y );
		Right = new Vector2 ( (Id.x + 1), Id.y ) ;
	}

	public void OnMouseUp(){
		OnPClick ();
	}

	public void OnPClick(){
		this.GetComponent<SpriteRenderer>().color = GameColor.Error;
	}


    
}