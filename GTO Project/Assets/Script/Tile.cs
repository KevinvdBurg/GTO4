using System;
using UnityEngine;
using System.Collections;
using Assets;


public class Tile : MonoBehaviour
{

    public int[] ID; //0 = coll, 1 row 
    public Player Player; //owner
    public bool isShadow;

    public int[] up;
    public int[] down;
    public int[] left;
    public int[] right;


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
        
    }

    void getNeighbor()
    {
        up = new int[2] { ID[0] , (ID[1] + 1) };
        down = new int[2] { ID[0], (ID[1] - 1) };
        left = new int[2] { (ID[0] - 1), ID[1] };
        right = new int[2] { (ID[0] + 1), ID[1] };
    }

    
}