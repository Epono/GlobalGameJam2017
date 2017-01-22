using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentPlayerTypeScript : MonoBehaviour
{
    public bool isServer;

    void Awake() {
        DontDestroyOnLoad(this);
    }

    void Start() {

    }

    void Update () {
		
	}
}
