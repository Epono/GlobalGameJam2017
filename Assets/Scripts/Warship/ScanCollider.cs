using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanCollider : MonoBehaviour
{
	[SerializeField]
	private Vector2 _defaultScale;
	public Vector2 DefaultScale
	{
		get { return _defaultScale; }
		set { _defaultScale = value; }
	}

	[SerializeField]
	private Transform _scanTransform;

	[SerializeField]
	private GameObject _discoveryPoint;

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
	}

	void Update()
	{
		if( DoScan )
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
	}

	public void OnTriggerEnter2D( Collider2D col )
	{
		if(DoScan)
		{
			if( col.tag.Equals("WARSHIP") )
			{
				//verify with Raycast
				coroutine = ShowShip(col.transform.position);
				StartCoroutine(coroutine);
				Debug.Log("Collider warship");
				//Instanciate le point de repere
			}
		}
		
	}

	private IEnumerator coroutine;

	public IEnumerator ShowShip(Vector2 position)
	{
		var go = Instantiate(_discoveryPoint) as GameObject;
		go.transform.position = position;
		//start anim
		yield return new WaitForSeconds(4f);
		Destroy(go);

	}
}
