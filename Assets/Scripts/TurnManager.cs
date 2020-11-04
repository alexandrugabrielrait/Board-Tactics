using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TurnManager : MonoBehaviour {

    public int turn = 1;
    public int player;
    public int moves=4;
    public int movesMax=4;
    public int x = 0;
    public bool win = false;
    public float t;
    public Unit baseEmpty;
    public Unit base0;
    public Unit base1;
    public int mode = 0;
    public int firstPick;
    public ButtonVictory vic;
    public Color color0;
    public Color color1;
    public MapManager mapager;
    public GameObject select;
    public int ability;
    public int firstColor=-1;
    public int resource0 = 1;
    public int resource1 = 0;
    public int gain0 = 1;
    public int gain1 = 1;
    float y = 0;
    public bool pc = false;
    public GameObject multiple;
    public DataManager datager;

    // Use this for initialization
    void Start () {
        //Screen.orientation = ScreenOrientation.Portrait;
        player = 0;
        /*int rand = Random.Range(0, 9);
        switch (rand)
        {
            case 0: color1 = Color.blue; break;
            case 1: color1 = Color.white; break;
            case 2: color1 = Color.cyan; break;
            case 3: color1 = Color.green; break;
            case 4: color1 = Color.magenta; break;
            case 5: color1 = Color.red; break;
            case 6: color1 = Color.yellow; break;
            case 7: color1 = new Color(1, 0.4f, 0, 1); break;
            case 8: color1 = new Color(0.133f, 0.35f, 0.17f, 1); break;
        }
        int randy = rand;
        while (rand == randy)
        {
            rand = Random.Range(0, 9);
        }
        switch (rand)
        {
            case 0: color0 = Color.blue; break;
            case 1: color0 = Color.white; break;
            case 2: color0 = Color.cyan; break;
            case 3: color0 = Color.green; break;
            case 4: color0 = Color.magenta; break;
            case 5: color0 = Color.red; break;
            case 6: color0 = Color.yellow; break;
            case 7: color0 = new Color(1, 0.4f, 0, 1); break;
            case 8: color0 = new Color(0.133f, 0.35f, 0.17f, 1); break;
        }*/
    }

    public void Win(int won)
    {
        if (won == -1) return;
        //timer = t;
        ability = 0;
        mapager.selected = baseEmpty;
        Destroy(mapager.select);
        player = 2;
        win = true;
        switch (won) {
        case 0: vic.Color.GetComponent<SpriteRenderer>().color = color0;break;
        case 1: vic.Color.GetComponent<SpriteRenderer>().color = color1;  vic.GetComponent<SpriteRenderer>().flipX = true; vic.GetComponent<SpriteRenderer>().flipY = true; break;
        default: vic.Color.GetComponent<SpriteRenderer>().color = new Color(0.68f, 0.812f, 1,1); vic.GetComponent<SpriteRenderer>().sprite = Resources.Load("buttonDrawWhite", typeof(Sprite)) as Sprite;break; }
        //new Vector2(1.02f, 1.97f);
    }

    public void Passed()
    {
        turn++;
        moves = movesMax;
        player = 1 - player;
        if (player == 1 && GetComponent<Tutorial>() != null) GetComponent<Tutorial>().AITurn();
        foreach (HiglightLand land in FindObjectsOfType<HiglightLand>())
        {
            land.Ballistic(player);
        }
        ability = 0;
        mapager.ClearHighlights();
        mapager.selected = null;
        foreach (Unit base2 in FindObjectsOfType<Unit>())
        {
            if (base2.type == 0 && player == base2.player) mapager.selected = base2;
        }
        if (mapager.selected == null)
            foreach (Unit base2 in FindObjectsOfType<Unit>())
            {
                if (base2.type == 3 && player == base2.player) mapager.selected = base2;
            }
        if (player == 0) {resource0+=gain0; }
        else {resource1+=gain1; }
    }

    void Update()
    {
        if (win == true)
            return;
        if (Input.GetKeyDown(KeyCode.Menu))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (t == 2&&mode!=-1)
        {
            transform.position = new Vector3(multiple.transform.position.x, multiple.transform.position.y, transform.position.z);
            mapager.selected = base0;
            if (mode == 1)
            {
                Unit unit;
                unit = (Unit)Instantiate(mapager.transport, new Vector3(base0.xx - 3.5f, base0.yy - 2.5f, base0.transform.position.z), base0.transform.rotation); unit.player = base0.player; unit.xx = base0.xx; unit.yy = base0.yy; unit.name = mapager.transport.name;
                unit.GetComponent<TransportCar>().transported1 = mapager.rifleman;
                unit.GetComponent<TransportCar>().transported2 = mapager.rifleman;
                unit.GetComponent<TransportCar>().transported3 = mapager.rifleman;
                mapager.selected = unit; 
                Destroy(base0.gameObject);
                unit = (Unit)Instantiate(mapager.transport, new Vector3(base1.xx - 3.5f, base1.yy - 2.5f, base1.transform.position.z), base1.transform.rotation); unit.player = base1.player; unit.xx = base1.xx; unit.yy = base1.yy; unit.name = mapager.transport.name;
                unit.GetComponent<TransportCar>().transported1 = mapager.rifleman;
                unit.GetComponent<TransportCar>().transported2 = mapager.rifleman;
                unit.GetComponent<TransportCar>().transported3 = mapager.rifleman;
                Destroy(base1.gameObject);
                resource0 = 7;
                resource1 = 7;
                gain0 = 0;
                gain1 = 0;
            }
            if (mode == 2)
            {
                base0.xx = 5;base0.yy = 1;
                base1.xx = 5; base1.yy = 8;
                Unit unit;
                for (int i = 1; i <= 8; i++)
                {
                unit = (Unit)Instantiate(mapager.rifleman, new Vector3(base0.xx - 3.5f, base0.yy - 2.5f, base0.transform.position.z), base0.transform.rotation); unit.player = base0.player; unit.xx = i; unit.yy = 2; unit.name = mapager.rifleman.name;
                unit = (Unit)Instantiate(mapager.rifleman, new Vector3(base1.xx - 3.5f, base1.yy - 2.5f, base1.transform.position.z), base1.transform.rotation); unit.player = base1.player; unit.xx = i; unit.yy = 7; unit.name = mapager.rifleman.name;
                }
                unit = (Unit)Instantiate(mapager.rifleman, new Vector3(base0.xx - 3.5f, base0.yy - 2.5f, base0.transform.position.z), base0.transform.rotation); unit.player = base0.player; unit.xx = 2; unit.yy = 1;
                unit.name = "pieceRider"; unit.GetComponent<SpriteRenderer>().sprite = Resources.Load("pieceRider", typeof(Sprite)) as Sprite; unit.type = 10; unit.desc = datager.riderDesc; unit.healthMax = 2; unit.health = 2; unit.move = 4;
                unit = (Unit)Instantiate(mapager.rifleman, new Vector3(base1.xx - 3.5f, base1.yy - 2.5f, base1.transform.position.z), base1.transform.rotation); unit.player = base1.player; unit.xx = 2; unit.yy = 8;
                unit.name = "pieceRider"; unit.GetComponent<SpriteRenderer>().sprite = Resources.Load("pieceRider", typeof(Sprite)) as Sprite; unit.type = 10; unit.desc = datager.riderDesc; unit.healthMax = 2; unit.health = 2; unit.move = 4;
                unit = (Unit)Instantiate(mapager.rifleman, new Vector3(base0.xx - 3.5f, base0.yy - 2.5f, base0.transform.position.z), base0.transform.rotation); unit.player = base0.player; unit.xx = 7; unit.yy = 1;
                unit.name = "pieceRider"; unit.GetComponent<SpriteRenderer>().sprite = Resources.Load("pieceRider", typeof(Sprite)) as Sprite; unit.type = 10; unit.desc = datager.riderDesc; unit.healthMax = 2; unit.health = 2; unit.move = 4;
                unit = (Unit)Instantiate(mapager.rifleman, new Vector3(base1.xx - 3.5f, base1.yy - 2.5f, base1.transform.position.z), base1.transform.rotation); unit.player = base1.player; unit.xx = 7; unit.yy = 8;
                unit.name = "pieceRider"; unit.GetComponent<SpriteRenderer>().sprite = Resources.Load("pieceRider", typeof(Sprite)) as Sprite; unit.type = 10; unit.desc = datager.riderDesc; unit.healthMax = 2; unit.health = 2; unit.move = 4;
                unit = (Unit)Instantiate(mapager.tank, new Vector3(base0.xx - 3.5f, base0.yy - 2.5f, base0.transform.position.z), base0.transform.rotation); unit.player = base0.player; unit.xx = 4; unit.yy = 1; unit.name = mapager.tank.name; unit.moveCost = 2;
                unit = (Unit)Instantiate(mapager.tank, new Vector3(base1.xx - 3.5f, base1.yy - 2.5f, base1.transform.position.z), base1.transform.rotation); unit.player = base1.player; unit.xx = 4; unit.yy = 8; unit.name = mapager.tank.name; unit.moveCost = 2;
                unit = (Unit)Instantiate(mapager.rifleman, new Vector3(base0.xx - 3.5f, base0.yy - 2.5f, base0.transform.position.z), base0.transform.rotation); unit.player = base0.player; unit.xx = 1; unit.yy = 1;
                unit.name = "pieceRanger"; unit.GetComponent<SpriteRenderer>().sprite = Resources.Load("pieceRanger", typeof(Sprite)) as Sprite; unit.type = 11; unit.desc = datager.rangerDesc; unit.range = 7;
                unit = (Unit)Instantiate(mapager.rifleman, new Vector3(base1.xx - 3.5f, base1.yy - 2.5f, base1.transform.position.z), base1.transform.rotation); unit.player = base1.player; unit.xx = 1; unit.yy = 8;
                unit.name = "pieceRanger"; unit.GetComponent<SpriteRenderer>().sprite = Resources.Load("pieceRanger", typeof(Sprite)) as Sprite; unit.type = 11; unit.desc = datager.rangerDesc; unit.range = 7;
                unit = (Unit)Instantiate(mapager.rifleman, new Vector3(base0.xx - 3.5f, base0.yy - 2.5f, base0.transform.position.z), base0.transform.rotation); unit.player = base0.player; unit.xx = 8; unit.yy = 1;
                unit.name = "pieceRanger"; unit.GetComponent<SpriteRenderer>().sprite = Resources.Load("pieceRanger", typeof(Sprite)) as Sprite; unit.type = 11; unit.desc = datager.rangerDesc; unit.range = 7;
                unit = (Unit)Instantiate(mapager.rifleman, new Vector3(base1.xx - 3.5f, base1.yy - 2.5f, base1.transform.position.z), base1.transform.rotation); unit.player = base1.player; unit.xx = 8; unit.yy = 8;
                unit.name = "pieceRanger"; unit.GetComponent<SpriteRenderer>().sprite = Resources.Load("pieceRanger", typeof(Sprite)) as Sprite; unit.type = 11; unit.desc = datager.rangerDesc; unit.range = 7;
                unit = (Unit)Instantiate(mapager.drone, new Vector3(base0.xx - 3.5f, base0.yy - 2.5f, base0.transform.position.z), base0.transform.rotation); unit.player = base0.player; unit.xx = 3; unit.yy = 1; unit.name = mapager.drone.name;
                unit = (Unit)Instantiate(mapager.drone, new Vector3(base1.xx - 3.5f, base1.yy - 2.5f, base1.transform.position.z), base1.transform.rotation); unit.player = base1.player; unit.xx = 3; unit.yy = 8; unit.name = mapager.drone.name;
                unit = (Unit)Instantiate(mapager.drone, new Vector3(base0.xx - 3.5f, base0.yy - 2.5f, base0.transform.position.z), base0.transform.rotation); unit.player = base0.player; unit.xx = 6; unit.yy = 1; unit.name = mapager.drone.name;
                unit = (Unit)Instantiate(mapager.drone, new Vector3(base1.xx - 3.5f, base1.yy - 2.5f, base1.transform.position.z), base1.transform.rotation); unit.player = base1.player; unit.xx = 6; unit.yy = 8; unit.name = mapager.drone.name;
                movesMax = 2;
                resource0 = 0;
                resource1 = 0;
                gain0 = 0;
                gain1 = 0;
            }
                if (mode == 3)
            {
                resource0 = resource1 = 1000;
                gain0 = gain1 = 10;
                movesMax = 9999;
                mode = 0;
            }
            moves = movesMax;
            t++;
        }
        int x = -1;
        if (mapager.bases1 == 0) x = 0;
        if (mapager.bases0 == 0) x += 2;
        Win(x);
        if (ability == 0) select.GetComponent<SpriteRenderer>().color = Color.clear;
        else 
            select.GetComponent<SpriteRenderer>().color = new Color(0.615f, 1, 0, 1);
    }

    void FixedUpdate()
    {
        if (win == true)
        {
            y += Time.deltaTime;
            if (y >= 5)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            return;
        }
        if (pc == true)
        {
            //this.GetComponent<Camera>().orthographicSize = 6.36764f;
            if (player == 0)
            {
                //transform.position = new Vector3(0.95f, -0.3f, -5);
                transform.rotation = Quaternion.Euler(0, 7.5249f, 0);
                //transform.localScale = new Vector3(9, 3.82f, 1);
            }
            if (player == 1)
            {
                //transform.position = new Vector3(1f, 4.28f, -5);
                transform.rotation = Quaternion.Euler(0, 7.5249f, -180);
                //transform.localScale = new Vector3(9, 3.82f, 1);
            }
        }
    }


        // Update is called once per frame
        /*void FixedUpdate () {
            if (x < 2) return;
            timer -= Time.deltaTime;
            if (player==0&&time!=61)
            timer0 -= Time.deltaTime;
            if (player == 1&&time!=61)
            timer1 -= Time.deltaTime;
            if ((player == 0 && timer0 <= 0) || (player == 1 && timer1 <= 0)) { player = 1 - player; moves = 99; }
            if (timer0 <= 0 && timer1 <= 0) Win(-1);
            if (timer <= 0&&x==2)
            {
                if (player == -1) player = 0;
                //if (win)
                   //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                //else
                //Moved();
            }
            //watch0.transform.localScale = new Vector3(10 / time * timer0, watch0.transform.localScale.y, 1);
            //watch1.transform.localScale = new Vector3(10 / time * timer1, watch1.transform.localScale.y, 1);
        }*/

    }
