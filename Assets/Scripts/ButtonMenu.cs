using UnityEngine;
using System.Collections;

public class ButtonMenu : MonoBehaviour {

    public TurnManager manager;

    // Use this for initialization
    void OnMouseDown()
    {
        manager.transform.position = new Vector3(43.79f - manager.transform.position.x, manager.transform.position.y, manager.transform.position.z);
    }
}
