using UnityEngine;
using System.Collections;

public class RotateAroundParent : MonoBehaviour {
	public float speed = 1;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (transform.parent.position, transform.forward, speed);
		transform.position = Vector3.MoveTowards (transform.position, transform.parent.position, Time.deltaTime / 100);
	}
}
