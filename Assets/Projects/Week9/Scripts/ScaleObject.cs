using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleObject : MonoBehaviour {

	public float scale;
	// Update is called once per frame
	void Update () {
		transform.localScale += Vector3.one * Time.deltaTime * scale;
	}
}
