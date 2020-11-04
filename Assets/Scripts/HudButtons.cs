using UnityEngine;
using System.Collections;

public class HudButtons : MonoBehaviour
{

    //public TankController enemy;
    public TurnManager manager;
    public MapManager mapager;
    //public GameObject bomb;
    public int player;

    // Use this for initialization
    void Update()
    {
        if (player == 2) return;
        if (mapager.selected == null) return;
        if (player != manager.player)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonEmpty", typeof(Sprite)) as Sprite;
        }
        else
        {
            if (mapager.selected.player != manager.player && ((mapager.selected.type != 5 && mapager.selected.type != 12) || transform.name != "buttonAbility6") && transform.name != "buttonPass") GetComponent<SpriteRenderer>().color = Color.grey;
            else GetComponent<SpriteRenderer>().color = Color.white;
            switch (mapager.selected.type)
            {
                case 0:
                    switch (transform.name)
                    {
                        case "buttonAbility1":
                            GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonRifleman", typeof(Sprite)) as Sprite; break;

                        case "buttonAbility2":
                            GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonEngineer", typeof(Sprite)) as Sprite; break;

                        case "buttonAbility3":
                            GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonTransport", typeof(Sprite)) as Sprite; break;

                        case "buttonAbility4":
                            GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonTank", typeof(Sprite)) as Sprite; break;

                        case "buttonAbility5":
                            GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonPlane", typeof(Sprite)) as Sprite; break;

                        case "buttonAbility6":
                            if ((mapager.selected.player == 0 && manager.gain0 < 10) || (mapager.selected.player == 1 && manager.gain1 < 10))
                                GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonInvest", typeof(Sprite)) as Sprite;
                            else GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonNuke", typeof(Sprite)) as Sprite;
                            break;
                    }
                    break;

                default: GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonEmpty", typeof(Sprite)) as Sprite; break;
            }
            if (mapager.selected.type > 0 && mapager.selected.type <= 14)
            {
                if (transform.name == "buttonAbility1") GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonMove", typeof(Sprite)) as Sprite;
                if (transform.name == "buttonAbility2") GetComponent<SpriteRenderer>().sprite = Resources.Load("button" + mapager.selected.transform.name.Replace("piece", ""), typeof(Sprite)) as Sprite;
            }
            if (mapager.selected.type > 0 && mapager.selected.type == 10)
            {
                if (transform.name == "buttonAbility1") GetComponent<SpriteRenderer>().sprite = Resources.Load("button" + mapager.selected.transform.name.Replace("piece", ""), typeof(Sprite)) as Sprite;
                if (transform.name == "buttonAbility2") GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonRifleman", typeof(Sprite)) as Sprite;
            }
            if (mapager.selected.type == 1)
            {
                if (transform.name == "buttonAbility3") GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonRusher", typeof(Sprite)) as Sprite;
                if (transform.name == "buttonAbility4") GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonRanger", typeof(Sprite)) as Sprite;
                if (transform.name == "buttonAbility5") GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonRider", typeof(Sprite)) as Sprite;
                if (transform.name == "buttonAbility6") GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonRocketLauncher", typeof(Sprite)) as Sprite;
            }
            if (mapager.selected.type == 2)
            {
                if (transform.name == "buttonAbility3") GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonDemolitionist", typeof(Sprite)) as Sprite;
                if (transform.name == "buttonAbility4") GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonSaboteur", typeof(Sprite)) as Sprite;
            }
            if (mapager.selected.type == 3)
            {
                TransportCar car = mapager.selected.GetComponent<TransportCar>();
                if (car.transported1 != null && transform.name == "buttonAbility3") GetComponent<SpriteRenderer>().sprite = Resources.Load(car.transported1.name.Replace("piece", "button"), typeof(Sprite)) as Sprite;
                if (car.transported2 != null && transform.name == "buttonAbility4") GetComponent<SpriteRenderer>().sprite = Resources.Load(car.transported2.name.Replace("piece", "button"), typeof(Sprite)) as Sprite;
                if (car.transported3 != null && transform.name == "buttonAbility5") GetComponent<SpriteRenderer>().sprite = Resources.Load(car.transported3.name.Replace("piece", "button"), typeof(Sprite)) as Sprite;
                if (transform.name == "buttonAbility6") GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonBase", typeof(Sprite)) as Sprite;
            }
            if (mapager.selected.type == 4)
            {
                if (transform.name == "buttonAbility3") GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonMissileLauncher", typeof(Sprite)) as Sprite;
                if (transform.name == "buttonAbility4") GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonTankUpMove", typeof(Sprite)) as Sprite;
                if (transform.name == "buttonAbility5") GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonTankUpAttack", typeof(Sprite)) as Sprite;
                if (transform.name == "buttonAbility6")
                {
                    if (mapager.selected.stealth == false) GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonTankUpStealth", typeof(Sprite)) as Sprite;
                    else GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonEmpty", typeof(Sprite)) as Sprite;
                }
            }
            if (mapager.selected.type == 5)
            {
                if (transform.name == "buttonAbility3") GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonBomber", typeof(Sprite)) as Sprite;
                if (transform.name == "buttonAbility6") GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonSelectBelow", typeof(Sprite)) as Sprite;
            }
            if (mapager.selected.type == 14)
            {
                if (transform.name == "buttonAbility6") GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonSelectBelow", typeof(Sprite)) as Sprite;
            }
            if ((mapager.selected.type == 2|| mapager.selected.type == 6|| mapager.selected.type == 9))
            {

                if (transform.name == "buttonAbility6") if (mapager.selected.drones > 0)
                        GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonDrone", typeof(Sprite)) as Sprite;
                    else GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonEmpty", typeof(Sprite)) as Sprite;
            }
            if (mapager.selected.type == 8)
            { 
                if (transform.name == "buttonAbility2") GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonAttackAir", typeof(Sprite)) as Sprite;
                if (transform.name == "buttonAbility3") GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonBallistic", typeof(Sprite)) as Sprite;
                if (transform.name == "buttonAbility4") GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonTankUpMove", typeof(Sprite)) as Sprite;
                if (transform.name == "buttonAbility5") GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonTankUpAttack", typeof(Sprite)) as Sprite;
                if (transform.name == "buttonAbility6")
                {if(mapager.selected.stealth==false) GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonTankUpStealth", typeof(Sprite)) as Sprite;
                else GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonEmpty", typeof(Sprite)) as Sprite;
                }
            }
                if (mapager.selected.type == 12)
            {
                if (transform.name == "buttonAbility1") GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonMove", typeof(Sprite)) as Sprite;
                if (transform.name == "buttonAbility2") GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonDroneAttack", typeof(Sprite)) as Sprite;
                if (transform.name == "buttonAbility3") GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonDroneRepair", typeof(Sprite)) as Sprite;
                if (transform.name == "buttonAbility6") GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonSelectBelow", typeof(Sprite)) as Sprite;
            }
            if (mapager.selected.type == 13)
            {
                if (transform.name == "buttonAbility2") GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonRocketLauncher", typeof(Sprite)) as Sprite;
                if (transform.name == "buttonAbility3") GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonAttackAir", typeof(Sprite)) as Sprite;
            }
                if (transform.name == "buttonPass")
            {
                if (manager.ability == 0)
                    GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonPass", typeof(Sprite)) as Sprite;
                else GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonCancel", typeof(Sprite)) as Sprite;
            }
        }


