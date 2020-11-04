using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class HighScoreDisplay : MonoBehaviour {
	int score;
	void OnGUI () {
			GUI.color = Color.black;
			score = GameObject.FindObjectOfType<ScoreManager> ().score;
			if (score == 1) {
			GUI.Label (new Rect (0, Screen.height*0.005f-5, 600, 100), "High Score: " + score + " second");
			} else
			GUI.Label (new Rect (0, Screen.height*0.005f-5, 600, 100), "High Score: " + score + " seconds");
		}

}