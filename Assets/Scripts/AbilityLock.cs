using UnityEngine;
using System.Collections;

public class AbilityLock : MonoBehaviour {

    public Vector3 pos;
    public TurnManager manager;
    public int player;

	// Update is called once per frame
	void Update () {
        //if ((player == 0&&manager.timer0 <= 0)||(player==1&&manager.timer1<=0)) transform.position = pos;
	}
}
