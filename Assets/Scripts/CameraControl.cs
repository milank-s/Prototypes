using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public Transform player;

	Rigidbody2D r;
	public float shakeTimer = 0;
	public float intensity = 1000;
	public float followIntensity = 100;
	public float lookDistance = 10;
	// Use this for initialization
	void Start () {
		r = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		ShakeScreen ();
		FollowPlayer ();
	}

	void FollowPlayer(){
		r.AddForce ((player.position - transform.position).normalized * followIntensity);
	}

	public void ShakeScreen(){
		if (shakeTimer > 0) {
			shakeTimer -= Time.deltaTime;
			r.AddForce (Random.insideUnitSphere * intensity);
		}
	}
}
