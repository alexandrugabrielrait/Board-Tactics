using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpManager : MonoBehaviour {

    public Unit[] units;
    public Unit[] nonUnits;
    public Unit currentUnit;
    public int unitType = 0;
    public GameObject currentPiece;
    public int contentIndex=1;

    void start()
    {
        currentUnit = units[0];
    }

	public void ChangeUnit (int integer) {
        contentIndex = 1;
        unitType += integer;
        currentUnit = units[unitType];
        currentPiece.GetComponent<SpriteRenderer>().sprite = currentUnit.GetComponent<SpriteRenderer>().sprite;

    }

    public void ChangeUnitTo(int integer)
    {
        contentIndex = 1;
        unitType = integer;
        currentUnit = units[unitType];
        currentPiece.GetComponent<SpriteRenderer>().sprite = currentUnit.GetComponent<SpriteRenderer>().sprite;

    }

    public void ChangePageToNonUnit(int integer)
    {
        contentIndex = 1;
        unitType = integer;
        currentUnit = nonUnits[-unitType-1];
        currentPiece.GetComponent<SpriteRenderer>().sprite = currentUnit.GetComponent<SpriteRenderer>().sprite;

    }

}
