using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManger : MonoBehaviour {

	public Text VersionNumber;
	private Settings settings;
	public GameObject Menu;
	public Text width;
	public Text height;
	public Canvas overlay;

	// Use this for initialization
	void Start () {
		GameObject SettingsObject = GameObject.FindGameObjectWithTag ("Settings");

		if (SettingsObject != null) {
			settings = SettingsObject.GetComponent <Settings>();
			VersionNumber.text = "Version: " + settings.VersionNr;
		}
		if (SettingsObject == null) {
			Debug.Log ("Cannot find Settings");
		}
		UpdateUI ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void ButtonStartGame(){
		SceneManager.LoadScene ("MainScene");
	}

	public void ButtonOptions(GameObject optionsPanel){
		SwitchPanel (optionsPanel);
	}

	public void ButtonAbout(GameObject aboutPanel){
		SwitchPanel (aboutPanel);
	}

	public void ButtonBack(GameObject thisPanel){
		if (thisPanel.name != "Menu") {
			thisPanel.SetActive (false);
			Menu.SetActive (true);
		}	
	}

	public void SwitchPanel(GameObject thisPanel){
		thisPanel.SetActive (true);
		Menu.SetActive (false);
	}
		

	public void ChangeWidth(int change){
		
		int w = (settings.PlayfieldWidth + change);
		if (w < 4) {
			w = 4;
		} else if(w > 20 ){
			w = 20;
		}
		else {
			settings.PlayfieldWidth = w;

		}
		UpdateUI ();



	}

	public void ChangeHeight(int change){
		
		int h = (settings.PlayfieldHeight + change);
		if (h < 6) {
			h = 6;
		} else if(h > 16){
			h = 16;
		}
		else {
			settings.PlayfieldHeight = h;
		}
		UpdateUI ();
	}

	public void UpdateUI(){
		width.text = settings.PlayfieldWidth + "";
		height.text = settings.PlayfieldHeight + "";
	}
		


}
