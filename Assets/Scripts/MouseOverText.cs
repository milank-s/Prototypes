using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverText : MonoBehaviour {
	public string[] text;
	public Color[] color;

	TextMesh t;
	// Use this for initialization
	void Start () {
		t = GameObject.Find("Text").GetComponent<TextMesh> ();

		foreach (Transform t in gameObject.GetComponentsInChildren<Transform>()) {
			if (t.gameObject.GetComponent<MouseOverText> () == null) {
				t.gameObject.AddComponent<MouseOverText> ();
				t.gameObject.GetComponent<MouseOverText> ().text = text;
				t.gameObject.GetComponent<MouseOverText> ().color = color;
			}
		}

	}
	public void OnMouseEnter(){
		t.text = gameObject.name;
		t.color = color[Random.Range(0, color.Length)];
	}
	public void OnMouseOver(){
		t.gameObject.GetComponentInChildren<Light> ().transform.position = transform.position + GetComponent<CapsuleCollider> ().center;
	}

	public void OnMouseExit(){
		t.text = "";
	}
}
