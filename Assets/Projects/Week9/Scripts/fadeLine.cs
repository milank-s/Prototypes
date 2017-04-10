using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeLine : MonoBehaviour {

	LineRenderer l;
	// Use this for initialization
	void Start () {
		l = GetComponent<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (l.widthMultiplier <= 0) {
			Destroy (gameObject);
		}
		l.widthMultiplier -= Time.deltaTime;
	}
}
