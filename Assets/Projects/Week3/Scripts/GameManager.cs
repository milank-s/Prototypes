using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject Player1;
	public GameObject Player2;
	public float size;

	public Maze mazePrefab;

	private Maze mazeInstance;

	private void Start () {
		BeginGame();
	}
	
	private void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			RestartGame();
		}
	}

	private void BeginGame () {
		mazePrefab.GetComponent<Maze> ().size.x = (int)size;
		mazePrefab.GetComponent<Maze> ().size.z = (int)size;
		foreach (BoxCollider b in mazePrefab.GetComponentsInChildren<BoxCollider>()) {
			Vector3 boxSize = new Vector3 (size, 1, size);
			b.size = boxSize;
		}
		mazeInstance = Instantiate(mazePrefab) as Maze;
		StartCoroutine(mazeInstance.Generate());
		Player1.transform.position = new Vector3 (-size/2 + 0.5f, 0, size/2 - 0.5f);
		Player2.transform.position = new Vector3 (size/2 - 0.5f, 0, -size/2 + 0.5f);

		GetComponent<Writer> ().SetAndSplitString (size.ToString ());
		GetComponent<Writer> ().BroadcastMessage ("UsedByPlayer");
	}

	private void RestartGame () {
		size += 1;
		StopAllCoroutines();
		Destroy(mazeInstance.gameObject);
		BeginGame();
	}
}