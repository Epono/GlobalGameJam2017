using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
	[SerializeField]
	private SpriteRenderer _sprite;

	[SerializeField]
	private int _damages;
	public int Damages
	{
		get
		{
			return _damages;
		}
		set
		{
			_damages = value;
		}
	}

	[SerializeField]
	private float _speed;
	public float Speed
	{
		get
		{
			return _speed;
		}
		set
		{
			_speed = value;
		}
	}

	[SerializeField]
	private int _range;
	public int Range
	{
		get
		{
			return _range;
		}
		set
		{
			_range = value;
		}
	}



	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

}
