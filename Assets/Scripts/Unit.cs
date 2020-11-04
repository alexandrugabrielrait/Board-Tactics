using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public int player;
    public int type; //0-6 base, rifleman, engineer, shotgunner, transport, tank, bomber 
    public int xx;
    public int yy;
    public int move;
    public int health;
    public int healthMax;
    public int attack;
    public int range;
    public int moveCost = 0;
    public int cost=0;
    public TurnManager manager;
    public MapManager mapager;
    public int moveCounter = 0;
    public string desc = "";
    public int drones = 0;
    public bool stealth = false;

    void Start()
    {
        desc = desc.Replace("nLN", "\n");
    }

    public void TakeDamage(int dmg)
    {
        if (dmg >= 50 && GetComponent<Bomb>() != null) { GetComponent<Bomb>().Explode(); return;}
        health -= dmg;
    }

    public void GetHealed(int dmg)
    {
        health += dmg;
        if (health > healthMax) health = healthMax;
    }

    void OnMouseDown()
    {
        if (Application.loadedLevelName != "Help")
        {
            mapager.selected = this.gameObject.GetComponent<Unit>();
            manager.ability = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<BoxCollider2D>() != null&&xx!=-1&&yy!=-1&&type>=0)
        {
            if (mapager.hasPlane[xx, yy] == 1 && type != 5) GetComponent<BoxCollider2D>().enabled = false;
            else GetComponent<BoxCollider2D>().enabled = true;
        }
        if (stealth == true)
        {
            if (player != mapager.manager.player)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().enabled = true;
                GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        //Color c = this.gameObject.GetComponent<SpriteRenderer>().color;
        if (type == -2) player = manager.player;
        if (manager.moves >= moveCounter || moveCounter <= 0)
        {
            if (player == 0) this.gameObject.GetComponent<SpriteRenderer>().color = manager.color0;
            if (player == 1)
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = manager.color1;
                this.gameObject.GetComponent<SpriteRenderer>().flipY = true;
                this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            if(player== -1)
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(25/255f, 77/255f, 0);
            }
            if (player == -2)
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(25 / 255f, 77 / 255f, 0);
                this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        else this.gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
        //if (type == 6) this.gameObject.GetComponent<SpriteRenderer>().color = new Color(c.r,c.g,c.b,0.2f);

        if (xx != -1 && yy != -1) transform.position = new Vector3(mapager.firstLand.transform.position.x+xx-1, mapager.firstLand.transform.position.y +yy-1, 0);
        else if (type >= 0) transform.position = new Vector3(-10, -2, 0);
        if (health <= 0)
        {
            if (mapager.selected == this.GetComponent<Unit>())
            {
                mapager.ClearHighlights();
                foreach (Unit base2 in FindObjectsOfType<Unit>())
                {
                    if (base2.type == 0 && player == base2.player && base2 != this) mapager.selected = base2;
                }
                if(mapager.selected==null)
                foreach (Unit base2 in FindObjectsOfType<Unit>())
                {
                    if (base2.type == 3 && player == base2.player && base2 != this) mapager.selected = base2;
                }
                if (mapager.selected == null)
                    mapager.selected = manager.baseEmpty;
            }
            Destroy(this.gameObject);
            if (type == 0||((manager.mode== 1||manager.mode == 4)&& type==3))
            {
                if (player == 0) mapager.bases0--;
                else mapager.bases1--;
            }
        }
    }
}
