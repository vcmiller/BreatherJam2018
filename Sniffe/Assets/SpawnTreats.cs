using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTreats : MonoBehaviour {

    public GameObject treat;
    public float frequency;
    public float variance;

    float tim;

	// Use this for initialization
	void Start () {
        ResetTim();
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > tim)
        {
            tim = float.PositiveInfinity;
            Instantiate(treat, new Vector3(Random.Range(-transform.position.x, transform.position.x), Random.Range(-transform.position.y, transform.position.y), 0), Quaternion.identity);
        }
	}

    public void ResetTim()
    {
        tim = Time.time + frequency + Random.Range(-variance , variance);
    }
}
