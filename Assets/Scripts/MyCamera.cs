using UnityEngine;
using System.Collections;

public class MyCamera : MonoBehaviour {

	public float y = 4f;

	void Start () {
		//Screen.orientation = ScreenOrientation.Portrait;
		//GameObject.FindObjectOfType<ScoreManager> ().Load ();
	}

	void FixedUpdate () {
		y -= Time.deltaTime;
		if (y <= 2) {
			this.transform.position=new Vector3(0, -100, -10);
		}
		if (y <= 0) {
			//Application.LoadLevel("Forest");
		}
	}
}
