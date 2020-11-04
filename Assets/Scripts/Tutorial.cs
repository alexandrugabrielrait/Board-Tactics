using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    public TurnManager manager;
    public Unit rifle;
    bool AI = false;
    float t = 0;
    bool waited = false;

    // Use this for initialization
    void Start()
    {
        transform.position = new Vector3(manager.multiple.transform.position.x, manager.multiple.transform.position.y, transform.position.z);
        manager.color0 = Color.blue;
        manager.color1 = Color.red;
        manager.mapager.bases1 = 2;
        manager.base0.xx = 1;
        manager.base0.yy = 1;
    }

    public void AITurn()
    {
        int w;
        AI = true;
        if (manager.moves > 0 && rifle != null)
        {
            foreach (Unit unit in FindObjectsOfType<Unit>())
            {
                if (Mathf.Abs(unit.xx - rifle.xx) <= 2 && Mathf.Abs(unit.yy - rifle.yy) <= 2)
                    if (unit.type != 5 && unit.type != 12 && unit.type != 14)
                    {
                        waited = false;
                        t = 2;
                        while (waited == false) w = 1;
                        unit.TakeDamage(rifle.attack); manager.moves--;
                    }
            }
        }
        AI = false;
        manager.Passed();
    }

    void FixedUpdate()
    {
        if (AI == true) manager.mapager.selected = null;
        if (t > 0)
        {
            t -= Time.deltaTime;
            if (t <= 0)
            {
                waited = true;
            }
        }
    }
}