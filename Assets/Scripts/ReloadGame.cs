using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine("RerollGame");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator RerollGame()
    {
        yield return new WaitForSeconds(7.0f);
        SceneManager.LoadScene(0);
    }
}