       // else GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonEmpty", typeof(Sprite)) as Sprite;

            if (player == 0 && manager.player == -1)
                GetComponent<SpriteRenderer>().sprite = Resources.Load(transform.name + "Disabled", typeof(Sprite)) as Sprite;
        }
    

    /*bool EnemyCheck(int x1, int y1)
    {
        if (tank.xx + x1 == enemy.xx && tank.yy + y1 == enemy.yy) {
            if (tank.country == 3) { Destroy(enemy.gameObject); return true;}
        return false; }
        return true;
    }*/


    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (this.name == "MoveCounter0")
            {
                manager.gain0 = manager.gain1 = 10;
                manager.resource0 = manager.resource1 = 1000;
                manager.moves = 99;
            }
            if (this.name == "MoveCounter1")
            {
                manager.pc = true;
            }
        }
    }

    void OnMouseDown()
    {
        if (player == 2) return;
        if (manager.player != player) return;
        mapager.ClearHighlights();
        if (mapager.selected.player != manager.player && ((mapager.selected.type != 5&& mapager.selected.type != 12) || transform.name != "buttonAbility6") && transform.name != "buttonPass") return;
        if (transform.name != "buttonPass")
        {
            manager.ability = System.Int32.Parse(transform.name.Replace("buttonAbility", ""));
        }
        manager.select.transform.position = this.transform.position;
        switch (transform.name)
        {
            /*case "buttonUp":
                manager.Moved();
                if (tank.country == 2 && tank.mode == 1) return;
                switch (tank.direction)
                {
                    case -1: mapager.TankMove(tank, 0, -tank.move); break;
                    case 0: mapager.TankMove(tank, tank.move, 0); break;
                    case 1: mapager.TankMove(tank, 0, tank.move); break;
                    default: mapager.TankMove(tank, -tank.move, 0); break;
                }
                break;
            case "buttonFire":
                manager.Moved();
                if (tank.country == 3 && tank.mode == 1) break;
                if (tank.country == 7)
                    switch (tank.direction)
                    {
                        case -1: mapager.TankMove(tank, 0, tank.move); break;
                        case 0: mapager.TankMove(tank, -tank.move, 0); break;
                        case 1: mapager.TankMove(tank, 0, -tank.move); break;
                        default: mapager.TankMove(tank, tank.move, 0); break;
                    }
                else
                if (tank.country == 2 && tank.mode == 1)
                {
                    mapager.TankAttack(tank, -tank.range, 0);
                    mapager.TankAttack(tank, tank.range, 0);
                    mapager.TankAttack(tank, 0, tank.range);
                    mapager.TankAttack(tank, 0, -tank.range);
                }
                else
                if (tank.country == 5)
                {
                    switch (tank.direction)
                    {
                        case 2: mapager.TankAttack(tank, -tank.range, 0);
                            mapager.TankAttack(tank, -1, -1);
                            mapager.TankAttack(tank, -1, 1); break;
                        case 0: mapager.TankAttack(tank, tank.range, 0);
                            mapager.TankAttack(tank, 1, 1);
                            mapager.TankAttack(tank, 1, -1); break;
                        case -1: mapager.TankAttack(tank, 0, tank.range);
                            mapager.TankAttack(tank, 1, 1);
                            mapager.TankAttack(tank, -1, 1); break;
                        default: mapager.TankAttack(tank, 0, -tank.range);
                            mapager.TankAttack(tank, -1, -1);
                            mapager.TankAttack(tank, 1, -1); break;
                    }
                }else
                switch (tank.direction)
                {
                    case 2: mapager.TankAttack(tank, -tank.range, 0); break;
                    case 0: mapager.TankAttack(tank, tank.range, 0); break;
                    case -1: mapager.TankAttack(tank, 0, tank.range); break;
                    default: mapager.TankAttack(tank, 0, -tank.range); break;
                }
                break;
            case "buttonAbility2":
                switch (tank.country)
                {
                    case 1:
                        if (tank.direction >= 2) tank.direction -= 2;
                        else tank.direction += 2; break;
                    case 2:
                        tank.mode = 1 - tank.mode; manager.Moved();
                        break;
                    case 3:
                        tank.mode = 1 - tank.mode; manager.Moved();
                        break;
                    case 6:
                        manager.Moved();
                        if (tank.mode < 3)
                        {
                            tank.mode++;
                            Instantiate(bomb, new Vector3(tank.transform.position.x, tank.transform.position.y, tank.transform.position.z), transform.rotation);
                        }
                        switch (tank.direction)
                        {
                            case -1: mapager.TankMove(tank, 0, -tank.move); break;
                            case 0: mapager.TankMove(tank, tank.move, 0); break;
                            case 1: mapager.TankMove(tank, 0, tank.move); break;
                            default: mapager.TankMove(tank, -tank.move, 0); break;
                        }
                        break;
                    default:
                        manager.Moved();
                        switch (tank.direction)
                        {
                            case 2: mapager.TankAttack(tank, -tank.range, 0); break;
                            case 0: mapager.TankAttack(tank, tank.range, 0); break;
                            case -1: mapager.TankAttack(tank, 0, tank.range); break;
                            default: mapager.TankAttack(tank, 0, -tank.range); break;
                        }
                        break;
                }
                break;
            case "buttonTurnLeft":
                if (tank.direction == 2) tank.direction = -1;
                else tank.direction++;
                if (tank.country != 1) manager.Moved(); break;
            case "buttonTurnRight":
                if (tank.direction == -1) tank.direction = 2;
                else tank.direction--;
                if (tank.country != 1) manager.Moved(); break;
        */
            case "buttonPass": if (manager.ability == 0)
                    manager.Passed();
                else manager.ability = 0;
                break;

        }
    }
}
