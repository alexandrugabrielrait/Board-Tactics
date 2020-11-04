using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonVictory : MonoBehaviour {

    public GameObject Color;
    public TurnManager manager;

	void OnMouseDown () {
        //SceneManager.LoadScene("Forest");
    }
	void FixedUpdate()
    {
        if(manager.win==true)
            transform.position = new Vector2(manager.transform.position.x + 0.68f, manager.transform.position.y + -0.03f);
    }
}
