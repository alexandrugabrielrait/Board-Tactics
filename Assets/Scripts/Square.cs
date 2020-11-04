using UnityEngine;
using System.Collections;

public class Square : MonoBehaviour {

    public bool isMissle = false;
    public float timer;

    // Use this for initialization
    void Start () {
	
	}

    void FixedUpdate()
    {
        if (!isMissle) return;
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }
       }
     }
