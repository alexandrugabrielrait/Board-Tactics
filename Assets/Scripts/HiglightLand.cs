using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiglightLand : MonoBehaviour {

    public int xx;
    public int yy;
    public int ballistic0 = 0;
    public int ballistic1 = 0;
    public MapManager mapager;
    public DataManager datager;

    Unit FindTarget(bool plane)
    {
        foreach(Unit unit in FindObjectsOfType<Unit>())
        {
            if (unit.xx == xx && unit.yy == yy)
                if((plane == true && (unit.type==5 || unit.type == 12 || unit.type == 14))||(plane==false&&(unit.type!=5&&unit.type!=12 && unit.type != 14)))
                return unit;
        }
        return null;
    }

    public void Ballistic(int player)
    {
        Unit target = FindTarget(false);
        if (target == null) return;
        if (player == 0&&ballistic0 > 0) { target.TakeDamage(ballistic0); ballistic0 = 0; }
        else if (player == 1 && ballistic1 > 0)
        {
            target.TakeDamage(ballistic1);
            ballistic1 = 0;
        }
    }

    void OnMouseDown()
    { 
        if (mapager.highlight[xx, yy] != 1) return;
        Unit target = FindTarget(false);
        Unit airTarget = FindTarget(true);
        int res;
        if (mapager.selected.player == 0) res = mapager.manager.resource0;
        else res = mapager.manager.resource1;
        switch (mapager.selected.type)
        {
            case 0: Unit unit;
                switch (mapager.manager.ability)
                {
                    case 1: if (res >= mapager.rifleman.cost) { res -= mapager.rifleman.cost; unit = (Unit)Instantiate(mapager.rifleman, new Vector3(xx - 3.5f, yy - 2.5f, transform.position.z), transform.rotation); unit.player = mapager.selected.player; unit.xx = xx; unit.yy = yy; unit.name = mapager.rifleman.name; } break;
                    case 2: if (res >= mapager.engineer.cost) { res -= mapager.engineer.cost; unit = (Unit)Instantiate(mapager.engineer, new Vector3(xx - 3.5f, yy - 2.5f, transform.position.z), transform.rotation); unit.player = mapager.selected.player; unit.xx = xx; unit.yy = yy; unit.name = mapager.engineer.name; } break;
                    case 3: if (res >= mapager.transport.cost) { res -= mapager.transport.cost; unit = (Unit)Instantiate(mapager.transport, new Vector3(xx - 3.5f, yy - 2.5f, transform.position.z), transform.rotation); unit.player = mapager.selected.player; unit.xx = xx; unit.yy = yy; unit.name = mapager.transport.name; } break;
                    case 4: if (res >= mapager.tank.cost) { res -= mapager.tank.cost; unit = (Unit)Instantiate(mapager.tank, new Vector3(xx - 3.5f, yy - 2.5f, transform.position.z), transform.rotation); unit.player = mapager.selected.player; unit.xx = xx; unit.yy = yy; unit.name = mapager.tank.name; } break;
                    case 5: if (res >= mapager.bomber.cost) { res -= mapager.bomber.cost; unit = (Unit)Instantiate(mapager.bomber, new Vector3(xx - 3.5f, yy - 2.5f, transform.position.z), transform.rotation); unit.player = mapager.selected.player; unit.xx = xx; unit.yy = yy; unit.name = mapager.bomber.name; } break;
                    case 6: if ((mapager.selected.player == 0 && mapager.manager.gain0 < 10) || (mapager.selected.player == 1 && mapager.manager.gain1 < 10)) { if (res >= datager.investCost) { res -= datager.investCost; if (mapager.selected.player == 0) mapager.manager.gain0++; else mapager.manager.gain1++; } }
                        else if (res >= datager.nukeCost)
                        {
                            res -= datager.nukeCost;
                            foreach (Unit unit2 in FindObjectsOfType<Unit>())
                            {
                                if (unit2.xx >= xx - 2 && unit2.xx <= xx + 2 && unit2.yy >= yy - 2 && unit2.yy <= yy + 2)
                                    if(unit2.xx>=1&&unit2.xx<=8&& unit2.yy >= 1 && unit2.yy <= 8) unit2.TakeDamage(50);
                            }
                        }
                        break;
                }
                break;
            case 1:
                switch (mapager.manager.ability)
                {
                    case 1: if (mapager.manager.moves >= mapager.selected.moveCost) { mapager.manager.moves -= mapager.selected.moveCost; mapager.selected.xx = xx; mapager.selected.yy = yy; if (target != null) target.TakeDamage(50); } break;
                    case 2: if (target != null && mapager.manager.moves >= mapager.selected.moveCost) { mapager.manager.moves -= mapager.selected.moveCost; target.TakeDamage(mapager.selected.attack); if (mapager.selected.type == 6) mapager.selected.TakeDamage(50); } break;
                    case 3: if (mapager.manager.moves >= mapager.selected.moveCost && res >= datager.rusherCost) { mapager.manager.moves -= mapager.selected.moveCost; res -= datager.rusherCost; mapager.selected.xx = xx; mapager.selected.yy = yy;
                    mapager.selected.name = "pieceRusher"; mapager.selected.GetComponent<SpriteRenderer>().sprite = Resources.Load("pieceRusher", typeof(Sprite)) as Sprite; mapager.selected.type = 7; mapager.selected.attack = 3; mapager.selected.range = 1; mapager.selected.desc=datager.rusherDesc;} break;
                    case 4: if (mapager.manager.moves >= mapager.selected.moveCost && res >= datager.rangerCost) { mapager.manager.moves -= mapager.selected.moveCost; res -= datager.rangerCost; mapager.selected.xx = xx; mapager.selected.yy = yy;
                    mapager.selected.name = "pieceRanger"; mapager.selected.GetComponent<SpriteRenderer>().sprite = Resources.Load("pieceRanger", typeof(Sprite)) as Sprite; mapager.selected.type = 11; mapager.selected.range = 7; mapager.selected.desc=datager.rangerDesc;} break;
                    case 5: if (mapager.manager.moves >= mapager.selected.moveCost && res >= datager.riderCost) { mapager.manager.moves -= mapager.selected.moveCost; res -= datager.riderCost; mapager.selected.xx = xx; mapager.selected.yy = yy;
                    mapager.selected.name = "pieceRider"; mapager.selected.GetComponent<SpriteRenderer>().sprite = Resources.Load("pieceRider", typeof(Sprite)) as Sprite; mapager.selected.type = 10; mapager.selected.desc=datager.riderDesc; mapager.selected.healthMax = mapager.selected.health = 2;mapager.selected.move = 3; if (target != null) target.TakeDamage(50);} break;
                    case 6: if (mapager.manager.moves >= mapager.selected.moveCost && res >= datager.rocketLauncherCost) { mapager.manager.moves -= mapager.selected.moveCost; res -= datager.rocketLauncherCost; mapager.selected.xx = xx; mapager.selected.yy = yy;
                    mapager.selected.name = "pieceRocket Troop"; mapager.selected.GetComponent<SpriteRenderer>().sprite = Resources.Load("pieceRocketLauncher", typeof(Sprite)) as Sprite; mapager.selected.type = 13; mapager.selected.attack = 2; mapager.selected.desc=datager.rocketLauncherDesc; mapager.selected.healthMax = mapager.selected.health = 2;} break;
                }
                break;
            case 2:
                switch (mapager.manager.ability)
                {
                    case 1: if (mapager.manager.moves >= mapager.selected.moveCost) { mapager.manager.moves-= mapager.selected.moveCost; mapager.selected.xx = xx; mapager.selected.yy = yy; } break;
                    case 2: if (target != null && (target.type == 0 || target.type == 3 || target.type == 4 || target.type == 8) && mapager.manager.moves >= mapager.selected.moveCost) {mapager.manager.moves-= mapager.selected.moveCost; target.GetHealed(mapager.selected.attack);} break;
                    case 3: if (mapager.manager.moves >= mapager.selected.moveCost&&res>=datager.demolitionistCost) { mapager.manager.moves -= mapager.selected.moveCost; res -= datager.demolitionistCost; mapager.selected.xx = xx; mapager.selected.yy = yy;
                    mapager.selected.name = "pieceDemolitionist"; mapager.selected.GetComponent<SpriteRenderer>().sprite = Resources.Load("pieceDemolitionist", typeof(Sprite)) as Sprite; mapager.selected.type = 6; mapager.selected.attack = 4; mapager.selected.desc = datager.demolitionistDesc;
                        } break;
                    case 4:
                        if (mapager.manager.moves >= mapager.selected.moveCost && res >= datager.saboteurCost)
                        {
                            mapager.manager.moves -= mapager.selected.moveCost; res -= datager.saboteurCost; mapager.selected.xx = xx; mapager.selected.yy = yy;
                            mapager.selected.name = "pieceSaboteur"; mapager.selected.GetComponent<SpriteRenderer>().sprite = Resources.Load("pieceSaboteur", typeof(Sprite)) as Sprite; mapager.selected.type = 9; mapager.selected.attack = 0; mapager.selected.health = mapager.selected.healthMax = 5; mapager.selected.desc = datager.saboteurDesc; mapager.selected.stealth = true;
                        }
                        break;
                    case 6: if (res >= mapager.drone.cost) { res -= mapager.drone.cost; mapager.selected.drones--; Unit unit1 = (Unit)Instantiate(mapager.drone, new Vector3(xx - 3.5f, yy - 2.5f, transform.position.z), transform.rotation); unit1.player = mapager.selected.player; unit1.xx = xx; unit1.yy = yy; mapager.selected = unit1; unit1.name = mapager.drone.name; } break;
                } break;
            case 3:
                TransportCar car = mapager.selected.GetComponent<TransportCar>();
                switch (mapager.manager.ability)
                {
                    case 1: if (mapager.manager.moves >= mapager.selected.moveCost) { mapager.manager.moves -= mapager.selected.moveCost; mapager.selected.xx = xx; mapager.selected.yy = yy; if (target != null) target.TakeDamage(50); } break;
                    case 2: if (target != null && target.player == mapager.selected.player && (target.type == 1 || target.type == 2 || target.type == 7 || target.type == 9 || target.type == 11))
                        {
                            if (car.transported1 == null) { car.transported1 = target; target.xx = -1; break; }
                            if (car.transported2 == null) { car.transported2 = target; target.xx = -1; break; }
                            if (car.transported3 == null) { car.transported3 = target; target.xx = -1; break; }
                        }
                        break;
                    case 3: if (car.transported1 != null) { Unit unit1 = (Unit)Instantiate(car.transported1, new Vector3(xx - 3.5f, yy - 2.5f, transform.position.z), transform.rotation); unit1.player = mapager.selected.player; unit1.xx = xx; unit1.yy = yy; if (mapager.manager.mode != 1) Destroy(car.transported1); car.transported1 = null; unit1.transform.name=unit1.transform.name.Replace("(Clone)","").Trim(); }break;
                    case 4: if (car.transported2 != null) { Unit unit1 = (Unit)Instantiate(car.transported2, new Vector3(xx - 3.5f, yy - 2.5f, transform.position.z), transform.rotation); unit1.player = mapager.selected.player; unit1.xx = xx; unit1.yy = yy; if (mapager.manager.mode != 1) Destroy(car.transported2); car.transported2 = null; unit1.transform.name=unit1.transform.name.Replace("(Clone)","").Trim(); } break;
                    case 5: if (car.transported3 != null) { Unit unit1 = (Unit)Instantiate(car.transported3, new Vector3(xx - 3.5f, yy - 2.5f, transform.position.z), transform.rotation); unit1.player = mapager.selected.player; unit1.xx = xx; unit1.yy = yy; if (mapager.manager.mode != 1) Destroy(car.transported3); car.transported3 = null; unit1.transform.name=unit1.transform.name.Replace("(Clone)","").Trim(); } break;
                    case 6: if (res >= datager.baseCost){ res -= datager.baseCost; if (mapager.selected.player == 0&&mapager.manager.mode!=2&&mapager.manager.mode!=4) mapager.bases0++; else mapager.bases1++; Unit unit1 = (Unit)Instantiate(car.base1, new Vector3(xx - 3.5f, yy - 2.5f, transform.position.z), transform.rotation); unit1.player = mapager.selected.player; unit1.xx = xx; unit1.yy = yy;
                            if (car.transported1 != null)
                            {
                                switch (car.transported1.type)
                                {
                                    case 6: res += datager.demolitionistCost; break;
                                    case 7: res += datager.rusherCost; break;
                                    case 9: res += datager.saboteurCost; break;
                                    case 11: res += datager.rangerCost; break;
                                }
                                res += car.transported1.cost;
                            }
                            if (car.transported2 != null)
                            {
                                switch (car.transported2.type) 
                                    {
                                    case 6: res += datager.demolitionistCost; break;
                                    case 7: res += datager.rusherCost; break;
                                    case 9: res += datager.saboteurCost; break;
                                    case 11: res += datager.rangerCost; break;
                                }
                            res += car.transported2.cost;
                        }
                            if (car.transported3 != null)
                            {
                                switch (car.transported3.type)
                                {
                                    case 6: res += datager.demolitionistCost; break;
                                    case 7: res += datager.rusherCost; break;
                                    case 9: res += datager.saboteurCost; break;
                                    case 11: res += datager.rangerCost; break;
                                }
                                res += car.transported3.cost;
                            }
             if(mapager.manager.mode==1) mapager.manager.mode=4;
             if(mapager.manager.mode==4) mapager.manager.mode=0;
             Destroy(mapager.selected.gameObject); unit1.transform.name = unit1.transform.name.Replace("(Clone)", "");mapager.selected = unit1; } break;
                }
                break;
            case 4:
                switch (mapager.manager.ability)
                {
                    case 1: if (mapager.manager.moves >= mapager.selected.moveCost) { mapager.manager.moves -= mapager.selected.moveCost; mapager.selected.xx = xx; mapager.selected.yy = yy; if (target != null) target.TakeDamage(50); } break;
                    case 2: if (target != null && mapager.manager.moves >= 1) { mapager.manager.moves-= 1; target.TakeDamage(mapager.selected.attack); } break;
                    case 3: mapager.selected.name = "pieceMissile Tank"; mapager.selected.GetComponent<SpriteRenderer>().sprite = Resources.Load("pieceMissileLauncher", typeof(Sprite)) as Sprite; mapager.selected.type = 8; mapager.selected.range = 1; mapager.selected.desc = datager.missileTankDesc;
                        break;
                    case 4: if (res >= datager.tankMoveCost) { res -= datager.tankMoveCost; mapager.selected.move++; } break;
                    case 5: if (res >= datager.tankAttackCost) { res -= datager.tankAttackCost; mapager.selected.attack++; } break;
                    case 6: if (res >= datager.tankStealthCost && mapager.selected.stealth == false) { res -= datager.tankStealthCost; mapager.selected.stealth = true; } break;
                }
                break;
            case 5:
                switch (mapager.manager.ability)
                {
                    case 1: if (mapager.manager.moves >= mapager.selected.moveCost) { mapager.manager.moves-= mapager.selected.moveCost; mapager.selected.xx = xx; mapager.selected.yy = yy; if (airTarget != null) airTarget.TakeDamage(50); } break;
                    case 2: if (target != null && mapager.manager.moves >= 2) { mapager.manager.moves-= 2; target.TakeDamage(mapager.selected.attack); } break;
                    case 3: mapager.selected.name = "pieceBomber"; mapager.selected.GetComponent<SpriteRenderer>().sprite = Resources.Load("pieceBomber", typeof(Sprite)) as Sprite; mapager.selected.type = 14; mapager.selected.range = 0; mapager.selected.attack=5; mapager.selected.desc = datager.bomberDesc;mapager.selected.stealth = true; break;
                    case 6: if (target != null&&!(target.stealth==true&&target.player!=mapager.manager.player)) mapager.selected = target; mapager.manager.ability = 0;  break;
                }
                break;
            case 6:
                switch (mapager.manager.ability)
                {
                    case 1: if (mapager.manager.moves >= mapager.selected.moveCost) { mapager.manager.moves -= mapager.selected.moveCost; mapager.selected.xx = xx; mapager.selected.yy = yy; if (target != null) target.TakeDamage(50); } break;
                    case 2: if (target != null && mapager.manager.moves >= mapager.selected.moveCost) { mapager.manager.moves -= mapager.selected.moveCost; target.TakeDamage(mapager.selected.attack); mapager.selected.TakeDamage(50); } break;
                    case 3: mapager.selected.stealth = !mapager.selected.stealth; if(mapager.selected.stealth == false) mapager.selected.GetComponent<SpriteRenderer>().sprite = Resources.Load("pieceDemolitionist", typeof(Sprite)) as Sprite; else mapager.selected.GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonNuke", typeof(Sprite)) as Sprite; break;
                    case 6: if (res >= mapager.drone.cost) { res -= mapager.drone.cost; mapager.selected.drones--; Unit unit1 = (Unit)Instantiate(mapager.drone, new Vector3(xx - 3.5f, yy - 2.5f, transform.position.z), transform.rotation); unit1.player = mapager.selected.player; unit1.xx = xx; unit1.yy = yy; mapager.selected = unit1; unit1.name = mapager.drone.name; } break;
                }
                break;
            case 8:
                switch (mapager.manager.ability)
                {
                    case 1: if (mapager.manager.moves >= mapager.selected.moveCost) { mapager.manager.moves -= mapager.selected.moveCost; mapager.selected.xx = xx; mapager.selected.yy = yy; if (target != null) target.TakeDamage(50); } break;
                    case 2: if (airTarget != null && mapager.manager.moves >= 1) { mapager.manager.moves -= 1; airTarget.TakeDamage(mapager.selected.attack); } break;
                    case 3: if (res >= datager.ballisticCost && mapager.manager.moves >= 1) { mapager.manager.moves -= 1; res -= datager.ballisticCost; if (mapager.selected.player == 0) ballistic0 += mapager.selected.attack; else ballistic1 += mapager.selected.attack; } break; 
                    case 4: if (res >= datager.tankMoveCost) { res -= datager.tankMoveCost; mapager.selected.move++; } break;
                    case 5: if (res >= datager.tankAttackCost) { res -= datager.tankAttackCost; mapager.selected.attack++; } break;
                    case 6: if (res >= datager.tankStealthCost && mapager.selected.stealth == false) { res -= datager.tankStealthCost; mapager.selected.stealth = true; } break;
                }
                break;
            case 9:
                switch (mapager.manager.ability)
                {
                    case 1: if (mapager.manager.moves >= mapager.selected.moveCost) { mapager.manager.moves -= mapager.selected.moveCost; mapager.selected.xx = xx; mapager.selected.yy = yy; if (target != null) target.TakeDamage(50);} break;
                    case 2: if (target != null && mapager.manager.moves >= 4 && target.type == 0)
                        {
                            mapager.manager.moves -= 4; target.TakeDamage(mapager.selected.attack);
                            if (target.player == 0) { if (mapager.manager.gain0 > 0) mapager.manager.gain0--; }
                            else if (mapager.manager.gain1 > 0) mapager.manager.gain1--;
                        } break;
                    case 6: if (res >= mapager.drone.cost) { res -= mapager.drone.cost; mapager.selected.drones--; Unit unit1 = (Unit)Instantiate(mapager.drone, new Vector3(xx - 3.5f, yy - 2.5f, transform.position.z), transform.rotation); unit1.player = mapager.selected.player; unit1.xx = xx; unit1.yy = yy; mapager.selected = unit1; unit1.name = mapager.drone.name; } break;
                }
                break;
            case 12:
                switch (mapager.manager.ability)
                {
                    case 1: if (mapager.manager.moves >= mapager.selected.moveCost) { mapager.manager.moves -= mapager.selected.moveCost; mapager.selected.xx = xx; mapager.selected.yy = yy; if (airTarget != null) target.TakeDamage(50); } break;
                    case 2: if (target != null && mapager.manager.moves >= mapager.selected.moveCost && target.health<=mapager.selected.attack) { mapager.manager.moves -= mapager.selected.moveCost; target.TakeDamage(mapager.selected.attack); } break;
                    case 3: if (target != null && (target.type == 0 || target.type == 3 || target.type == 4 || target.type == 8) && mapager.manager.moves >= mapager.selected.moveCost) { mapager.manager.moves -= mapager.selected.moveCost; target.GetHealed(mapager.selected.attack); } break;
                    case 6: if (target != null && !(target.stealth == true && target.player != mapager.manager.player)) mapager.selected = target; mapager.manager.ability = 0; break;
                }
                break;
            case 13:
                switch (mapager.manager.ability)
                {
                    case 1: if (mapager.manager.moves >= mapager.selected.moveCost) { mapager.manager.moves -= mapager.selected.moveCost; mapager.selected.xx = xx; mapager.selected.yy = yy; if (target != null) target.TakeDamage(50); } break;
                    case 2: if (target != null && mapager.manager.moves >= 2) { mapager.manager.moves -= 2; target.TakeDamage(mapager.selected.attack); } break;
                    case 3: if (airTarget != null && mapager.manager.moves >= 2) { mapager.manager.moves -= 2; airTarget.TakeDamage(mapager.selected.attack); } break;
                }
                break;
            case 14:
                switch (mapager.manager.ability)
                {
                    case 1: if (mapager.manager.moves >= mapager.selected.moveCost) { mapager.manager.moves -= mapager.selected.moveCost; mapager.selected.xx = xx; mapager.selected.yy = yy; if (airTarget != null) airTarget.TakeDamage(50); } break;
                    case 2: if (target != null && mapager.manager.moves >= mapager.selected.moveCost) { mapager.manager.moves -= mapager.selected.moveCost; target.TakeDamage(mapager.selected.attack); } break;
                    case 6: if (target != null && !(target.stealth == true && target.player != mapager.manager.player)) mapager.selected = target; mapager.manager.ability = 0; break;
                }
                break;
            default:
                switch (mapager.manager.ability)
                {
                    case 1: if (mapager.manager.moves >= mapager.selected.moveCost) { mapager.manager.moves-= mapager.selected.moveCost; mapager.selected.xx = xx; mapager.selected.yy = yy; if (target != null) target.TakeDamage(50); } break;
                    case 2: if (target != null && mapager.manager.moves >= mapager.selected.moveCost) { mapager.manager.moves -= mapager.selected.moveCost; target.TakeDamage(mapager.selected.attack); } break;
                }
                break;
                /* case 3:
                     if (manager.ability == 0) break;
                     if (manager.ability == 1)
                         HighlightPath(selected.xx, selected.yy, selected.move, 2, true);
                     if (manager.ability == 2)
                         HighlightPath(selected.xx, selected.yy, selected.range, 0, true);
                     break;
                 case 4:
                     if (manager.ability == 0) break;
                     if (manager.ability == 1)
                         HighlightPath(selected.xx, selected.yy, selected.move, 3, true);
                     if (manager.ability == 2)
                         HighlightPath(selected.xx, selected.yy, selected.range, 5, true);
                     break;
                 case 5:
                     if (manager.ability == 0) break;
                     if (manager.ability == 1)
                         for (int i = 0; i < 9; i++)
                             for (int j = 0; j < 9; j++)
                             {
                                 if (i != selected.xx && j != selected.yy)
                                     highlight[i, j] = 1;
                             }
                     if (manager.ability == 2)
                         highlight[selected.xx, selected.yy] = 1;
                     break;
                 default:
                     if (manager.ability == 0) break;
                     if (manager.ability == 1)
                         HighlightPath(selected.xx, selected.yy, selected.move, 1, true);
                     if (manager.ability == 2)
                         HighlightPath(selected.xx, selected.yy, selected.range, 5, true);
                     //else highlight[selected.xx, selected.yy] = 1;
                     break;*/
        }
        //if (mapager.manager.moves > 0 && mapager.selected.type != 0 && mapager.selected.type != 2 && mapager.selected.type != 3) mapager.manager.moves--;
        if (mapager.selected.player == 0) mapager.manager.resource0 = res;
        else mapager.manager.resource1 = res;
        mapager.manager.ability = 0;
    }

    // Update is called once per frame
    void Update () {
        if (mapager.manager.win == true)
            return;
        if (mapager.highlight[xx, yy] == 0) GetComponent<BoxCollider2D>().enabled = false;
        else GetComponent<BoxCollider2D>().enabled = true;
        if (mapager.highlight[xx, yy] == 0) gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        if (mapager.highlight[xx, yy] == 1) gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        if (mapager.highlight[xx, yy] == 2) gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        if(mapager.highlight[xx, yy] == 0 && (xx + yy) % 2 != 0 && mapager.manager.mode == 2) gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
    }
}
