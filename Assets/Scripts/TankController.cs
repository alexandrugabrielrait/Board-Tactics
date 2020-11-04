using UnityEngine;
using System.Collections;

public class TankController : MonoBehaviour {

    public int player;
    public int direction; //-1down,0right,1up,2left
    public int country; //0-7 USA, GB, Fr, USSR, Ger, It, Jp, Ro
    public int xx;
    public int yy;
    public int move=1;
    public int range=2;
    public int mode=0;
    public TurnManager manager;
    public GameObject tank;

    public void Death()
    {
        //tank.GetComponent<SpriteRenderer>().sprite = null;
        manager.Win(1-player);
        Destroy(tank);
    }

    // Update is called once per frame
    void Update () {
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 90.0f * direction);
        transform.position = new Vector3(xx - 3.5f, yy -2.5f, 0);
        if(mode==0)
        switch (country)
        {
            case 0: GetComponent<SpriteRenderer>().sprite = Resources.Load("tankAmerican", typeof(Sprite)) as Sprite;break;
            case 1: GetComponent<SpriteRenderer>().sprite = Resources.Load("tankBritish", typeof(Sprite)) as Sprite; break;
            case 2: GetComponent<SpriteRenderer>().sprite = Resources.Load("tankFrench", typeof(Sprite)) as Sprite; break;
            case 3: GetComponent<SpriteRenderer>().sprite = Resources.Load("tankRussian", typeof(Sprite)) as Sprite; break;
            case 4: GetComponent<SpriteRenderer>().sprite = Resources.Load("tankGerman", typeof(Sprite)) as Sprite; break;
            case 5: GetComponent<SpriteRenderer>().sprite = Resources.Load("tankItalian", typeof(Sprite)) as Sprite; break;
            case 6: GetComponent<SpriteRenderer>().sprite = Resources.Load("tankJapanese", typeof(Sprite)) as Sprite; break;
            default: GetComponent<SpriteRenderer>().sprite = Resources.Load("tankRomanian", typeof(Sprite)) as Sprite; break;
        }
        else
        {
            switch (country)
            {
                case 2: GetComponent<SpriteRenderer>().sprite = Resources.Load("tankFrenchStationary", typeof(Sprite)) as Sprite; break;
                case 3: GetComponent<SpriteRenderer>().sprite = Resources.Load("tankRussianArmor", typeof(Sprite)) as Sprite;break;
                default: GetComponent<SpriteRenderer>().sprite = Resources.Load("tankJapanese", typeof(Sprite)) as Sprite;break;
            }
        }
	}
}
