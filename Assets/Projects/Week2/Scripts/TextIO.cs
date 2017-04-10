using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TextIO : MonoBehaviour {

	public float speed;
	public Font[] fonts;

	public int lineLength = 20;
	public TextMesh display;

	public TextAsset sourceText;
	public TextAsset injectedText;

	public string lineBreak;

	string[] formattedText;

	string[][] canto;
	string[] line;
	string[] insertedText;

	float time;
	public float interval = 0.25f; //interval to change words at
	int wordCount; //how far the words are going
	int lineCount;
	float letterSpacing = 1;
	Vector3 lastWord;
	/*
	 * box of 600x400 fits 1254 chara at 14pt 2 in Space Mono
	 * 66 chara per line, 19 lines
	 * 
	 */

	// Use this for initialization
	void Start () {
		formattedText = sourceText.text.Split("\n"[0]);
		insertedText = injectedText.text.Split (new char[] { ' ' });
		canto = new string[formattedText.Length][];
		lastWord = transform.position;

		bool scanning = true;
		int j = 0;
		int k = 0;

		while (scanning) {
			line = formattedText [j].Split(new char[] { ' ' });
			line [Random.Range (0, line.Length)] += " " + insertedText [j % insertedText.Length];
//				int injectionIndex = line [k].Length / insertedText.Length;
//				Debug.Log (line);
//				for(int i = 0; i < insertedText.Length; i++){
//					line [i * injectionIndex] += insertedText [i];
//				}
			canto[j] = line;

//				k++;

//			else{
//				if (formattedText [j].Length > 5) {
//					canto[k] += injectedText;
//				}
//				canto [k] += formattedText [j];
//			}

			j++;

			if (j >= formattedText.Length) {
				scanning = false;
			}
		}
		GetComponent<Writer> ().SetString (canto [0]);
	}

	// Update is called once per frame
	void Update () {
		if (interval < 0){
			NextCanto ();
			interval = speed;
		}
		interval -= Time.deltaTime;
		Vector3 tempPosition = transform.position;
		tempPosition.x = GameObject.Find ("Chester").transform.position.x - 10;
		transform.position = tempPosition;

	}

	public void NextCanto(){
			

		if (wordCount >= canto [lineCount % canto.Length].Length) {
			wordCount = 0;
			lineCount++;
			lastWord = transform.position;
			GetComponent<Writer> ().SetString (canto [lineCount % canto.Length]);
			lastWord.y -= (float)lineCount;
		} else {
			GameObject newWord = GetComponent<Writer> ().CreateWord (lastWord);
			Font curFont = fonts [Random.Range (0, fonts.Length)];
			newWord.GetComponent<TextMesh> ().font = curFont;
			curFont = fonts [Random.Range(0, fonts.Length)];
			if (newWord.GetComponent<TextMesh> ().font == curFont) {
			}
			newWord.GetComponent<Renderer> ().sharedMaterial = curFont.material;
			newWord.AddComponent<BoxCollider2D> ();
			newWord.GetComponent<BoxCollider2D> ().isTrigger = true;
			lastWord.x += newWord.GetComponent<BoxCollider2D> ().bounds.size.x + letterSpacing;
			if(canto[lineCount % canto.Length][wordCount].Contains(lineBreak)){
				newWord.transform.localScale *= 5;	
				curFont = fonts [4];
				newWord.GetComponent<TextMesh> ().font = curFont;
				newWord.GetComponent<Renderer> ().sharedMaterial = curFont.material;
			}
			wordCount++;
		}
			
	}
}
