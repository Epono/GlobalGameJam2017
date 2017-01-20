using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarshipType1 : WarshipAttributes //class Exemple
{
	public WarshipType1()
	{
		Ammunition		= 1;
		HealthPoint		= 1;
		Position		= Vector3.zero;
		Rotation		= new Quaternion(0, 0, 0, 0);
		Shield			= 1;
		CooldownRadar	= 1f;
		CooldownShot	= 1f;
		MoveSpeed		= 1f;
		RotationSpeed	= 1f;
	}
}
