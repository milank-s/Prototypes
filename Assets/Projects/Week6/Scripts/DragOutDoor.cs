using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragOutDoor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter(Collider col){
		foreach(Rigidbody r in col.gameObject.GetComponentsInChildren<Rigidbody>()){
			r.useGravity = true;
			r.drag = 0;
		}

		foreach(SphereCollider s in col.gameObject.GetComponentsInChildren<SphereCollider>()){
			s.isTrigger = false;
		}
	}
}
