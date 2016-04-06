using UnityEngine;
using System.Collections;


public class GameAchievement {
	public string AchievementName;
	public int AchievementPoints;
	public bool isActive;

	public GameAchievement (string AchievementName, int AchievementPoints)
	{
		this.AchievementName = AchievementName;
		this.AchievementPoints = AchievementPoints;
		isActive = true;
	}

	public string GetAchievementName(){
		return this.AchievementName;
	}
}
