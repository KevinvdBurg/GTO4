using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public string Name;
    public int MovementPoints;
    public int BuildingPoints;
    public int Money;
    public bool HisTurn;
    public bool OnStartPos = false;

	private Vector3 _currentLocation;

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
				Debug.Log("Pressing A - " + Name);
				if(this.Name == GetNeighborOwner(Left))
					setLocation (new Vector3 (Left.x, Left.y, -2));
				
			}
			if (Input.GetKeyUp (KeyCode.W)) {
				Debug.Log("Pressing W - " + Name);
				if(this.Name == GetNeighborOwner(Up))
					setLocation (new Vector3 (Up.x, Up.y, -2));
			}
			if (Input.GetKeyUp (KeyCode.S)) {
				Debug.Log("Pressing S - " + Name);
				if(this.Name == GetNeighborOwner(Down))
					setLocation (new Vector3 (Down.x, Down.y, -2));
			}
			if (Input.GetKeyUp (KeyCode.D)) {
				Debug.Log ("Pressing D - " + Name);
				if(this.Name == GetNeighborOwner(Right))
					setLocation (new Vector3 (Right.x, Right.y, -2));
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

	void GetNeighbor()
	{
		Up = new Vector2 ( _currentLocation.x , (_currentLocation.y + 1));
		Down = new Vector2 ( _currentLocation.x, (_currentLocation.y - 1) );
		Left = new Vector2 ( (_currentLocation.x - 1), _currentLocation.y );
		Right = new Vector2 ( (_currentLocation.x + 1), _currentLocation.y );
	}



	public bool setLocation(Vector3 newLocation)
	{
		_currentLocation = newLocation;
		this.transform.position = newLocation;
		if (this.transform.position == CheckBehind ()) {
			GetNeighbor ();
			return true;
		} else {
			return false;
		}


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
			return hit.collider.gameObject.GetComponent<Tile> ().Id;
		} else {
			return Vector2.zero;
		}
	}

	public string GetNeighborOwner(Vector2 moveTo)
	{
		List<GameObject> Tl = GameManager.instance.GetTiles ();
		foreach (GameObject i in Tl) {
			Tile it = i.GetComponent<Tile> ();
		    Debug.Log(moveTo + " > " + it.Id);
			if (it.Id == moveTo)
				return it.Player.Name;

		}
		return "Unknown";

		//return Tl.Find(obj => obj.GetComponent<Tile>().ID == moveTo).GetComponent<Tile> ().player.Name;

	}




}