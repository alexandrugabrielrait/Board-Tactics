using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportCar : MonoBehaviour {

    public Unit transported1;
    public Unit transported2;
    public Unit transported3;
    public Unit base1;
    public int cargo;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        cargo = 0;
		if (transported1 != null) cargo++;
        if (transported2 != null) cargo++;
        if (transported3 != null) cargo++;
    }
}
