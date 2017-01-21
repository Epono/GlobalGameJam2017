using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLightHouseScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Pour que plus c'est petit, plus ça aille vite
        float scaledValue = (gameObject.transform.lossyScale.x - 0.05f) / (0.40f - 0.05f);
	    scaledValue = scaledValue * 2;

        gameObject.transform.Rotate(0, 0, -20 * Time.deltaTime / scaledValue);
    }
}
