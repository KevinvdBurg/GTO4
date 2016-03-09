using UnityEngine;

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
				//setLocation (new Vector3 ((left.x * currentCellSize) / 2 , (left.y * currentCellSize) / 2, -2));
				setLocation (new Vector3 (left.x * 4, left.y * 4, -2));
			}
			if (Input.GetKeyUp (KeyCode.W)) {
				Debug.Log("Pressing W - " + Name);
				//setLocation (new Vector3 ((up.x * currentCellSize) / 2 , (up.y * currentCellSize) / 2, -2));
				setLocation (new Vector3 (up.x, up.y, -2));
			}
			if (Input.GetKeyUp (KeyCode.S)) {
				Debug.Log("Pressing S - " + Name);
				//setLocation (new Vector3 ((down.x * currentCellSize) / 2 , (down.y * currentCellSize) / 2, -2));
				setLocation (new Vector3 (down.x, down.y, -2));
			}
			if (Input.GetKeyUp (KeyCode.D)) {
				Debug.Log ("Pressing D - " + Name);
				//setLocation (new Vector3 ((right.x * currentCellSize) / 2, (right.y * currentCellSize) / 2, -2));
				setLocation (new Vector3 (right.x, right.y, -2));
			}
				
        }
    }
    

    public override string ToString()
    {
        return "Name: " + Name + ", MovementPoints: " + MovementPoints + ", BuildingPoints: " + BuildingPoints +
               ", Money: " + Money + ", HisTurn: " + HisTurn;
    }

    void setLocation()
    {
        Debug.Log(this.transform.position.y);
        Debug.Log(this.transform.position.x);
        //CurrentLocation = new int[2] { (int) this.transform.position.y, (int)this.transform.position.x};
    }

	void getNeighbor()
	{
		up = new Vector2 ( CurrentLocation.x , (CurrentLocation.y + 1));
		down = new Vector2 ( CurrentLocation.x, (CurrentLocation.y - 1) );
		left = new Vector2 ( (CurrentLocation.x - 1), CurrentLocation.y );
		right = new Vector2 ( (CurrentLocation.x + 1), CurrentLocation.y ) ;
	}

	public void setLocation(Vector3 newLocation)
	{
		CurrentLocation = newLocation;
		this.transform.position = newLocation;
		getNeighbor ();
	}


}