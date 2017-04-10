using UnityEngine;
using System.Collections;

public class  SpawnPlanets: MonoBehaviour {
	public float initDistance;
	public float minSpeed, maxSpeed;
	public float childDistanceMult;
	public GameObject planet;
	public Sprite[] planets;
	public AudioClip[] sounds;
	public int planetMin, planetMax;
	public float minSize, maxSize;
	public float minTint, maxTint;
	public bool recursion;
	// Use this for initialization
	void Start () {
		
		int planetAmount = Random.Range (planetMin, planetMax);
		Transform lastPlanet = transform;

		for(int i = 1; i < planetAmount; i++){
			Vector2 newPos = Random.insideUnitCircle.normalized * ((i/childDistanceMult) + initDistance);
			Vector3 curPosition = transform.position;
			if (recursion) {
				curPosition = lastPlanet.position;
			}
			GameObject newPlanet = (GameObject)Instantiate (planet, new Vector3(newPos.x, newPos.y, 0) + curPosition, Quaternion.identity);
			newPlanet.GetComponent<RotateAroundParent> ().speed = Random.Range(minSpeed, maxSpeed)/((float)i/2);
//			newPlanet.transform.Rotate (0, 0, Random.Range (0, 360));
			newPlanet.GetComponent<SpriteRenderer> ().sprite = planets [Random.Range (0, planets.Length)];
			float col = Random.Range (minTint, maxTint);
			float scale = Random.Range (minSize, maxSize) + ((float)i/planetAmount);
			newPlanet.transform.localScale *= scale;
			newPlanet.GetComponent<SpriteRenderer> ().color = new Color (col, col, col);
			newPlanet.transform.parent = lastPlanet;

			if (recursion) {
				lastPlanet = newPlanet.transform;
			}

			GameObject.Find ("Player").transform.position = newPlanet.transform.position + transform.up;
			GameObject.Find ("Player").GetComponent<PlayerControl> ().transform.parent = newPlanet.transform;
		}
			
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
