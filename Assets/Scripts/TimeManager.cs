using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class TimeManager : MonoBehaviour {
    public int day = 1, hour = 0, min = 0, sec = 0;
    float y=0;
	void OnGUI () {
			GUI.color = Color.green;
        GUI.skin.label.fontSize = Screen.width / 20;
        GUI.Label (new Rect (Screen.width*0.67f, Screen.height*0.005f-5, 600, 100), "Day " + day + " " + hour + ":" + min + ":" + sec);
}

    void FixedUpdate()
    {
        y += Time.deltaTime;
        if (y >= 1) {
            if (sec < 59) sec++;
            else { sec = 0; min++; };
            if (min == 59){ min = 0; hour++; }
            if (hour == 23){ hour = 0; day++; };
            y = 0; }
    }

}