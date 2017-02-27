using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets;
using System.Linq;

public class Rupee : MonoBehaviour {



	public Vector2 Location;
	public int[] valueAmounts;
	public int Value;
	public bool isActive = true;


	// Use this for initialization
	void Start () {
		//int Value = valueAmounts[Random.Range (0, valueAmounts.Length)];
		int randomInt = Random.Range(0, 100);
		if (TestRange (randomInt, 0, 60)) {
			this.Value = valueAmounts [0];
			this.GetComponentInParent<SpriteRenderer> ().color = GameColor.RupeeGreen;
		} else if (TestRange (randomInt, 61, 80)) {
			this.Value = valueAmounts [1];
			this.GetComponentInParent<SpriteRenderer> ().color = GameColor.RupeeBlue;
		} else if (TestRange (randomInt, 81, 90)) {
			this.Value = valueAmounts [2];
			this.GetComponentInParent<SpriteRenderer> ().color = GameColor.RupeeRed;
		} else if (TestRange (randomInt, 91, 95)) {
			this.Value = valueAmounts [3];
			this.GetComponentInParent<SpriteRenderer> ().color = GameColor.RupeePurple;
		} else if (TestRange (randomInt, 96, 98)) {
			this.Value = valueAmounts [4];
			this.GetComponentInParent<SpriteRenderer> ().color = GameColor.RupeeOrange;
		} else {
			this.Value = valueAmounts [5];
			this.GetComponentInParent<SpriteRenderer> ().color = GameColor.RupeeSilver;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	bool TestRange (int numberToCheck, int bottom, int top)
	{
		return (numberToCheck >= bottom && numberToCheck <= top);
	}

	public int CollectRupee(){
		GameManager.instance.PlayEffect (2);
		this.GetComponentInParent<SpriteRenderer> ().color = GameColor.Transparent;
		this.transform.position = new Vector3 (-100, -100, 10);
		isActive = false;
		return Value;
	}
}
