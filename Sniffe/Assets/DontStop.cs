using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontStop : MonoBehaviour {

    static bool nah;
    bool theone;
	// Use this for initialization
	protected virtual void Awake () {
        if (nah) Destroy(gameObject);

        theone = true;
        nah = true;
        DontDestroyOnLoad(gameObject);	
	}

    private void OnDestroy()
    {
        if (theone)
            nah = false;
    }

}
