using UnityEngine;
using System.Collections;

public class MapManager : MonoBehaviour
{

    public int[,] isBlocked = new int[10, 10];
    public int[,] hasPlane = new int[10, 10];
    public int[,] highlight = new int[10, 10];
    public HiglightLand firstLand;
    public GameObject[,] block = new GameObject[10, 10];
    public GameObject select;
    public TurnManager manager;
    public Unit selected;
    public Unit rifleman;
    public Unit engineer;
    public Unit transport;
    public Unit tank;
    public Unit bomber;
    public Unit drone;
    public int bases0 = 1;
    public int bases1 = 1;
    public string rusherDesc;
    public string demolitionistDesc;
    public string missileTankDesc;
    public string investDesc;
    public string nukeDesc;
    public string moveDesc;
    public string attackDesc;
    public string repairDesc;
    public string loadDesc;
    public string selectBelowDesc;
    public string baseDesc;
    public string saboteurDesc;
    public string sabotageDesc;
    public string riderDesc;
    public string rangerDesc;
    public string zapDesc;
    public string rocketLauncherDesc;
    public string attackAirDesc;
    public string bomberDesc;
    public string tankMoveDesc;
    public string tankAttackDesc;
    public string tankStealthDesc;
    public string ballisticDesc;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i <= 9; i++) { isBlocked[0, i] = isBlocked[9, i] = isBlocked[i, 0] = isBlocked[i, 9] = 4; }
        //isBlocked[tank0.xx, tank0.yy] = isBlocked[tank1.xx, tank1.yy] = 1;
        //block[tank0.xx, tank0.yy] = tank0.gameObject;
        //block[tank1.xx, tank1.yy] = tank1.gameObject;
    }

    void Update()
    {
        if (manager.win == true) return;
        for (int i = 0; i < 9; i++)
            for (int j = 0; j < 9; j++) { isBlocked[i, j] = hasPlane[i, j] = 0; }
        if (manager.ability == 0) ClearHighlights();
        foreach (Unit unit in GameObject.FindObjectsOfType<Unit>())
        {
            if (unit.xx != -1 && unit.yy != -1)
            {
                if (unit.type == 1 || unit.type == 2 || unit.type == 6 || unit.type == 7 || unit.type == 9 || unit.type == 11||unit.type==13)
                    isBlocked[unit.xx, unit.yy] = 1;//infantry
                if (unit.type == 3|| unit.type == 10)
                    isBlocked[unit.xx, unit.yy] = 2;//car/horse
                if (unit.type == 0 || unit.type == 4 || unit.type == 8)
                    isBlocked[unit.xx, unit.yy] = 3;//tank/building
                if (unit.type == 5||unit.type==14)
                    hasPlane[unit.xx, unit.yy] = 1;//plane
                if (unit.type == 12)
                    hasPlane[unit.xx, unit.yy] = 2;//drone
            }
        }
        //Debug.Log(isBlocked[3, 3]);
        if (selected != null)
        {
            select.transform.position = selected.transform.position;
            if (selected.player == manager.player)
                select.GetComponent<SpriteRenderer>().color = new Color(0.616f, 1, 0, 1);
            else select.GetComponent<SpriteRenderer>().color = new Color(0.19f, 0, 0, 1);
        }


        switch (selected.type)
        {
            case 0:
                if (manager.ability == 0) break;
                if (manager.ability < 5)
                    HighlightPath(selected.xx, selected.yy, 2, 0, true);
                if (manager.ability == 5)
                    HighlightPath(selected.xx, selected.yy, 2, 4, true);
                if (manager.ability == 6) { if ((selected.player == 0 && manager.gain0 < 10) || (selected.player == 1 && manager.gain1 < 10)) highlight[selected.xx, selected.yy] = 1;
                    else
                    {
                        foreach (Unit unit2 in FindObjectsOfType<Unit>())
                        {
                            if (unit2.type == 0 && (unit2.xx != -1 || unit2.yy != -1))
                                for (int i = unit2.xx - 2; i <= unit2.xx + 2; i++)
                                    for (int j = unit2.yy - 2; j <= unit2.yy + 2; j++)
                                        if (!(i < 0 || i > 8 || j < 0 || j > 9))
                                        {
                                            if (i == unit2.xx && j == unit2.yy)
                                                highlight[i, j] = 1;
                                            if (highlight[i, j] != 1)
                                                highlight[i, j] = 2;
                                        }
                        }
                    }
                }
                break;
            case 1:
                if (manager.ability == 0) break;
                if (manager.ability == 1)
                    HighlightPath(selected.xx, selected.yy, selected.move, 1, true);
                if (manager.ability == 2)
                    HighlightPath(selected.xx, selected.yy, selected.range, 5, true);
                if (manager.ability == 3)
                    HighlightPath(selected.xx, selected.yy, 1, 0, true);
                if (manager.ability == 4)
                    HighlightPath(selected.xx, selected.yy, selected.move, 1, true);
                if (manager.ability == 5)
                { int x2 = 0, y2 = 0;
                    for (int i = 1; i <= 8; i++)
                    {
                        switch (i)
                        {
                            case 1: x2 = 2; y2 = 1; break;
                            case 2: x2 = 2; y2 = -1; break;
                            case 3: x2 = -2; y2 = -1; break;
                            case 4: x2 = -2; y2 = 1; break;
                            case 5: x2 = 1; y2 = 2; break;
                            case 6: x2 = 1; y2 = -2; break;
                            case 7: x2 = -1; y2 = -2; break;
                            case 8: x2 = -1; y2 = 2; break;
                        }
                        if (selected.xx + x2 > 0 && selected.xx + x2 < 9 && selected.yy + y2 > 0 && selected.yy + y2 < 9)
                            if ((isBlocked[selected.xx + x2, selected.yy + y2] == 0 || isBlocked[selected.xx + x2, selected.yy + y2] == 1))
                                highlight[selected.xx + x2, selected.yy + y2] = 1;
                    }
                }
                if (manager.ability == 6)
                    HighlightPath(selected.xx, selected.yy, selected.move, 1, true);
                break;
            case 2:
                if (manager.ability == 0) break;
                if (manager.ability == 1)
                    HighlightPath(selected.xx, selected.yy, selected.move, 1, true);
                if (manager.ability == 2)
                    HighlightPath(selected.xx, selected.yy, selected.range, 5, true);
                if (manager.ability == 3)
                    HighlightPath(selected.xx, selected.yy, 1, 1, true);
                if (manager.ability == 4)
                    HighlightPath(selected.xx, selected.yy, 1, 1, true);
                if (manager.ability == 6&&selected.drones>0) highlight[selected.xx, selected.yy] = 1;
                break;
            case 3:
                if (manager.ability == 0) break;
                if (manager.ability == 1)
                    HighlightPath(selected.xx, selected.yy, selected.move, 2, true);
                if (manager.ability == 2)
                    HighlightPath(selected.xx, selected.yy, selected.range, 5, true);
                if (manager.ability >= 3 && manager.ability <= 5)
                    HighlightPath(selected.xx, selected.yy, selected.range, 0, true);
                if (manager.ability == 6) highlight[selected.xx, selected.yy] = 1;
                break;
            case 4:
                if (manager.ability == 0) break;
                if (manager.ability == 1)
                    HighlightPath(selected.xx, selected.yy, selected.move, 3, true);
                if (manager.ability == 2)
                    HighlightPath(selected.xx, selected.yy, selected.range, 5, true);
                if (manager.ability == 3) highlight[selected.xx, selected.yy] = 1;
                if (manager.ability == 4) highlight[selected.xx, selected.yy] = 1;
                if (manager.ability == 5) highlight[selected.xx, selected.yy] = 1;
                if (manager.ability == 6) highlight[selected.xx, selected.yy] = 1;
                break;
            case 5:
                if (manager.ability == 0) break;
                if (manager.ability == 1)
                    for (int i = 0; i < 9; i++)
                        for (int j = 0; j < 9; j++)
                        {
                            if (hasPlane[i, j] == 1) highlight[i, j] = 0;
                            else highlight[i, j] = 1;

                        }
                if (manager.ability == 2)
                    HighlightPath(selected.xx, selected.yy, selected.range, 5, true);
                if (manager.ability == 3)
                    highlight[selected.xx, selected.yy] = 1;
                if (manager.ability == 6)
                    highlight[selected.xx, selected.yy] = 1;
                break;
            case 6:
                if (manager.ability == 0) break;
                if (manager.ability == 1)
                    HighlightPath(selected.xx, selected.yy, selected.move, 1, true);
                if (manager.ability == 2) HighlightPath(selected.xx, selected.yy, selected.range, 5, true);
                if (manager.ability == 6 && selected.drones > 0) highlight[selected.xx, selected.yy] = 1;
                break;
            case 7:
                if (manager.ability == 0) break;
                if (manager.ability == 1)
                    HighlightPath(selected.xx, selected.yy, selected.move, 0, true);
                if (manager.ability == 2)
                    HighlightPath(selected.xx, selected.yy, selected.range, 5, true);
                break;
            case 8:
                if (manager.ability == 0) break;
                if (manager.ability == 1)
                    HighlightPath(selected.xx, selected.yy, selected.move, 3, true);
                if (manager.ability == 2)
                {
                    for (int i = -1; i <= 1; i++)
                        for (int j = -1; j <= 1; j++)
                            highlight[selected.xx + i, selected.yy + j] = 1;
                }
                if (manager.ability == 3)
                    foreach (Unit unit2 in FindObjectsOfType<Unit>())
                    {
                        if (unit2.type != 0 && (unit2.xx != -1 || unit2.yy != -1))
                            highlight[unit2.xx, unit2.yy] = 1;
                    }
                if (manager.ability == 4) highlight[selected.xx, selected.yy] = 1;
                if (manager.ability == 5) highlight[selected.xx, selected.yy] = 1;
                if (manager.ability == 6) highlight[selected.xx, selected.yy] = 1;
                break;
            case 10:
                if (manager.ability == 0) break;
                if (manager.ability == 1)
                {
                    int x2 = 0, y2 = 0;
                    for (int i = 1; i <= 8; i++)
                    {
                        switch (i)
                        {
                            case 1: x2 = 2; y2 = 1; break;
                            case 2: x2 = 2; y2 = -1; break;
                            case 3: x2 = -2; y2 = -1; break;
                            case 4: x2 = -2; y2 = 1; break;
                            case 5: x2 = 1; y2 = 2; break;
                            case 6: x2 = 1; y2 = -2; break;
                            case 7: x2 = -1; y2 = -2; break;
                            case 8: x2 = -1; y2 = 2; break;
                        }
                        if (selected.xx + x2 > 0 && selected.xx + x2 < 9 && selected.yy + y2 > 0 && selected.yy + y2 < 9)
                            if ((isBlocked[selected.xx + x2, selected.yy + y2] == 0 || isBlocked[selected.xx + x2, selected.yy + y2] == 1))
                                highlight[selected.xx + x2, selected.yy + y2] = 1;
                    }
                }
                if (manager.ability == 2)
                    HighlightPath(selected.xx, selected.yy, selected.range, 5, true);
                break;
            case 11:
                if (manager.ability == 0) break;
                if (manager.ability == 1)
                    HighlightPath(selected.xx, selected.yy, selected.move, 1, true);
                if (manager.ability == 2)
                {
                    for (int j = 1; j <= 4;j++) { int h = 0, v = 0;
                        switch (j)
                        {
                            case 1: h = 1; v = 0; break;
                            case 2: h = -1; v = 0; break;
                            case 3: h = 0; v = 1; break;
                            default: h = 0; v = -1; break;
                        }
                        for (int i = 1; i <= 7; i++)
                        {
                            if (selected.xx + i * h > 0 && selected.xx + i * h < 9 && selected.yy + i * v > 0 && selected.yy + i * v < 9)
                                if (isBlocked[selected.xx + i * h, selected.yy + i * v] > 0) { highlight[selected.xx + i * h, selected.yy + i * v] = 1; i = 8; }
                        }
                    }
                }
                break;
            case 12:
                if (manager.ability == 0) break;
                if (manager.ability == 1)
                {
                    for (int j = 1; j <= 4;j++) { int h = 0, v = 0;
                        switch (j)
                        {
                            case 1: h = 1; v = 0; break;
                            case 2: h = -1; v = 0; break;
                            case 3: h = 0; v = 1; break;
                            default: h = 0; v = -1; break;
                        }
                        for (int i = 1; i <= 7; i++)
                        {
                            if (selected.xx + i * h > 0 && selected.xx + i * h < 9 && selected.yy + i * v > 0 && selected.yy + i * v < 9)
                                if (hasPlane[selected.xx + i * h, selected.yy + i * v] == 0) { highlight[selected.xx + i * h, selected.yy + i * v] = 1; }
                                else i = 8;
                        }
                    }
                }
                if (manager.ability == 2)
                    highlight[selected.xx, selected.yy] = 1;
                if (manager.ability == 6)
                    highlight[selected.xx, selected.yy] = 1;
                break;
            case 13:
                if (manager.ability == 0) break;
                if (manager.ability == 1)
                    HighlightPath(selected.xx, selected.yy, selected.move, 1, true);
                if (manager.ability == 2)
                    HighlightPath(selected.xx, selected.yy, selected.range, 5, true);
                if (manager.ability == 3)
                {
                    for (int i = -1; i <= 1; i++)
                        for (int j = -1; j <= 1; j++)
                            if(Mathf.Abs(i)+Mathf.Abs(j)!=2)
                            highlight[selected.xx + i, selected.yy + j] = 1;
                }
                break;
            case 14:
                if (manager.ability == 0) break;
                if (manager.ability == 1)
                    for (int i = 0; i < 9; i++)
                        for (int j = 0; j < 9; j++)
                        {
                            if (hasPlane[i, j] == 1) highlight[i, j] = 0;
                            else highlight[i, j] = 1;

                        }
                if (manager.ability == 2)
                    highlight[selected.xx, selected.yy] = 1;
                if (manager.ability == 6)
                    highlight[selected.xx, selected.yy] = 1;
                break;
            default:
                    if (manager.ability == 0) break;
                    if (manager.ability == 1)
                        HighlightPath(selected.xx, selected.yy, selected.move, 1, true);
                    if (manager.ability == 2)
                        HighlightPath(selected.xx, selected.yy, selected.range, 5, true);
                if (manager.ability == 6 && selected.drones > 0) highlight[selected.xx, selected.yy] = 1;
                //else highlight[selected.xx, selected.yy] = 1;
                break;
            }
    }

    public void ClearHighlights()
    {
        for (int i = 0; i < 9; i++)
            for (int j = 0; j < 9; j++)
            {
                highlight[i, j] = 0;
            }
    }

    public void HighlightPath(int x, int y, int squares, int type, bool original)
    //0 not blocked, 1 no collision, 2 crush infantry, 3 crush vehicles, 4 plane, 5 attack
    {
        if (x < 0 || x > 8 || y < 0 || y > 8) return;
        //Debug.Log(x + " " + y + " " + squares);
        if (squares < 0) return;
        if (isBlocked[x, y] == 4) return;//border
        if (isBlocked[x, y] == 1 && type <= 2 && type > 0 && original == false) { if (type == 2) highlight[x, y] = 1; return; }//infantry
        if (isBlocked[x, y] <= 2 && type <= 3 && type > 0 && original == false && isBlocked[x,y] >0) { if (type == 3) highlight[x, y] = 1; return; }//vehicle
        if (isBlocked[x, y] == 3 && type <= 4 && type > 0 && original == false) return;//building
        if (((isBlocked[x, y] == 0 && type == 0) || (isBlocked[x, y] > 0 && type == 5)  || 
        (type != 0 && type!=5)) && original== false && !( x == selected.xx && y == selected.yy)) highlight[x, y] = 1;
        if (hasPlane[x, y] == 1 && type == 4) highlight[x, y] = 0;
        HighlightPath(x + 1, y, squares - 1, type, false);
        HighlightPath(x - 1, y, squares - 1, type, false);
        HighlightPath(x, y + 1, squares - 1, type, false);
        HighlightPath(x, y - 1, squares - 1, type, false);
        if (type == 0||type==4) { HighlightPath(x + 1, y + 1, squares - 1, type, false);
            HighlightPath(x - 1, y - 1, squares - 1, type, false);
            HighlightPath(x + 1, y - 1, squares - 1, type, false);
            HighlightPath(x - 1, y + 1, squares - 1, type, false);
        }

    }

    /*public void KillObject(int x, int y)
    {
        TankController[] tanks = FindObjectsOfType(typeof(TankController)) as TankController[];
        foreach (TankController tanc in tanks)
        {
            if (tanc.xx == x && tanc.yy == y)
                /*if(tanc.country==3&tanc.mode==1)
                    switch (tanc.direction)
                    {
                        case -1: if (y <= 0)
                            {
                                tanc.Death(); isBlocked[tanc.xx, tanc.yy] = 0; block[tanc.xx, tanc.yy] = null;
                                tanc.Death();
                            } break;
                        case 0: if (x >= 0)
                            {
                                tanc.Death(); isBlocked[tanc.xx, tanc.yy] = 0; block[tanc.xx, tanc.yy] = null;
                                tanc.Death();
                            }
                            break;
                        case 1: if (y >= 0)
                            {
                                tanc.Death(); isBlocked[tanc.xx, tanc.yy] = 0; block[tanc.xx, tanc.yy] = null;
                                tanc.Death();
                            }
                            break;
                        case 2: if (x <= 0)
                            {
                                tanc.Death(); isBlocked[tanc.xx, tanc.yy] = 0; block[tanc.xx, tanc.yy] = null;
                                tanc.Death();
                            }; break;

                    }
            else
                    tanc.Death();
                    isBlocked[tanc.xx, tanc.yy] = 0;
                    block[tanc.xx, tanc.yy] = null;

        }
        Obstacle[] obstacles = FindObjectsOfType(typeof(Obstacle)) as Obstacle[];
        foreach (Obstacle obs in obstacles)
        {
            if (obs.xx == x && obs.yy == y)
                    obs.Death();
                    isBlocked[obs.xx, obs.yy] = 0;
                    block[obs.xx, obs.yy] = null;
                
        }
    }

    // Update is called once per frame
    public void TankMove(TankController tank, int x, int y)
    {
        if ((isBlocked[tank.xx + x, tank.yy + y] <= 1 && tank.country == 3) || isBlocked[tank.xx + x, tank.yy + y] == 0)
        {
            isBlocked[tank.xx, tank.yy] = 0;
            block[tank.xx, tank.yy] = null;
            if (tank.country == 3)
            {
                KillObject(tank.xx + x, tank.yy + y);
            }
            tank.xx += x;
            tank.yy += y;
            isBlocked[tank.xx, tank.yy]= 1;
            block[tank.xx, tank.yy] = tank.gameObject;
        }
    }

    void Attacking(TankController tank, int i, int j, int x, int y)
    {
        if ((x == 0 || y == 0) && i == 0 & j == 0) return;
        if ((x!=0&&y!=0)&&(i == 0 || j == 0)) return;
        if (tank.xx + i <= 0 || tank.yy - j <= 0 || tank.xx + i >= 9 || tank.yy - j >= 9) return;
        //Square mis = (Square)Instantiate(missle, new Vector3(tank.xx + i - 3.5f, tank.yy - j - 2.5f, tank.transform.position.z), transform.rotation);
        mis.isMissle = true;
        if (isBlocked[tank.xx + i, tank.yy - j] == 1)
        {
            if (block[tank.xx + i, tank.yy - j].GetComponent(typeof(TankController)) != null)
            {
                TankController tanc = (TankController)block[tank.xx + i, tank.yy - j].GetComponent<TankController>();
                if (tanc.country == 3)
                    if (tanc.mode == 1)
                    {

                        switch (tanc.direction)
                        {
                            case -1: if (j <= 0) KillObject(tanc.xx, tanc.yy); break;
                            case 2: if (i <= 0) KillObject(tanc.xx, tanc.yy); break;
                            case 1: if (j >= 0) KillObject(tanc.xx, tanc.yy); break;
                            default: if (i <= 0) KillObject(tanc.xx, tanc.yy); break;
                        }
                        return;
                    }
                KillObject(tanc.xx, tanc.yy);
            }
        }
    }

    public void TankAttack(TankController tank, int x, int y)
    {
        if (x < 0) {
            for (int i = 0; i >= x; i--)
                if (y < 0)
                    for (int j = 0; j >= y; j--) Attacking(tank, i, j, x, y);
                else for (int j = 0; j <= y; j++) Attacking(tank, i, j, x, y);
    }else for(int i = 0; i <= x; i++)
                if (y < 0)
            for (int j = 0; j >= y; j--) Attacking(tank, i, j, x, y);
        else for (int j = 0; j <= y; j++) Attacking(tank, i, j, x, y);

    }*/
}
