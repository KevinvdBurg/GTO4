using System;
using UnityEngine;
using System.Collections;
using Assets;


public class Tile : MonoBehaviour
{

    public int ID; 
    public Player Player; //owner
    public bool isShadow;

    // Use this for initialization
    void Start()
    {
        
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

}