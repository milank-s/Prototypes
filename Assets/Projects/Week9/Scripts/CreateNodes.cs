using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateNodes : MonoBehaviour {

	public float maxDistance;
	public GameObject n;
	public GameObject emptyNode;
	public GameObject sun;
	public int maxJoints;
	public int highLimit, lowLimit, spring;
	public AudioClip[] muzak;
	GameObject lastNode;
	Vector3 deltaPoint;
	int starCount = 1;
	int trackNumber = 0;
	public int spawnAmount;
	public Vector3 spawnPoint;

	void Start () {
		lastNode = gameObject;
		lastNode.tag = "Untagged";
		deltaPoint = lastNode.transform.position;
		SpawnBehind ();
//		Destroy(lastNode.GetComponent<CharacterJoint> ());
	}

	void Update () {
		if (Vector3.Distance (lastNode.transform.position, deltaPoint) > maxDistance) {
			if (starCount < spawnAmount) {
				SpawnBehind ();
//			SpawnInFront ();
			}

			deltaPoint = lastNode.transform.position;
		}
		if (!GetComponent<AudioSource> ().isPlaying && starCount < maxJoints) {
			GetComponent<AudioSource> ().clip = muzak [(int)(((float)starCount/(float)maxJoints) * (muzak.Length-1))];
			GetComponent<AudioSource> ().Play ();
		}
		spawnPoint = lastNode.transform.position + ((deltaPoint - lastNode.transform.position).normalized * maxDistance);
		Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 50 + (starCount * 0.20f), Time.deltaTime);
	}

	void SpawnInFront(){
		GameObject newNode = Instantiate (n, transform.position, Quaternion.identity) as GameObject;
		newNode.AddComponent<CharacterJoint> ().connectedBody = lastNode.GetComponent<Rigidbody> ();
		newNode.AddComponent<DrawLineToTarget> ().t = lastNode.transform;
		newNode.tag = "Untagged";
		lastNode = newNode;
		newNode.GetComponent<Rigidbody> ().AddForce (GetComponent<Rigidbody> ().velocity * 15);
		starCount++;
//		newNode.transform.parent = transform;
	}

	public void SpawnBehind(){

		GameObject newNode = Instantiate (n, deltaPoint, Quaternion.identity) as GameObject;
		ConnectNode (newNode);
//			lastNode.AddComponent<CharacterJoint> ();
//			lastNode.GetComponent<CharacterJoint> ().connectedBody = newNode.GetComponent<Rigidbody> ();
//			lastNode.AddComponent<DrawLineToTarget> ().t = newNode.transform;
//			lastNode.tag = "Untagged";
//			lastNode = newNode;
//			lastNode.GetComponent<Rigidbody> ().AddForce (lastNode.GetComponent<Rigidbody> ().velocity * 10);
//			starCount++;
//		newNode.transform.parent = transform;
	}

	void AttachToTail(){
		Collider[] newNodes = Physics.OverlapSphere (deltaPoint, lastNode.GetComponent<SphereCollider>().radius);
		GameObject newNode = null;
		foreach (Collider c in newNodes) {
			if (c.tag == "seed") {
				newNode = c.gameObject;
				break;
			}
		}
		if (newNode) {
			
			ConnectNode (newNode);
		}
	}

	public void ConnectNode(GameObject newNode){
//		if (starCount < maxJoints) {
			Instantiate (emptyNode, newNode.transform.position, Quaternion.identity);
			newNode.transform.position = lastNode.transform.position - (lastNode.transform.up * maxDistance);
			newNode.transform.up = lastNode.transform.up;
			newNode.transform.parent = transform;
			CharacterJoint j = lastNode.AddComponent<CharacterJoint> ();
			j.autoConfigureConnectedAnchor = true;
			j.axis = newNode.transform.position - lastNode.transform.position;
			j.connectedAnchor = newNode.transform.position - lastNode.transform.position;
			j.anchor = Vector3.zero;
			j.connectedBody = newNode.GetComponent<Rigidbody> ();
			lastNode.GetComponent<nodeInfo> ().nextNode = newNode.GetComponent<nodeInfo> ();
			lastNode.GetComponent<nodeInfo> ().index = starCount;
			newNode.GetComponent<nodeInfo> ().attached = true;
			//			newNode.GetComponent<Rigidbody> ().drag = 0.5f + ((float)starCount / (float)maxJoints);

			SoftJointLimitSpring s = new SoftJointLimitSpring ();
			s.spring = spring;
			s.damper = 0;
			SoftJointLimit swing1 = new SoftJointLimit ();
			SoftJointLimit swing2 = new SoftJointLimit ();
			swing1.limit = highLimit;
			swing2.limit = -lowLimit;

			j.swing1Limit = swing1;
			j.swing2Limit = swing2;
			j.swingLimitSpring = s;
			j.swingAxis = Vector3.up;

			lastNode = newNode;
			lastNode.tag = "Untagged";
			//		lastNode.GetComponent<Rigidbody> ().AddForce (lastNode.GetComponent<Rigidbody> ().velocity * 10);
			if (starCount % 4 == 0) {
				lastNode.transform.GetChild (0).gameObject.SetActive (true);
			}
			GetComponent<MovePlayer3D> ().speed++;
			starCount++;

			GetComponentInChildren<SpawnStars> ().SpawnStar ();
//			lastNode.GetComponentInChildren<TextMesh> ().text = starCount.ToString ();
	}

	public void SpawnSun(Vector3 pos, float size){
		GameObject newSun = Instantiate (sun, pos, Quaternion.identity) as GameObject;
		newSun.transform.localScale += (Vector3.one * (size - (sun.GetComponent<SphereCollider> ().radius * 2)))/(sun.GetComponent<SphereCollider> ().radius * 2);
	}

}
