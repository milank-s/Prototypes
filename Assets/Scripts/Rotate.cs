﻿using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	public float x, y, z;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (x, y, z);
	}
}
