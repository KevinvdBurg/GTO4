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
				//Debug.Log("Pressing A - " + Name);
				Vector3 newPos = GetMoveToTile ("left");
				if(this.Name == GetNeighborOwner(newPos))
					setLocation (newPos);
			}
			if (Input.GetKeyUp (KeyCode.W)) {
				//Debug.Log("Pressing W - " + Name);
				Vector3 newPos = GetMoveToTile ("up");
				if(this.Name == GetNeighborOwner(newPos))
					setLocation (newPos);
			}
			if (Input.GetKeyUp (KeyCode.S)) {
				Debug.Log("Pressing S - " + Name);
				Vector3 newPos = GetMoveToTile ("down");
				if(this.Name == GetNeighborOwner(newPos))
					setLocation (newPos);
			}
			if (Input.GetKeyUp (KeyCode.D)) {
				Debug.Log ("Pressing D - " + Name);
				Vector3 newPos = GetMoveToTile ("right");
				if(this.Name == GetNeighborOwner(newPos))
					setLocation (newPos);
			}

			if (Input.GetKeyUp (KeyCode.Space)) {
				Debug.Log ("Pressing Space - " + Name);
				PlaceBlock ();
			}
				
        }
    }
    

    public override string ToString()
    {
        return "Name: " + Name + ", MovementPoints: " + MovementPoints + ", BuildingPoints: " + BuildingPoints +
               ", Money: " + Money + ", HisTurn: " + HisTurn;
    }

//    void setLocation()
//    {
//        Debug.Log(this.transform.position.y);
//        Debug.Log(this.transform.position.x);
//        //CurrentLocation = new int[2] { (int) this.transform.position.y, (int)this.transform.position.x};
//    }


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
		Debug.DrawRay(transform.position, -back * 4, Color.green);

		if (Physics.Raycast (transform.position, -back, out hit, 4)) {
			Debug.Log (hit.collider.gameObject.GetComponent<Tile> ().Id.ToString ());
			Vector2 TileID = hit.collider.gameObject.GetComponent<Tile> ().Id;
			return TileID;
		} else {
			return Vector2.zero;
		}
	}

	public string GetNeighborOwner(Vector2 moveTo)
	{
		List<GameObject> Tl = GameManager.instance.GetTiles();

		//Vector2 search = moveTo;
		//GameObject result = Tl.Single(s => s.GetComponent<Tile> ().Id.Equals(search));

		foreach (GameObject i in Tl) {
			Tile it = i.GetComponent<Tile> ();
			//Debug.Log ("Searching" + moveTo);
			//Debug.Log ("Searching: " + it.Id + " - " + moveTo);

			if (it.Id == moveTo) {
				//Debug.Log ("FOUND!: " + it.Id + " - " + it.Player.Name);
				return it.Player.Name; 
				//Debug.Log (it.Id + " - " + it.Player.Name);
			}
			
		}
		return "Unknown";

		//return Tl.Find(obj => obj.GetComponent<Tile>().ID == moveTo).GetComponent<Tile> ().player.Name;

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

	Vector3 GetMoveToTile (string move)
	{
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
			Debug.Log ("Not a good move");
			moveToVector.x = _currentLocation.x;
			moveToVector.y = _currentLocation.y;
			return moveToVector;
		}
	}

	void PlaceBlock(){
		if (this.Name == "Good") {
			Vector3 blockPlace = GetMoveToTile ("down");
			if ("Evil" == GetNeighborOwner (blockPlace)) {
				Tile CurrentTile = GetTile (blockPlace);
				CurrentTile.SwitchOwner (this, false);
				//return true;
			}
		} else if (this.Name == "Evil") {
			Vector3 blockPlace = GetMoveToTile ("up");
			if ("Good" == GetNeighborOwner (blockPlace)) {
				Tile CurrentTile = GetTile (blockPlace);
				CurrentTile.SwitchOwner (this, false);
				//return true;
			}

		} else {
			//return false;
		}
	
	}




}