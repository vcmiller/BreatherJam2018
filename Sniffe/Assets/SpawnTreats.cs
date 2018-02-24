using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTreats : MonoBehaviour {

    public GameObject treat;
    public float delay;
    public float delayVariance;

    float tim;

	// Use this for initialization
	void Start () {
        ResetTim();
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > tim)
        {
            //tim = float.PositiveInfinity;
            ResetTim();

            Vector3 position;
            do
            {
                position = new Vector3(Random.Range(-transform.position.x, transform.position.x), Random.Range(-transform.position.y, transform.position.y), 0);
            }while (nearDoge(position));

            Instantiate(treat, position, Quaternion.identity);
        }
	}

    private bool nearDoge(Vector3 position)
    {
        foreach(Doog d in FindObjectsOfType<Doog>())
        {
            float arbitrary = 1;
            if(Vector3.Distance(d.transform.position, position) < arbitrary){
                return true;
            }
        }
        return false;
    }

    public void ResetTim()
    {
        tim = Time.time + delay + Random.Range(-delayVariance , delayVariance);
    }
}
