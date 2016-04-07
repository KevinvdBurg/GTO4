using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEditor;
using Object = UnityEngine.Object;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	private Settings settings;
    private int Width = 5;
	private int Height = 14;
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
	public AchievementManager AchievementManager;

	public Text WinnerText;
	public GameObject GameoverOverview;
	public Text DekuBlock;
	public Text BunnyBlock;



    // Use this for initialization
    void Start () {


		GameObject SettingsObject = GameObject.FindGameObjectWithTag ("Settings");

		if (SettingsObject != null) {
			settings = SettingsObject.GetComponent <Settings>();
			Width = settings.PlayfieldWidth;
			Height = settings.PlayfieldHeight;
		}
		if (SettingsObject == null) {
			Debug.Log ("Cannot find Settings -- Going with default Values");
		}


        instance = this;
       
        _goodPlayer = GoGoodPlayer.GetComponent<Player>();
        _evilPlayer = GoEvilPlayer.GetComponent<Player>();
		_playerList.Add(_goodPlayer);
		_playerList.Add(_evilPlayer);

        float camX = ((Width * Cellsize) / 4) ;
        float camy = ((Height * Cellsize) /4);

        MainCamera.transform.localPosition = new Vector3(camX, camy, -25);
        GenGrid();
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.T)) {
			AchievementManager.AchievementGet ("Hoi");
		}
		if (Input.GetKeyUp (KeyCode.M)) {
			AchievementManager.AchievementGet ("Milton");
		}
    }

    void GenGrid()
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                if (j >= (Height/2))
                {
					if (j == (Height - 1)) 
						GridBlock (i, j, _goodPlayer, true);
					else 
						GridBlock (i, j, _goodPlayer, false); 


                    if (!_goodPlayer.OnStartPos){
						_goodPlayer.setLocation (new Vector3((i * Cellsize) / 2, (j * Cellsize) / 2, -2));
						_goodPlayer.OnStartPos = true;
                    }

                }
                else
                {
					if(j == 0)
						GridBlock(i, j, _evilPlayer, true);
					else
						GridBlock(i, j, _evilPlayer, false);
					
					
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

	void GridBlock(int i, int j, Player player, bool isStone)
    {
        Tile newTile = tilePrefab.GetComponent<Tile>();
        //newTile.ID = new int[2] {i,j};
		newTile.Id = new Vector2 ((i * Cellsize) / 2, (j * Cellsize) / 2);
		newTile.SetPlayer(player);
		newTile.SetShadow(false);

		if (isStone) {
			newTile.SetStoneState (5);
			player.maxStone += 1;
			player.currentStone += 1;
		} else {
			newTile.SetStoneState (-1);
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

	public void CheckGameover(Player player){
		if (player.Name == _goodPlayer.Name) {
			CreateGameoverScreen(_evilPlayer);
			_goodPlayer.HisTurn = false;
			_evilPlayer.HisTurn = false;
		} else if (player.Name == _evilPlayer.Name) {
			CreateGameoverScreen(_goodPlayer);
			_goodPlayer.HisTurn = false;
			_evilPlayer.HisTurn = false;
		} else {
			Debug.Log ("Error! The player is not right");
		}
	}

	void CreateGameoverScreen(Player playerWhoWon){
		GameoverOverview.SetActive (true);
		WinnerText.text = "Winner: " + playerWhoWon.Name;
		DekuBlock.text = GetBlockAmount (_evilPlayer) + "";
		BunnyBlock.text = GetBlockAmount (_goodPlayer) + "";
	}

	public void BackToMainMenu(){
		SceneManager.LoadScene ("MainMenu");
	}

	int GetBlockAmount(Player player){
		int count = 0;

		foreach (GameObject tile in Tiles) {
			Tile t = tile.GetComponent<Tile>();
			if (t.Player.Name == player.Name) {
				count += 1;
			}
		}
		return count;
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
