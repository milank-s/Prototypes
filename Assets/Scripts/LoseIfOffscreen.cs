using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseIfOffscreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		InvokeRepeating ("CheckPlayerPos", 1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CheckPlayerPos(){
		if (!GameObject.Find ("Chester").GetComponent<SpriteRenderer> ().isVisible) {
			gameObject.AddComponent<GameOver> ();
			this.enabled = false;
		}
	}
}
