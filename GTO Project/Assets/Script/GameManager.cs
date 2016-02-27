using System;
using UnityEngine;
using System.Collections;
using Assets;

public class GameManager : MonoBehaviour
{

    public Camera MainCamera;
    private int width = 15; //can be public 
    private int height = 22; //can be public 
    private int cellsize = 4; //can be public 
    public GameManager instance;
    public GameObject gridBackground;
    public GameObject GOGoodPlayer;
    public GameObject GOEvilPlayer;
    private Player GoodPlayer;
    private Player EvilPlayer;




    // Use this for initialization
    void Start () {
        instance = this;
        GoodPlayer = GOGoodPlayer.GetComponent<Player>();
        EvilPlayer = GOEvilPlayer.GetComponent<Player>();
        float camX = ((width * cellsize) /4) ;
        float camy = ((height * cellsize) /4);
        MainCamera.transform.localPosition = new Vector3(camX, camy, -25);
        GenGrid();
    }
	
	// Update is called once per frame
	void Update () {
	
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
                }
                else
                {
                    GridBlock(i, j, EvilPlayer);
                }

            }
        }
    }

    void GridBlock(int i, int j, Player player)
    {
        Grid newGrid = gridBackground.GetComponent<Grid>();
        newGrid.ID = Convert.ToInt32((i + "" + j));
        newGrid.Player = player;


        if (player.Name == "Good")
        {
            newGrid.GetComponent<SpriteRenderer>().color = GameColor.Good;
        }
        else if (player.Name == "Evil")
        {
            newGrid.GetComponent<SpriteRenderer>().color = GameColor.Evil;
        }
        else
        {
            newGrid.GetComponent<SpriteRenderer>().color = GameColor.Error;
        }

        newGrid.transform.localScale = new Vector3(cellsize, cellsize, cellsize);

        Instantiate(newGrid, new Vector3((i * cellsize) / 2, (j * cellsize) / 2, 0), Quaternion.identity);
    }
}
