using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanScript : MonoBehaviour
{
	[SerializeField]
	private Transform _warshipTransform;

	[SerializeField]
	private ScanCollider _scanCollider;

	[SerializeField]
	private float _angle;
	public float Angle
	{
		get { return _angle; }
		set { _angle = value; }
	}

	[SerializeField]
	private Transform _target;

	[SerializeField]
	private Transform _spawnRocket;
	public Transform SpawnRocket
	{
		get { return _spawnRocket; }
		set { _spawnRocket = value; }
	}


	[SerializeField]
	private Transform _scanTransform;

	public void Update()
	{
		_scanCollider.WarshipPosition = _warshipTransform.position;
		//MoveScan();
	}

	public void MoveScan()
	{
		var v1 = new Vector2(_target.position.x,_target.position.y)  - new Vector2(_warshipTransform.position.x,_warshipTransform.position.y);
		var v2 = new Vector2(_spawnRocket.position.x,_spawnRocket.position.y) - new Vector2(_warshipTransform.position.x,_warshipTransform.position.y);
		_angle = Vector2.Angle(_target.position, _spawnRocket.position);
		//if( _angle > 180 )
		//	_angle = 360 -_angle;
		_scanTransform.Rotate(0, 0, _angle);
	}

	public void RunScan()
	{
		MoveScan();
		_scanCollider.DoScan = true;
	}
}
