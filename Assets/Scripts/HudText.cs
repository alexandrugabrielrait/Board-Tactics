using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudText : MonoBehaviour
{

    public int type; //0 name, 1 current health/cost, 2 health, 3 damage, 4 attack range, 5 move, 6 description, 7 res0+gain0, 8 res1+gain1
    public int colored;
    public MapManager mapager;
    public HelpManager manager;
    public DataManager datager;
    public Unit selection;

    // Update is called once per frame
    void Update()
    {
        if (Application.loadedLevelName != "Help")
            selection = mapager.selected;
        else selection = manager.currentUnit;
        if (selection == null) return;
        if (Application.loadedLevelName != "Help")
        {
            if (mapager.manager.win == true) return;
            if (colored == 1) GetComponent<Text>().color = mapager.manager.color0;
            if (colored == 2) GetComponent<Text>().color = mapager.manager.color1;
            if (type == 7) { GetComponent<Text>().text = mapager.manager.resource0 + "+" + mapager.manager.gain0 + " per turn"; return; }
            if (type == 8) { GetComponent<Text>().text = mapager.manager.resource1 + "+" + mapager.manager.gain1 + " per turn"; return; }
        }
        bool x = false;
        if (Application.loadedLevelName != "Help")
            if (mapager.manager.ability == 0) x = true;
        if (Application.loadedLevelName == "Help") x = true;
        if(x)
        {
            if (type == 0) GetComponent<Text>().text = selection.name.Replace("piece", "").Replace("0", "").Replace("1", "");
            if (Application.loadedLevelName == "Help")
            {
                if (type == 1) GetComponent<Text>().text = "Cost: " + selection.cost;
                if (type == 2) GetComponent<Text>().text = "Health: " + selection.healthMax;
            }
            else
            {
                if (type == 1) GetComponent<Text>().text = "Health: " + selection.health;
                if (type == 2) GetComponent<Text>().text = "Maximum: " + selection.healthMax;
            }
            if (type == 3 && selection.attack <= 0)
            {
                if (selection.type != 3) GetComponent<Text>().text = "";
                else { GetComponent<Text>().text = "Cargo: " + selection.GetComponent<TransportCar>().cargo + "/3"; }
            }
            if (type == 5 && selection.move <= 0) GetComponent<Text>().text = "";
            if (type == 3 && selection.attack > 0)
            {
                if (selection.type != 2) GetComponent<Text>().text = "Damage: " + selection.attack;
                else GetComponent<Text>().text = "Repair: " + selection.attack;
            }
            if (type == 4) GetComponent<Text>().text = "Range: " + selection.range;
            if (type == 5 && selection.move > 0) GetComponent<Text>().text = "Move: " + selection.move + " (" + selection.moveCost + "/4)";
            if (type == 6) GetComponent<Text>().text = selection.desc;
            if (type == 7) GetComponent<Text>().text = manager.unitType.ToString();
            if (Application.loadedLevelName == "Help") if (manager.unitType < 0 && type > 1 && type != 6)
                {
                    GetComponent<Text>().text = "";
                    if (manager.unitType == -1 && type == 3) GetComponent<Text>().text = "Gain: +1/Turn";
                    if (manager.unitType == -1 && type == 4) GetComponent<Text>().text = "Max: 10/Turn";
                    if (manager.unitType == -2 && type == 4) GetComponent<Text>().text = "Range: 2";
                    if (manager.unitType == -4 && type == 5) GetComponent<Text>().text = "Move: +1 (2/4)";
                    if (manager.unitType == -5 && type == 3) GetComponent<Text>().text = "Damage: +1";

                }
        }
        else
        {
            if (selection.type == 0)
            {
                Unit reference;
                switch (mapager.manager.ability)
                {
                    case 1: reference = mapager.rifleman; break;
                    case 2: reference = mapager.engineer; break;
                    case 3: reference = mapager.transport; break;
                    case 4: reference = mapager.tank; break;
                    case 5: reference = mapager.bomber; break;
                    default: reference = null; break;
                }
                if (mapager.manager.ability <= 5)
                {
                    if (type == 0) if (reference.type == 8) GetComponent<Text>().text = "Missile Tank"; else GetComponent<Text>().text = reference.name.Replace("piece", "").Replace("0", "").Replace("1", "");
                    if (type == 1) GetComponent<Text>().text = "Cost: " + reference.cost;
                    if (type == 2) GetComponent<Text>().text = "Health: " + reference.healthMax;
                    if (type == 3 && reference.attack <= 0)
                    {
                        if (reference.type != 3) GetComponent<Text>().text = "";
                        else { GetComponent<Text>().text = "Cargo: 0/3"; }
                    }
                    if (type == 5 && reference.move <= 0) GetComponent<Text>().text = "";
                    if (type == 3 && reference.attack > 0)
                    {
                        if (reference.type != 2) GetComponent<Text>().text = "Damage: " + reference.attack;
                        else GetComponent<Text>().text = "Repair: " + reference.attack;
                    }
                    if (type == 4) GetComponent<Text>().text = "Range: " + reference.range;
                    if (type == 5 && reference.move > 0) GetComponent<Text>().text = "Move: " + reference.move + " (" + reference.moveCost + "/4)";
                    if (type == 6) GetComponent<Text>().text = reference.desc;
                }
                else
                {
                    if (type == 0) if ((selection.player == 0 && mapager.manager.gain0 < 10) || (selection.player == 1 && mapager.manager.gain1 < 10)) GetComponent<Text>().text = "Invest"; else GetComponent<Text>().text = "Nuclear Strike";
                    if (type == 1) if ((selection.player == 0 && mapager.manager.gain0 < 10) || (selection.player == 1 && mapager.manager.gain1 < 10)) GetComponent<Text>().text = "Cost: " + datager.investCost; else GetComponent<Text>().text = "Cost: " + datager.nukeCost;
                    if (type == 2) if ((selection.player == 0 && mapager.manager.gain0 < 10) || (selection.player == 1 && mapager.manager.gain1 < 10)) GetComponent<Text>().text = "Placement: Selected Base"; else GetComponent<Text>().text = "Placement: Any Base";
                    if (type == 3) if ((selection.player == 0 && mapager.manager.gain0 < 10) || (selection.player == 1 && mapager.manager.gain1 < 10)) GetComponent<Text>().text = "Gain: +1/Turn: "; else GetComponent<Text>().text = "Damage: 50";
                    if (type == 4) if ((selection.player == 0 && mapager.manager.gain0 < 10) || (selection.player == 1 && mapager.manager.gain1 < 10)) GetComponent<Text>().text = "Max: 10/Turn: "; else GetComponent<Text>().text = "Range: 2";
                    if (type == 5) GetComponent<Text>().text = "";
                    if (type == 6) if ((selection.player == 0 && mapager.manager.gain0 < 10) || (selection.player == 1 && mapager.manager.gain1 < 10)) GetComponent<Text>().text = datager.investDesc; else GetComponent<Text>().text = datager.nukeDesc;
                }
            }
            if (selection.type >= 1 && selection.type <= 14)
            {
                if (mapager.manager.ability == 1)
                {
                    if (type == 0) GetComponent<Text>().text = "Move";
                    if (type == 1) GetComponent<Text>().text = "Cost: 0";
                    if (type == 2)
                    {
                        if (selection.type >= 1 && selection.type <= 11)
                            GetComponent<Text>().text = "Straight";
                        if (selection.type == 5 || selection.type == 7 || selection.type == 14)
                            GetComponent<Text>().text = "All Directions";
                        if (selection.type == 10)
                            GetComponent<Text>().text = "Horse";
                        if (selection.type == 12)
                            GetComponent<Text>().text = "Straight Line";
                    }
                    if (type == 3)
                    {
                        if (selection.type == 1 || selection.type == 2 || selection.type == 6 || selection.type == 7 || selection.type == 9 || selection.type == 11||selection.type==13)
                            GetComponent<Text>().text = "No Crushing";
                        if (selection.type == 3 || selection.type == 10)
                            GetComponent<Text>().text = "Crush Infantry";
                        if (selection.type == 4 || selection.type == 8)
                            GetComponent<Text>().text = "Crush Small";
                        if (selection.type == 5|| selection.type == 14)
                            GetComponent<Text>().text = "Flight";
                        if (selection.type == 12)
                            GetComponent<Text>().text = "Hover";
                    }
                    if (type == 4) GetComponent<Text>().text = "Range: " + selection.move;
                    if (type == 5) GetComponent<Text>().text = "Points: " + selection.moveCost + "/4";
                    if (type == 6) GetComponent<Text>().text = datager.moveDesc;
                }
                if (mapager.manager.ability == 2)
                {
                    if (type == 0)
                        switch (selection.type)
                        {
                            case 2: GetComponent<Text>().text = "Repair"; break;
                            case 3: GetComponent<Text>().text = "Load"; break;
                            case 6: GetComponent<Text>().text = "Explode"; break;
                            case 8: GetComponent<Text>().text = "Attack Air"; break;
                            case 9: GetComponent<Text>().text = "Sabotage"; break;
                            case 12: GetComponent<Text>().text = "Zap"; break;
                            default: GetComponent<Text>().text = "Attack"; break;
                        }
                    if (type == 1) GetComponent<Text>().text = "Cost: 0";
                    if (type == 2)
                    {
                        if (selection.type >= 1 && selection.type <= 13)
                            GetComponent<Text>().text = "Straight";
                        if (selection.type == 12|| selection.type == 14)
                            GetComponent<Text>().text = "Below";
                        if (selection.type == 8)
                            GetComponent<Text>().text = "All Directions";
                        if (selection.type == 11)
                            GetComponent<Text>().text = "Straight Line";
                    }
                    if (type == 3)
                    {
                        switch (selection.type)
                        {
                            case 2: GetComponent<Text>().text = "Repair: " + selection.attack; break;
                            case 3: GetComponent<Text>().text = "Cargo: " + selection.GetComponent<TransportCar>().cargo + "/3"; break;
                            default: GetComponent<Text>().text = "Damage: " + selection.attack; ; break;
                        }
                    }
                    if (type == 4) GetComponent<Text>().text = "Range: " + selection.range;
                    if (type == 5)
                        switch (selection.type)
                        {
                            case 3: GetComponent<Text>().text = "Points: 0/4"; break;
                            case 9: GetComponent<Text>().text = "Points: 4/4"; break;
                            case 13: GetComponent<Text>().text = "Points: 2/4"; break;
                            case 14: GetComponent<Text>().text = "Points: " + selection.moveCost + "/4"; break;
                            default: GetComponent<Text>().text = "Points: 1/4"; break;
                        }
                    if (type == 6)
                        switch (selection.type)
                        {
                            case 2: GetComponent<Text>().text = datager.repairDesc; break;
                            case 3: GetComponent<Text>().text = datager.loadDesc; break;
                            case 9: GetComponent<Text>().text = datager.sabotageDesc; break;
                            case 8: GetComponent<Text>().text = datager.attackAirDesc; break;
                            case 12: GetComponent<Text>().text = datager.zapDesc; break;
                            default: GetComponent<Text>().text = datager.attackDesc; break;
                        }
                }
                if (mapager.manager.ability == 3)
                {
                    if (selection.type == 3 && selection.GetComponent<TransportCar>().transported1 == null)
                    {
                        GetComponent<Text>().text = "";
                        return;
                    }
                    if (type == 0)
                        switch (selection.type)
                        {
                            case 1: GetComponent<Text>().text = "Rusher"; break;
                            case 2: GetComponent<Text>().text = "Demolitionist"; break;
                            case 3: GetComponent<Text>().text = selection.GetComponent<TransportCar>().transported1.name.Replace("piece", ""); break;
                            case 4: GetComponent<Text>().text = "Missile Tank"; break;
                            case 5: GetComponent<Text>().text = "Bomber"; break;
                            case 8: GetComponent<Text>().text = "Ballistic"; break;
                            case 12: GetComponent<Text>().text = "Repair"; break;
                            case 13: GetComponent<Text>().text = "Attack Air"; break;
                            default: GetComponent<Text>().text = ""; break;
                        }
                    if (type == 1)
                    {
                        switch (selection.type)
                        {
                            case 1: GetComponent<Text>().text = "Cost: " + datager.rusherCost; break;
                            case 2: GetComponent<Text>().text = "Cost: " + datager.demolitionistCost; break;
                            case 3: GetComponent<Text>().text = "Health: " + selection.GetComponent<TransportCar>().transported1.health; break;
                            case 4: GetComponent<Text>().text = "Cost: " + datager.missileTankCost; break;
                            case 5: GetComponent<Text>().text = "Cost: " + datager.bomberCost; break;
                            case 8: GetComponent<Text>().text = "Cost: " + datager.ballisticCost; break;
                            case 12: GetComponent<Text>().text = "Cost: 0";break;
                            case 13: GetComponent<Text>().text = "Cost: 0"; break;
                            default: GetComponent<Text>().text = ""; break;
                        }
                    }
                    if (type == 2)
                    {
                        switch (selection.type)
                        {
                            case 1: GetComponent<Text>().text = "Health: 1"; break;
                            case 2: GetComponent<Text>().text = "Health: 1"; break;
                            case 3: GetComponent<Text>().text = "Maximum: " + selection.GetComponent<TransportCar>().transported1.healthMax; break;
                            case 4: GetComponent<Text>().text = "Health: 5"; break;
                            case 5: GetComponent<Text>().text = "Health: 5"; break;
                            case 8: GetComponent<Text>().text = "Placement: Any non-Base"; break;
                            case 12: GetComponent<Text>().text = "Below"; break;
                            case 13: GetComponent<Text>().text = "Straight" ;break;
                            default: GetComponent<Text>().text = ""; break;
                        }
                    }
                    if (type == 3)
                        switch (selection.type)
                        {
                            case 1: GetComponent<Text>().text = "Attack: 3"; break;
                            case 2: GetComponent<Text>().text = "Attack: 5"; break;
                            case 3:

                                if (selection.GetComponent<TransportCar>().transported1.type != 2) GetComponent<Text>().text = "Damage: " + selection.GetComponent<TransportCar>().transported1.attack;
                                else GetComponent<Text>().text = "Repair: " + selection.GetComponent<TransportCar>().transported1.attack;
                                break;
                            case 4: GetComponent<Text>().text = "Attack: 5"; break;
                            case 5: GetComponent<Text>().text = "Attack: 5"; break;
                            case 8: GetComponent<Text>().text = "Damage: " + selection.attack; break;
                            case 12: GetComponent<Text>().text = "Repair: " + selection.attack; break;
                            case 13: GetComponent<Text>().text = "Damage: "+selection.attack; break;
                            default: GetComponent<Text>().text = ""; ; break;
                        }
                    if (type == 4)
                    {
                        switch (selection.type)
                        {
                            case 1: GetComponent<Text>().text = "Range: 1"; break;
                            case 2: GetComponent<Text>().text = "Range: 1"; break;
                            case 3: GetComponent<Text>().text = "Range: " + selection.GetComponent<TransportCar>().transported1.range; break;
                            case 4: GetComponent<Text>().text = "Range: 2"; break;
                            case 5: GetComponent<Text>().text = "Range: 0"; break;
                            case 8: GetComponent<Text>().text = "Points: 1/4"; break;
                            case 12: GetComponent<Text>().text = "Range: " + selection.range;break;
                            case 13: GetComponent<Text>().text = "Range: 1"; break;
                            default: GetComponent<Text>().text = ""; ; break;
                        }
                    }
                    if (type == 5)
                    {
                        switch (selection.type)
                        {
                            case 1: GetComponent<Text>().text = "Move: 1 (1/4)"; break;
                            case 2: GetComponent<Text>().text = "Move: 2 (1/4)"; break;
                            case 3: GetComponent<Text>().text = "Move: " + selection.GetComponent<TransportCar>().transported1.move + " (" + selection.GetComponent<TransportCar>().transported1.moveCost + "/4)"; break;
                            case 4: GetComponent<Text>().text = "Move: 2 (3/4)"; break;
                            case 5: GetComponent<Text>().text = "Move: 14 (4/4)"; break;
                            case 12: GetComponent<Text>().text = "Points: 1/4"; break;
                            case 13: GetComponent<Text>().text = "Points: 2/4"; break;
                            default: GetComponent<Text>().text = ""; break;
                        }
                    }
                    if (type == 6)
                        switch (selection.type)
                        {
                            case 1: GetComponent<Text>().text = datager.rusherDesc; break;
                            case 2: GetComponent<Text>().text = datager.demolitionistDesc; break;
                            case 3: GetComponent<Text>().text = selection.GetComponent<TransportCar>().transported1.desc; break;
                            case 4: GetComponent<Text>().text = datager.missileTankDesc; break;
                            case 5: GetComponent<Text>().text = datager.bomberDesc; break;
                            case 8: GetComponent<Text>().text = datager.ballisticDesc; break;
                            case 12: GetComponent<Text>().text = datager.repairDesc; break;
                            case 13: GetComponent<Text>().text = datager.attackAirDesc; break;
                            default: GetComponent<Text>().text = ""; break;
                        }
                }
                if (mapager.manager.ability == 4)
                {
                    if (selection.type == 3 && selection.GetComponent<TransportCar>().transported2 == null)
                    {
                        GetComponent<Text>().text = "";
                        return;
                    }
                    if (type == 0)
                        switch (selection.type)
                        {
                            case 1: GetComponent<Text>().text = "Ranger"; break;
                            case 2: GetComponent<Text>().text = "Saboteur"; break;
                            case 3: GetComponent<Text>().text = selection.GetComponent<TransportCar>().transported2.name.Replace("piece", ""); break;
                            case 4: GetComponent<Text>().text = "Upgrade Movement"; break;
                            case 8: GetComponent<Text>().text = "Upgrade Movement"; break;
                            default: GetComponent<Text>().text = ""; break;
                        }
                    if (type == 1)
                    {
                        switch (selection.type)
                        {
                            case 1: GetComponent<Text>().text = "Cost: " + datager.rangerCost; break;
                            case 2: GetComponent<Text>().text = "Cost: " + datager.saboteurCost; break;
                            case 3: GetComponent<Text>().text = "Health: " + selection.GetComponent<TransportCar>().transported2.health; break;
                            case 4: GetComponent<Text>().text = "Cost: " + datager.tankMoveCost; break;
                            case 8: GetComponent<Text>().text = "Cost: " + datager.tankMoveCost; break;
                            default: GetComponent<Text>().text = ""; break;
                        }
                    }
                    if (type == 2)
                    {
                        switch (selection.type)
                        {
                            case 1: GetComponent<Text>().text = "Health: 1"; break;
                            case 2: GetComponent<Text>().text = "Health: 5"; break;
                            case 3: GetComponent<Text>().text = "Maximum: " + selection.GetComponent<TransportCar>().transported2.healthMax; break;
                            default: GetComponent<Text>().text = ""; ; break;
                        }
                    }
                    if (type == 3)
                        switch (selection.type)
                        {
                            case 1: GetComponent<Text>().text = "Attack: 1"; break;
                            case 3:
                                if (selection.GetComponent<TransportCar>().transported2.type != 2) GetComponent<Text>().text = "Damage: " + selection.GetComponent<TransportCar>().transported2.attack;
                                else GetComponent<Text>().text = "Repair: " + selection.GetComponent<TransportCar>().transported2.attack;
                                break;
                            default: GetComponent<Text>().text = ""; ; break;
                        }
                    if (type == 4)
                    {
                        switch (selection.type)
                        {
                            case 1: GetComponent<Text>().text = "Range: 7"; break;
                            case 2: GetComponent<Text>().text = "Range: 1"; break;
                            case 3: GetComponent<Text>().text = "Range: " + selection.GetComponent<TransportCar>().transported2.range; break;
                            default: GetComponent<Text>().text = ""; ; break;
                        }
                    }
                    if (type == 5)
                    {
                        switch (selection.type)
                        {
                            case 1: GetComponent<Text>().text = "Move: 1 (1/4)"; break;
                            case 2: GetComponent<Text>().text = "Move: 2 (1/4)"; break;
                            case 3: GetComponent<Text>().text = "Move: " + selection.GetComponent<TransportCar>().transported2.move + " (" + selection.GetComponent<TransportCar>().transported2.moveCost + "/4)"; break;
                            case 4: GetComponent<Text>().text = "Move: "+selection.move+"+1 (2/4)"; break;
                            case 8: GetComponent<Text>().text = "Move: " + selection.move + "+1 (2/4)"; break;
                            default: GetComponent<Text>().text = ""; ; break;
                        }
                    }
                    if (type == 6)
                        switch (selection.type)
                        {
                            case 1: GetComponent<Text>().text = datager.rangerDesc; break;
                            case 2: GetComponent<Text>().text = datager.saboteurDesc; break;
                            case 3: GetComponent<Text>().text = selection.GetComponent<TransportCar>().transported2.desc; break;
                            case 4: GetComponent<Text>().text = datager.tankMoveDesc; break;
                            case 8: GetComponent<Text>().text = datager.tankMoveDesc; break;
                            default: GetComponent<Text>().text = ""; ; break;
                        }
                }
                if (mapager.manager.ability == 5)
                {
                    if (selection.type == 3 && selection.GetComponent<TransportCar>().transported3 == null)
                    {
                        GetComponent<Text>().text = "";
                        return;
                    }
                    if (type == 0)
                        switch (selection.type)
                        {
                            case 1: GetComponent<Text>().text = "Rider"; break;
                            case 3: GetComponent<Text>().text = selection.GetComponent<TransportCar>().transported3.name.Replace("piece", ""); break;
                            case 4: GetComponent<Text>().text = "Upgrade Attack"; break;
                            case 8: GetComponent<Text>().text = "Upgrade Attack"; break;
                            default: GetComponent<Text>().text = ""; break;
                        }
                    if (type == 1)
                    {
                        switch (selection.type)
                        {
                            case 1: GetComponent<Text>().text = "Cost: " + datager.riderCost; break;
                            case 3: GetComponent<Text>().text = "Health: " + selection.GetComponent<TransportCar>().transported3.health; break;
                            case 4: GetComponent<Text>().text = "Cost: " + datager.tankAttackCost; break;
                            case 8: GetComponent<Text>().text = "Cost: " + datager.tankAttackCost; break;
                            default: GetComponent<Text>().text = ""; break;
                        }
                    }
                    if (type == 2)
                    {
                        switch (selection.type)
                        {
                            case 1: GetComponent<Text>().text = "Health: 2"; break;
                            case 3: GetComponent<Text>().text = "Maximum: " + selection.GetComponent<TransportCar>().transported3.healthMax; break;
                            default: GetComponent<Text>().text = ""; ; break;
                        }
                    }
                    if (type == 3)
                        switch (selection.type)
                        {
                            case 1: GetComponent<Text>().text = "Attack: 1"; break;
                            case 3:
                                if (selection.GetComponent<TransportCar>().transported3.type != 2) GetComponent<Text>().text = "Damage: " + selection.GetComponent<TransportCar>().transported3.attack;
                                else GetComponent<Text>().text = "Repair: " + selection.GetComponent<TransportCar>().transported3.attack;
                                break;
                            case 4: GetComponent<Text>().text = "  Damage: "+selection.attack+"+1"; break;
                            case 8: GetComponent<Text>().text = "  Damage: " + selection.attack + "+1"; break;
                            default: GetComponent<Text>().text = ""; ; break;
                        }
                    if (type == 4)
                    {
                        switch (selection.type)
                        {
                            case 1: GetComponent<Text>().text = "Range: 2"; break;
                            case 3: GetComponent<Text>().text = "Range: " + selection.GetComponent<TransportCar>().transported3.range; break;
                            default: GetComponent<Text>().text = ""; ; break;
                        }
                    }
                    if (type == 5)
                    {
                        switch (selection.type)
                        {
                            case 1: GetComponent<Text>().text = "Move: 3 (1/4)"; break;
                            case 3: GetComponent<Text>().text = "Move: " + selection.GetComponent<TransportCar>().transported3.move + " (" + selection.GetComponent<TransportCar>().transported3.moveCost + "/4)"; break;
                            default: GetComponent<Text>().text = ""; ; break;
                        }
                    }
                    if (type == 6)
                        switch (selection.type)
                        {
                            case 1: GetComponent<Text>().text = datager.riderDesc; break;
                            case 3: GetComponent<Text>().text = selection.GetComponent<TransportCar>().transported3.desc; break;
                            case 4: GetComponent<Text>().text = datager.tankAttackDesc; break;
                            case 8: GetComponent<Text>().text = datager.tankAttackDesc; break;
                            default: GetComponent<Text>().text = ""; ; break;
                        }
                }
                    if (mapager.manager.ability == 6)
                    {

                        if (type == 0)
                            switch (selection.type)
                            {
                            case 1: GetComponent<Text>().text = "Rocket Troop"; break;
                            case 2: GetComponent<Text>().text = "Drone"; break;
                            case 6: GetComponent<Text>().text = "Drone"; break;
                            case 9: GetComponent<Text>().text = "Drone"; break;
                            case 3: GetComponent<Text>().text = "Build Base"; break;
                            case 4: GetComponent<Text>().text = "Cloaking Field"; break;
                            case 8: GetComponent<Text>().text = "Cloaking Field"; break;
                            case 5: GetComponent<Text>().text = "Select Below"; break;
                            case 12: GetComponent<Text>().text = "Select Below"; break;
                            case 14: GetComponent<Text>().text = "Select Below"; break;
                            default: GetComponent<Text>().text = ""; break;
                            }
                        if (type == 1)
                        {
                            switch (selection.type)
                            {
                                case 1: GetComponent<Text>().text = "Cost: " + datager.rocketLauncherCost; break;
                                case 2: GetComponent<Text>().text = "Cost: " + mapager.drone.cost; break;
                                case 6: GetComponent<Text>().text = "Cost: " + mapager.drone.cost; break;
                                case 9: GetComponent<Text>().text = "Cost: " + mapager.drone.cost; break;
                                case 3: GetComponent<Text>().text = "Cost: " + datager.baseCost; break;
                                case 4: GetComponent<Text>().text = "Cost: " + datager.tankStealthCost; break;
                                case 8: GetComponent<Text>().text = "Cost: " + datager.tankStealthCost; break;
                                default: GetComponent<Text>().text = ""; break;
                            }
                        }
                        if (type == 2)
                        {
                            switch (selection.type)
                            {
                            case 1: GetComponent<Text>().text = "Health: 2"; break;
                            case 2: GetComponent<Text>().text = "Health: " + mapager.drone.healthMax; break;
                            case 6: GetComponent<Text>().text = "Health: " + mapager.drone.healthMax; break;
                            case 9: GetComponent<Text>().text = "Health: " + mapager.drone.healthMax; break;
                            case 3: GetComponent<Text>().text = "Health: 10"; break;
                            case 5: GetComponent<Text>().text = "Placement: Selected Unit"; break;
                            case 12: GetComponent<Text>().text = "Placement: Selected Unit"; break;
                            case 14: GetComponent<Text>().text = "Placement: Selected Unit"; break;
                            default: GetComponent<Text>().text = ""; break;
                            }
                        }
                        if (type == 3)
                            switch (selection.type)
                            {
                            case 1: GetComponent<Text>().text = "Attack: 2"; break;
                            case 2: GetComponent<Text>().text = "Damage: " + mapager.drone.attack; break;
                            case 6: GetComponent<Text>().text = "Damage: " + mapager.drone.attack; break;
                            case 9: GetComponent<Text>().text = "Damage: " + mapager.drone.attack; break;
                            default: GetComponent<Text>().text = ""; break;
                            }
                        if (type == 4)
                        {
                            switch (selection.type)
                            {
                            case 1: GetComponent<Text>().text = "Range: 2";break;
                            case 2: GetComponent<Text>().text = "Range: " + mapager.drone.range; break;
                            case 6: GetComponent<Text>().text = "Range: " + mapager.drone.range; break;
                            case 9: GetComponent<Text>().text = "Range: " + mapager.drone.range; break;
                            case 3: GetComponent<Text>().text = "Range: 2"; break;
                                default: GetComponent<Text>().text = ""; ; break;
                            }
                        }
                        if (type == 5)
                        {
                            switch (selection.type)
                            {
                            case 1: GetComponent<Text>().text = "Move: 1 (1/4)"; break;
                            case 2: GetComponent<Text>().text = "Move: " + mapager.drone.move+" ("+mapager.drone.moveCost+"/4)"; break;
                            case 6: GetComponent<Text>().text = "Move: " + mapager.drone.move + " (" + mapager.drone.moveCost + "/4)"; break;
                            case 9: GetComponent<Text>().text = "Move: " + mapager.drone.move + " (" + mapager.drone.moveCost + "/4)"; break;
                            default: GetComponent<Text>().text = ""; break;
                            }
                        }
                        if (type == 6)
                            switch (selection.type)
                            {
                                case 1: GetComponent<Text>().text = datager.rocketLauncherDesc; break;
                                case 2: GetComponent<Text>().text = mapager.drone.desc; break;
                                case 6: GetComponent<Text>().text = mapager.drone.desc; break;
                                case 9: GetComponent<Text>().text = mapager.drone.desc; break;  
                                case 3: GetComponent<Text>().text = datager.baseDesc; break;
                                case 4: GetComponent<Text>().text = datager.tankStealthDesc; break;
                                case 8: GetComponent<Text>().text = datager.tankStealthDesc; break;
                                case 5: GetComponent<Text>().text = datager.selectBelowDesc; break;
                                case 12: GetComponent<Text>().text = datager.selectBelowDesc; break;
                                case 14: GetComponent<Text>().text = datager.selectBelowDesc; break;
                                default: GetComponent<Text>().text = ""; break;
                            }
                    }
                }
                    /* switch (selection.type)
                     {
                         case 0:
                             if (mapager.manager.ability == 0){
                                 if (type == 0) GetComponent<Text>().text = "Name: Base";
                                 if (type == 1) GetComponent<Text>().text = "Health: " + selection.health;
                                 if (type == 2) GetComponent<Text>().text = "Maximum: " + selection.healthMax;
                                 if (type == 3) GetComponent<Text>().text = "";
                                 if (type == 4) GetComponent<Text>().text = "";
                                 if (type == 5) GetComponent<Text>().text = "";
                                 //if (type == 3) GetComponent<Text>().text = "Damage: " + selection.attack;
                                 //if (type == 4) GetComponent<Text>().text = "Range: " + selection.range;
                                 //if (type == 5) GetComponent<Text>().text = "Move: " + selection.move+" ("+selection.moveCost+"/4)";
                             }
                             //if (mapager.manager.ability < 5)
                                // mapager.HighlightPath(selection.xx, selection.yy, 2, 0, true);
                             //if (mapager.manager.ability == 5)
                              //   mapager.HighlightPath(selection.xx, selection.yy, 2, 4, true);
                             //if (mapager.manager.ability == 6) mapager.highlight[selection.xx, selection.yy] = 1;
                             break;
                             /*case 1:
                                 if (mapager.manager.ability == 0) break;
                                 if (mapager.manager.ability == 1)
                                     mapager.HighlightPath(selection.xx, selection.yy, selection.move, 1, true);
                                 if (mapager.manager.ability == 2)
                                     mapager.HighlightPath(selection.xx, selection.yy, selection.range, 5, true);
                                 if (mapager.manager.ability == 3)
                                     mapager.HighlightPath(selection.xx, selection.yy, 1, 0, true);
                                 break;
                             case 2:
                                 if (mapager.manager.ability == 0) break;
                                 if (mapager.manager.ability == 1)
                                     mapager.HighlightPath(selection.xx, selection.yy, selection.move, 1, true);
                                 if (mapager.manager.ability == 2)
                                     mapager.HighlightPath(selection.xx, selection.yy, selection.range, 5, true);
                                 if (mapager.manager.ability == 3)
                                     mapager.HighlightPath(selection.xx, selection.yy, 1, 1, true);
                                 break;
                             case 3:
                                 if (mapager.manager.ability == 0) break;
                                 if (mapager.manager.ability == 1)
                                     mapager.HighlightPath(selection.xx, selection.yy, selection.move, 2, true);
                                 if (mapager.manager.ability == 2)
                                     mapager.HighlightPath(selection.xx, selection.yy, selection.range, 5, true);
                                 if (mapager.manager.ability >= 3 && mapager.manager.ability <= 5)
                                     mapager.HighlightPath(selection.xx, selection.yy, selection.range, 0, true);
                                 if (mapager.manager.ability == 6) mapager.highlight[selection.xx, selection.yy] = 1;
                                 break;
                             case 4:
                                 if (mapager.manager.ability == 0) break;
                                 if (mapager.manager.ability == 1)
                                     mapager.HighlightPath(selection.xx, selection.yy, selection.move, 3, true);
                                 if (mapager.manager.ability == 2)
                                     mapager.HighlightPath(selection.xx, selection.yy, selection.range, 5, true);
                                 break;
                             case 5:
                                 if (mapager.manager.ability == 0) break;
                                 if (mapager.manager.ability == 1)
                                     for (int i = 0; i < 9; i++)
                                         for (int j = 0; j < 9; j++)
                                         {
                                             if (!(i == selection.xx && j == selection.yy))
                                                 mapager.highlight[i, j] = 1;
                                         }
                                 if (mapager.manager.ability == 2)
                                     mapager.highlight[selection.xx, selection.yy] = 1;
                                 break;
                             case 7:
                                 if (mapager.manager.ability == 0) break;
                                 if (mapager.manager.ability == 1)
                                     mapager.HighlightPath(selection.xx, selection.yy, selection.move, 0, true);
                                 if (mapager.manager.ability == 2)
                                     mapager.HighlightPath(selection.xx, selection.yy, selection.range, 5, true);
                                 break;
                             default:
                                 if (mapager.manager.ability == 0) break;
                                 if (mapager.manager.ability == 1)
                                     mapager.HighlightPath(selection.xx, selection.yy, selection.move, 1, true);
                                 if (mapager.manager.ability == 2)
                                     mapager.HighlightPath(selection.xx, selection.yy, selection.range, 5, true);
                                 //else highlight[selection.xx, selection.yy] = 1;
                                 break;
                     }*/
                
            }
        }
    }
