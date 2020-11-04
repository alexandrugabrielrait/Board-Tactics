using UnityEngine;
using System.Collections;

public class GameModeButtons : MonoBehaviour {

    public TurnManager manager;
    public GameObject arrow;
    public int mode=0;

	// Use this for initialization
	void OnMouseDown () {
        manager.mode = mode;
    }
	
	// Update is called once per frame
	void Update () {
        if (manager.mode == mode)
        {
            arrow.transform.position = new Vector3(arrow.transform.position.x, this.transform.position.y, arrow.transform.position.z);
            if (mode == -2)
            {
                arrow.GetComponent<SpriteRenderer>().flipX = true;
                arrow.GetComponent<SpriteRenderer>().flipY = true;
            }
            else
            {
                arrow.GetComponent<SpriteRenderer>().flipX = false;
                arrow.GetComponent<SpriteRenderer>().flipY = false;
            }
        }
        }
}
