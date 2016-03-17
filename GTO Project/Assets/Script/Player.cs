using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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


	private int _currentCellSize;

    // Use this for initialization
    void Start()
    {
		_currentCellSize = 4;
    }

    // Update is called once per frame
    void Update()
    {		
        if (HisTurn)
        {
			if (Input.GetKeyUp (KeyCode.A)) {
				Vector3 newPos = GetMoveToTile ("left", Name);
				WalkTo (newPos);

			}
			if (Input.GetKeyUp (KeyCode.W)) {
				Vector3 newPos = GetMoveToTile ("up", Name);
				WalkTo (newPos);
			}
			if (Input.GetKeyUp (KeyCode.S)) {
				Vector3 newPos = GetMoveToTile ("down", Name);
				WalkTo (newPos);
			}
			if (Input.GetKeyUp (KeyCode.D)) {
				Vector3 newPos = GetMoveToTile ("right", Name);
				WalkTo (newPos);
			}
			if (Input.GetKeyUp (KeyCode.Space)) {
				PlaceBlock ();
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

	Vector2 CheckBehind()
	{
		var back = transform.TransformDirection(Vector3.back);
		//note the use of var as the type. This is because in c# you 
		// can have lamda functions which open up the use of untyped variables
		//these variables can only live INSIDE a function. 
		RaycastHit hit;

		if (Physics.Raycast (transform.position, -back, out hit, 4)) {
			//Debug.Log (hit.collider.gameObject.GetComponent<Tile> ().Id.ToString ());
			Vector2 TileID = hit.collider.gameObject.GetComponent<Tile> ().Id;
			return TileID;
		} else {
			return Vector2.zero;
		}
	}

	public Player GetNeighborOwner(Vector2 moveTo)
	{
		List<GameObject> Tl = GameManager.instance.GetTiles();

		//Vector2 search = moveTo;
		//GameObject result = Tl.Single(s => s.GetComponent<Tile> ().Id.Equals(search));

		foreach (GameObject i in Tl) {
			Tile it = i.GetComponent<Tile> ();

			if (it.Id == moveTo) {
				return it.Player;
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

	void PlaceBlock(){
		//This code needs to be cleaned later! most of the code is the same ( please fix futher kevin thx in advance!)
		if (this.Name == "Good") {
			Vector3 blockPlace = GetMoveToTile ("down", this.Name);
			if ("Evil" == GetNeighborOwner (blockPlace).Name) {
				Tile OtherTile = GetTile (blockPlace);
				Tile ThisTile = GetTile (_currentLocation);
				OtherTile.SwitchOwner (this, true);
				ThisTile.SwitchOwner (this, true);
				AddShadow (OtherTile);
				AddShadow (ThisTile);
				setLocation (GetMoveToTile ("up", this.Name));
			}
		} else if (this.Name == "Evil") {
			Vector3 blockPlace = GetMoveToTile ("down", this.Name);
			if ("Good" == GetNeighborOwner (blockPlace).Name) {
				Tile OtherTile = GetTile (blockPlace);
				Tile ThisTile = GetTile (_currentLocation);
				OtherTile.SwitchOwner (this, true);
				ThisTile.SwitchOwner (this, true);
				AddShadow (OtherTile);
				AddShadow (ThisTile);
				setLocation (GetMoveToTile ("up", this.Name));
			}

		} else {
			//return false;
		}

		CheckShadows();
	
	}

	void WalkTo(Vector3 to){
		bool newPosShadow = GetTile (to).IsShadow;
		if (!newPosShadow) {
			if (this.Name == GetNeighborOwner (to).Name) {
				setLocation (to);
			}
		}
	}

	public void AddShadow(Tile tile){
		ShadowBlocks.Add (tile);
	}



	public void CheckShadows(){
		//Sorts the list
		List<Tile> xSort = ShadowBlocks.OrderBy(x => x.Id[1]).ThenBy(x => x.Id[0]).ToList();
		//List<Tile> ySort = ShadowBlocks.OrderBy(x => x.Id[0]).ThenBy(x => x.Id[1]).ToList();


		List<List<Tile>> allSeq = new List<List<Tile>>(); //a List with all the seqlists
		List<Tile> tempSeq = new List<Tile>(); // a temp list this will be only used while looping
		Tile lastInsertedTile = null; // the last inserted tile in a seq

		Tile lastTile = xSort[xSort.Count - 1]; //Gets the last tile in the xSort


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
}