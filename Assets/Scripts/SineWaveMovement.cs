using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWaveMovement : MonoBehaviour {

	Vector3 pos;
	// Use this for initialization
	void Start () {
		pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (pos.x + Mathf.Sin (Time.time * 3) * 10, pos.y, pos.z);
	}
}
