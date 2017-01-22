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

	[SerializeField]
	private Transform _warshipTransform;
	public Transform WarshipPosition
	{
		get { return _warshipTransform; }
		set { _warshipTransform = value; }
	}

	public float    speed;
	public float    timeScan;//init a 3.5
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
				_scanTransform.position = _warshipTransform.position;
				_scanTransform.parent = _warshipTransform;
				_scanTransform.localScale = _defaultScale;
				return;
			}
			else
			{
				_scanTransform.Translate(Time.deltaTime * speed, 0, 0, _scanTransform);
				_scanTransform.localScale = new Vector2(_scanTransform.localScale.x + ( _scanTransform.localScale.x * Time.deltaTime * 0.7f )
														, _scanTransform.localScale.y + ( _scanTransform.localScale.y * Time.deltaTime * 0.7f ));

			}
		}
	}

	public void OnTriggerEnter2D( Collider2D col )
	{
		if(DoScan)
		{
			if( col.tag.Equals("WARSHIP") )
			{
				if( Vector3.Distance(col.transform.position, _warshipTransform.position) < 0.5f )
					return;
				//verify with Raycast
				coroutine = ShowPoint(col.transform.position);
				StartCoroutine(coroutine);
				Debug.Log("Collider warship");
				//Instanciate le point de repere
			}
		}
		
	}

	private IEnumerator coroutine;

	public IEnumerator ShowPoint(Vector2 position)
	{
		var go = Instantiate(_discoveryPoint) as GameObject;
		go.transform.position = position;
		yield return new WaitForSeconds(3f);
		Destroy(go);

	}
}
