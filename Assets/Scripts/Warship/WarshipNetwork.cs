using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class WarshipNetwork : NetworkBehaviour
{
	private WarshipScript _warship;
	// Use this for initialization
	void Start()
	{
		if(!isLocalPlayer)
		{
			_warship = GetComponent<WarshipScript>();
			_warship.WarshipSprite.enabled = false;
			_warship.TargetSprite.enabled = false;
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
