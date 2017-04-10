using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFade : MonoBehaviour {

	Color color;
	float time;
	// Use this for initialization
	void Start () {
		color = GetComponent<TextMesh> ().color;
		time = Random.Range (0.00f, Mathf.PI * 2);
	}
	
	// Update is called once per frame
	void Update () {
		color.a = Mathf.Abs(Mathf.Sin (Time.time * 2 + time)/2);
		GetComponent<TextMesh> ().color = color;
	}
}
