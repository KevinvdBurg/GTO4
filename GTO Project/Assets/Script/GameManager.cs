using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEditor;
using Object = UnityEngine.Object;

public class GameManager : MonoBehaviour
{

    public Camera MainCamera;
    public int width = 15;
    public int height = 22;
    private static int cellsize = 4; //can be public 
    public static GameManager instance;

    public GameObject tilePrefab;

    public GameObject GOGoodPlayer;
    public GameObject GOEvilPlayer;
    private Player GoodPlayer;
    private Player EvilPlayer;
    public List<Player> PlayerList;
    public GameObject theGrid;
    public List<GameObject> Tiles; 

    // Use this for initialization
    void Start () {
        instance = this;
       
        GoodPlayer = GOGoodPlayer.GetComponent<Player>();
        EvilPlayer = GOEvilPlayer.GetComponent<Player>();
        PlayerList.Add(GoodPlayer);
        PlayerList.Add(EvilPlayer);

        float camX = ((width * cellsize) /4) ;
        float camy = ((height * cellsize) /4);
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
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (j >= (height/2))
                {
                    GridBlock(i, j, GoodPlayer);
                    if (!GoodPlayer.onStartPos)
                    {
                        //GOGoodPlayer.transform.position = new Vector3((i * cellsize) / 2, (j * cellsize) / 2, -2);
                        GoodPlayer.onStartPos = true;
						GoodPlayer.setLocation (new Vector3((i * cellsize) / 2, (j * cellsize) / 2, -2));
                    }

                }
                else
                {
                    GridBlock(i, j, EvilPlayer);
                    if (!EvilPlayer.onStartPos)
                    {
                        //GOEvilPlayer.transform.position = new Vector3((i * cellsize) / 2, (j * cellsize) / 2, -2);
                        EvilPlayer.onStartPos = true;
						EvilPlayer.setLocation (new Vector3((i * cellsize) / 2, (j * cellsize) / 2, -2));
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
		newTile.ID = new Vector2 (j, i);
        newTile.player = player;
        newTile.isShadow = false;
        
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

        newTile.transform.localScale = new Vector3(cellsize, cellsize, cellsize);

        GameObject gp = (GameObject)Instantiate(tilePrefab, new Vector3((i * cellsize) / 2, (j * cellsize) / 2, 0), Quaternion.identity);
        Tiles.Add(gp);
        gp.transform.parent = theGrid.transform;

    }

    public Player getPlayer(string name)
    {
        if (GoodPlayer.name == name)
            return GoodPlayer;
        else if (EvilPlayer.name == name)
            return EvilPlayer;
        return null;
    }

	public int getCellsize(){
		return cellsize;
	}

	public List<GameObject> getTiles(){
		return Tiles;
	}
		

    



}
