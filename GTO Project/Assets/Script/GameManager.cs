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
	private List<Player> _playerList = new List<Player>();
    public GameObject TheGrid;
    public List<GameObject> Tiles; 

    // Use this for initialization
    void Start () {
        instance = this;
       
        _goodPlayer = GoGoodPlayer.GetComponent<Player>();
        _evilPlayer = GoEvilPlayer.GetComponent<Player>();
		_playerList.Add(_goodPlayer);
		_playerList.Add(_evilPlayer);

        float camX = ((Width * Cellsize) /4) ;
        float camy = ((Height * Cellsize) /4);
        MainCamera.transform.localPosition = new Vector3(camX, camy, -25);
        GenGrid();
    }
	
	// Update is called once per frame
	void Update () {
		
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
						_goodPlayer.setLocation (new Vector3((i * Cellsize) / 2, (j * Cellsize) / 2, -2));
						_goodPlayer.OnStartPos = true;
                    }

                }
                else
                {
                    GridBlock(i, j, _evilPlayer);
					if (j >= (Height / 2) - 1 && i >= Width - 1) {
						if (!_evilPlayer.OnStartPos) {
							_evilPlayer.setLocation (new Vector3 ((i * Cellsize) / 2, (j * Cellsize) / 2, -2));
							_evilPlayer.OnStartPos = true;
						}   
					}
                }
            }
        }
    }

    void GridBlock(int i, int j, Player player)
    {
        Tile newTile = tilePrefab.GetComponent<Tile>();
        //newTile.ID = new int[2] {i,j};
		newTile.Id = new Vector2 ((i * Cellsize) / 2, (j * Cellsize) / 2);
		newTile.SetPlayer(player);
		newTile.SetShadow(false);
        

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

	public List<Player> GetPlayerList(){
		return _playerList;
	}

}
