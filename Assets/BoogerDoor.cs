using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoogerDoor : MonoBehaviour {
    public Booger boog;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!boog) {
            Destroy(gameObject);
        }
	}
}
