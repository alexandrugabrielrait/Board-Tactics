using UnityEngine;
using System.Collections;

public class PlayerFlag : MonoBehaviour {

    public TankController tank;
    public bool isCountry;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(isCountry)
        switch (tank.country)
        {
            case 0: GetComponent<SpriteRenderer>().sprite = Resources.Load("flagUSA", typeof(Sprite)) as Sprite; break;
            case 1: GetComponent<SpriteRenderer>().sprite = Resources.Load("flagUK", typeof(Sprite)) as Sprite; break;
            case 2: GetComponent<SpriteRenderer>().sprite = Resources.Load("flagFrance", typeof(Sprite)) as Sprite; break;
            case 3: GetComponent<SpriteRenderer>().sprite = Resources.Load("flagUSSR", typeof(Sprite)) as Sprite; break;
            case 4: GetComponent<SpriteRenderer>().sprite = Resources.Load("flagNazi", typeof(Sprite)) as Sprite; break;
            case 5: GetComponent<SpriteRenderer>().sprite = Resources.Load("flagItaly", typeof(Sprite)) as Sprite; break;
            case 6: GetComponent<SpriteRenderer>().sprite = Resources.Load("flagJapan", typeof(Sprite)) as Sprite; break;
            default: GetComponent<SpriteRenderer>().sprite = Resources.Load("flagRomania", typeof(Sprite)) as Sprite; break;
        }
        else if(tank.country<4) GetComponent<SpriteRenderer>().sprite = Resources.Load("flagAllies", typeof(Sprite)) as Sprite;
        else GetComponent<SpriteRenderer>().sprite = Resources.Load("flagAxis", typeof(Sprite)) as Sprite;
    }
}
