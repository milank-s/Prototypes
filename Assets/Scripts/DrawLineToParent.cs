using UnityEngine;
using System.Collections;

public class DrawLineToParent: MonoBehaviour {

	LineRenderer l;
	// Use this for initialization
	void Start () {
		if (GetComponent<LineRenderer> () != null) {
			l = GetComponent<LineRenderer> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		l.SetPosition (0, transform.position);
		l.SetPosition (1, transform.parent.position);
	}
}
