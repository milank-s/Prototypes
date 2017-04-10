﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SpacebarToStart : MonoBehaviour {

	public string SceneToLoad;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			SceneManager.LoadScene(SceneToLoad);
		}
	}
}
