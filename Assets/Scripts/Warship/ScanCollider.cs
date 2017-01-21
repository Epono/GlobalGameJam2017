using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanCollider : MonoBehaviour
{
	[SerializeField]
	private PolygonCollider2D _collider;

	[SerializeField]
	private Vector2 _defaultScale;
	public Vector2 DefaultScale
	{
		get { return _defaultScale; }
		set { _defaultScale = value; }
	}

	[SerializeField]
	private Quaternion _angle;
	public Quaternion Angle
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

	public float	speed = 1.0F;
	public float	timeScan = 5f;
	public bool		DoScan = false;
	private float _timeSpend = 0f;

	void Start()
	{
		_defaultScale = _collider.transform.localScale;
	}

	void Update()
	{
		if ( DoScan )
		{
			_timeSpend += Time.deltaTime;
			if(_timeSpend >= timeScan)
			{
				_timeSpend = 0;
				DoScan = false;
				_collider.transform.position = _warshipPosition;
				_collider.transform.localScale = _defaultScale;
				return;
			}
			_collider.transform.Translate(Time.deltaTime * speed, 0, 0, _collider.transform);
			_collider.transform.localScale = new Vector2(_collider.transform.localScale.x + ( _collider.transform.localScale.x * Time.deltaTime * 0.5f )
													, _collider.transform.localScale.y + ( _collider.transform.localScale.y * Time.deltaTime * 0.5f ));
		}
		
	}

	public void OnTriggerEnter2D( Collider2D col )
	{
		if (col.tag.Equals("WARSHIP"))
		{
			Debug.Log("Collider warship");
			//Instanciate le point de repere
		}
	}

}
