using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tale : MonoBehaviour {

    public Doog doog;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Doog otherDoog;
        if ((otherDoog = collision.GetComponent<Doog>()) 
            && otherDoog != doog 
            && otherDoog.boyeparts != null)
        {
            FuckinDie();
        }
    }

    public void FuckinDie()
    {
        if (doog.boyeparts == null) return;

        foreach (var joint in doog.boyeparts)
        {
            joint.connectedBody.velocity = Random.insideUnitCircle.normalized * Mathf.Pow(Random.value, 2) * 100;
            Destroy(joint);
        }

        //doog.boyeparts.ForEach((joint) => Destroy(joint));
        doog.boyeparts = null;
        doog.GetComponent<Joint2D>().connectedBody = transform.parent.GetComponent<Rigidbody2D>();

        doog.speeeed = 100;

        Doog winner;
        if (livingDoogs(out winner) == 1)
        {
            FindObjectOfType<ScoreManager>().Win(winner.name);
            Invoke("BeginAnewAndFightOnceMore", 5);
        }
    }

    public void BeginAnewAndFightOnceMore()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public int livingDoogs(out Doog winner)
    {
        int count = 0;
        winner = null;

        foreach(Doog d in FindObjectsOfType<Doog>())
        {
            if (d.boyeparts != null)
            {
                winner = d;
                count++;
            }
        }

        return count;
    }

}
