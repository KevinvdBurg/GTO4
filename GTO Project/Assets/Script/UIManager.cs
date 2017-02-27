using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {


    public Text TurnIndicator;
    public Text MP;
    public Text BP;
    public Text GP;
	public Text BPCostText;
	public Text MPCostText;

	public int BPcost;
	public int MPcost;

	public Camera MCamera;

	public GameObject Background;

	public GameObject CostPanel;

	// Use this for initialization
	void Start () {
		UpdateStaticUI ();
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

		public void FlipCamera(string playerName){
			if (playerName == "Good") {
				MCamera.transform.rotation = Quaternion.Euler (0, 0, 0);
				//Background.transform.rotation = Quaternion.Euler (0, 0, 0);
			} else {
				MCamera.transform.rotation = Quaternion.Euler(0,0,180);
				//Background.transform.rotation = Quaternion.Euler (0, 0, 180);
			}
				
		}

	void UpdateStaticUI(){
		BPCostText.text = "E : " + BPcost;
		MPCostText.text = "Q : " + MPcost;
	}

	public void UpdateUI(Player updateUiPlayer)
	{
		
		MP.text = " " + updateUiPlayer.MovementPoints;
		BP.text = " " + updateUiPlayer.BuildingPoints;
		GP.text = " " + updateUiPlayer.Money;
	}

	public void BuyMovementPoints(Player player){
		int cost = MPcost;
		if (player.Money >= cost) {
			player.Money  = player.Money - cost;
			player.MovementPoints = player.MovementPoints + 3;
			GameManager.instance.PlayEffect (7);
			UpdateUI (player);
		}

	}

	public void BuyBuildingPoints(Player player){
		int cost = BPcost;
		if (player.Money >= cost ) {
			player.Money = player.Money - cost;
			player.BuildingPoints = player.BuildingPoints + 1;
			GameManager.instance.PlayEffect (7);
			UpdateUI (player);
		}

	}

	public void SpendBuildingPoints(Player player){
		player.BuildingPoints = player.BuildingPoints - 1;
		UpdateUI (player);
	}

	public void SpendMovementPoints(Player player){
		if (player.MovementPoints <= 0) {
			player.MovementPoints = 0;
		} else {
			player.MovementPoints -= 1;
		}
		UpdateUI (player);
	}

	public void ToggleCostPanel(bool open){
		if (open) {
			CostPanel.SetActive (true);
		}else{
			CostPanel.SetActive (false);
		}
	}

}
