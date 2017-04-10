using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour {

	public TextMesh score1, score2, timer, player1, player2;
	public float levelTime;
	public int scoreTeam1, scoreTeam2;
	public MovePlayer p1, p2;

	// Use this for initialization
	void Start () {
		timer.text = "levelTime" + ":" + "00";
	}
	
	// Update is called once per frame
	void Update () {
		float time = levelTime - Time.timeSinceLevelLoad;
		if (time <= 0) {
			EndGame ();
		} else {
			string minutes = Mathf.Floor (time / 60).ToString ("0");
			string seconds = (time % 60).ToString ("00");
			timer.text = minutes + ":" + seconds;
		}
		score1.transform.localScale = Vector3.Lerp(score1.transform.localScale, Vector3.one/2, Time.deltaTime * 10);
		score2.transform.localScale = Vector3.Lerp(score2.transform.localScale, Vector3.one/2, Time.deltaTime * 10);
	}

	public void updateScore(int team, int amount){
		if (team == 2) {
//			scoreTeam2 = GameObject.FindGameObjectsWithTag ("team2").Length;
			scoreTeam2 += amount;
			score2.transform.localScale = Vector3.one;
			score2.text = scoreTeam2.ToString ();
		} else {
			scoreTeam1 += amount;
//			scoreTeam1 = GameObject.FindGameObjectsWithTag ("team1").Length;
			score1.transform.localScale = Vector3.one;
			score1.text = scoreTeam1.ToString ();
		}
	}
	void EndGame(){
		if (scoreTeam1 > scoreTeam2) {
			player1.text = "I am full. I have more fish \n you cannot contend with \n my vast amounts of mackerel";
		} else {
			player2.text = "you reap fallow fields \n and I sow my oats";
		}
		timer.text = "R to Replay";
		p1.enabled = false;
		p2.enabled = false;
	}

}
