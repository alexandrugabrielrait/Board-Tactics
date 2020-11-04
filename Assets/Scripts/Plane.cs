using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour {

    public Unit below;

    Unit FindTarget()
    {
        Unit uPlane = this.GetComponent<Unit>();
        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            if (unit.xx == uPlane.xx && unit.yy == uPlane.yy)
                if (unit.type != 5)
                    return unit;
        }
        return null;
    }

    // Update is called once per frame
    void Update () {
        below = FindTarget();
        
	}
}
