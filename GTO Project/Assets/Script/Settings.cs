using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	
	}
	private bool Spawned = false;
	public float VersionNr;

	[Range(5, 16)]
	public int PlayfieldWidth = 5;
	[Range(6, 16)]
	public int PlayfieldHeight = 14;

	void Awake()
	{
		if(Spawned == false)
		{
			Spawned = true;
			DontDestroyOnLoad(this);
		}
		else
		{
			DestroyImmediate(gameObject); //This deletes the new object/s that you
			// mentioned were being created
		}
	}

}
