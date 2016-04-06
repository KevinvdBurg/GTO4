using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManger : MonoBehaviour {

	public Text VersionNumber;
	private Settings settings;
	// Use this for initialization
	void Start () {
		GameObject SettingsObject = GameObject.FindGameObjectWithTag ("Settings");

		if (SettingsObject != null) {
			settings = SettingsObject.GetComponent <Settings>();
			VersionNumber.text = "Version: " + settings.VersionNr;
		}
		if (SettingsObject == null) {
			Debug.Log ("Cannot find GameControler");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ButtonStartGame(){
		Debug.Log ("Start Game Click");
		SceneManager.LoadScene ("MainScene");

	}

	public void ButtonOptions(){
		Debug.Log ("Options Click");
	}

	public void ButtonAbout(){
		Debug.Log ("About Click");
	}


}
