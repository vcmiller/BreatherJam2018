using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doog : MonoBehaviour {

    [Header("Keys")]
    //public KeyCode up;
    //public KeyCode left;
    //public KeyCode down;
    //public KeyCode right;
    public string horizontal = "Horizontal";
    public string vertical = "Vertical";

    [Header("Vals")]
    public float speeeed;
    public float handling;

    [Header("Parts")]
    public Rigidbody2D rb;
    public Rigidbody2D piceofeboi;
    public Rigidbody2D tale;
    public List<Joint2D> boyeparts; 
	
	// Update is called once per frame
	void Update () {
        Vector2 input = new Vector2(Input.GetAxis(horizontal), Input.GetAxis(vertical));

        rb.velocity = Vector2.MoveTowards(rb.velocity, input.normalized * speeeed, handling * Time.deltaTime);//, Vector2.up, ForceMode2D.Force);
        if (rb.velocity != Vector2.zero)
        {
            transform.up = rb.velocity;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            E_X_T_E_N_D();
        }

        tale.transform.Rotate(Vector3.up * Time.deltaTime * 420 * rb.velocity.magnitude);
    }

    int GetKey(KeyCode code)
    {
        return Input.GetKey(code) ? 1 : 0;
    }

    public void E_X_T_E_N_D()
    {
        if (boyeparts == null) return;

        if (boyeparts.Count >= 20)
        {
            foreach (Doog d in FindObjectsOfType<Doog>())
            {
                if (this != d)
                {
                    d.tale.GetComponentInChildren<tale>().FuckinDie();
                }
            }
            return;
        }

        tale.position -= (Vector2) tale.transform.up;
        tale.velocity = -tale.transform.up;
        speeeed += 5;
        handling += 5;

        Joint2D joint = GetComponent<Joint2D>();
        while (joint.connectedBody != tale)
        {
            joint = joint.connectedBody.GetComponent<Joint2D>();
        }
        Rigidbody2D reeb = Instantiate(piceofeboi, joint.transform.position - joint.transform.up / 2, Quaternion.identity);
        joint.connectedBody = reeb;
        reeb.GetComponent<Joint2D>().connectedBody = tale;

        boyeparts.Add(reeb.GetComponent<Joint2D>());
    }
}
