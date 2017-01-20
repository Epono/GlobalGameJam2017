using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WarshipAttributes
{
	[SerializeField]
	private int _ammunition;
	public int Ammunition
	{
		get	{return _ammunition;}
		set	{_ammunition = value;}
	}

	[SerializeField]
	private int _healthPoint;
	public int HealthPoint
	{
		get	{return _healthPoint;}
		set	{_healthPoint = value;}
	}

	[SerializeField]
	private Vector3 _position;
	public Vector3 Position
	{
		get	{return _position;}
		set	{_position = value;}
	}

	[SerializeField]
	public Quaternion _rotation;
	public Quaternion Rotation
	{
		get	{return _rotation;}
		set {_rotation = value;}
	}

	[SerializeField]
	private int _shield;
	public int Shield
	{
		get {return _shield;}
		set	{_shield = value;}
	}

	[SerializeField]
	private float _cooldownRadar;
	public float CooldownRadar
	{
		get	{return _cooldownRadar;}
		set	{_cooldownRadar = value;}
	}

	[SerializeField]
	public float _cooldownShot;
	public float CooldownShot
	{
		get	{return _cooldownShot;}
		set	{_cooldownShot = value;}
	}

	[SerializeField]
	public float _moveSpeed;
	public float MoveSpeed
	{
		get {return _moveSpeed;}
		set	{_moveSpeed = value;}
	}

	[SerializeField]
	public float _rotationSpeed;
	public float RotationSpeed
	{
		get	{return _rotationSpeed;}
		set	{_rotationSpeed = value;}
	}
}
