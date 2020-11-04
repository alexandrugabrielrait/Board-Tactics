using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{

    public TurnManager manager;
    public MapManager mapager;
    bool AI = false;
    float t = 0;
    bool waited = false;
    public int level;
    Unit unit;

    // Use this for initialization
    void Start()
    {
        transform.position = new Vector3(manager.multiple.transform.position.x, manager.multiple.transform.position.y, transform.position.z);
        switch (level)
        {
            case 1:
                manager.color0 = Color.blue;
                manager.color1 = Color.red;
                manager.base0.xx = 1;
                manager.base0.yy = 1;
                manager.base1.xx = 8;
                manager.base1.yy = 8;
                mapager.selected = manager.base0;
                manager.gain0 = 10;
                manager.resource0 = 30;
                CreateUnit(mapager.tank, 1, 2, 1);
                CreateUnit(mapager.tank, 2, 1, 1);
                CreateUnit(mapager.tank, 2, 2, 1);
                break;
        }
    }

    void CreateUnit(Unit type, int xx, int yy, int player)
    {
        unit = (Unit)Instantiate(type, new Vector3(xx - 3.5f, yy - 2.5f, transform.position.z), transform.rotation); unit.player = player; unit.xx = xx; unit.yy = yy; unit.name = type.name;
    }

    /*public void AITurn()
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
    }*/

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