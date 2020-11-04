using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    public int xx;
    public int yy;
    public TurnManager manager;

    public void Death()
    {
        this.transform.position = new Vector3(99, 99, 99);
    }

    // Update is called once per frame
    void Update() {
        if (GetComponent<TankController>() != null) {
            xx = GetComponent<TankController>().xx;
        yy = GetComponent<TankController>().yy; }
    }
}
