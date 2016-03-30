﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class Player : MonoBehaviour
{
	public string Name;
    public int MovementPoints;
    public int BuildingPoints;
    public int Money;
    public bool HisTurn;
    public bool OnStartPos = false;

	public Vector3 _currentLocation;

	public Vector2 Up;
	public Vector2 Down;
	public Vector2 Left;
	public Vector2 Right;

	public List<Tile> ShadowBlocks = new List<Tile>();
	public Animator animator;

	public bool DoneChecking = true;
//	private int _currentCellSize;

	public UIManager uiManager;



    // Use this for initialization
    void Start()
    {
		//_currentCellSize = 4;
    }

    // Update is called once per frame
    void Update()
    {		
		CheckBehind ();
		if (HisTurn) {
			if (MovementPoints > 0) {
				if (Input.GetKeyUp (KeyCode.W)) {
					Vector3 newPos = GetMoveToTile ("up", Name);
					WalkTo (newPos, 1);
				}
				if (Input.GetKeyUp (KeyCode.D)) {
					Vector3 newPos = GetMoveToTile ("right", Name);
					WalkTo (newPos, 2);
				}
				if (Input.GetKeyUp (KeyCode.S)) {
					Vector3 newPos = GetMoveToTile ("down", Name);
					WalkTo (newPos, 3);
				}
				if (Input.GetKeyUp (KeyCode.A)) {
					Vector3 newPos = GetMoveToTile ("left", Name);
					WalkTo (newPos, 4);
				}
			}
			if (BuildingPoints > 0) {
				if (Input.GetKeyUp (KeyCode.Space)) {
					PlaceBlock ();

				}
			}

			if (Money > 0) {
				if (Input.GetKeyUp (KeyCode.Q)) {
					BuyMovementPoints ();
				}
				if (Input.GetKeyUp (KeyCode.E)) {
					BuyBuildingPoints ();
				}
			}
		}
    }
    

    public override string ToString()
    {
        return "Name: " + Name + ", MovementPoints: " + MovementPoints + ", BuildingPoints: " + BuildingPoints +
               ", Money: " + Money + ", HisTurn: " + HisTurn;
    }


	public void setLocation(Vector3 newLocation)
	{
		_currentLocation = newLocation;
		this.transform.position = newLocation;
	}

	public void setLocation(Vector3 newLocation, bool isPlacingBlock)
	{
		StartCoroutine(JumpFromTo(_currentLocation, newLocation, 0.2f));
		_currentLocation = newLocation;

	}

	public void setLocation(Vector3 newLocation, int animationIndex)
	{
		StartCoroutine(MoveFromTo(_currentLocation, newLocation, 0.4f, animationIndex));
		//this.transform.position = Vector3.Lerp(_currentLocation, newLocation, Mathf.PingPong(Time.deltaTime, 1.0f));
		_currentLocation = newLocation;
		//this.transform.position = newLocation;
	}

	public Tile CheckBehind()
	{
		var back = transform.TransformDirection(Vector3.back);
		//note the use of var as the type. This is because in c# you 
		// can have lamda functions which open up the use of untyped variables
		//these variables can only live INSIDE a function. 
		RaycastHit hit;

		if (Physics.Raycast (transform.position, -back, out hit, 4)) {
			//Debug.Log (hit.collider.gameObject.GetComponent<Tile> ().Id.ToString ());
			Tile TileID = hit.collider.gameObject.GetComponent<Tile> ();
			if (TileID.Player.Name != this.Name) {
				Vector3 newPos = GetMoveToTile ("up", Name);
				WalkTo (newPos, 1);
			}
			return TileID;
		} else {
			return null;
		}
	}

	public Tile GetNeighborOwner(Vector2 moveTo)
	{
		List<GameObject> Tl = GameManager.instance.GetTiles();

		//Vector2 search = moveTo;
		//GameObject result = Tl.Single(s => s.GetComponent<Tile> ().Id.Equals(search));

		foreach (GameObject i in Tl) {
			Tile it = i.GetComponent<Tile> ();

			if (it.Id == moveTo)
			{
			  return it;
			}
			
		}
		return null;

	}

	public Tile GetTile(Vector2 tileLocation)
	{
		List<GameObject> Tl = GameManager.instance.GetTiles();

		foreach (GameObject i in Tl) {
			Tile it = i.GetComponent<Tile> ();
			if (it.Id == tileLocation) {
				return it; 
			}
		}
		return null;
	}

	Vector3 GetMoveToTile (string move, string playerName)
	{
		if (playerName == "Good") {
			Vector3 moveToVector = new Vector2 ();
			moveToVector.z = -2;
			if (move == "up") {
				moveToVector.x = _currentLocation.x;
				moveToVector.y = _currentLocation.y + 2;
				return moveToVector;
			} else if (move == "down") {
				moveToVector.x = _currentLocation.x;
				moveToVector.y = _currentLocation.y - 2;
				return moveToVector;
			} else if (move == "left") {
				moveToVector.x = _currentLocation.x - 2;
				moveToVector.y = _currentLocation.y;
				return moveToVector;
			} else if (move == "right") {
				moveToVector.x = _currentLocation.x + 2;
				moveToVector.y = _currentLocation.y;
				return moveToVector;
			} else {
				moveToVector.x = _currentLocation.x;
				moveToVector.y = _currentLocation.y;
				return moveToVector;
			}
		} else {
			Vector3 moveToVector = new Vector2 ();
			moveToVector.z = -2;
			if (move == "down") {
				moveToVector.x = _currentLocation.x;
				moveToVector.y = _currentLocation.y + 2;
				return moveToVector;
			} else if (move == "up") {
				moveToVector.x = _currentLocation.x;
				moveToVector.y = _currentLocation.y - 2;
				return moveToVector;
			} else if (move == "right") {
				moveToVector.x = _currentLocation.x - 2;
				moveToVector.y = _currentLocation.y;
				return moveToVector;
			} else if (move == "left") {
				moveToVector.x = _currentLocation.x + 2;
				moveToVector.y = _currentLocation.y;
				return moveToVector;
			} else {
				moveToVector.x = _currentLocation.x;
				moveToVector.y = _currentLocation.y;
				return moveToVector;
			}
		}
	}

	Vector3 GetMoveToTile (string move, string playerName, Vector3 location)
	{
		if (playerName == "Good") {
			Vector3 moveToVector = new Vector2 ();
			moveToVector.z = -2;
			if (move == "up") {
				moveToVector.x = location.x;
				moveToVector.y = location.y + 2;
				return moveToVector;
			} else if (move == "down") {
				moveToVector.x = location.x;
				moveToVector.y = location.y - 2;
				return moveToVector;
			} else if (move == "left") {
				moveToVector.x = location.x - 2;
				moveToVector.y = location.y;
				return moveToVector;
			} else if (move == "right") {
				moveToVector.x = location.x + 2;
				moveToVector.y = location.y;
				return moveToVector;
			} else {
				moveToVector.x = location.x;
				moveToVector.y = location.y;
				return moveToVector;
			}
		} else {
			Vector3 moveToVector = new Vector2 ();
			moveToVector.z = -2;
			if (move == "down") {
				moveToVector.x = location.x;
				moveToVector.y = location.y + 2;
				return moveToVector;
			} else if (move == "up") {
				moveToVector.x = location.x;
				moveToVector.y = location.y - 2;
				return moveToVector;
			} else if (move == "right") {
				moveToVector.x = location.x - 2;
				moveToVector.y = location.y;
				return moveToVector;
			} else if (move == "left") {
				moveToVector.x = location.x + 2;
				moveToVector.y = location.y;
				return moveToVector;
			} else {
				moveToVector.x = location.x;
				moveToVector.y = location.y;
				return moveToVector;
			}
		}
	}



	void PlaceBlock(){

			Vector3 blockPlace = GetMoveToTile ("down", this.Name);
			Tile NeighborOwner = GetNeighborOwner (blockPlace);

			if (this.Name == "Good") {
				if (NeighborOwner.Player.Name == "Evil" || NeighborOwner.IsShadow) {
					Block (blockPlace, NeighborOwner);
				} 
			} else if (this.Name == "Evil") {
				if (NeighborOwner.Player.Name == "Good" || NeighborOwner.IsShadow) {
					Block (blockPlace, NeighborOwner);
				}
			} 
	}

	void Block(Vector3 blockPlace, Tile NeighborOwner){
 		if (GetTile(_currentLocation).StoneState >= 0) {
			Vector3 bPlace = blockPlace;
			if (NeighborOwner.Player.Name != this.Name) {
				bool isOwner = false;
				while (!isOwner) {
					Vector3 movePlace = GetMoveToTile ("down", this.Name, bPlace);
					Debug.Log (GetTile (movePlace).Player.Name);
					isOwner = true;
				}
				SwitchTile (GetTile(bPlace), true);


			} else if (NeighborOwner.IsShadow) {
				bool isShadow = true;
				while (isShadow) {
					Vector3 movePlace = GetMoveToTile ("down", this.Name, bPlace);
					Tile neigbor = GetNeighborOwner (movePlace);
					bPlace = movePlace;
					if (!neigbor.IsShadow)
						isShadow = false;
				}
				SwitchTile (GetTile(bPlace), true);

			}
			CheckShadows();
		} else {
			Vector3 bPlace = blockPlace;
 			if (NeighborOwner.IsShadow) {
				bool isShadow = true;
				while (isShadow) {
					Vector3 movePlace = GetMoveToTile ("down", this.Name, bPlace);
					Tile neigbor = GetNeighborOwner (movePlace);
					bPlace = movePlace;
					if (!neigbor.IsShadow)
						isShadow = false;

				}
				TileSwitcher (bPlace);

			} else if (NeighborOwner.StoneState > 0) {
				SpendBuildingPoints ();
				NeighborOwner.SetStoneState (NeighborOwner.GetStoneState () - 1);
			} else if (NeighborOwner.StoneState == 0) {
				SpendBuildingPoints ();
			}
			else {
				TileSwitcher (bPlace);
			}
		}	
	}

	void TileSwitcher(Vector3 bPlace){
		Tile OtherTile = GetTile (bPlace);
		Tile ThisTile = GetTile (_currentLocation);
		if (OtherTile.StoneState >= 0) {
			OtherTile.SetStoneState (OtherTile.GetStoneState () - 1);
		} else {
			SwitchTile (OtherTile, true);
		}

		SwitchTile (ThisTile, true);
		setLocation (GetMoveToTile ("up", this.Name), true);
		CheckShadows();
		SpendBuildingPoints ();
	}

	void SwitchTile(Tile switchTile, bool setShadow){
		if (setShadow) {
			switchTile.SwitchOwner (this, true);
			AddShadow (switchTile);
		} else {
			switchTile.SwitchOwner (this, false);
		}


	}

	void WalkTo(Vector3 to, int animationIndex){
		if (GetTile (to) != null) {
			bool newPosShadow = GetTile (to).IsShadow;
			if (!newPosShadow) {
				if (this.Name == GetNeighborOwner (to).Player.Name) {
					setLocation (to, animationIndex);
					SpendMovementPoints ();
				}
			}
		}


	}

	public void AddShadow(Tile tile){
		ShadowBlocks.Add (tile);
	}



	public void CheckShadows(){
		//Sorts the list
		if (ShadowBlocks == null)
			return;

		List<Tile> xSort = ShadowBlocks.OrderBy(x => x.Id[1]).ThenBy(x => x.Id[0]).ToList();
		List<Tile> ySort = ShadowBlocks.OrderBy(x => x.Id[0]).ThenBy(x => x.Id[1]).ToList();


		List<List<Tile>> allSeq = new List<List<Tile>>(); //a List with all the seqlists
		List<Tile> tempSeq = new List<Tile>(); // a temp list this will be only used while looping
		Tile lastInsertedTile = null; // the last inserted tile in a seq

		Tile lastTile = xSort[xSort.Count - 1]; //Gets the last tile in the xSort


		//hotizantal
		foreach (Tile tile in xSort) {
			
			if (lastInsertedTile == null) {
				tempSeq.Add (tile);
			} else {
				//Checks if the tiles are in the same row
				if (lastInsertedTile.Id[1] == tile.Id[1]) {
					//check if the tiles are next to each other
					if (lastInsertedTile.Id[0] == tile.Id[0] + 2 || lastInsertedTile.Id[0] == tile.Id[0] - 2 ) {
						//adds the tile to the temp list
						tempSeq.Add (tile);
						//if it is the last tile it will be counted as the final seq
						if (tile == lastTile) {
							allSeq.Add (tempSeq);
						}
					} else {
						//adds the tempseq to the overall list with seq
						allSeq.Add (tempSeq);
						tempSeq = new List<Tile> (); // creates a new and clean list
						tempSeq.Add (tile);
					}
				} 
				else {
					allSeq.Add (tempSeq);
					tempSeq = new List<Tile> ();
					tempSeq.Add (tile);
				}
			}
				
			lastInsertedTile = tile; // sets the last inserted tile
		}

//		//vertical
		foreach (Tile tile in ySort) {

			if (lastInsertedTile == null) {
				tempSeq.Add (tile);
			} else {
				//Checks if the tiles are in the same row
				if (lastInsertedTile.Id[0] == tile.Id[0]) {
					//check if the tiles are next to each other
					if (lastInsertedTile.Id[1] == tile.Id[1] + 2 || lastInsertedTile.Id[1] == tile.Id[1] - 2 ) {
						//adds the tile to the temp list
						tempSeq.Add (tile);
						//if it is the last tile it will be counted as the final seq
						if (tile == lastTile) {
							allSeq.Add (tempSeq);
						}
					} else {
						//adds the tempseq to the overall list with seq
						allSeq.Add (tempSeq);
						tempSeq = new List<Tile> (); // creates a new and clean list
						tempSeq.Add (tile);
					}
				} 
				else {
					allSeq.Add (tempSeq);
					tempSeq = new List<Tile> ();
					tempSeq.Add (tile);
				}
			}

			lastInsertedTile = tile; // sets the last inserted tile
		}
			
		for(int i = 0; i < allSeq.Count; i++)
		{
			List<Tile> aSeq = allSeq[i];
			if (aSeq.Count >= 4) {
				foreach (Tile item in aSeq) {
					item.SetShadow (false);
					ShadowBlocks.Remove (item);
				}
			}

		}
	}
		
	IEnumerator  MoveFromTo (Vector3 pointA, Vector3 pointB, float time, int animationIndex){
		animator.SetInteger ("WalkingDication", animationIndex);

		float t = 0f;
		while (t < 1.0f) {
			t += Time.deltaTime / time; // Sweeps from 0 to 1 in time seconds
			transform.position = Vector3.Lerp(pointA, pointB, t); // Set position proportional to t
			yield return null;         // Leave the routine and return here in the next frame
		}
		animator.SetInteger ("WalkingDication", 0);
	}

	IEnumerator  JumpFromTo (Vector3 pointA, Vector3 pointB, float time){
		animator.SetBool ("PlacingBlock", true);

		float t = 0f;
		while (t < 1.0f) {
			t += Time.deltaTime / time; // Sweeps from 0 to 1 in time seconds
			transform.position = Vector3.Lerp(pointA, pointB, t); // Set position proportional to t
			yield return null;         // Leave the routine and return here in the next frame
		}

		animator.SetBool ("PlacingBlock", false);
	}

	void BuyMovementPoints(){
		int cost = 250;
		if (this.Money >= cost) {
			this.Money  = this.Money - cost;
			this.MovementPoints = this.MovementPoints + 3;
			uiManager.UpdateUI (this);
		}

	}

	void BuyBuildingPoints(){
		int cost = 175;
		if (this.Money >= cost ) {
			this.Money = this.Money - cost;
			this.BuildingPoints = this.BuildingPoints + 1;
			uiManager.UpdateUI (this);
		}

	}

	void SpendBuildingPoints(){
		BuildingPoints = BuildingPoints - 1;
		uiManager.UpdateUI (this);
	}

	void SpendMovementPoints(){
		MovementPoints = MovementPoints - 1;
		uiManager.UpdateUI (this);
	}

	public Vector2 toVector2 (Vector3 v) {
		return new Vector2 (v.y, v.z);
	}
}