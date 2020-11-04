using UnityEngine;
using System.Collections;

public class ColorButtons : MonoBehaviour {

    public TurnManager manager;
    public Color color;
    public int type;
    public bool randomizer = false;

    void Start()
    {
        if (randomizer == false)
        {
            switch (type)
            {
                case 0: color = GetComponent<SpriteRenderer>().color = Color.blue; break;
                case 1: color = GetComponent<SpriteRenderer>().color = Color.white; break;
                case 2: color = GetComponent<SpriteRenderer>().color = Color.cyan; break;
                case 3: color = GetComponent<SpriteRenderer>().color = Color.green; break;
                case 4: color = GetComponent<SpriteRenderer>().color = Color.magenta; break;
                case 5: color = GetComponent<SpriteRenderer>().color = Color.red; break;
                case 6: color = GetComponent<SpriteRenderer>().color = Color.yellow; break;
                case 7: color = GetComponent<SpriteRenderer>().color = new Color(1, 0.4f, 0, 1); break;
                case 8: color = GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.133f, 0.0117f, 1); break;
            }

        }
    }

	void OnMouseDown () {
        if (manager.t >= 2) return;
        if (randomizer == false)
        {
            if (manager.player == 0) { manager.color0 = color; manager.firstColor = type; }
            if (manager.player == 1) manager.color1 = color;
            Destroy(this.gameObject);
        }
        else
        {
            int rand = Random.Range(0, 9);
            if(manager.player==1)
            while (rand == manager.firstColor)
            {
                rand = Random.Range(0, 9);
            }
            Color color0=Color.white;
            foreach(ColorButtons btn in FindObjectsOfType<ColorButtons>())
            {
                if (rand == btn.type) { color0 = btn.color; Destroy(btn.gameObject); }
            }
            if (manager.player == 0) { manager.color0 = color0; manager.firstColor = rand; }
            else manager.color1 = color0;
        }
        manager.t++;
        manager.player=1-manager.player;
	}
}

