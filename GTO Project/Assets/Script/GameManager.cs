using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEditor;
using Object = UnityEngine.Object;

public class GameManager : MonoBehaviour
{

    private const int Width = 15;
    private const int Height = 22;
    private const int Cellsize = 4;

    public Camera MainCamera;

    public static GameManager instance;

    public GameObject tilePrefab;

    public GameObject GoGoodPlayer;
    public GameObject GoEvilPlayer;
    private Player _goodPlayer;
    private Player _evilPlayer;
    public List<Player> PlayerList;
    public GameObject TheGrid;
    public List<GameObject> Tiles; 

    // Use this for initialization
    void Start () {
        instance = this;
       
        _goodPlayer = GoGoodPlayer.GetComponent<Player>();
        _evilPlayer = GoEvilPlayer.GetComponent<Player>();
        PlayerList.Add(_goodPlayer);
        PlayerList.Add(_evilPlayer);

        float camX = ((Width * Cellsize) /4) ;
        float camy = ((Height * Cellsize) /4);
        MainCamera.transform.localPosition = new Vector3(camX, camy, -25);
        GenGrid();
    }
	
	// Update is called once per frame
	void Update () {
        /*if (Input.GetMouseButtonDown(0))
        {
            if (!one_click) // first click no previous clicks
            {
                Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                one_click = true;

                timer_for_double_click = Time.time; // save the current time
                target = Input.mousePosition;
                target = Camera.main.ScreenToWorldPoint(target);
                target.z = 0f;
                speed = 10;
            }
            else
            {
                one_click = false; // found a double click, now reset
                speed = 20;
            }
        }
        if (one_click)
        {
            // if the time now is delay seconds more than when the first click started. 
           
            if ((Time.time - timer_for_double_click) > delay)
            {

                //basically if thats true its been too long and we want to reset so the next click is simply a single click and not a double click.

                one_click = false;

            }
        }
	    float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
        //transform.position = Vector3.Lerp (transform.position, target, speed * Time.deltaTime);
        if (target == transform.position)
        {
            speed = 0f;
        }*/


    }

    void GenGrid()
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                if (j >= (Height/2))
                {
                    GridBlock(i, j, _goodPlayer);
                    if (!_goodPlayer.OnStartPos)
                    {
                        //GOGoodPlayer.transform.position = new Vector3((i * cellsize) / 2, (j * cellsize) / 2, -2);
                        _goodPlayer.OnStartPos = true;
						_goodPlayer.setLocation (new Vector3((i * Cellsize) / 2, (j * Cellsize) / 2, -2));
                    }

                }
                else
                {
                    GridBlock(i, j, _evilPlayer);
                    if (!_evilPlayer.OnStartPos)
                    {
                        //GOEvilPlayer.transform.position = new Vector3((i * cellsize) / 2, (j * cellsize) / 2, -2);
                        _evilPlayer.OnStartPos = true;
						_evilPlayer.setLocation (new Vector3((i * Cellsize) / 2, (j * Cellsize) / 2, -2));
                    }
                        
                }
            }
        }

//		foreach (GameObject item in Tiles) {
//			item.GetComponent<Tile> ().getNeighborOwners ();
//		}
    }

    void GridBlock(int i, int j, Player player)
    {
        Tile newTile = tilePrefab.GetComponent<Tile>();
        //newTile.ID = new int[2] {i,j};
		newTile.Id = new Vector2 (j, i);
        newTile.Player = player;
        newTile.IsShadow = false;
        
        if (player.Name == "Good")
        {
            newTile.GetComponent<SpriteRenderer>().color = GameColor.Good;
        }
        else if (player.Name == "Evil")
        {
            newTile.GetComponent<SpriteRenderer>().color = GameColor.Evil;
        }
        else
        {
            newTile.GetComponent<SpriteRenderer>().color = GameColor.Error;
        }

        newTile.transform.localScale = new Vector3(Cellsize, Cellsize, Cellsize);

        GameObject gp = (GameObject)Instantiate(tilePrefab, new Vector3((i * Cellsize) / 2, (j * Cellsize) / 2, 0), Quaternion.identity);
        Tiles.Add(gp);
        gp.transform.SetParent(TheGrid.transform);

    }

    public Player GetPlayer(string name)
    {
        if (_goodPlayer.name == name)
            return _goodPlayer;
        else if (_evilPlayer.name == name)
            return _evilPlayer;
        return null;
    }

	public int GetCellsize(){
		return Cellsize;
	}

	public List<GameObject> GetTiles(){
		return Tiles;
	}
		

    



}
