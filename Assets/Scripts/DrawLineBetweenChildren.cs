using UnityEngine;
using System.Collections;

public class DrawLineBetweenChildren : MonoBehaviour {

	LineRenderer l;
	// Use this for initialization
	void Start () {
		if (GetComponent<LineRenderer> () != null) {
			l = GetComponent<LineRenderer> ();
		}
		l.SetVertexCount(transform.childCount +1);
	}
	
	// Update is called once per frame
	void Update () {
		l.SetPosition (0, transform.position);
		for(int i = 0; i < transform.childCount; i++) {
			Transform t = transform.GetChild (i);
			l.SetPosition (i +1, t.position);
		}
	}
}
