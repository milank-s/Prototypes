using UnityEngine;
using System.Collections;

public class DrawLineToTarget: MonoBehaviour {

	LineRenderer l;
	public Transform t;
	// Use this for initialization
	void Start () {
		if (GetComponent<LineRenderer> () != null) {
			l = GetComponent<LineRenderer> ();
		}
	}

	// Update is called once per frame
	void Update () {
		l.SetPosition (0, transform.position);
		l.SetPosition (1, t.position);
	}
}
