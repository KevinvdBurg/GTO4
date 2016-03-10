using System;
using UnityEngine;
using System.Collections;
using Assets;
using System.Collections.Generic;


public class Tile : MonoBehaviour
{

	public Vector2 ID; //0 = coll, 1 row 
    public Player player; //owner
    public bool isShadow;

	public Vector2 up;
	public Vector2 down;
	public Vector2 left;
	public Vector2 right;

	public string upOwner;
	public string downOwner;
	public string leftOwner;
	public string rightOwner;


    // Use this for initialization
    void Start()
    {
        getNeighbor();

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

    void setPlayer(Player player)
    {
		this.player = player;   
    }

	public Player getPlayer()
	{
		return player;
	}

	void getNeighbor()
	{
		up = new Vector2 ( ID.x , (ID.y + 1));
		down = new Vector2 ( ID.x, (ID.y - 1) );
		left = new Vector2 ( (ID.x - 1), ID.y );
		right = new Vector2 ( (ID.x + 1), ID.y ) ;
	}

	public void OnMouseUp(){
		onPClick ();
	}

	public void onPClick(){
		this.GetComponent<SpriteRenderer>().color = GameColor.Error;
	}


    
}