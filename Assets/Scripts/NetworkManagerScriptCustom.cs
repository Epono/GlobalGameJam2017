using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManagerScriptCustom : NetworkBehaviour {

    [SyncVar]
    public int seed;

    void OnServerInitialized() {
        Debug.Log("OnServerInitialized");
    }

    public override void OnStartServer() {
        Debug.Log("OnStartServer");
    }

    void OnConnectedToServer() {
        Debug.Log("OnConnectedToServer");
    }

    void OnPlayerConnected(NetworkPlayer player) 
    {
        Debug.Log("playerConnected");
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {

    }
}
