using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDolly : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Camera.main.rect = new Rect (0, 0, 1, 1);
		Camera.main.backgroundColor = Color.black;
		Camera.main.clearFlags = CameraClearFlags.SolidColor;
	}
}
