using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOver : MonoBehaviour {
	public Color TextColor;
	public GameObject Text;
	// Use this for initialization
	void Start () {
		EndGame ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void EndGame(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}
