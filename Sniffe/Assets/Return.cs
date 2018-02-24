using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Return : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("Ret", 5);
	}
	
	// Update is called once per frame
	public void Ret () {
        SceneManager.LoadScene(0);
	}
}
