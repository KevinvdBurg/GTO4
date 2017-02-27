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
	public AudioClip[] audioClipMusic;
	public AudioSource audioMusic;
	public AudioClip[] audioClipEffects;
	public AudioSource audioEffects;


	// Use this for initialization
	void Start () {
		//audioMusic = GetComponent<AudioSource>();
		GameObject SettingsObject = GameObject.FindGameObjectWithTag ("Settings");

		if (SettingsObject != null) {
			settings = SettingsObject.GetComponent <Settings>();
			VersionNumber.text = "Version: " + settings.VersionNr;
		}
		if (SettingsObject == null) {
			Debug.Log ("Cannot find Settings");
		}
		UpdateUI ();
		PlaySound (0);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void ButtonStartGame(){
		StartCoroutine (WaitUntilSoundEvent (1));
	}

	public void ButtonOptions(GameObject optionsPanel){
		StartCoroutine (WaitUntilSoundEvent (optionsPanel));
	}

	public void ButtonAbout(GameObject aboutPanel){
		StartCoroutine (WaitUntilSoundEvent (aboutPanel));
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
		StartCoroutine (WaitUntilSoundEvent (0));
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
		StartCoroutine (WaitUntilSoundEvent (0));
		UpdateUI ();
	}

	public void ChangeH(){
	
	}

	public void UpdateUI(){
		width.text = settings.PlayfieldWidth + "";
		height.text = settings.PlayfieldHeight + "";
	}
		
	public void PlaySound(int clip){
		audioMusic.clip = audioClipMusic [clip];
		if (clip == 0) {
			audioMusic.Play ();
			StartCoroutine (WaitUntilSoundIsDone (audioMusic.clip.length));
		} else {
			audioMusic.loop = true;
			audioMusic.Play ();
		}
	}
	public float PlayEffect(int clip){
		audioEffects.clip = audioClipEffects [clip];
		audioEffects.PlayOneShot(audioClipEffects[clip], 0.7F);
		return audioEffects.clip.length;
	}

	IEnumerator WaitUntilSoundIsDone(float time){
		yield return new WaitForSeconds(time);
		PlaySound (1);
	}


	IEnumerator WaitUntilSoundEvent(GameObject panel){
		float time = PlayEffect (0);
		yield return new WaitForSeconds(time);
		SwitchPanel (panel);
	}

	//1 = start game
	IEnumerator WaitUntilSoundEvent(int action){
		float time = 0f;
		if (action == 1) {
			time = PlayEffect (1);
		}		
		else {
			time = PlayEffect (0);
		}
			
		yield return new WaitForSeconds(time);
		if (action == 1) {
			SceneManager.LoadScene ("MainScene");
		}
	}

}
