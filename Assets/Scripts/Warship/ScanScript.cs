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
	private SpriteRenderer _sprite;
	[SerializeField]
	private PolygonCollider2D _collider;

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

	private Vector3 targetScanPos;

	private bool _canScan = true;

	private Vector2 _stickAxis;

	public void Update()
	{
		_scanCollider.WarshipPosition = _warshipTransform;
		if(!_scanCollider.DoScan)
			MoveScan();
	}

	public void MoveScan()
	{
		var v1 = new Vector2(_target.position.x,_target.position.y)  - new Vector2(_warshipTransform.position.x,_warshipTransform.position.y);
		var v2 = new Vector2(_spawnRocket.position.x,_spawnRocket.position.y) - new Vector2(_warshipTransform.position.x,_warshipTransform.position.y);
		_angle = Vector2.Angle(v1,v2);
		//if( _angle > 180 )
		//	_angle = 360 - _angle;
		_scanTransform.Rotate(0, 0, _angle);

	}

	public void RunScan()
	{
		if(_canScan)
		{
			_canScan = false;
			MoveScan();
			StartCoroutine("DoScan");
		}
		
	}

	public IEnumerator DoScan()
	{
		_scanTransform.parent = null;
		_scanCollider.DoScan = true;
		_sprite.enabled = true;
		_collider.enabled = true;
		yield return new WaitForSeconds(3.5f);
		_scanTransform.parent = _warshipTransform;
		_sprite.enabled = false;
		_collider.enabled = false;
		_canScan = true;
		yield return null;
	}
}
