using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doog : MonoBehaviour {

    [Header("Keys")]
    public KeyCode up;
    public KeyCode left;
    public KeyCode down;
    public KeyCode right;

    [Header("Vals")]
    public float speeeed;
    public float handling;

    [Header("Parts")]
    public Rigidbody2D rb;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 input = new Vector2(GetKey(right) - GetKey(left), GetKey(up) - GetKey(down));

        rb.velocity = Vector2.MoveTowards(rb.velocity, input.normalized * speeeed, handling * Time.deltaTime);//, Vector2.up, ForceMode2D.Force);
        if (rb.velocity != Vector2.zero)
        {
            transform.up = rb.velocity;
        }
	}

    int GetKey(KeyCode code)
    {
        return Input.GetKey(code) ? 1 : 0;
    }
}
