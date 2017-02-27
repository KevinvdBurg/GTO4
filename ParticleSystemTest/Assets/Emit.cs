using UnityEngine;
using System.Collections;

public class Emit : MonoBehaviour {

	public ParticleSystem ps;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			ps.Emit (100);
			ps.startColor = Color.red;
		
		}
			
		
	}
}
