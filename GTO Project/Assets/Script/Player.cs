using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public string Name;
    public int MovementPoints;
    public int BuildingPoints;
    public int Money;
    public bool HisTurn;
    public bool onStartPos = false;

	private Vector3 CurrentLocation;

	public Vector2 up;
	public Vector2 down;
	public Vector2 left;
	public Vector2 right;



	private int currentCellSize;

    // Use this for initialization
    void Start()
    {
		currentCellSize = 4;
    }

    // Update is called once per frame
    void Update()
    {
		
        if (HisTurn)
        {
			if (Input.GetKeyUp (KeyCode.A)) {
				Debug.Log("Pressing A - " + Name);
				if(this.Name == getNeighborOwner(left))
					setLocation (new Vector3 (left.x, left.y, -2));
				
			}
			if (Input.GetKeyUp (KeyCode.W)) {
				Debug.Log("Pressing W - " + Name);
				if(this.Name == getNeighborOwner(up))
					setLocation (new Vector3 (up.x, up.y, -2));
			}
			if (Input.GetKeyUp (KeyCode.S)) {
				Debug.Log("Pressing S - " + Name);
				if(this.Name == getNeighborOwner(down))
					setLocation (new Vector3 (down.x, down.y, -2));
			}
			if (Input.GetKeyUp (KeyCode.D)) {
				Debug.Log ("Pressing D - " + Name);
				if(this.Name == getNeighborOwner(right))
					setLocation (new Vector3 (right.x, right.y, -2));
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

	void getNeighbor()
	{
		up = new Vector2 ( CurrentLocation.x , (CurrentLocation.y + 1));
		down = new Vector2 ( CurrentLocation.x, (CurrentLocation.y - 1) );
		left = new Vector2 ( (CurrentLocation.x - 1), CurrentLocation.y );
		right = new Vector2 ( (CurrentLocation.x + 1), CurrentLocation.y );
	}



	public void setLocation(Vector3 newLocation)
	{
		CurrentLocation = newLocation;
		this.transform.position = newLocation;
		checkBehind ();
		getNeighbor ();
	}

	void checkBehind()
	{
		var back = transform.TransformDirection(Vector3.back);
		//note the use of var as the type. This is because in c# you 
		// can have lamda functions which open up the use of untyped variables
		//these variables can only live INSIDE a function. 
		RaycastHit hit;
		Debug.DrawRay(transform.position, -back * 4, Color.green);

		if (Physics.Raycast(transform.position, -back, out hit, 4))
		{
			Debug.Log(hit.collider.gameObject.GetComponent<Tile> ().ID.ToString());
		}
	}

	public string getNeighborOwner(Vector2 moveTo) 
	{
		List<GameObject> Tl = GameManager.instance.getTiles ();

		foreach (GameObject i in Tl) {
			Tile it = i.GetComponent<Tile> ();
			if (it.ID == moveTo)
				return it.player.Name;
		}
		return "Unknown";

		//return Tl.Find(obj => obj.GetComponent<Tile>().ID == moveTo).GetComponent<Tile> ().player.Name;

	}


}