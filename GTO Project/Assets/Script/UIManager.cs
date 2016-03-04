using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    //Update Turn
    //Update Points ( Building/ Movemet / Money )


    public Text TurnIndicator;
    public Text MP;
    public Text BP;
    public Text GP;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateTurnIndicator(string player)
    {
        TurnIndicator.text = player;
    }

    public void UpdateMovementPoints(string mp)
    {
        MP.text = mp;
    }

    public void UpdateBlockPoints(string bp)
    {
        BP.text = bp;
    }

    public void UpdateGoldPoints(string gp)
    {
        GP.text = gp;
    }

}
