using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButton : MonoBehaviour
{

    public string type;
    public int index;
    public HelpManager manager;

    void OnMouseDown()
    {
        if (type == "right") manager.ChangeUnit(1);//manager.transform.position = new Vector3(manager.transform.position.x + 10, manager.transform.position.y, manager.transform.position.z);
        if (type == "left") manager.ChangeUnit(-1);//manager.transform.position = new Vector3(manager.transform.position.x - 10, manager.transform.position.y, manager.transform.position.z);
        if (type == "unit") { manager.transform.position = new Vector3(manager.transform.position.x + 11.42091f, manager.transform.position.y, manager.transform.position.z); if (index >= 0) manager.ChangeUnitTo(index); else manager.ChangePageToNonUnit(index); }
        if (type == "content") { manager.transform.position = new Vector3(manager.transform.position.x - 11.42091f * manager.contentIndex, manager.transform.position.y, manager.transform.position.z); manager.contentIndex *= -1; }
    }

    void FixedUpdate()
    {
        GetComponent<BoxCollider2D>().enabled = true; GetComponent<SpriteRenderer>().enabled = true;
        if (type == "left" && manager.unitType <= 0) { GetComponent<BoxCollider2D>().enabled = false; GetComponent<SpriteRenderer>().enabled = false; }
        if (type == "right" && (manager.unitType >= 14|| manager.unitType < 0)) { GetComponent<BoxCollider2D>().enabled = false; GetComponent<SpriteRenderer>().enabled = false; }
        if (type == "content") if (manager.contentIndex == -1) GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonCancel", typeof(Sprite)) as Sprite; else GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonTechtree", typeof(Sprite)) as Sprite;
    }
}
