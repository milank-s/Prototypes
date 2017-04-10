using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodeInfo : MonoBehaviour {

	public nodeInfo nextNode;
	public int index;
	public GameObject linePrefab;
	public bool attached = false;
	nodeInfo minXNode, maxXNode, minYNode, maxYNode;
	CreateNodes player;

	void Start(){
		player = GameObject.Find ("Player").GetComponent<CreateNodes> ();
	}

	public void OnTriggerEnter(Collider col){

		if (col.gameObject.GetComponent<nodeInfo> () && col.gameObject.GetComponent<nodeInfo>().attached && attached) {
			GetComponent<LineRenderer> ().enabled = true;
			nodeInfo info = col.gameObject.GetComponent<nodeInfo> ();
			int difference = Mathf.Abs(index - info.index);
			if (difference < 10) {
				return;
			}
			nodeInfo node;
			node = info.index > index ? this : info;

			float minX = node.transform.position.x;
			float maxX = node.transform.position.x;
			float minY = node.transform.position.y;
			float maxY = node.transform.position.y;

			minXNode = node;
			maxXNode = node;
			minYNode = node;
			maxYNode = node;

			GetComponent<LineRenderer> ().numPositions = difference;
			Vector3[] positions  = new Vector3[difference];

			positions [0] = transform.position;

			for (int i = 1; i < difference; i++) {
				if (node.nextNode == null) {
					break; 
				} else {
					node = node.nextNode;
				}

				if (node.transform.position.x > maxXNode.transform.position.x) {
					maxXNode = node;
				}
				if (node.transform.position.x < minXNode.transform.position.x) {
					minXNode = node;
				}
				if (node.transform.position.y > maxYNode.transform.position.y) {
					maxYNode = node;
				}
					if (node.transform.position.y < minYNode.transform.position.y) {
					minYNode = node;
				}
				positions [i] = node.transform.position;
			}

			GetComponent<LineRenderer> ().SetPositions(positions);

			GameObject newLine = Instantiate (linePrefab, transform.position, Quaternion.identity);
			newLine.GetComponent<LineRenderer> ().numPositions = difference;
			newLine.GetComponent<LineRenderer> ().SetPositions (positions);

			Collider[] trappedObjects;

			bool x = Mathf.Abs(maxX - minX) > Mathf.Abs(minY - maxY) ? true : false;

			float length;
			float width;

			if (x) {
				trappedObjects = Physics.OverlapCapsule(maxXNode.transform.position, minXNode.transform.position, Vector3.Distance (maxYNode.transform.position, minYNode.transform.position) / 4);
				width = Vector3.Distance (maxYNode.transform.position, minYNode.transform.position);
				length = Vector3.Distance (maxXNode.transform.position, minXNode.transform.position); 
			} else {
				trappedObjects = Physics.OverlapCapsule(maxYNode.transform.position, minYNode.transform.position, Vector3.Distance (maxXNode.transform.position, minXNode.transform.position) / 4);
				length = Vector3.Distance (maxYNode.transform.position, minYNode.transform.position);
				width = Vector3.Distance (maxXNode.transform.position, minXNode.transform.position); 
			}

			minXNode.GetComponent<LineRenderer> ().enabled = true;
			minXNode.GetComponent<LineRenderer>().SetPosition(0, minXNode.transform.position);
			minXNode.GetComponent<LineRenderer>().SetPosition(1, maxXNode.transform.position);


			minYNode.GetComponent<LineRenderer> ().enabled = true;
			minYNode.GetComponent<LineRenderer>().SetPosition(0, minYNode.transform.position);
			minYNode.GetComponent<LineRenderer>().SetPosition(1, maxYNode.transform.position);

			bool existingSun = false;
			GameObject Sun = null;
			foreach (Collider c in trappedObjects) {
				if (c.tag == "seed") {
					player.ConnectNode (c.gameObject);
				} 
				if (c.tag == "sun") {
					Sun = c.gameObject;
				}
			}

			if (width > player.sun.GetComponent<SphereCollider>().radius * 2 && length > player.sun.GetComponent<SphereCollider>().radius * 2) {
				if (Sun) {
					Destroy (Sun);
				}
				player.SpawnSun (minXNode.transform.position + (maxXNode.transform.position - minXNode.transform.position)/2, width);
			}

			Camera.main.GetComponent<AudioSource> ().volume = 0.33f;
		}
	}
	public void OnTriggerExit(Collider col){
		GetComponent<LineRenderer> ().enabled = false;
		if (minYNode) {
			minYNode.GetComponent<LineRenderer> ().enabled = false;
			minXNode.GetComponent<LineRenderer> ().enabled = false;
		}
	}
}
