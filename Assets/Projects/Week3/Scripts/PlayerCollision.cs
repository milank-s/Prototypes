using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {
	bool isCentered;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnCollisionEnter(Collision col){
		if (col.gameObject.name == "Player") {
			GameObject.Find ("Game Manager").BroadcastMessage ("RestartGame");
		}
	}

	public void OnTriggerEnter(Collider col){
		if (col.gameObject.name == "Middle") {
			isCentered = true;
		}
	}
	public void OnTriggerExit(Collider col){
		if (col.gameObject.name == "Middle") {
			isCentered = false;
		}
	}
}
