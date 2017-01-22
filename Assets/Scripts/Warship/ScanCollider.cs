using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanCollider : MonoBehaviour
{
	[SerializeField]
	private Transform _target;

	[SerializeField]
	private Transform _scanTransform;

	[SerializeField]
	private Transform _spawnRocket;
	public Transform SpawnRocket
	{
		get { return _spawnRocket; }
		set { _spawnRocket = value; }
	}

	private Vector2 _oldSpawnRocket;

	[SerializeField]
	private Vector2 _defaultScale;
	public Vector2 DefaultScale
	{
		get { return _defaultScale; }
		set { _defaultScale = value; }
	}

	[SerializeField]
	private float _angle;
	public float Angle
	{
		get { return _angle; }
		set { _angle = value; }
	}

	private Vector2 _warshipPosition;
	public Vector2 WarshipPosition
	{
		get { return _warshipPosition; }
		set { _warshipPosition = value; }
	}

	public float    speed;
	public float    timeScan;
	public bool     DoScan = false;
	private float   _timeSpend;

	void Start()
	{
		_defaultScale = _scanTransform.localScale;
		_oldSpawnRocket = _spawnRocket.position;
	}

	void Update()
	{
		if( DoScan )//Voir Quand activer le radar
		{
			_timeSpend += Time.deltaTime;
			if( timeScan - _timeSpend <= 0f )
			{
				_timeSpend = 0f;
				DoScan = false;
				_scanTransform.position = _warshipPosition;
				_scanTransform.localScale = _defaultScale;
				return;
			}
			else
			{
				_scanTransform.Translate(Time.deltaTime * speed, 0, 0, _scanTransform);
				_scanTransform.localScale = new Vector2(_scanTransform.localScale.x + ( _scanTransform.localScale.x * Time.deltaTime * 0.5f )
														, _scanTransform.localScale.y + ( _scanTransform.localScale.y * Time.deltaTime * 0.5f ));

			}
		}
		else
		{
			MoveScan();
		}

	}

	public void OnTriggerEnter2D( Collider2D col )
	{
		if( col.tag.Equals("WARSHIP") )
		{



			Debug.Log("Collider warship");
			//Instanciate le point de repere
		}
	}

	public void MoveScan()
	{
		var v1 = new Vector2(_target.position.x,_target.position.y)  - _warshipPosition;
		var v2 = new Vector2(_spawnRocket.position.x,_spawnRocket.position.y) - _warshipPosition;
		_angle = Vector2.Angle(_target.position, _spawnRocket.position);
		_scanTransform.Rotate(0, 0, _angle);
	}

}
