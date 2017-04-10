using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDeleteThis : MonoBehaviour {

	// Use this for initialization

	private static DontDeleteThis instance = null;

	public static DontDeleteThis Instance {
		get { 
			return instance;
		}
	}

	void Awake () {
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
		} else {
			instance = this;
		}

		DontDestroyOnLoad (this);
	}
}
