using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swangin : MonoBehaviour {

    public float xLen;
    public float yLen;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(Mathf.Sin(Time.time) * xLen, Mathf.Cos(Time.time) * yLen, -1);
	}
}
