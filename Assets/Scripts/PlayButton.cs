using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour {

    public string level;

    void OnMouseDown()
    {
        Application.LoadLevel(level);
    }
}
