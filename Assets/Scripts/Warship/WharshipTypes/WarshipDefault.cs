using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarshipDefault : WarshipAttributes
{
	public WarshipDefault()
	{
		Ammunition		= 0;
		HealthPoint		= 0;
		Position		= Vector3.zero;
		Rotation		= new Quaternion(0, 0, 0, 0);
		Shield			= 0;
		CooldownRadar	= 0f;
		CooldownShot	= 0f;
		MoveSpeed		= 0f;
		RotationSpeed	= 0f;
	}
}
