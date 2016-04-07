using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour {

	public Canvas AchievementCanvas;
	public Image AchievementPanel;
	public Text AchievementText;
	public List<GameAchievement> AchievementList;

	// Use this for initialization
	void Start () {
		AchievementPanel.CrossFadeAlpha(0f, 0f, true);
		AchievementText.CrossFadeAlpha(0f, 0f, true);
		AchievementList = new List<GameAchievement>();
		AchievementList.Add(new GameAchievement ("Destoy Wallpiece as Deku", 50));
		AchievementList.Add(new GameAchievement ("Destoy Wallpiece as Bunny", 50));
		AchievementList.Add(new GameAchievement ("Move as Deku", 10));
		AchievementList.Add(new GameAchievement ("Move as Bunny", 10));
		AchievementList.Add(new GameAchievement ("Steal as Deku", 10));
		AchievementList.Add(new GameAchievement ("Steal as Bunny", 10));
		AchievementList.Add(new GameAchievement ("Winner", 100));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AchievementGet(string AchievementName){
		foreach (GameAchievement achievement in AchievementList) {
			if (achievement.AchievementName == AchievementName && achievement.isActive) {
					StartCoroutine (showAchievement(achievement, 4f));
			}
		}
	}

	IEnumerator showAchievement(GameAchievement achievement, float time){
		AchievementText.text = achievement.AchievementName + " : " + achievement.AchievementPoints;
		AchievementPanel.CrossFadeAlpha(1f, 0.5f, true);
		AchievementText.CrossFadeAlpha(1f, 0.5f, true);
		float t = 0f;
		while (t < 1.0f) {
			t += Time.deltaTime / time; // Sweeps from 0 to 1 in time seconds
			yield return null;         // Leave the routine and return here in the next frame
		}
		AchievementPanel.CrossFadeAlpha(0f, 1f, true);
		AchievementText.CrossFadeAlpha(0f, 1f, true);
		achievement.isActive = false;
	}




}
