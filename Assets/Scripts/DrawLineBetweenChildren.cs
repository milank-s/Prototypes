using UnityEngine;
using System.Collections;

public class DrawLineBetweenChildren : MonoBehaviour {

	LineRenderer l;
	// Use this for initialization
	void Start () {
		if (GetComponent<LineRenderer> () != null) {
			l = GetComponent<LineRenderer> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		int i = 0;
		foreach(Transform t in GetComponentsInChildren<Transform>()) {
			l.SetVertexCount (i + 1);
			l.SetPosition (i, t.position);
			i++;
		}
	}
}
