using UnityEngine;

public class Player : MonoBehaviour
{
    public string Name;
    public int MovementPoints;
    public int BuildingPoints;
    public int Money;
    public bool HisTurn;
    public bool onStartPos = false;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override string ToString()
    {
        return "Name: " + Name + ", MovementPoints: " + MovementPoints + ", BuildingPoints: " + BuildingPoints +
               ", Money: " + Money + ", HisTurn: " + HisTurn;
    }
}