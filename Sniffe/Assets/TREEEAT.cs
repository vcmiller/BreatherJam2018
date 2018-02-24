using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TREEEAT : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Doog doog;
        if (doog = collision.GetComponent<Doog>())
        {
            FindObjectOfType<SpawnTreats>().ResetTim();
            doog.E_X_T_E_N_D();
            Destroy(gameObject);
        }
    }


}
