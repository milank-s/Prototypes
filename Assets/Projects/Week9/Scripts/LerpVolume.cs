using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpVolume : MonoBehaviour {

	AudioSource a;
	// Use this for initialization
	void Start () {
		a = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		a.volume = Mathf.Lerp (a.volume, 0, Time.deltaTime * 5);
	}
}
