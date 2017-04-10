using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  CrossOutText : MonoBehaviour {
	public GameObject graphic;
	GameObject newGraphic;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnMouseOver(){
//		GetComponent<TextFormatting> ().fade = true;
		newGraphic = (GameObject)Instantiate(graphic, GetComponent<MeshRenderer>().bounds.center, Quaternion.identity);
		newGraphic.transform.localScale = GetComponent<MeshRenderer> ().bounds.extents/2;
		newGraphic.transform.parent = this.transform;
		GetComponent<Collider2D> ().isTrigger = false;
		GetComponent<TextStyling> ().delete = true;
		GetComponent<TextStyling> ().speed = 0.5f;
//		Destroy (newGraphic);
		Destroy (this);
	}
}
