using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUpText : MonoBehaviour {

	public TextMesh t;
	int count = 0;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		t.text = (Mathf.Round (Time.time) % 12).ToString () + ":00" + "AM" + "\n"+ "Day " + count;
		if((Time.time % 12) < Time.deltaTime){
			count++;
		}
	}
}
