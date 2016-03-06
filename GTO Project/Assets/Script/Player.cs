using UnityEngine;

public class Player : MonoBehaviour
{
    public string Name;
    public int MovementPoints;
    public int BuildingPoints;
    public int Money;
    public bool HisTurn;
    public bool onStartPos = false;
    public int[] CurrentLocation;



    // Use this for initialization
    void Start()
    {
        setLocation();
    }

    // Update is called once per frame
    void Update()
    {
        if (HisTurn)
        {
            if (Input.GetKey(KeyCode.A))
                Debug.Log("Pressing A");

            if (Input.GetKey(KeyCode.W))
                Debug.Log("Pressing W");

            if (Input.GetKey(KeyCode.S))
                Debug.Log("Pressing S");

            if (Input.GetKey(KeyCode.D))
                Debug.Log("Pressing D");
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
}