using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Embiggen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.localScale = Vector3.right;	
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one / 2, 5 * Time.deltaTime);
	}
}
