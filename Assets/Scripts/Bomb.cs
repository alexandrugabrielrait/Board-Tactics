using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{

    public MapManager mapager;
    int xx, yy;

    public void Explode()
    {
        xx = GetComponent<Unit>().xx;
        yy = GetComponent<Unit>().yy;
        if (xx == -1 || yy == -1) return;
        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            if (unit.xx == xx && unit.yy == yy && unit != GetComponent<Unit>())
                if (unit.type != 5 && unit.type != 12)
                {
                    unit.TakeDamage(GetComponent<Unit>().attack);
                }
        }
        GetComponent<Unit>().TakeDamage(49);
    }
}
